namespace Programming_7312_Part_1.Views.Home
{
    
    
        public class Issue
        {
            public string Location { get; set; }

            public string Category { get; set; }

            public string Description { get; set; }

            public string AttachedFilePath { get; set; }  // Path or filename after upload

            public DateTime ReportedDate { get; set; }

            public Issue()
            {
                ReportedDate = DateTime.Now;
            }
        }
    }

