using System.Collections.Generic;
using Newtonsoft.Json;
using Spectre.Console;

namespace Razenager_modded.RazeData
{
    public static class PersonalInfo
    {
        public class Data
        {
            public bool isApproval { get; set; }
            public bool isRequired { get; set; }
            public object isEditProfessor { get; set; }
            public List<ProfileInfo> profileInfo { get; set; }
            public User user { get; set; }
            public string info { get; set; }
        }

        public class EditRules
        {
            public object minLength { get; set; }
            public object maxLength { get; set; }
            public object pattern { get; set; }
            public bool optional { get; set; }
            public List<Value> values { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public int jpegQuality { get; set; }
        }

        public class Image
        {
            public bool needsApproval { get; set; }
            public object hasPendingApproval { get; set; }
            public bool isEditable { get; set; }
            public string url { get; set; }
            public EditRules editRules { get; set; }
        }

        public class List
        {
            public string id { get; set; }
            public string name { get; set; }
            public string type { get; set; }
            public string value { get; set; }
            public bool isEditable { get; set; }
            public int maxLines { get; set; }
            public EditRules editRules { get; set; }
            public List<List> list { get; set; }
            public string description { get; set; }
        }

        public class ProfileInfo
        {
            public string id { get; set; }
            public string name { get; set; }
            public bool isEditable { get; set; }
            public int maxLines { get; set; }
            public bool isMultipleSection { get; set; }
            public List<List> list { get; set; }
        }

        public class Root
        {
            public Data data { get; set; }
        }

        public class User
        {
            public string name { get; set; }
            public Image image { get; set; }
        }

        public class Value
        {
            public string description { get; set; }
            public string id { get; set; }
        }
    }

    public static class PInfoBuild 
    {
        public static void Organize(string result)
        {
            var jsonInfo = JsonConvert.DeserializeObject<PersonalInfo.Root>(result);

            AnsiConsole.Write(new Rule("[green]Personal Information[/]").RuleStyle("red dim"));

            AnsiConsole.Write(new Table().LeftAligned()
                             .AddColumn("User")
                             .AddRow($"Name: {jsonInfo.data.user.name}\n")
                             .AddRow($"Photo URL: {jsonInfo.data.user.image.url}"));

            if (jsonInfo.data.profileInfo != null)
            {
                foreach (var item in jsonInfo.data.profileInfo)
                {
                    Table tableProfileInfo = new Table().LeftAligned().AddColumns($"{item.id}");

                    Table tableContent = new Table();

                    List<string> textValues = new List<string>();
                    List<Table> panelValues = new List<Table>();

                    foreach (var item2 in item.list)
                    {
                        tableContent.AddColumn($"{item2.id}");
                        if ($"{item2.id}" != $"{item2.value}")
                        {
                            textValues.Add($"{item2.value}");
                        }

                        if (item2.list != null)
                        {
                            Table tableEx = new Table();

                            List<string> exContent = new List<string>();

                            foreach (var item3 in item2.list)
                            {
                                tableEx.AddColumn($"{item3.id}");
                                exContent.Add($"{item3.value}");
                            }

                            tableEx.AddRow(exContent.ToArray());
                            panelValues.Add(tableEx);
                        }
                    }
                    tableContent.AddRow(textValues.ToArray());
                    tableContent.AddRow(panelValues.ToArray());
                    tableProfileInfo.AddRow(tableContent);

                    AnsiConsole.Write(tableProfileInfo);
                }
            }

            AnsiConsole.Write(new Rule().RuleStyle("red dim"));
        }
    }
}
