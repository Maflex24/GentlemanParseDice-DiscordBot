using GentelmanParserDiscordBot.Dice;

namespace GentelmanParserDiscordBot.Logger
{
    public class Log
    {
        public LogType Type { get; set; }
        public string Author { get; set; }
        public string Message { get; set; }
        public string Command { get; set; }
        public RollData RollData { get; set; }
        public string Reply { get; set; }
        public long ElapsedMiliseconds { get; set; }
    }
}