using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TechTestConsole.Data;
using TechTestConsole.Services;

namespace TechTestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //setup our DI
            var serviceProvider = new ServiceCollection()
                .AddTransient<IStudentGenerator, StudentGenerator>()
                .AddTransient<IGradeAdjuster, GradeAdjuster>()
                .AddTransient<ISummaryCalculator, SummaryCalculator>()
                .AddSingleton<Application>()
                .AddDbContext<ApplicationDbContext>()
                .BuildServiceProvider();
            
            var app = serviceProvider.GetService<Application>();
            app.Run();

            Console.WriteLine("Done");
        }
    }
}