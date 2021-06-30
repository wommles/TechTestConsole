using System.Collections.Generic;
using TechTestConsole.Data.Entities;

namespace TechTestConsole.Services
{
    public interface IGradeAdjuster
    {
        IList<ExamResult> AdjustGrade(IList<ExamResult> result);
    }
    
    public class GradeAdjuster : IGradeAdjuster
    {
        public IList<ExamResult> AdjustGrade(IList<ExamResult> results)
        {
            List<ExamResult> adjustedResults = new List<ExamResult>();
            foreach (ExamResult result in results)
            {
                Student s = result.Student;
                string pc = s.Postcode;
                int mathsID = 3;
                int techID = 1;

                switch (pc)
                {
                    case "AA1":
                        result.Grade *= 1.1;
                        break;

                    case "BB4":
                        if (result.SubjectId == mathsID)
                        {
                            result.Grade *= 1.02;
                        }
                        else
                        {
                            result.Grade *= 0.97;
                        }
                        break;
                    case "CC1":
                        result.Grade *= 0.85;
                        break;
                    case "DD9":
                        if (result.SubjectId == techID)
                        {
                            result.Grade *= 1.03;
                        }
                        else
                        {
                            result.Grade *= 0.98;
                        }
                        break;
                    default:
                        break;
                }
                if (result.Grade > 100)
                {
                    result.Grade = 100;
                }
                adjustedResults.Add(result);

            }
            return adjustedResults;
        }
    }
}