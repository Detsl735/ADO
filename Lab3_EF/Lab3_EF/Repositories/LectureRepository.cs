using System;
using System.Linq;
using Lab3_EF.Data;
using Lab3_EF.Models;


namespace Lab3_EF.Repositories
{
    public class LectureRepository
    {
        private readonly AppDbContext _context;

        public LectureRepository(AppDbContext context)
        {
            _context = context;
        }

        public void AddLecture(DateTime date, string topic)
        {
            _context.Lectures.Add(new Lecture { Date = date, Topic = topic });
            _context.SaveChanges();
        }
    }
}
