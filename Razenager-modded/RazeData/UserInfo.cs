using System.Collections.Generic;

namespace Razenager_modded.RazeData
{
    internal static class UserInfo
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
            public List<object> values { get; set; }
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
}
