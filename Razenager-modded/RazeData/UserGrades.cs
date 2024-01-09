using System.Collections.Generic;

namespace Razenager_modded.RazeData
{
    internal static class UserGrades
    {
        public class Data
        {
            public List<Programs> programs { get; set; }
        }

        public class Grade
        {
            public string style { get; set; }
            public string value { get; set; }
        }

        public class Programs
        {
            public Program program { get; set; }
            public Summary summary { get; set; }
            public List<Semester> semesters { get; set; }
        }

        public class Program
        {
            public string enrollmentId { get; set; }
            public string campusId { get; set; }
            public string campusName { get; set; }
            public string schoolId { get; set; }
            public string schoolName { get; set; }
            public string programId { get; set; }
            public string programName { get; set; }
            public string level { get; set; }
            public string status { get; set; }
            public string programType { get; set; }
            public string enrollmentTerm { get; set; }
            public ProgramContext programContext { get; set; }
            public string programCreationTerm { get; set; }
            public string programCurrentTerm { get; set; }
            public string semesters { get; set; }
            public string totalCredits { get; set; }
            public string earnedCredits { get; set; }
            public string extracurricularCredits { get; set; }
            public string gpa { get; set; }
        }

        public class ProgramContext
        {
            public string name { get; set; }
            public string term { get; set; }
            public string mooProgramToken { get; set; }
        }

        public class Root
        {
            public Data data { get; set; }
        }

        public class Semester
        {
            public string semester { get; set; }
            public List<Subject> subjects { get; set; }
            public string semesterSubtitle { get; set; }
        }

        public class Subject
        {
            public string name { get; set; }
            public string code { get; set; }
            public Grade grade { get; set; }
            public List<object> moreInfo { get; set; }
            public string subjectSubtitle { get; set; }
        }

        public class Summary
        {
            public List<SummaryInfo> summaryInfo { get; set; }
            public object info { get; set; }
            public object gpa { get; set; }
        }

        public class SummaryInfo
        {
            public string value { get; set; }
            public string key { get; set; }
        }
    }
}
