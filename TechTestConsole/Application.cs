using System;
using System.Collections.Generic;
using TechTestConsole.Data;
using TechTestConsole.Data.Entities;
using TechTestConsole.Services;

namespace TechTestConsole
{
    public class Application
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IStudentGenerator _studentGenerator;
        private readonly IGradeAdjuster _gradeAdjuster;
        private readonly ISummaryCalculator _summaryCalculator;

        public Application(ApplicationDbContext dbContext, IStudentGenerator studentGenerator, 
            IGradeAdjuster gradeAdjuster, ISummaryCalculator summaryCalculator)
        {
            _dbContext = dbContext;
            _studentGenerator = studentGenerator;
            _gradeAdjuster = gradeAdjuster;
            _summaryCalculator = summaryCalculator;
        }

        public void Run()
        {
            Console.WriteLine("Starting");
            var students = _studentGenerator.GenerateStudents(10);
            var subjects = _studentGenerator.GenerateSubjects(students);
            IList<ExamResult> results = _studentGenerator.GenerateResults(students, subjects);
            _summaryCalculator.ResultSummary(results, students, subjects);

            _summaryCalculator.OutputResults();
            Console.Write("\n\n \t\tGRADES HAVE BEEN ADJUSTED \n\n");
            _summaryCalculator.ResultSummary(_gradeAdjuster.AdjustGrade(results), students, subjects);

            _summaryCalculator.OutputResults();
        }
    }
}