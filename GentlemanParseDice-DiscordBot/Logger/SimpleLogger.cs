using GentlemanParserDiscordBot;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace GentelmanParserDiscordBot.Logger
{
    public static class SimpleLogger
    {
        public static void Log(LogType logtype, ref Log log, ref Stopwatch stopwatch)
        {
            StringBuilder builder = new StringBuilder();
            var separator = " | ";

            builder.Append(DateTime.Now.ToString().PadRight(20));
            builder.Append(separator);

            builder.Append(log.Author.PadRight(15));
            builder.Append(separator);

            builder.Append(logtype.ToString().PadRight(8));
            builder.Append(separator);

            string rolledOrCommandInfo;
            var rolledOrCommandInfoPadLength = 140;

            if (logtype is LogType.Roll)
                rolledOrCommandInfo = $"For {log.Command} rolled: {log.RollData.Sum}";
            else
            {
                rolledOrCommandInfo = log.Command.PadRight(10) + separator;

                var length = rolledOrCommandInfo.Length;
                var replyMaxLegth = rolledOrCommandInfoPadLength - length;

                var reply = log.Reply.Replace("\n", "");

                if (replyMaxLegth < log.Reply.Length)
                {
                    reply = reply.Substring(0, replyMaxLegth - 3);
                    reply += "...";
                }

                rolledOrCommandInfo += reply;
            }

            builder.Append(rolledOrCommandInfo.PadRight(rolledOrCommandInfoPadLength));
            builder.Append(separator);

            builder.Append($"{stopwatch.ElapsedMilliseconds} ms");

            var logFile = DevelopmentInfo.GetLogFilePath();
            File.AppendAllText(logFile, "\n" + builder.ToString());

            Console.WriteLine(builder.ToString());
        }
    }
}