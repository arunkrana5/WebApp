using System;

namespace DataModal.Models
{
    public class Calender
    {

        public class Events
        {
            public string title { get; set; }
            public DateTime date { get; set; }
            public string start { get; set; }
            public string end { get; set; }
            public string className { get; set; }
            public string description { get; set; }

        }
    }
}
