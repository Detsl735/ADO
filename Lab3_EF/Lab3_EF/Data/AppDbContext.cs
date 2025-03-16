using Lab3_EF.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab3_EF.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<Attendance> AttendanceRecords { get; set; }
    }
}
