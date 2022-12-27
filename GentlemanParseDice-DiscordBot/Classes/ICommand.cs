using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace GentelmanParserDiscordBot
{
    public interface ICommand
    {
        public string CommandContent { get; set; }
        public void ExecuteCommand(SocketMessage message);
    }
}
