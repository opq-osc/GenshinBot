using GenshinBotCore.Configs;
using GenshinBotCore.Entities;
using GenshinBotCore.Services;
using GenshinBotCore.Services.Data;
using GenshinBotCore.Services.Workers;
using Microsoft.EntityFrameworkCore;
using YukinoshitaBot.Extensions;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        var configuration = context.Configuration;
        services.AddYukinoshitaBot();
        services.AddDbContext<ApplicationDbContext>(builder => builder.UseSqlite(configuration.GetConnectionString("DbConn")));
        services.AddScoped<IMihoyoApi, MihoyoAccountApi>();
        services.AddScoped<ISecretHeaderGenerator, TakumiSecretHeaderGenerator>(services =>
        {
            var userManager = services.GetRequiredService<IUserManager>();
            var secretManager = services.GetRequiredService<ISecretManager>();
            var config = configuration.GetSection("MihoyoClient").Get<TakumiSecretHeaderGeneratorConfiguration>();
            return new TakumiSecretHeaderGenerator(userManager, secretManager, configuration =>
            {
                configuration.AppVersion = config.AppVersion;
                configuration.ClientType = config.ClientType;
                configuration.Salt = config.Salt;
            });
        });
        services.AddTransient<ISecretManager, UserSecretManager>(services =>
        {
            var dbContext = services.GetRequiredService<ApplicationDbContext>();
            var logger = services.GetRequiredService<ILogger<UserSecretManager>>();
            var config = configuration.GetSection("SecretManager").Get<SecretManagerConfiguration>();
            return new UserSecretManager(dbContext, logger, configuration =>
            {
                configuration.HashSalt = config.HashSalt;
                configuration.SymmetricKey = config.SymmetricKey;
                configuration.SymmetricSalt = config.HashSalt;
            });
        });
        services.AddScoped<ITakumiApi, TakumiApi>(services =>
        {
            var userManager = services.GetRequiredService<IUserManager>();
            var secretHeader = services.GetRequiredService<ISecretHeaderGenerator>();
            return new TakumiApi(userManager, secretHeader, configuration =>
            {
                configuration.BaseUrl = "https://api-takumi.mihoyo.com";
                configuration.GameAccountsUrl = "/game_record/app/card/wapi/getGameRecordCard";
                configuration.IndexUrl = "/game_record/app/genshin/api/index";
                configuration.LoginTicketUrl = "/auth/api/getMultiTokenByLoginTicket";
                configuration.DailyNoteUrl = "/game_record/app/genshin/api/dailyNote";
            });
        });
        services.AddScoped<IUserManager, UserManager>();
        services.AddScoped<IPictureStorage, DbCachedPictureStorage>();
        services.AddSingleton<EmoticonSet>();
        services.AddHostedService<EmoticonsInitializer>();
        services.AddHostedService<SqliteDbInitializer>();
#if RELEASE
        services.AddHostedService<ControllerWrapper>();
#endif
    })
    .Build();

await host.RunAsync();
