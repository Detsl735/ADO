using System;
using Lab3_EF.Data;
using Microsoft.EntityFrameworkCore;

namespace Lab3_EF.Services
{
    public class DatabaseInitializer
    {
        private readonly AppDbContext _context;

        public DatabaseInitializer(AppDbContext context)
        {
            _context = context;
        }

        public void Initialize()
        {
            _context.Database.EnsureCreated();
        }
    }
}
