using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace CodingTracker.Data
{
    internal class DataAccess
    {
        private readonly string _connectionString;
        
        public DataAccess()
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            _connectionString =
                config.GetConnectionString("CodingTrackerDb")
                ?? throw new InvalidOperationException("Connectionstring failed");

            CreateCodingSessionsTable();
        }
        
        public void InsertSession(CodingSession session)
        {
            string sql =
                """
                INSERT INTO CodingSessions (StartTime, EndTime, DurationMinutes, Date)
                VALUES (@StartTime, @EndTime, @DurationMinutes, @Date);
                """;
            using var con = new SqliteConnection(_connectionString);
            con.Execute(sql, session);
        }

        public List<CodingSession> SelectSessions()
        {
            string sql = 
                """
                SELECT
                    Id,
                    StartTime,
                    EndTime,
                    DurationMinutes,
                    Date
                FROM CodingSessions;
                """;
            using var con = new SqliteConnection(_connectionString);
            return con.Query<CodingSession>(sql).ToList();
        }
        public List<CodingSession> SelectSessions(string date)
        {
            string sql =
                """
                SELECT
                    Id,
                    StartTime,
                    EndTime,
                    DurationMinutes,
                    Date
                FROM CodingSessions
                WHERE Date = @Date;
                """;
            using var con = new SqliteConnection(_connectionString);
            return con.Query<CodingSession>(sql, new { Date = date }).ToList();
        }

        public void DeleteSession(CodingSession session)
        {
            string sql =
                """
                DELETE FROM CodingSessions
                WHERE Id = @Id
                """;
            using var con = new SqliteConnection(_connectionString);
            con.Execute(sql, session);
        }

        public void UpdateSession(CodingSession session)
        {
            string sql =
                """
                UPDATE CodingSessions
                SET
                    StartTime = @StartTime,
                    EndTime = @EndTime,
                    DurationMinutes = @DurationMinutes,
                    Date = @Date
                WHERE Id = @Id
                """;
            var con = new SqliteConnection(_connectionString);
            con.Execute(sql, session);
        }

        public void TestConnection()
        {
            try
            {
                var con = new SqliteConnection(_connectionString);
                con.Open();
                con.Close();
                Console.WriteLine("Connection Success");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void CreateCodingSessionsTable()
        {
            string sql =
                """
                CREATE TABLE IF NOT EXISTS CodingSessions (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    StartTime TEXT NOT NULL,
                    EndTime TEXT,
                    DurationMinutes INTEGER,
                    Date TEXT NOT NULL
                );
                """;
            using var con = new SqliteConnection(_connectionString);
            con.Execute(sql);
        }
    }
}
