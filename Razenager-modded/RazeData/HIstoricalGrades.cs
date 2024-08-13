using System.Collections.Generic;
using Newtonsoft.Json;
using Spectre.Console;

namespace Razenager_modded.RazeData
{
    public static class HistoricalGrades
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

    public static class HGradesBuild
    {
        public static void Organize(string result)
        {
            var jsonInfo = JsonConvert.DeserializeObject<HistoricalGrades.Root>(result);

            AnsiConsole.Write(new Rule("[darkorange3_1]Historical Grades[/]").RuleStyle("red dim"));

            AnsiConsole.Write(new Table().LeftAligned()
                              .AddColumn($"program")
                              .AddRow(new Table()
                                      .AddColumns("enrollmentId", "campusId", "campusName", "schoolId", "schoolName", "programId", "programName")
                                      .AddRow($"{jsonInfo.data.programs[0].program.enrollmentId}",
                                              $"{jsonInfo.data.programs[0].program.campusId}",
                                              $"{jsonInfo.data.programs[0].program.campusName}",
                                              $"{jsonInfo.data.programs[0].program.schoolId}",
                                              $"{jsonInfo.data.programs[0].program.schoolName}",
                                              $"{jsonInfo.data.programs[0].program.programId}",
                                              $"{jsonInfo.data.programs[0].program.programName}"
                                              ))
                              .AddRow(new Table()
                                      .AddColumns("level", "status", "programType", "enrollmentTerm", "programCreationTerm", "programCurrentTerm", "semesters")
                                      .AddRow($"{jsonInfo.data.programs[0].program.level}",
                                              $"{jsonInfo.data.programs[0].program.status}",
                                              $"{jsonInfo.data.programs[0].program.programType}",
                                              $"{jsonInfo.data.programs[0].program.enrollmentTerm}",
                                              $"{jsonInfo.data.programs[0].program.programCreationTerm}",
                                              $"{jsonInfo.data.programs[0].program.programCurrentTerm}",
                                              $"{jsonInfo.data.programs[0].program.semesters}"))
                              .AddRow(new Table()
                                      .AddColumns("totalCredits", "earnedCredits", "extracurricularCredits", "gpa")
                                      .AddRow($"{jsonInfo.data.programs[0].program.totalCredits}",
                                              $"{jsonInfo.data.programs[0].program.earnedCredits}",
                                              $"{jsonInfo.data.programs[0].program.extracurricularCredits}",
                                              $"{jsonInfo.data.programs[0].program.gpa}"))
                              .AddRow(new Table()
                                      .AddColumns("name","term", "mooProgramToken")
                                      .AddRow($"{jsonInfo.data.programs[0].program.programContext.name}",
                                              $"{jsonInfo.data.programs[0].program.programContext.term}",
                                              $"{jsonInfo.data.programs[0].program.programContext.mooProgramToken}")));

            if (jsonInfo.data.programs[0].summary.summaryInfo != null)
            {
                Table tableSummary = new Table().LeftAligned().AddColumn("summary");

                Table tableContent = new Table();

                List<string> textValues = new List<string>();

                foreach (var item in jsonInfo.data.programs[0].summary.summaryInfo)
                {
                    tableContent.AddColumn($"{item.key}");
                    textValues.Add($"{item.value}");
                }
                tableContent.AddColumn("info");
                tableContent.AddColumn("gpa");
                textValues.Add($"{jsonInfo.data.programs[0].summary.info}");
                textValues.Add($"{jsonInfo.data.programs[0].summary.gpa}");

                tableContent.AddRow(textValues.ToArray());
                tableSummary.AddRow(tableContent);

                AnsiConsole.Write(tableSummary);
            }

            if (jsonInfo.data.programs[0].semesters != null)
            {
                Table tableSemesters = new Table().LeftAligned().AddColumn("semesters");

                foreach (var item in jsonInfo.data.programs[0].semesters)
                {
                    Table tableInfo = new Table().AddColumn($"{item.semester} - {item.semesterSubtitle}");
                    
                    if (item.subjects != null)
                    {
                        Table tableContent = new Table();

                        List<Table> textCodes = new List<Table>();
                        List<Table> textGrades = new List<Table>();
                        List<Table> textSubjectSubtitles = new List<Table>();

                        foreach (var item2 in item.subjects)
                        {
                            tableContent.AddColumn($"{item2.name}");
                            textCodes.Add(new Table().AddColumn("code").AddRow($"{item2.code}"));
                            textGrades.Add(new Table().AddColumn("grade").AddRow($"{item2.grade.style} - {item2.grade.value}"));
                            textSubjectSubtitles.Add(new Table().AddColumn("subjectSubtitle").AddRow($"{item2.subjectSubtitle}"));
                        }

                        tableContent.AddRow(textCodes.ToArray());
                        tableContent.AddRow(textGrades.ToArray());
                        tableContent.AddRow(textSubjectSubtitles.ToArray());
                        tableInfo.AddRow(tableContent);
                    }

                    tableSemesters.AddRow(tableInfo);
                }

                AnsiConsole.Write(tableSemesters);
            }

            AnsiConsole.Write(new Rule().RuleStyle("red dim"));
        }
    }
}
