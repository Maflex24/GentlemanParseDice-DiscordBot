using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace GentlemanParserDiscordBot
{
    public class CommandHandler
    {
        private readonly IDataHandler _dataHandler;

        public CommandHandler(SocketMessage message, IDataHandler dataHandler)
        {
            _dataHandler = dataHandler;
        }

        public static Task Handler(SocketMessage message)
        {
            if (!message.Content.StartsWith(DevelopmentInfo.CommandPrefix) || message.Author.IsBot)
                return Task.CompletedTask;

            int lengthOfCommand = message.Content.Contains(' ') ? message.Content.IndexOf(' ') : message.Content.Length;
            var command = new Command(message.Content.Substring(1, lengthOfCommand - 1).ToLower());


            if (DiceParser.IsADiceRoll(command.CommandContent))
            {
                message.Channel.SendMessageAsync(message.Author.Mention + ": " + DiceParser.RollOutput(command.CommandContent));
                return Task.CompletedTask;
            }

            if (!command.IsCommandValid())
            {
                message.Channel.SendMessageAsync(message.Author.Mention + " Your command is not valid, Dear");
                return Task.CompletedTask;
            }

            command.ExecuteCommand(message);
            return Task.CompletedTask;
        }
    }
}
