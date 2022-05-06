using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace GentelmanParserDiscordBot
{
    public class CommandHandler
    {
        public static Task Handler(SocketMessage message)
        {
            //variables
            string command = "";
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

            command = message.Content.Substring(1, lengthOfCommand - 1).ToLower();

            if (!Command.IsCommandValid(command))
            {
                message.Channel.SendMessageAsync(message.Author.Mention + " Your comment is not valid, Dear");
                return Task.CompletedTask;
            }

            if (DiceParser.IsADiceRoll(command))
            {
                message.Channel.SendMessageAsync(message.Author.Mention + ": " + DiceParser.Roll(command));
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }

    }
}
