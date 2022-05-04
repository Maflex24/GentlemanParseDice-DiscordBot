using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GentelmanParserDiscordBot
{
    public class DiceParser
    {
        public static string Roll(string command)
        {
            RollData rollData = DiceParser.GetRollData(command);
            //int howManyRolls = RollData[0];
            //int diceType = RollData[1];
            //int bonuses = RollData[2];

            return $"{rollData.HowManyRolls} rzutów kostką {rollData.DiceType}";
        }

        private static RollData GetRollData(string command)
        {
            RollData rollData = new RollData();
            int nextIndexToCheck = 0;

            List<int> rollDigits = new List<int>();
            for (int i = 0; i < command.Length; i++)
            {
                if (Regex.IsMatch(command[i].ToString(), @"\d"))
                {
                    rollDigits.Add(int.Parse(command[i].ToString()));
                    nextIndexToCheck = i + 1;
                    continue;
                }

                nextIndexToCheck = i + 1;
                break;
            }

            rollData.HowManyRolls = int.Parse(String.Join("", rollDigits.ToArray()));

            List<int> diceDigits = new List<int>(); // todo try only regex solution, without loops
            for (int i = nextIndexToCheck; i < command.Length; i++)
            {
                if (Regex.IsMatch(command[i].ToString(), @"\d"))
                    diceDigits.Add(int.Parse(command[i].ToString()));
                else
                    break;

                nextIndexToCheck = i + 1;
            }

            string rollsDiceString = String.Join("", diceDigits.ToArray()); // todo It's how to convert list to string!
            rollData.DiceType = int.Parse(rollsDiceString);

            return rollData;
        }

        //private int GetRollsQtyData(string command)
        //{

        //}

        public static bool IsADiceRoll(string command)
        {
            if (command == null)
                return false;

            if (Regex.IsMatch(command[0].ToString(), @"\d|k|d"))
                return true;

            return false;
        }
    }
}
