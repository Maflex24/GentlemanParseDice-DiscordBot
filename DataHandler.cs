using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GentelmanParserDiscordBot
{
    public interface IDataHandler
    {
        public static Dictionary<string, List<string>> GetCommandsList() => GetCommandsList();
    }

    public class DataHandler : IDataHandler
    {
        private static bool objectExist = false;

        private static string path = File.ReadAllText("../../../path.txt");

        public static Dictionary<string, List<string>> GetCommandsList()
        {
            Dictionary<string, List<string>> outDictionary = new Dictionary<string, List<string>>();

            string fileContext = File.ReadAllText(path);

            return outDictionary = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(fileContext);
        }
    }
}
