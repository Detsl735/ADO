using ADOLAB1.Models;
using System;

namespace ADOLAB1.Services
{
    public class LectureService
    {
        private readonly DatabaseService _databaseService;

        public LectureService(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public void AddLecture(string date, string topic)
        {
            string query = "INSERT INTO Lecture (Date, Topic) VALUES (@Date, @Topic)";
            _databaseService.ExecuteCommand(query, cmd =>
            {
                cmd.Parameters.AddWithValue("@Date", date);
                cmd.Parameters.AddWithValue("@Topic", topic);
            });
            Console.WriteLine($"Лекция {date} - {topic} добавлена!");
        }
    }
}
