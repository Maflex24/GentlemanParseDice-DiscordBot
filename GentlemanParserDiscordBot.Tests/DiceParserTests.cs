using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GentlemanParserDiscordBot.Tests
{
    public class DiceParserTests
    {
        [Theory]
        [InlineData("d10")]
        [InlineData("k10")]
        [InlineData("1k10")]
        [InlineData("1d10")]
        [InlineData("1k10+4")]
        [InlineData("1d10-4")]
        [InlineData("15k10+4+4")]
        [InlineData("15d10-4-4")]
        public void IsADiceRoll_ForStringInput_ReturnTrue(string command)
        {
            var result = DiceParser.IsADiceRoll(command);
            Assert.True(result);
        }

        [Theory]
        [InlineData("dk10")]
        [InlineData("-1d10")]
        [InlineData("-110")]
        public void IsADiceRoll_ForStringInput_ReturnFalse(string command)
        {
            var result = DiceParser.IsADiceRoll(command);
            Assert.False(result);
        }

        [Theory]
        [InlineData("d10", 1)]
        [InlineData("3k6", 3)]
        [InlineData("10d100", 10)]
        [InlineData("11d6", 11)]
        public void GetRollBasicsInformation_ForStringInput_ReturnHowManyRolls(string command, int expected)
        {
            var rollData = DiceParser.GetRollBasicsInformation(command);
            Assert.Equal(expected, rollData.HowManyRolls);
        }

        [Theory]
        [InlineData("d10", 10)]
        [InlineData("3k6", 6)]
        [InlineData("10d100", 100)]
        [InlineData("11d6", 6)]
        public void GetRollBasicsInformation_ForStringInput_ReturnDiceType(string command, int expected)
        {
            var rollData = DiceParser.GetRollBasicsInformation(command);
            Assert.Equal(expected, rollData.DiceType);
        }

        [Theory]
        [InlineData("d10", 0)]
        [InlineData("3k6-3", -3)]
        [InlineData("10d100-20+10", -10)]
        [InlineData("11d6+4+2-2", 4)]
        public void GetRollBasicsInformation_ForStringInput_ReturnBonuses(string command, int expected)
        {
            var rollData = DiceParser.GetRollBasicsInformation(command);
            Assert.Equal(expected, rollData.Bonuses);
        }

        [Theory]
        [InlineData("5d6+4")]
        [InlineData("5d10+4")]
        [InlineData("5d10-2")]
        [InlineData("6d100+4")]
        public void Roll_ForRollCommand_CheckSumBetweenMinimumAndMaximum(string command)
        {
            var basicRollData = DiceParser.GetRollBasicsInformation(command);
            var minimumValue = basicRollData.HowManyRolls * 1 + basicRollData.Bonuses;
            var maximumValue = basicRollData.HowManyRolls * basicRollData.DiceType + basicRollData.Bonuses;

            for (int i = 0; i < basicRollData.DiceType; i++)
            {
                RollData rollData = new RollData();
                rollData.Bonuses = basicRollData.Bonuses;
                rollData.DiceType = basicRollData.DiceType;
                rollData.HowManyRolls = basicRollData.HowManyRolls;

                DiceParser.Roll(ref rollData);

                Assert.InRange(rollData.Sum, minimumValue, maximumValue);
            }
        }

        [Theory]
        [InlineData("30d6", 2, 5)]
        [InlineData("30d10", 4, 6)]
        [InlineData("30d20", 8, 12)]
        [InlineData("30d100", 40, 60)]
        public void Roll_ForRollCommand_CheckIsAverageInNorm(string command, int min, int max)
        {
            var rollData = DiceParser.GetRollBasicsInformation(command);
            DiceParser.Roll(ref rollData);
            DiceParser.GetRollDetails(ref rollData);

            Assert.InRange(rollData.Average, min, max);
        }

        [Theory]
        [InlineData("30d6", 1, 6)]
        [InlineData("60d10", 1, 10)]
        [InlineData("120d20", 1, 20)]
        [InlineData("600d100", 1, 100)]
        public void Roll_ForRollCommand_CheckMinimumAndMaximum(string command, int min, int max)
        {
            var rollData = DiceParser.GetRollBasicsInformation(command);
            DiceParser.Roll(ref rollData);

            var minRolled = rollData.Rolls.Min();
            var maxRolled = rollData.Rolls.Max();

            Assert.Equal(min, minRolled);
            Assert.Equal(max, maxRolled);
        }

    }
}
