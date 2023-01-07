using GentelmanParserDiscordBot.Handlers;
using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GentelmanParserDiscordBot.Dice
{
    public class DiceParser
    {
        public static string RollOutput(string command, ref Log log)
        {
            var rollData = GetRollBasicsInformation(command);

            if (rollData == null)
                return "Use wise numbers please";

            if (rollData.HowManyRolls > 30)
                return "Eeeh, really?";

            Roll(ref rollData);
            GetRollDetails(ref rollData);

            log.RollData = rollData;

            return OutputFormatter(rollData);
        }

        private static string OutputFormatter(RollData rollData)
        {
            var output = new StringBuilder();

            output.Append($"**{rollData.Sum}**");
            output.Append("\n```\n");

            output.Append($"{rollData.HowManyRolls}d{rollData.DiceType}");

            if (rollData.Bonuses != 0)
            {
                if (rollData.Bonuses > 0) output.Append("+");
                output.Append(rollData.Bonuses);
            }

            output.Append(" | ");
            output.Append($"[{string.Join(", ", rollData.Rolls)}]");

            if (rollData.Bonuses != 0)
                output.Append($" [{rollData.Bonuses}]");

            if (rollData.HowManyRolls > 1)
                output.Append($"\nAverage: {rollData.Average}");

            if (rollData.DiceType != 10 && rollData.HowManyRolls > 1)
                output.Append($"\nPower: {rollData.PercentOfMaximumResult}%");

            output.Append("\n```");

            return output.ToString();
        }

        public static void GetRollDetails(ref RollData rollData)
        {
            rollData.Average = Math.Round(rollData.Rolls.Average(), 2);
            rollData.PercentOfMaximumResult = rollData.Rolls.Sum() / (rollData.DiceType * (decimal)rollData.HowManyRolls) * 100;
            rollData.PercentOfMaximumResult = Math.Floor(rollData.PercentOfMaximumResult);
        }

        public static void Roll(ref RollData rollData)
        {
            var dice = new Random();

            for (var i = 0; i < rollData.HowManyRolls; i++)
            {
                rollData.Rolls.Add(dice.Next(1, rollData.DiceType + 1));
            }

            rollData.Sum = rollData.Rolls.Sum() + rollData.Bonuses;
        }

        public static RollData GetRollBasicsInformation(string command)
        {
            var rollData = new RollData();
            var digitPattern = @"\d";
            var digitOrNegativePattern = @"\d+|-\d+";

            // Rolls Qty
            try
            {
                var rollsQtyValues = command.TakeWhile(c => Regex.IsMatch(c.ToString(), digitPattern)).ToArray();
                rollData.HowManyRolls = rollsQtyValues.Length < 1 ? 1 : int.Parse(rollsQtyValues);

                // DiceType
                var dkIndex = command.IndexOfAny(new char[] { 'd', 'k' });
                var diceTypeValues = command.Substring(dkIndex + 1).TakeWhile(c => Regex.IsMatch(c.ToString(), digitPattern)).ToArray();
                rollData.DiceType = int.Parse(diceTypeValues);

                // Bonuses
                var bonusesStatedIndex = command.IndexOfAny(new char[] { '+', '-' });

                if (bonusesStatedIndex < 0)
                    rollData.Bonuses = 0;
                else
                {
                    var bonusValues = Regex.Matches(command.Substring(bonusesStatedIndex), digitOrNegativePattern);

                    foreach (Match element in bonusValues)
                    {
                        rollData.Bonuses += int.Parse(element.Value);
                    }
                }
            }
            catch
            {
                Console.WriteLine("Too big number for int type");
                return null;
            }

            return rollData;
        }

        public static bool IsADiceRoll(string command) => Regex.IsMatch(command, @"^[1-9]\d*[d|k][1-9]\d*[-|+|\d]*|^d[1-9]*\d*[-|+|\d]*|^k[1-9]*\d*[-|+|\d]*");
    }
}