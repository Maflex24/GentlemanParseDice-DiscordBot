using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GentlemanParserDiscordBot
{
    public static class DevelopmentInfo
    {
        private static bool isInDevelopment { get; set; } = false;
        private static string commandFileName = "multicommands.json";

        public static char CommandPrefix
        {
            get;
        } = '!';

        public static string GetCommandListPath() => isInDevelopment ? $"../../../{commandFileName}" : commandFileName;
        public static string GetImagesPath() => isInDevelopment ? $"../../../images/" : "images/";
        public static void SetInDevelopmentStatus() => isInDevelopment = true;
    }
}
