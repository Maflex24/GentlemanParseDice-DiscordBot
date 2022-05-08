using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GentelmanParserDiscordBot
{
    public class DataHandler
    {
        private static bool objectExist = false;

        private DataHandler() { }

        public static DataHandler CreateHandler()
        {
            if (!objectExist)
            {
                objectExist = true;
                DataHandler dataHandler = new DataHandler();
                return dataHandler;
            }

            return null;
        }
    }
}
