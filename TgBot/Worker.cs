using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TgBot.Commands;

namespace TgBot;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly ITelegramBotClient _client;
    private readonly AccountOptions _options;
    private readonly ICommand _commands;
    private  CancellationTokenSource _cts;
    
    public Worker(ILogger<Worker> logger, ITelegramBotClient telegramBotClient,IOptions<AccountOptions> options)
    {
        _logger = logger;
        _client = telegramBotClient;
        _commands = new TelegramCommands();
        _options = options.Value;
        _cts = new CancellationTokenSource();
    }
    
    private Task HandleErrorAsync(ITelegramBotClient client, Exception exception, CancellationToken token)
    {
        var errorMessage = exception switch
        {
            ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };
        Console.WriteLine(errorMessage);
        return Task.CompletedTask;
    }
    
    private async Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken Token)
    {
        if (update.Type != UpdateType.Message)
            return;
        if (update.Message!.Type != MessageType.Text)
            return;
        if(update.Message.Chat.Id != _options.Id)
            return;
        if (_commands.Contains(update.Message))
            await _commands.Execute(_client, update.Message);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _client.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            new ReceiverOptions{ AllowedUpdates = {}},
            _cts.Token);
        _logger.LogInformation("{}",DateTime.Now );
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            await Task.Delay(36000, stoppingToken);
        }
    }
}