using Discord;
using Discord.WebSocket;
using System;
using System.IO;
using System.Threading.Tasks;
using GentelmanParserDiscordBot;

namespace GentelmanParserDiscordBot
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
            IDataHandler dataHandler = DataHandler.CreateHandler();

            var token = File.ReadAllText("../../../token.txt");

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
