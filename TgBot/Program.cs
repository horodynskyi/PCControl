using TgBot;
using TgBot.Commands;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        IConfiguration configuration = hostContext.Configuration;
        var botOptions = configuration.GetSection("TgBot").Get<BotOptions>();
        services.Configure<AccountOptions>(configuration.GetSection("TelegramAccount"));
        services.AddTelegramClient(botOptions);
        services.AddHostedService<Worker>();
      //  services.AddTransient<ICommand, TelegramCommands>();
    })
    .UseWindowsService()
    .Build();

await host.RunAsync();