using System.Linq;
using Lab3_EF.Data;
using Lab3_EF.Models;


namespace Lab3_EF.Repositories
{
    public class StudentRepository
    {
        private readonly AppDbContext _context;

        public StudentRepository(AppDbContext context)
        {
            _context = context;
        }

        public void AddStudent(string name)
        {
            _context.Students.Add(new Student { Name = name });
            _context.SaveChanges();
        }
    }
}
