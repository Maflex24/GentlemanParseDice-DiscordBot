using Discord.WebSocket;
using GentelmanParserDiscordBot.Commands;
using GentelmanParserDiscordBot.Dice;
using GentelmanParserDiscordBot.Logger;
using GentlemanParserDiscordBot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GentelmanParserDiscordBot.Handlers
{
    public class CommandHandler
    {
        private readonly IDataHandler _dataHandler;

        public CommandHandler(IDataHandler dataHandler)
        {
            _dataHandler = dataHandler;
        }

        private static readonly Dictionary<string, List<string>> commandsAndOutputMessages = DataHandler.GetCommandsList();

        private static bool CommandExist(string commandContent) => commandsAndOutputMessages.Keys.Contains(commandContent);

        public Task Handler(SocketMessage message)
        {
            var stopwatch = Stopwatch.StartNew();

            var log = new Log();
            log.Author = message.Author.Username;

            if (!message.Content.StartsWith(DevelopmentInfo.CommandPrefix) || message.Author.IsBot)
                return Task.CompletedTask;

            var commandLength = message.Content.Contains(' ') ? message.Content.IndexOf(' ') : message.Content.Length;
            var commandContext = message.Content.Substring(1, commandLength - 1).ToLower();
            log.Command = commandContext;

            if (DiceParser.IsADiceRoll(commandContext))
            {
                var rollOutput = DiceParser.RollOutput(commandContext, ref log);

                message.Channel.SendMessageAsync(message.Author.Mention + ": " + rollOutput);

                stopwatch.Stop();
                SimpleLogger.Log(LogType.Roll, ref log, ref stopwatch);
                return Task.CompletedTask;
            }

            // For "Omerta" game system. I tried to use actual code, not to change too much, try to do id fast
            if (commandContext.StartsWith("om"))
            {
                try
                {
                    var logDifficultRollData = new Log();
                    var logActionRollData = new Log();
                    logDifficultRollData.Author = message.Author.Username;
                    logActionRollData.Author = message.Author.Username;

                    var difficultRollData = new RollData(2, 10);
                    DiceParser.Roll(ref difficultRollData);

                    var omertaActionCommand = "1d6";
                    omertaActionCommand += commandContext.Substring(2);
                    var actionRollData = DiceParser.GetRollBasicsInformation(omertaActionCommand);

                    DiceParser.Roll(ref actionRollData);

                    var resultType = string.Empty;

                    if (actionRollData.Sum > difficultRollData.Rolls.Max())
                        resultType = "Sukces";
                    else if (actionRollData.Sum < difficultRollData.Rolls.Min())
                        resultType = "Porażka";
                    else
                        resultType = "Częściowy sukces";

                    var bonusInfo = string.Empty;

                    if (actionRollData.Bonuses > 0)
                        bonusInfo = $"[+{actionRollData.Bonuses}]";
                    else if (actionRollData.Bonuses == 0)
                        bonusInfo = string.Empty;
                    else
                        bonusInfo = $"[{actionRollData.Bonuses}]";

                    var output = $"**{resultType}**";
                    output += $"\n**Poziom trudności:** {difficultRollData.Rolls.Min()} - {difficultRollData.Rolls.Max()}";
                    output += $"\n**Wylosowałeś łącznie:** {actionRollData.Sum} | [{actionRollData.Rolls[0]}] {bonusInfo}";

                    message.Channel.SendMessageAsync(message.Author.Mention + ": " + output);

                    stopwatch.Stop();
                    logDifficultRollData.RollData = difficultRollData;
                    logActionRollData.RollData = actionRollData;
                    SimpleLogger.Log(LogType.Roll, ref logDifficultRollData, ref stopwatch);
                    SimpleLogger.Log(LogType.Roll, ref logActionRollData, ref stopwatch);
                    return Task.CompletedTask;
                }
                catch
                {
                    message.Channel.SendMessageAsync(message.Author.Mention + ": " + "Coś pochrzaniłeś");
                }
            }

            if (!CommandExist(commandContext))
            {
                message.Channel.SendMessageAsync(message.Author.Mention + " Your command is not valid, Dear");
                return Task.CompletedTask;
            }

            var indexOfOutputMessage = rollOutputIndex(commandContext);
            var command = getNewCommand(commandContext, indexOfOutputMessage, message);

            log.Reply = command.ExecuteCommand(message);

            stopwatch.Stop();
            SimpleLogger.Log(LogType.Command, ref log, ref stopwatch);

            return Task.CompletedTask;
        }

        private ICommand getNewCommand(string commandContext, int index, SocketMessage message)
        {
            var imageExtensions = new List<string>() { @".jpg$", @".png$", @".gif$", $".jpeg$" };
            var isImageType = false;

            foreach (var imageExtension in imageExtensions)
            {
                if (!Regex.IsMatch(commandsAndOutputMessages[commandContext][index], imageExtension))
                    continue;

                isImageType = true;
                break;
            }

            if (isImageType)
                return new ImageCommand(commandContext, commandsAndOutputMessages[commandContext][index], message);

            return new TextCommand(commandContext, commandsAndOutputMessages[commandContext][index], message);
        }

        private int rollOutputIndex(string commandContext)
        {
            var random = new Random();
            return random.Next(0, commandsAndOutputMessages[commandContext].Count);
        }
    }
}