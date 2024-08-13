using System.Collections.Generic;
using Newtonsoft.Json;
using Spectre.Console;

namespace Razenager_modded.RazeData
{
    public static class CurrentCourses
    {
        public class Data
        {
            public List<string> headerKeys { get; set; }
            public List<List> list { get; set; }
        }

        public class List
        {
            public SubjectContext subjectContext { get; set; }
            public List<MoreInfoWeb> moreInfoWeb { get; set; }
        }

        public class MoreInfoWeb
        {
            public string style { get; set; }
            public string value { get; set; }
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

        public class SubjectContext
        {
            public string name { get; set; }
            public string displayCode { get; set; }
            public string mooSubjectToken { get; set; }
            public string modality { get; set; }
            public string style { get; set; }
            public string courseState { get; set; }
            public string startDate { get; set; }
            public ProgramContext programContext { get; set; }
        }
    }

    public static class CCoursesBuild 
    {
        public static void Organize(string result)
        {
            var jsonInfo = JsonConvert.DeserializeObject<CurrentCourses.Root>(result);

            AnsiConsole.Write(new Rule("[aqua]Current Courses[/]").RuleStyle("red dim"));

            if (jsonInfo.data.list != null)
            {
                Table tableCourses = new Table().LeftAligned().AddColumn("subjects");

                List<string> textValues = new List<string>();

                foreach (var item in jsonInfo.data.list)
                {
                    tableCourses.AddRow(new Table()
                                      .AddColumn($"{item.subjectContext.name}")
                                      .AddRow(new Table()
                                              .AddColumns("displayCode", "modality", "style", "courseState", "startDate")
                                              .AddRow($"{item.subjectContext.displayCode}",
                                                      $"{item.subjectContext.modality}",
                                                      $"{item.subjectContext.style}",
                                                      $"{item.subjectContext.courseState}",
                                                      $"{item.subjectContext.startDate}"))
                                      .AddRow(new Table()
                                              .AddColumn("mooSubjectToken")
                                              .AddRow($"{item.subjectContext.mooSubjectToken}")));
                }

                AnsiConsole.Write(tableCourses);
            }

            AnsiConsole.Write(new Rule().RuleStyle("red dim"));
        }
    }
}