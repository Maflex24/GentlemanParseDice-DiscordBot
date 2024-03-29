﻿using Discord.WebSocket;

namespace GentelmanParserDiscordBot.Commands
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

        public string ExecuteCommand(SocketMessage message)
        {
            message.Channel.SendMessageAsync(_reply);

            return _reply;
        }
    }
}