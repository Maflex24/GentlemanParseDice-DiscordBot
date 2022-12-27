using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GentlemanParserDiscordBot
{
    public static class DevelopmentInfo
    {
        private static bool inDevelopment { get; set; } = false;
        private static string commandFileName = "multicommands.json";

        public static char CommandPrefix { get; } = '!';

        public static string GetCommandListPath() => inDevelopment ? $"../../../{commandFileName}" : commandFileName;
        public static string GetImagesPath() => inDevelopment ? $"../../../images/" : "images/";
        public static void SetInDevelopmentStatus() => inDevelopment = true;
    }
}
