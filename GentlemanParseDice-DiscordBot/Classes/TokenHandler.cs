using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GentlemanParserDiscordBot;

namespace GentelmanParserDiscordBot
{
    public class TokenHandler
    {
        public string GetToken()
        {
            string token;

            if (File.Exists("DevelopmentToken.txt"))
            {
                token = File.ReadAllText("DevelopmentToken.txt");
                DevelopmentInfo.SetInDevelopmentStatus();
            }
            else
                token = File.ReadAllText("token.txt");

            return token;
        }
    }
}
