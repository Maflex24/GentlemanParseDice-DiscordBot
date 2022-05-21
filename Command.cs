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
        private static Dictionary<string, List<string>> commandsAndOutputMessages = DataHandler.GetCommandsList();
        private static Dictionary<string, int> lastCommandIndex = getLastCommandIndex();

        public string CommandContent { get; set; }

        public Command(string commandCommandContent)
        {
            CommandContent = commandCommandContent;
            _dataHandler = new DataHandler();
        }

        private static Dictionary<string, int> getLastCommandIndex()
        {
            Dictionary<string, int> output = new Dictionary<string, int>();

            foreach (var command in commandsAndOutputMessages.Keys)
            {
                output.Add(command, 0);
            }

            return output;
        }

        public bool IsCommandValid()
        {
            if (commandsAndOutputMessages.Keys.Contains(CommandContent))
                return true;

            return false;
        }

        public void ExecuteCommand(SocketMessage message)
        {
            int outputElements = commandsAndOutputMessages[CommandContent].Count;
            int lastIndex = lastCommandIndex[CommandContent];

            if (outputElements > 1)
            {
                if (lastIndex + 1 == outputElements)
                    lastCommandIndex[CommandContent] = 0;
                else
                    lastCommandIndex[CommandContent]++;

                lastIndex = lastCommandIndex[CommandContent];
            }

            message.Channel.SendMessageAsync(commandsAndOutputMessages[CommandContent][lastIndex]);
        }
    }
}
