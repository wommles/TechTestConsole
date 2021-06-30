using System;
using System.Collections.Generic;
using System.Linq;
using TechTestConsole.Data;
using TechTestConsole.Data.Entities;

namespace TechTestConsole.Services
{
    public interface ISummaryCalculator
    {
        void ResultSummary(IList<ExamResult> _results, IList<Student> _students, ICollection<Subject> _subjects);
        void OutputResults();
    }

    public class SummaryCalculator : ISummaryCalculator
    {
        bool repeat = false;
        private static readonly List<string> _validPostCodes = new()
        {
            "AA1",
            "BB4",
            "CC1",
            "DD9"
        };
        //Initialise variables to be accessiables throughout class
        double overallScore = 0;
        int failedCount = 0;
        Dictionary<string, double> pcScoreDict = new Dictionary<string, double>();
        Dictionary<int, double> studentScoreDict = new Dictionary<int, double>();
        Dictionary<KeyValuePair<string, string>, double> subjectPcScoreDict = new Dictionary<KeyValuePair<string, string>, double>();
        Dictionary<string, int> studentsPostcodeDict = new Dictionary<string, int>();
        IList<Student> students;
        IList<ExamResult> results;
        ICollection<Subject> subjects;
        public void ResultSummary(IList<ExamResult> _results, IList<Student> _students, ICollection<Subject> _subjects)
        {
          
            students = _students;
            results = _results;
            subjects = _subjects;
            if(repeat == false)
            {
                Initialise();
                repeat = true;
            }
            //Zero all data holders so that the new adjusted results can be added
            else
            {
                double overallScore = 0;
                int failedCount = 0;
                pcScoreDict.Clear();
                studentScoreDict.Clear();
                subjectPcScoreDict.Clear();
                studentsPostcodeDict.Clear();
                Initialise();
            }  
            AddGrades();
            AverageOut();
            

        }
        private void Initialise()
        {
            //Initialise dictionary with postcode and score            
            for (int i = 0; i < _validPostCodes.Count; i++)
            {
                pcScoreDict.Add(_validPostCodes[i], 0);
            }
            //Initialise dictionary with student id and score          
            for (int i = 0; i < students.Count; i++)
            {
                studentScoreDict.Add(students[i].Id, 0);
            }

            //Initialise dictionary with <subject name, postcode and score of 0>
            //Also record how many students in each postcode <Postcode, Count>
            for (int j = 0; j < _validPostCodes.Count; j++)
            {
                string pc = _validPostCodes[j];
                studentsPostcodeDict.Add(pc, 0);
                for (int i = 0; i < SubjectTitle.GetNames(typeof(SubjectTitle)).Length; i++)
                {
                    string title = SubjectTitle.GetNames(typeof(SubjectTitle))[i];
                    var d = KeyValuePair.Create(title, pc);
                    subjectPcScoreDict.Add(d, 0);                   
                }
            }
        }
        private void AddGrades()
        {
            //For each result add the score to the relevant data locations
            for (int j = 0; j < results.Count; j++)
            {
                string title = SubjectTitle.GetNames(typeof(SubjectTitle))[results[j].SubjectId];
                Student student = results[j].Student;
                double score = results[j].Grade;
                string pc = student.Postcode;

                //Add to each of the relevant data holders
               var d = KeyValuePair.Create(title, pc);
                subjectPcScoreDict[d] += score;
                studentScoreDict[student.Id] += score;
                overallScore += score;
                if (score < 40)
                {
                    failedCount++;
                }
                //Count students in each postcode
                studentsPostcodeDict[pc]++;

            }
        }

        private void AverageOut()
        {
            //Divide the added scores by the number of students in the postcode to get average score
            foreach(KeyValuePair<KeyValuePair<string, string>, double> kp in subjectPcScoreDict)
            {
                KeyValuePair<string, string> d = kp.Key;
                string pc = d.Value;
                int pcPopulation = studentsPostcodeDict[pc] / 4;

                var n = kp.Value / pcPopulation;
                subjectPcScoreDict[kp.Key] = n;
            }

            //Divide overal score by total students to get average
            overallScore = overallScore / students.Count / subjects.Count;

            foreach (KeyValuePair<int, double> kp in studentScoreDict)
            {
                var n = kp.Value / subjects.Count;
                studentScoreDict[kp.Key] = n;
            }
        }

        public void OutputResults()
        {
            Console.WriteLine("*********RESULTS*************\n");
            Console.WriteLine("Overall average result of all students: " + Math.Round(overallScore, 2));
            Console.WriteLine("\nOverall result per student:");
            foreach(KeyValuePair<int,double> kp in studentScoreDict)
            {
                Student student = students.Where(x => x.Id == kp.Key).FirstOrDefault();
                string s = (" {0, -8} {1, -10}  - {2:n1}");
                string msg = string.Format(s, student.FirstName, student.LastName, kp.Value);
                Console.WriteLine(msg);
            }
            Console.WriteLine("\nOverall result per subject per postcode:");
            Console.WriteLine(" Postcode:\tSubject:\tScore:");
            foreach (KeyValuePair<KeyValuePair<string, string>, double> kp in subjectPcScoreDict)
            {
                
                if (Double.IsNaN(kp.Value))
                {
                    Console.WriteLine(" No students in postcode: " + kp.Key.Value);
                }
                else
                {
                    string s = (" {0, -13} {1, -15} {2:n1}");
                    string msg = string.Format(s, kp.Key.Value, kp.Key.Key, kp.Value);
                    Console.WriteLine(msg);
                }
            }
            Console.WriteLine("\nNumber of exams failed: " + failedCount);
            Console.WriteLine("\nNumber of students failing on average mark: " + studentScoreDict.Values.Where(x => x < 40).Count());
        }
    }
}