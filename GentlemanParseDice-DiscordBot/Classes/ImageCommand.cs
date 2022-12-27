using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using GentlemanParserDiscordBot;

namespace GentelmanParserDiscordBot.Classes
{
    public class ImageCommand : ICommand
    {
        private readonly string _reply;
        private readonly SocketMessage _message;
        public string CommandContent { get; set; }

        public ImageCommand(string commandContent, string reply, SocketMessage message)
        {
            _reply = reply;
            _message = message;
            CommandContent = commandContent;
        }

        public void ExecuteCommand(SocketMessage message)
        {
            message.Channel.SendFileAsync(
                new FileAttachment(Path.Combine(DevelopmentInfo.GetImagesPath(), _reply)));
        }
    }
}
