using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace GentelmanParserDiscordBot
{
    public class Command
    {
        private readonly IDataHandler _dataHandler;
        private static Dictionary<string, List<string>> CommandsAndOutputMessages = DataHandler.GetCommandsList();

        public string CommandContent { get; set; }

        public Command(string commandCommandContent)
        {
            CommandContent = commandCommandContent;
            _dataHandler = new DataHandler();
        }

        public bool IsCommandValid()
        {
            if (CommandsAndOutputMessages.Keys.Contains(CommandContent))
                return true;

            return false;
        }

        public void ExecuteCommand(SocketMessage message)
        {
            int outputIndex = 0;
            int outputElements = CommandsAndOutputMessages[CommandContent].Count;

            if (outputElements > 1)
            {
                Random random = new Random();
                outputIndex = random.Next(0, outputElements);
            }

            message.Channel.SendMessageAsync(CommandsAndOutputMessages[CommandContent][outputIndex]);
        }
    }
}
