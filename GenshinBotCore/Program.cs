using YukinoshitaBot.Extensions;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddYukinoshitaBot();
    })
    .Build();

await host.RunAsync();
