using GenshinBotCore.Entities;
using GenshinBotCore.Services;
using YukinoshitaBot.Extensions;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.EntityFrameworkCore;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddYukinoshitaBot();
        services.AddDbContext<ApplicationDbContext>(builder => builder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=GenshinBot;Trusted_Connection=True;"));
        services.AddScoped<IMihoyoApi, MihoyoAccountApi>();
        services.AddScoped<ISecretHeaderGenerator, TakumiSecretHeaderGenerator>(services =>
        {
            var userManager = services.GetRequiredService<IUserManager>();
            var secretManager = services.GetRequiredService<ISecretManager>();
            return new TakumiSecretHeaderGenerator(userManager, secretManager, configuration =>
            {
                configuration.AppVersion = "2.16.1";
                configuration.ClientType = 5;
                configuration.Salt = "xV8v4Qu54lUKrEYFZkJhB8cuOh9Asafs";
            });
        });
        services.AddTransient<ISecretManager, UserSecretManager>(services =>
        {
            var dbContext = services.GetRequiredService<ApplicationDbContext>();
            var logger = services.GetRequiredService<ILogger<UserSecretManager>>();
            return new UserSecretManager(dbContext, logger, configuration =>
            {
                configuration.HashSalt = "iusdLYGDAKGjniayeighzakljGKJHe";
                configuration.SymmetricKey = "iajehbgbzfsgawbgoytqbzdfbg";
                configuration.SymmetricSalt = "ajwehgjhzdfgawegrtiSGdfbo";
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
            });
        });
        services.AddScoped<IUserManager, UserManager>();
    })
    .Build();

await host.RunAsync();
