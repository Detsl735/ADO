using System;
using Npgsql;

namespace ADOLAB1.Services
{
    public class DatabaseService
    {
        private readonly string _connectionString;

        public DatabaseService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void InitDatabase()
        {
            string createTablesQuery = @"
                CREATE TABLE IF NOT EXISTS Student (
                    Name TEXT PRIMARY KEY
                );

                CREATE TABLE IF NOT EXISTS Lecture (
                    Date TEXT PRIMARY KEY,
                    Topic TEXT NOT NULL
                );

                CREATE TABLE IF NOT EXISTS Attendance (
                    LectureDate TEXT REFERENCES Lecture(Date),
                    StudentName TEXT REFERENCES Student(Name),
                    Mark INT,
                    PRIMARY KEY (LectureDate, StudentName)
                );

                -- Создание хранимой процедуры для отметки посещаемости
                CREATE OR REPLACE FUNCTION MarkAttendance(_studentName TEXT, _lectureDate TEXT, _mark INTEGER) RETURNS VOID AS $$
                BEGIN
                    INSERT INTO Attendance (LectureDate, StudentName, Mark) 
                    VALUES (_lectureDate, _studentName, _mark);
                END;
                $$ LANGUAGE plpgsql;
                ";

            ExecuteCommand(createTablesQuery);
            Console.WriteLine("База данных инициализирована!");
        }

        public void ExecuteCommand(string query, Action<NpgsqlCommand> parameterize = null)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand(query, connection))
                {
                    parameterize?.Invoke(command);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Метод для выполнения SQL-запросов (SELECT)
        public List<T> ExecuteQuery<T>(string query, Func<NpgsqlDataReader, T> readRow)
        {
            List<T> result = new List<T>();

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(readRow(reader));
                        }
                    }
                }
            }

            return result;
        }
    }
}
