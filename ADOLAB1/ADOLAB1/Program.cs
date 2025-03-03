using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using ADOLAB1.Services;

class Program
{
    static void Main()
    {
        IConfigurationRoot config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        string connectionString = config.GetConnectionString("PostgreSqlConnection");
        var dbService = new DatabaseService(connectionString);

        var studentService = new StudentService(dbService);
        var lectureService = new LectureService(dbService);
        var attendanceService = new AttendanceService(dbService);

        while (true)
        {
            Console.Write("\nВведите команду: ");
            string input = Console.ReadLine();
            string[] args = input.Split(' ');

            switch (args[0].ToLower())
            {
                case "sc-init":
                    dbService.InitDatabase();
                    break;
                case "sc-student":
                    studentService.AddStudent(args[1]);
                    break;
                case "sc-lecture":
                    lectureService.AddLecture(args[1], args[2]);
                    break;
                case "sc-attend":
                    attendanceService.MarkAttendance(args[1], args[2], int.Parse(args[3]));
                    break;
                case "sc-report":
                    attendanceService.GenerateReport();
                    break;
                case "exit":
                    return;
                default:
                    Console.WriteLine("Неизвестная команда!");
                    break;
            }
        }
    }
}
