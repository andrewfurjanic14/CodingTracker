using System;
using System.Collections.Generic;
using System.Text;

namespace CodingTracker
{
    internal class CodingSession
    {
        public int Id { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int DurationMinutes { get; set; }
        public string Date { get; set; }

        public CodingSession() { }
        public CodingSession(string startTime, string endTime, int duration, string date)
        {
            StartTime = startTime;
            EndTime = endTime;
            DurationMinutes = duration;
            Date = date;
        }
        public override string ToString()
        {
            string output =
                $"""
                Id: {Id}
                Start Time: {StartTime}
                End Time: {EndTime}
                Duration: {DurationMinutes}
                Date: {Date}
                """;
            return output;
        }
    }
}
