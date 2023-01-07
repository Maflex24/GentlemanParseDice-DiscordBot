using Discord;
using Discord.WebSocket;
using GentlemanParserDiscordBot;
using System.IO;

namespace GentelmanParserDiscordBot.Commands
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

        public string ExecuteCommand(SocketMessage message)
        {
            message.Channel.SendFileAsync(
                new FileAttachment(Path.Combine(DevelopmentInfo.GetImagesPath(), _reply)));

            return _reply;
        }
    }
}