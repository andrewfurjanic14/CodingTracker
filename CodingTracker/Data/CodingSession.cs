using System;
using System.Collections.Generic;
using System.Text;

namespace CodingTracker.Data
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
        public CodingSession(int id, (string startTime, string endTime, int duration) timeInfo, string date)
        {
            Id = id;
            StartTime = timeInfo.startTime;
            EndTime = timeInfo.endTime;
            DurationMinutes = timeInfo.duration;
            Date = date;
        }
        public CodingSession((string startTime, string endTime, int duration) timeInfo, string date)
        {
            StartTime = timeInfo.startTime;
            EndTime = timeInfo.endTime;
            DurationMinutes = timeInfo.duration;
            Date = date;
        }
        public override string ToString()
        {
            string output =
                $"""
                Date: {Date}
                Start Time: {StartTime}, End Time: {EndTime}
                Duration: {DurationMinutes}
                Id: {Id}
                """;
            return output;
        }
    }
}
