using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace GentlemanParserDiscordBot
{
    public class DiceParser
    {
        public static string Roll(string command)
        {
            RollData rollData = DiceParser.GetRollData(command);
            Random dice = new Random();

            if (rollData == null)
                return "Use wise numbers please";

            if (rollData.HowManyRolls > 30)
                return "Eeeh, really?";

            for (int i = 0; i < rollData.HowManyRolls; i++)
            {
                Thread.Sleep(1);
                rollData.Rolls.Add(dice.Next(1, rollData.DiceType + 1));
            }

            string output = $"**{rollData.Rolls.Sum() + rollData.bonuses}**\n";
            output += $"```[{command}] | Rolls: [{string.Join(", ", rollData.Rolls)}] [{rollData.bonuses}] ";

            if (rollData.HowManyRolls > 1)
                output += $"\nAverage: {Math.Round(rollData.Rolls.Average(), 2)}";

            if (rollData.DiceType != 10 && rollData.HowManyRolls > 1)
            {
                rollData.PercentOfMaximumResult = (float)rollData.Rolls.Sum() / ((float)rollData.DiceType * (float)rollData.HowManyRolls) * (float)100;
                output += $"\n{Math.Floor(rollData.PercentOfMaximumResult),2}%";
            }

            output += "```";

            return output;
        }

        private static RollData GetRollData(string command)
        {
            RollData rollData = new RollData();
            string isDigid = @"\d";
            string isDigidOrMinusDigid = @"\d+|-\d+";

            // Rolls Qty
            try
            {
                var rollsQtyValues = command.TakeWhile(c => Regex.IsMatch(c.ToString(), isDigid)).ToArray();
                if (rollsQtyValues.Length < 1)
                    rollData.HowManyRolls = 1;
                else
                    rollData.HowManyRolls = int.Parse(rollsQtyValues);

                // DiceType
                int dkIndex = command.IndexOfAny(new char[] { 'd', 'k' });
                var diceTypeValues = command.Substring(dkIndex + 1).TakeWhile(c => Regex.IsMatch(c.ToString(), isDigid)).ToArray();
                rollData.DiceType = int.Parse(diceTypeValues.ToArray());

                // Bonuses
                int bonusesStatedIndex = command.IndexOfAny(new char[] { '+', '-' });

                if (bonusesStatedIndex < 0)
                    rollData.bonuses = 0;
                else
                {
                    var bonusValues = Regex.Matches(command.Substring(bonusesStatedIndex), isDigidOrMinusDigid);

                    foreach (Match element in bonusValues)
                    {
                        rollData.bonuses += int.Parse(element.Value);
                    }
                }
            }
            catch (Exception notCorrectNumber)
            {
                Console.WriteLine("Too big number for int type");
                return null;
            }

            return rollData;
        }


        public static bool IsADiceRoll(string command)
        {
            if (command == null)
                return false;

            if (Regex.Matches(command, @"d\d+|k\d").Count > 0)
                return true;

            return false;
        }
    }
}
