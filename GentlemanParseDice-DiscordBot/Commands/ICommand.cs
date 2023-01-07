using Discord.WebSocket;

namespace GentelmanParserDiscordBot.Commands
{
    public interface ICommand
    {
        public string CommandContent { get; set; }

        public string ExecuteCommand(SocketMessage message);
    }
}