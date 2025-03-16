using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using Lab3_EF.Commands;
using Lab3_EF.Data;
using Lab3_EF.Repositories;
using Lab3_EF.Services;

class Program
{
    static void Main()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        var serviceProvider = new ServiceCollection()
            .AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(connectionString))
            .AddSingleton<DatabaseInitializer>()
            .AddSingleton<StudentRepository>()
            .AddSingleton<LectureRepository>()
            .AddSingleton<AttendanceRepository>()
            .AddSingleton<ReportService>()
            .AddSingleton<CommandProcessor>()
            .BuildServiceProvider();

        var commandProcessor = serviceProvider.GetService<CommandProcessor>();


        while (true)
        {
            Console.Write("\n> ");
            var input = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(input)) continue;

            if (input.ToLower() == "exit")
            {
                break;
            }

            try
            {
                var args = input.Split(' ');
                commandProcessor?.Process(args);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Внутренняя ошибка: {ex.InnerException.Message}");
                    Console.WriteLine($"Стек вызовов: {ex.InnerException.StackTrace}");
                }
            }

        }
    }
}
