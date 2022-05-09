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
        static Dictionary<string, List<string>> CommandsAndOutputMessages = new Dictionary<string, List<string>>()
        {
            {
                "onator", new List<string>() {"Eeee! Utopce!"}
            }
        };

        public string Content { get; set; }

        public Command(string commandContent)
        {
            Content = commandContent;
            _dataHandler = new DataHandler();
        }

        public bool IsCommandValid()
        {
            if (CommandsAndOutputMessages.Keys.Contains(Content))
                return true;

            return false;
        }

        public void ExecuteCommand(SocketMessage message)
        {
            int outputIndex = 0;
            int outputElements = CommandsAndOutputMessages[Content].Count;

            if (outputElements > 1)
            {
                Random random = new Random();
                outputIndex = random.Next(0, outputElements);
            }

            message.Channel.SendMessageAsync(CommandsAndOutputMessages[Content][outputIndex]);
        }
    }
}
