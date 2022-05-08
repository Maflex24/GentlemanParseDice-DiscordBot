using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace GentelmanParserDiscordBot
{
    public static class Command
    {
        static Dictionary<string, List<string>> CommandsAndOutputMessages = new Dictionary<string, List<string>>();

        public static bool IsCommandValid(string command)
        {
            if (CommandsAndOutputMessages.Keys.Contains(command))
                return true;

            return false;
        }

        public static void ExecuteCommand(string command, SocketMessage message)
        {
            int outputIndex = 0;
            int outputElements = CommandsAndOutputMessages[command].Count;

            if (outputElements > 1)
            {
                Random random = new Random();
                outputIndex = random.Next(0, outputElements);
            }

            message.Channel.SendMessageAsync(CommandsAndOutputMessages[command][outputIndex]);
        }
    }
}
