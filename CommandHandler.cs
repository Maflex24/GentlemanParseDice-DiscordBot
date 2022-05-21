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
            int lengthOfCommand = -1;

            //filtering messages begin here
            if (!message.Content.StartsWith('!')) //This is your prefix
                return Task.CompletedTask;

            if (message.Author.IsBot) //This ignores all commands from bots
                return Task.CompletedTask;

            if (message.Content.Contains(' '))
                lengthOfCommand = message.Content.IndexOf(' ');
            else
                lengthOfCommand = message.Content.Length;

            Command command = new Command(message.Content.Substring(1, lengthOfCommand - 1).ToLower());

            if (DiceParser.IsADiceRoll(command.CommandContent))
            {
                message.Channel.SendMessageAsync(message.Author.Mention + ": " + DiceParser.Roll(command.CommandContent));
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
