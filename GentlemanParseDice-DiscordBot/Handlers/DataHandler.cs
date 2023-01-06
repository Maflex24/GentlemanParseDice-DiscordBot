using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace GentlemanParserDiscordBot
{
    public interface IDataHandler
    {
        public static Dictionary<string, List<string>> GetCommandsList() => GetCommandsList();
    }

    public class DataHandler : IDataHandler
    {
        private static string commandListPath = DevelopmentInfo.GetCommandListPath();

        public static Dictionary<string, List<string>> GetCommandsList()
        {
            Dictionary<string, List<string>> outDictionary = new Dictionary<string, List<string>>();

            string fileContext = File.ReadAllText(commandListPath);

            return outDictionary = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(fileContext);
        }
    }
}