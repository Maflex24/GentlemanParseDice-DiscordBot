using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GentelmanParserDiscordBot
{
    public interface IDataHandler
    {
        public string GiveSomeString();
    }

    public class DataHandler : IDataHandler
    {
        private static bool objectExist = false;

        public DataHandler() { }

        //public static DataHandler CreateHandler()
        //{
        //    if (!objectExist)
        //    {
        //        objectExist = true;
        //        DataHandler dataHandler = new DataHandler();
        //        return dataHandler;
        //    }

        //    return null;
        //}

        public string GiveSomeString() => "utopce.exe";
    }
}
