using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Razenager_modded.Web;
using Razenager_modded.RazeData;
using Newtonsoft.Json;

namespace Razenager_modded
{
    internal class Program
    {
        const string token = "";
        const string API_UserInfo = "https://mimundo.upn.edu.pe/api/zubron/execute/%2Fprofile%2Fget";
        const string API_UserHistoricalGrades = "https://mimundo.upn.edu.pe/api/zubron/execute/%2Ftranscript%2Flist";

        static void Main(string[] args)
        {
            Console.Title = "Razenager-modded | BY M3RFR3T | github.com/RazeLeakers";
            string opc = string.Empty;

            do
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(@"
     ____                                                                    _     _          _ 
    |  _ \ __ _ _______ _ __   __ _  __ _  ___ _ __      _ __ ___   ___   __| | __| | ___  __| |
    | |_) / _` |_  / _ \ '_ \ / _` |/ _` |/ _ \ '__|____| '_ ` _ \ / _ \ / _` |/ _` |/ _ \/ _` |
    |  _ < (_| |/ /  __/ | | | (_| | (_| |  __/ | |_____| | | | | | (_) | (_| | (_| |  __/ (_| |
    |_| \_\__,_/___\___|_| |_|\__,_|\__, |\___|_|       |_| |_| |_|\___/ \__,_|\__,_|\___|\__,_|
                                    |___/                                                       
");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n\t[1] Show token");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n\t[2] Show Info");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\n\t[3] Show Grades");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n\t[4] Credits and info");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n\t[5] Exit");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("\n\t--> ");
                opc = Console.ReadLine();
                Console.Clear();
                switch (opc)
                {
                    case "1":
                        if (string.IsNullOrEmpty(token))
                        {
                            Console.WriteLine("Token empty!");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("Token: ");
                            Console.WriteLine();
                            Console.WriteLine(token);
                            Console.ReadKey();
                        }
                        break;
                    case "2":
                        ShowInfo(token);
                        break;
                    case "3":
                        ShowGrades(token);
                        break;
                    case "4":
                        Console.WriteLine("\n\n\t\t     Razenager was originally created by Happy Life");
                        Console.WriteLine("\n\n\t\t            (github.com/hluciana/razenager)");
                        Console.WriteLine("");
                        Console.WriteLine("\n\n\t\tThis version is modified to visually show data more easily");
                        Console.WriteLine("\n\n\t\t           New updates and proyects are coming!");
                        Console.WriteLine("");
                        Console.WriteLine("\n\n\t\t                     ATTE:");
                        Console.WriteLine("\n\n\t\t                    M3RFR3T");
                        Console.ReadKey();
                        break;
                }
                Console.Clear();
            } while (opc != "5");
        }

        static void ShowInfo(string tk)
        {
            if (string.IsNullOrEmpty(tk))
            {
                Console.WriteLine("Token empty!");
                Console.ReadKey();
                return;
            }

            string result = string.Empty;
            Task.Run(async () =>
            {
                using (PostNager postNager = new PostNager())
                {
                    result = await postNager.AuthToken(tk,new Uri(API_UserInfo),HttpMethod.Post);
                }
            }).Wait();

            if (string.IsNullOrEmpty(result))
            {
                Console.WriteLine("Error auth token...");
                Console.ReadKey();
                return;
            }

            UserInfo.Root json = JsonConvert.DeserializeObject<UserInfo.Root>(result);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Profile picture: ");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(json.data.user.image.url);
            Console.WriteLine("---------------------");

            for (int i = 0; i < json.data.profileInfo.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(json.data.profileInfo[i].name + ": ");
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Red;

                List<UserInfo.List> info = null;

                if (json.data.profileInfo[i].list[0].list != null)
                {
                    info = json.data.profileInfo[i].list[0].list;
                }
                else
                {
                    info = json.data.profileInfo[i].list;
                }

                for (int j = 0; j < info.Count; j++)
                {
                    Console.Write(info[j].name + ": ");
                    if (string.IsNullOrEmpty(info[j].value)) Console.WriteLine("---");
                    else Console.WriteLine(info[j].value);
                }
                
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("---------------------");
            }

            Console.ReadKey();
        }

        static void ShowGrades(string tk)
        {
            if (string.IsNullOrEmpty(tk))
            {
                Console.WriteLine("Token empty!");
                Console.ReadKey();
                return;
            }

            string result = string.Empty;
            Task.Run(async () =>
            {
                using (PostNager postNager = new PostNager())
                {
                    result = await postNager.AuthToken(tk, new Uri(API_UserHistoricalGrades), HttpMethod.Post);
                }
            }).Wait();

            if (string.IsNullOrEmpty(result))
            {
                Console.WriteLine("Error auth token...");
                Console.ReadKey();
                return;
            }

            UserGrades.Root json = JsonConvert.DeserializeObject<UserGrades.Root>(result);

            var jsonPrograms = json.data.programs[0];

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("programs: ");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("level: " + jsonPrograms.program.level);
            Console.WriteLine("status: " + jsonPrograms.program.status);
            Console.WriteLine("programType: " + jsonPrograms.program.programType);
            Console.WriteLine(jsonPrograms.program.programContext.name + "(mooProgramToken): " + jsonPrograms.program.programContext.mooProgramToken);
            Console.WriteLine("programCreationTerm: " + jsonPrograms.program.programCreationTerm);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n----------------------\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("summary: ");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("value: " + jsonPrograms.summary.summaryInfo[0].value);
            Console.WriteLine("key: " + jsonPrograms.summary.summaryInfo[0].key);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n----------------------\n");

            for (int i = 0; i < jsonPrograms.semesters.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(jsonPrograms.semesters[i].semester);
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Red;
                var grades = jsonPrograms.semesters[i].subjects;

                for (int j = 0; j < grades.Count; j++)
                {
                    Console.WriteLine("name:" + grades[j].name);
                    Console.WriteLine("code: " + grades[j].code);
                    Console.WriteLine("grade: " + grades[j].grade.style + " - " + grades[j].grade.value);
                    Console.WriteLine("subjectSubtitle: " + grades[j].subjectSubtitle);
                    Console.WriteLine();
                }

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\n----------------------\n");
            }

            Console.ReadKey();
        }
    }
}
