using GentlemanParserDiscordBot;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
            {
                var rolls = new StringBuilder();
                foreach (var roll in log.RollData.Rolls)
                {
                    rolls.Append(roll + " ");
                }

                rolledOrCommandInfo = $"For {log.Command} rolled: {log.RollData.Sum} | {log.RollData.DiceType}: {rolls.ToString()}";
                logRolls(log);
            }
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

        private static void logRolls(Log log)
        {
            var outBuilder = new StringBuilder();
            var separator = " | ";

            foreach (var roll in log.RollData.Rolls)
            {
                StringBuilder insideBuilder = new StringBuilder();

                insideBuilder.Append(DateTime.Now.ToString().PadRight(20));
                insideBuilder.Append(separator);

                insideBuilder.Append(log.Author.PadRight(15));
                insideBuilder.Append(separator);

                insideBuilder.Append($"d{log.RollData.DiceType}".PadRight(5));
                insideBuilder.Append(separator);

                insideBuilder.Append(roll);
                insideBuilder.Append(separator + "\n");

                outBuilder.Append(insideBuilder.ToString());
            }

            var logFile = DevelopmentInfo.GetRollsFilePath();
            File.AppendAllText(logFile, outBuilder.ToString());
        }
    }
}