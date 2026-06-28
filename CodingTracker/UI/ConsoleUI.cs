using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;
using CodingTracker.Data;

namespace CodingTracker.UI
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
            var menuOptions = PromptService.MenuOptions;
            var promptInput =
                new SelectionPrompt<UserOption>()
                        .Title("Choose an action: ")
                        .AddChoices(menuOptions)
                        .UseConverter(x => x.Label);

            bool exit = false;
            while (!exit)
            {
                var userChoice = AnsiConsole.Prompt(promptInput);

                switch (userChoice.Id)
                {
                    case UIOptionKeyword.View:
                        Console.WriteLine("You chose: View");
                        View();
                        break;
                    case UIOptionKeyword.Log:
                        Console.WriteLine("You chose: Log");
                        Log();
                        break;
                    case UIOptionKeyword.Delete:
                        Console.WriteLine("You chose: Delete");
                        Delete();
                        break;
                    case UIOptionKeyword.Update:
                        Console.WriteLine("You chose: Update");
                        Update();
                        break;
                    case UIOptionKeyword.Exit:
                        exit = true;
                        break;
                }
            }
        }

        private void Delete()
        {
            var userChoice = PromptService.ChooseFromAllOrDate();

            List<CodingSession> sessions = null;
            switch (userChoice.Id)
            {
                case UIOptionKeyword.ChooseFromAll:
                    sessions = _db.SelectSessions();
                    break;
                case UIOptionKeyword.ChooseFromDate:
                    var date = PromptService.Date();
                    sessions = _db.SelectSessions(date);
                    break;
            }
            if (sessions is null)
            {
                throw new ArgumentNullException($"{nameof(sessions)} from Delete");
            }
            var sessionChoice = PromptService.ChooseFromSessions(sessions);
            _db.DeleteSession(sessionChoice);
        }

        private void View()
        {
            var sessions = _db.SelectSessions();
            var t = SessionTable(sessions);
            AnsiConsole.Write(t);
        }

        private void Log()
        {
            var timeInfo = PromptService.TimeInfo();

            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            string instruction = $"Press ENTER to use today's date: {today.ToString("yyyy-MM-dd")}";
            var date = PromptService.Date(today, instruction);

            var session = new CodingSession(timeInfo, date);
            _db.InsertSession(session);

            var duration = timeInfo.Item3;
            Console.WriteLine($"Logged sessions with duration of: {duration} minutes");
        }

        private void Update()
        {
            var userChoice = PromptService.ChooseFromAllOrDate();

            List<CodingSession> sessions = null;
            switch (userChoice.Id)
            {
                case UIOptionKeyword.ChooseFromAll:
                    sessions = _db.SelectSessions();
                    break;
                case UIOptionKeyword.ChooseFromDate:
                    var date = PromptService.Date();
                    sessions = _db.SelectSessions(date);
                    break;
            }
            if (sessions is null)
            {
                throw new ArgumentNullException($"{nameof(sessions)} from Update");
            }
            var sessionChoice = PromptService.ChooseFromSessions(sessions);

            Console.WriteLine("Press ENTER to use the existing value");
            var newTimeInfo = PromptService.TimeInfo(sessionChoice);
            var defaultDate = DateOnly.Parse(sessionChoice.Date);
            var newDate = PromptService.Date(defaultDate);
            var updatedSession = new CodingSession(sessionChoice.Id, newTimeInfo, newDate);
            _db.UpdateSession(updatedSession);
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
    }
}
