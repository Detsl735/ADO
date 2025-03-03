using ADOLAB1.Models;
using System;

namespace ADOLAB1.Services
{
    public class StudentService
    {
        private readonly DatabaseService _databaseService;

        public StudentService(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public void AddStudent(string name)
        {
            string query = "INSERT INTO Student (Name) VALUES (@Name)";
            _databaseService.ExecuteCommand(query, cmd =>
            {
                cmd.Parameters.AddWithValue("@Name", name);
            });
            Console.WriteLine($"Студент {name} добавлен!");
        }
    }
}
