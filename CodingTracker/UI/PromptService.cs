using CodingTracker.Data;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodingTracker.UI
{
    internal static class PromptService
    {
        internal static readonly UserOption[] MenuOptions =
            {
                new() { Id = UIOptionKeyword.View, Label = "[yellow]VIEW[/] session logs" },
                new() { Id = UIOptionKeyword.Log, Label = "[yellow]LOG[/] new session" },
                new() { Id = UIOptionKeyword.Delete, Label = "[yellow]DELETE[/] session log" },
                new() { Id = UIOptionKeyword.Update, Label = "[yellow]UPDATE[/] session log" },
                new() { Id = UIOptionKeyword.Exit, Label = "[yellow]EXIT[/]"}
            };

        /// <summary>
        /// Prompts user for Date
        /// </summary>
        /// <param name="defaultDate">Optional default date</param>
        /// <param name="instruction">Optional instruction to be prepended to the prompt</param>
        /// <returns>The user-inputted date as a string</returns>
        internal static string Date(DateOnly? defaultDate = null, string? instruction = null)
        {
            string prompt = "Format: [[yyyy/mm/dd]]\n" +
                            "[blue]Enter Date: [/]";
            if (string.IsNullOrEmpty(instruction))
            {
                prompt = $"{instruction}\n{prompt}";
            }

            var input = new TextPrompt<DateOnly>(prompt);

            if (defaultDate.HasValue)
            {
                input.DefaultValue(defaultDate.Value);
            }

            return AnsiConsole.Prompt(input).ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// Prompts the user for start-time and end-time. Calculates duration-minutes based on input.
        /// </summary>
        /// <param name="defaultSession">An optional session that's fields are used as default values</param>
        /// <returns>Tuple containing string startTime, string endTime, and int durationMinutes</returns>
        internal static (string, string, int) TimeInfo(CodingSession? defaultSession = null)
        {
            var startTimePrompt = 
                new TextPrompt<TimeOnly>("Format: [[HH:MM]]\n" +
                                             "[grey](If using 12-Hour notation, include AM/PM)[/]\n" +
                                             "[blue]Enter Start Time: [/]");
            var endTimePrompt = new TextPrompt<TimeOnly>("[blue]Enter End Time: [/]");
            if (defaultSession is not null)
            {
                startTimePrompt.DefaultValue(TimeOnly.Parse(defaultSession.StartTime));
                endTimePrompt.DefaultValue(TimeOnly.Parse(defaultSession.EndTime));
            }

            var startTime = AnsiConsole.Prompt(startTimePrompt);
            var endTime = AnsiConsole.Prompt(endTimePrompt);

            int duration = (int)(endTime - startTime).TotalMinutes;

            return (startTime.ToString(), endTime.ToString(), duration);
        }
        internal static UserOption ChooseFromAllOrDate()
        {
            UserOption[] options =
            {
                new() { Id = UIOptionKeyword.ChooseFromAll, Label = "Choose from all"},
                new() { Id = UIOptionKeyword.ChooseFromDate, Label = "Choose from date"}
            };

            var userChoice =
                AnsiConsole.Prompt(
                    new SelectionPrompt<UserOption>()
                        .AddChoices(options)
                        .UseConverter(x => x.Label));
            return userChoice;
        }

        internal static CodingSession ChooseFromSessions(IEnumerable<CodingSession> sessions)
        {
            var sessionChoice =
                AnsiConsole.Prompt(
                    new SelectionPrompt<CodingSession>()
                        .PageSize(5)
                        .MoreChoicesText("[grey]Move down to reveal more choices[/]")
                        .AddChoices(sessions));
            return sessionChoice;
        }
    }
}
