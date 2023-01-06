using GentlemanParserDiscordBot;
using System.IO;

namespace GentelmanParserDiscordBot.Settings
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