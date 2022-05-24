using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace GentlemanParserDiscordBot
{
    public class Command
    {
        private readonly IDataHandler _dataHandler;
        private static readonly Dictionary<string, List<string>> commandsAndOutputMessages = DataHandler.GetCommandsList();
        private static readonly Dictionary<string, int> lastCommandIndex = getLastCommandIndex();

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

            if (outputElements > 1 && outputElements <= 4)
            {
                if (lastIndex + 1 == outputElements)
                    lastCommandIndex[CommandContent] = 0;
                else
                    lastCommandIndex[CommandContent]++;

                lastIndex = lastCommandIndex[CommandContent];
            }

            if (outputElements > 1 && outputElements > 4)
            {
                Random random = new Random();
                lastIndex = random.Next(0, outputElements);
            }

            var currentReply = commandsAndOutputMessages[CommandContent][lastIndex];
            if (currentReply.Contains(".jpg") || currentReply.Contains(".png"))
            {
                message.Channel.SendFileAsync(new FileAttachment(Path.Combine(DevelopmentInfo.GetImagesPath(), currentReply)));
                return;
            }

            message.Channel.SendMessageAsync(commandsAndOutputMessages[CommandContent][lastIndex]);
        }
    }
}
