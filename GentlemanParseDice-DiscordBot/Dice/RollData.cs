using System.Collections.Generic;

namespace GentelmanParserDiscordBot.Dice
{
    public class RollData
    {
        public int HowManyRolls { get; set; }
        public int DiceType { get; set; }
        public int Bonuses { get; set; }
        public List<int> Rolls = new List<int>();
        public int Sum { get; set; }
        public decimal PercentOfMaximumResult { get; set; }
        public double Average { get; set; }
    }
}