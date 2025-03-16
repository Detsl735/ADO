using System;
using System.Linq;
using Lab3_EF.Data;
using Lab3_EF.Models;


namespace Lab3_EF.Repositories
{
    public class AttendanceRepository
    {
        private readonly AppDbContext _context;

        public AttendanceRepository(AppDbContext context)
        {
            _context = context;
        }

        public void AddAttendance(string studentName, DateTime lectureDate, int mark)
        {
           
            var attendance = new Attendance
            {
                StudentName = studentName,
                LectureDate = lectureDate.ToUniversalTime(), 
                Mark = mark
            };

            _context.AttendanceRecords.Add(attendance);
            _context.SaveChanges();
        }
    }
}
