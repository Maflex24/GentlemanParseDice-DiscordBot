using Discord;
using Discord.WebSocket;
using GentelmanParserDiscordBot.Handlers;
using GentelmanParserDiscordBot.Settings;
using System;
using System.Threading.Tasks;

namespace GentlemanParserDiscordBot
{
    internal class Program
    {
        public static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        private DiscordSocketClient _client;

        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();
            CommandHandler commandHandler = new CommandHandler(new DataHandler());
            _client.MessageReceived += commandHandler.Handler;
            _client.Log += Log;

            var tokenHandler = new TokenHandler();
            var token = tokenHandler.GetToken();

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}