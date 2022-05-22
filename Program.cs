using Discord;
using Discord.WebSocket;
using System;
using System.IO;
using System.Threading.Tasks;
using GentelmanParserDiscordBot;
using GentlemanParserDiscordBot;

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
            _client.MessageReceived += CommandHandler.Handler;
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
