using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace CodingTracker
{
    internal class ConsoleUI
    {
        private DataAccess _db;
        public ConsoleUI(DataAccess db)
        {
            _db = db;
        }

        public void Run()
        {
            Menu();
        }

        private void Menu()
        {
            UserOption[] menuOptions =
            {
                new() { Id = UIOptionKeyword.View, Label = "[yellow]VIEW[/] session logs" },
                new() { Id = UIOptionKeyword.Log, Label = "[yellow]LOG[/] new session" },
                new() { Id = UIOptionKeyword.Delete, Label = "[yellow]DELETE[/] session log" },
                new() { Id = UIOptionKeyword.Update, Label = "[yellow]UPDATE[/] session log" },
                new() { Id = UIOptionKeyword.Exit, Label = "[yellow]EXIT[/]"}
            };

            bool exit = false;
            while (!exit)
            {
                var userChoice =
                AnsiConsole.Prompt(
                    new SelectionPrompt<UserOption>()
                        .Title("Choose an action: ")
                        .AddChoices(menuOptions)
                        .UseConverter(x => x.Label));

                switch (userChoice.Id)
                {
                    case UIOptionKeyword.View:
                        Console.WriteLine("You chose: View");
                        ViewAll();
                        break;
                    case UIOptionKeyword.Log:
                        Console.WriteLine("You chose: Log");
                        Log();
                        break;
                    case UIOptionKeyword.Delete:
                        Console.WriteLine("You chose: Delete");
                        break;
                    case UIOptionKeyword.Update:
                        Console.WriteLine("You chose: Update");
                        break;
                    case UIOptionKeyword.Exit:
                        exit = true;
                        break;
                }
            }
            
        }
        private void ViewAll()
        {
            var sessions = _db.SelectAllSessions();
            var t = SessionTable(sessions);
            AnsiConsole.Write(t);
        }
        private Table SessionTable(IEnumerable<CodingSession> sessions)
        {
            Table t = new Table();
            string[] columns =
            {
                "ID",
                "Start Time",
                "End Time",
                "Duration in Minutes",
                "Date"
            };
            t.AddColumns(columns);
            foreach (CodingSession s in sessions)
            {
                t.AddRow(s.Id.ToString(), s.StartTime, s.EndTime, s.DurationMinutes.ToString(), s.Date);
            }
            return t;
        }
        private void Log()
        {
            var startTime =
                AnsiConsole.Prompt(
                    new TextPrompt<TimeOnly>("Format: [[HH:MM]]\n" +
                                             "[grey](If using 12-Hour notation, include AM/PM)[/]\n" +
                                             "[blue]Enter Start Time: [/]"));
            var endTime =
                AnsiConsole.Prompt(
                    new TextPrompt<TimeOnly>("[blue]Enter End Time: [/]"));

            var duration = (int)(endTime.ToTimeSpan() - startTime.ToTimeSpan()).TotalMinutes;

            var date =
                AnsiConsole.Prompt(
                    new TextPrompt<DateOnly>("Format: [[yyyy/mm/dd]]\n" +
                                             "[blue]Enter Date: [/]")).ToString("yyyy-MM-dd");

            var session = new CodingSession(startTime.ToString(), endTime.ToString(), duration, date);
            _db.InsertSession(session);
        }
    }
}
