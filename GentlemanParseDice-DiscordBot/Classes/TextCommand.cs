using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace GentelmanParserDiscordBot
{
    public class TextCommand : ICommand
    {
        private readonly string _reply;
        private readonly SocketMessage _message;
        public string CommandContent { get; set; }

        public TextCommand(string commandContent, string reply, SocketMessage message)
        {
            _reply = reply;
            _message = message;
            CommandContent = commandContent;
        }

        public void ExecuteCommand(SocketMessage message)
        {
            message.Channel.SendMessageAsync(_reply);
        }
    }
}
