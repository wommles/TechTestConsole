using System;
using System.Collections.Generic;
using System.Linq;
using RandomNameGeneratorLibrary;
using TechTestConsole.Data;
using TechTestConsole.Data.Entities;

namespace TechTestConsole.Services
{
    public interface IStudentGenerator
    {
        IList<Student> GenerateStudents(int amount);
        ICollection<Subject> GenerateSubjects(ICollection<Student> students);
        IList<ExamResult> GenerateResults(IList<Student> students, ICollection<Subject> subjects);
    }

    public class StudentGenerator : IStudentGenerator
    {
        private static readonly Random _rng = new();
        private static readonly List<string> _validPostCodes = new()
        {
            "AA1",
            "BB4",
            "CC1",
            "DD9"
        };

        public IList<Student> GenerateStudents(int amount)
        {
            var personGenerator = new PersonNameGenerator(_rng);

            int maleCount = amount / 2;
            int femaleCount = amount - maleCount;

            var maleFirstNames = personGenerator.GenerateMultipleMaleFirstNames(maleCount);
            var femaleFirstNames = personGenerator.GenerateMultipleFemaleFirstNames(femaleCount);

            var lastNames = personGenerator.GenerateMultipleLastNames(amount).ToList();
            var firstNames = maleFirstNames.Concat(femaleFirstNames).ToList();

            var students = new List<Student>(amount);
            for (int i = 0; i < amount; i++)
            {
                students.Add(new Student
                {
                    Id = i,
                    FirstName = firstNames[i],
                    LastName = lastNames[i],
                    Postcode = GetRandomPostCode()
                }); ;
            }

            return students;
        }

        private static string GetRandomPostCode()
        {
            var index = _rng.Next(0, _validPostCodes.Count);
            return _validPostCodes[index];
        }

        public ICollection<Subject> GenerateSubjects(ICollection<Student> students)
        {
            int subjectCount = SubjectTitle.GetNames(typeof(SubjectTitle)).Length;
            var subjects = new List<Subject>(subjectCount);

            for (int j = 0; j < subjectCount; j++)
            {
                subjects.Add(new Subject
                {
                    Title = SubjectTitle.GetNames(typeof(SubjectTitle))[j],
                    Students = students
                });
            }
            return subjects;
        }

        public IList<ExamResult> GenerateResults(IList<Student> students, ICollection<Subject> subjects)
        {
            var results = new List<ExamResult>();
            //Each student
            for (int i = 0; i < students.Count; i++)
            {
                //Each subject
                for (int j = 0; j < SubjectTitle.GetNames(typeof(SubjectTitle)).Length; j++)
                {
                    results.Add(new ExamResult
                    {
                        StudentId = students[i].Id,
                        Student = students[i],
                        Grade = _rng.Next(0, 100),
                        Subject = subjects.ElementAt(j),
                        SubjectId = j
                    }); ;
                }
            }
            return results;
        }
    }
}
