using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using GentelmanParserDiscordBot;
using GentelmanParserDiscordBot.Classes;

namespace GentlemanParserDiscordBot
{
    public class CommandHandler
    {
        private readonly IDataHandler _dataHandler;

        public CommandHandler(IDataHandler dataHandler)
        {
            _dataHandler = dataHandler;
        }

        private static readonly Dictionary<string, List<string>> commandsAndOutputMessages = DataHandler.GetCommandsList();

        private static bool CommandExist(string commandContent) => commandsAndOutputMessages.Keys.Contains(commandContent) ? true : false;

        public Task Handler(SocketMessage message)
        {
            if (!message.Content.StartsWith(DevelopmentInfo.CommandPrefix) || message.Author.IsBot)
                return Task.CompletedTask;

            var commandLength = message.Content.Contains(' ') ? message.Content.IndexOf(' ') : message.Content.Length;
            var commandContext = message.Content.Substring(1, commandLength - 1).ToLower();

            if (DiceParser.IsADiceRoll(commandContext))
            {
                message.Channel.SendMessageAsync(message.Author.Mention + ": " + DiceParser.RollOutput(commandContext));
                return Task.CompletedTask;
            }

            if (!CommandExist(commandContext))
            {
                message.Channel.SendMessageAsync(message.Author.Mention + " Your command is not valid, Dear");
                return Task.CompletedTask;
            }

            var indexOfOutputMessage = rollOutputIndex(commandContext);
            var command = getNewCommand(commandContext, indexOfOutputMessage, message);

            command.ExecuteCommand(message);
            return Task.CompletedTask;
        }

        private ICommand getNewCommand(string commandContext, int index, SocketMessage message)
        {
            var imageExtensions = new List<string>() { @".jpg$", @".png$", @".gif$", $".jpeg$" };
            var isImageType = false;

            foreach (var imageExtension in imageExtensions)
            {
                if (!Regex.IsMatch(commandsAndOutputMessages[commandContext][index], imageExtension)) 
                    continue;

                isImageType = true;
                break;
            }

            if (isImageType)
                return new ImageCommand(commandContext, commandsAndOutputMessages[commandContext][index], message);

            return new TextCommand(commandContext, commandsAndOutputMessages[commandContext][index], message);
        }

        private int rollOutputIndex(string commandContext)
        {
            var random = new Random();
            return random.Next(0, commandsAndOutputMessages[commandContext].Count);
        }
    }
}
