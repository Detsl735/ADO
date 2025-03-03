using ADOLAB1.Models;
using System;
using System.Collections.Generic;

namespace ADOLAB1.Services
{
    public class AttendanceService
    {
        private readonly DatabaseService _databaseService;

        public AttendanceService(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public void MarkAttendance(string studentName, string date, int mark)
        {
            string query = @"
        CALL markattendance(@StudentName, @LectureDate, @Mark);";

            _databaseService.ExecuteCommand(query, cmd =>
            {
                cmd.Parameters.AddWithValue("@StudentName", studentName);
                cmd.Parameters.AddWithValue("@LectureDate", date);
                cmd.Parameters.AddWithValue("@Mark", mark);
            });
        }



        public void GenerateReport()
        {
            string query = @"
                SELECT Lecture.Date, Lecture.Topic, Student.Name, 
                       COALESCE(Attendance.Mark::TEXT, 'Не посещал') AS Mark
                FROM Lecture
                CROSS JOIN Student
                LEFT JOIN Attendance ON Lecture.Date = Attendance.LectureDate AND Student.Name = Attendance.StudentName
                ORDER BY Lecture.Date, Student.Name;";

            var reportData = _databaseService.ExecuteQuery(query, reader => new Attendance
            {
                LectureDate = reader["Date"].ToString(),
                StudentName = reader["Name"].ToString(),
                Mark = reader["Mark"].ToString() == "Не посещал" ? -1 : int.Parse(reader["Mark"].ToString())
            });

            Console.WriteLine("\nОтчет о посещаемости:");
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("| Дата лекции  | Тема         | Студент       | Оценка      |");
            Console.WriteLine("------------------------------------------------------------");

            foreach (var record in reportData)
            {
                string mark = record.Mark == -1 ? "Не посещал" : record.Mark.ToString();
                Console.WriteLine($"| {record.LectureDate,-12} | {record.StudentName,-12} | {mark,-10} |");
            }

            Console.WriteLine("------------------------------------------------------------");
        }
    }
}
