namespace GentlemanParserDiscordBot
{
    public static class DevelopmentInfo
    {
        private static bool inDevelopment { get; set; } = false;
        private static string commandFileName = "multicommands.json";

        private static string LogFileName { get; } = "logs.txt";
        private static string RollsFileName { get; } = "rolls.txt";

        public static char CommandPrefix { get; } = '!';

        public static string GetCommandListPath() => inDevelopment ? $"../../../{commandFileName}" : commandFileName;

        public static string GetLogFilePath() => inDevelopment ? $"../../../{LogFileName}" : LogFileName;

        public static string GetRollsFilePath() => inDevelopment ? $"../../../{RollsFileName}" : RollsFileName;

        public static string GetImagesPath() => inDevelopment ? $"../../../images/" : "images/";

        public static void SetInDevelopmentStatus() => inDevelopment = true;
    }
}