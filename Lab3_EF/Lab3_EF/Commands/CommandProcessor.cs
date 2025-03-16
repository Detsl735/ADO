using System;
using Lab3_EF.Repositories;
using Lab3_EF.Services;

namespace Lab3_EF.Commands
{
    public class CommandProcessor
    {
        private readonly DatabaseInitializer _dbInitializer;
        private readonly StudentRepository _studentRepo;
        private readonly LectureRepository _lectureRepo;
        private readonly AttendanceRepository _attendanceRepo;
        private readonly ReportService _reportService;

        public CommandProcessor(DatabaseInitializer dbInitializer, StudentRepository studentRepo, LectureRepository lectureRepo, AttendanceRepository attendanceRepo, ReportService reportService)
        {
            _dbInitializer = dbInitializer;
            _studentRepo = studentRepo;
            _lectureRepo = lectureRepo;
            _attendanceRepo = attendanceRepo;
            _reportService = reportService;
        }

        public void Process(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Команда не указана.");
                return;
            }

            switch (args[0])
            {
                case "sc-init":
                    _dbInitializer.Initialize();
                    break;
                case "sc-student":
                    if (args.Length > 1) _studentRepo.AddStudent(args[1]);
                    break;
                case "sc-lecture":
                    if (args.Length > 2 && DateTime.TryParse(args[1], out var date))
                        _lectureRepo.AddLecture(date, args[2]);
                    break;
                case "sc-attend":
                    var studentName = args[1];
                    var lectureDate = DateTime.Parse(args[2]).ToUniversalTime();
                    var mark = int.Parse(args[3]);
                    _attendanceRepo.AddAttendance(studentName, lectureDate, mark);
                    break;
                case "sc-report":
                    _reportService.GenerateReport();
                    break;
                default:
                    Console.WriteLine("Неизвестная команда.");
                    break;
            }
        }
    }
}
