using System;
using System.IO;
using System.Linq;
using System.Text;
using Lab3_EF.Data;
using Lab3_EF.Models;

namespace Lab3_EF.Services
{
    public class ReportService
    {
        private readonly AppDbContext _context;

        public ReportService(AppDbContext context)
        {
            _context = context;
        }

        public void GenerateReport()
        {
            
            var students = _context.Students.ToList();
            var lectures = _context.Lectures.ToList();
            var attendanceRecords = _context.AttendanceRecords.ToList();

            
            var csvBuilder = new StringBuilder();
            csvBuilder.AppendLine("Дата лекции,Тема,Студент,Оценка");

            
            foreach (var lecture in lectures)
            {
                var lectureAttendance = attendanceRecords
                    .Where(a => a.LectureDate.Date == lecture.Date.Date)  
                    .ToList();

                
                if (lectureAttendance.Count == 0)
                {
                    
                    csvBuilder.AppendLine($"{lecture.Date.ToUniversalTime():yyyy-MM-dd},{lecture.Topic},—,—");
                }
                else
                {
                   
                    foreach (var record in lectureAttendance)
                    {
                        csvBuilder.AppendLine($"{lecture.Date.ToUniversalTime():yyyy-MM-dd},{lecture.Topic},{record.StudentName},{record.Mark}");
                    }
                }
            }

           
            foreach (var student in students)
            {
                bool hasAttendance = attendanceRecords.Any(a => a.StudentName == student.Name);
                if (!hasAttendance)
                {
                    
                    csvBuilder.AppendLine($",,{student.Name},—");
                }
            }

          
            File.WriteAllText("attendance_report.csv", csvBuilder.ToString(), Encoding.UTF8);
            Console.WriteLine("Отчет сохранен в файл attendance_report.csv");
        }



    }
}
