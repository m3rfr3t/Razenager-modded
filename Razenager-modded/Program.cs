using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Razenager_modded.Web;
using Razenager_modded.RazeData;
using Spectre.Console;
using Spectre.Console.Json;

namespace Razenager_modded
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "Razenager-modded | BY M3RFR3T | github.com/RazeLeakers";
            Console.SetWindowSize(120,30);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();

            string opt = string.Empty;

            do
            {
                AnsiConsole.Write(new FigletText("Razenager-mod").Centered().Color(Color.White));
                AnsiConsole.WriteLine();
                
                opt = AnsiConsole.Prompt(new SelectionPrompt<string>()
                                         .HighlightStyle(new Style(Color.White))
                                         .AddChoices(new[] {
                                         "[green3_1](1) Personal Info[/]",
                                         "[darkorange3_1](2) Historical Grades[/]",
                                         "[aqua](3) Current Courses[/]",
                                         "[red3_1](4) Exit[/]"}));

                opt = opt.Substring(opt.IndexOf("("), opt.IndexOf(")") - opt.IndexOf("(") + 1);

                if (opt != "(4)")
                {
                    string type = AnsiConsole.Prompt(new SelectionPrompt<string>()
                                                     .HighlightStyle(new Style(Color.White))
                                                     .AddChoices(new[] {
                                                     "[green3_1](1) Format[/]",
                                                     "[darkorange3_1](2) JSON[/]",
                                                     "[red3_1](3) Cancel[/]"}));

                    if (type.Contains("(1)")) type = "FORMAT";
                    else if (type.Contains("(2)")) type = "JSON";
                    else
                    {
                        Console.Clear();
                        continue;
                    };

                    Console.Clear();

                    switch (opt)
                    {
                        case "(1)":
                            CallNager(API.PersonalInfo, type);
                            break;
                        case "(2)":
                            CallNager(API.HistoricalGrades, type);
                            break;
                        case "(3)":
                            CallNager(API.CurrentCourses, type);
                            break;
                    }
                }

                Console.Clear();
            } while (opt != "(4)");
        }

        public static void CallNager(string url,string type)
        {
            string result = string.Empty;

            Task.Run(async () => {
                await AnsiConsole.Status()
                .StartAsync("Loading...", async e => {
                    using (PostNager postNager = new PostNager())
                    {
                        result = await postNager.AuthToken(API.Token, new Uri(url));
                    }
                });
            }).Wait();

            if (string.IsNullOrEmpty(result))
            {
                AnsiConsole.WriteLine();
                AnsiConsole.Write(new Markup("Error auth token...", new Style(Color.Red)));
                Console.ReadKey();
                return;
            }

            if (type == "FORMAT")
            {
                switch (url)
                {
                    case API.PersonalInfo:
                        PInfoBuild.Organize(result);
                        break;
                    case API.HistoricalGrades:
                        HGradesBuild.Organize(result);
                        break;
                    case API.CurrentCourses:
                        CCoursesBuild.Organize(result);
                        break;
                }
            }
            else AnsiConsole.Write(new Panel(new JsonText(result)).Collapse().BorderColor(Color.Green));

            SaveContent(result);
        }

        private static void SaveContent(string content)
        {
            AnsiConsole.WriteLine();
            string opt = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Download data?(ONLY JSON)")
                .HighlightStyle(new Style(Color.White))
                .AddChoices(new[]
                {
                    "[green3_1]Yes[/]",
                    "[red3_1]No[/]"
                }));

            if (opt.Contains("Yes"))
            {
                Thread td = new Thread(() =>
                {
                    using (System.Windows.Forms.FolderBrowserDialog browserDG = new System.Windows.Forms.FolderBrowserDialog())
                    {
                        if (browserDG.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            string fileName = AnsiConsole.Prompt(
                                    new TextPrompt<string>("File Name:")
                                    .Validate(input =>
                                    {
                                        if (string.IsNullOrEmpty(input) || File.Exists(Path.Combine(browserDG.SelectedPath, $"{input}.txt")))
                                        {
                                            return ValidationResult.Error();
                                        }

                                        return ValidationResult.Success();
                                    }));

                            string pathSave = Path.Combine(browserDG.SelectedPath, $"{fileName}.txt");

                            try
                            {
                                File.WriteAllText(pathSave,content);

                                AnsiConsole.Write(new Markup($"Saved in: {pathSave}",new Style(Color.Green)));
                            }
                            catch (Exception ex)
                            {
                                AnsiConsole.WriteLine();
                                AnsiConsole.WriteLine();
                                AnsiConsole.WriteException(ex);
                            }
                            finally
                            {
                                AnsiConsole.WriteLine();
                                AnsiConsole.WriteLine();
                                AnsiConsole.Write(new Markup("Press any key to continue...", new Style(Color.Grey)));
                                Console.ReadKey();
                            }
                        }
                    }
                });

                td.SetApartmentState(ApartmentState.STA);
                td.Start();
                td.Join();
            }
        }
    }
}
