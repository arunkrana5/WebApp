using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModal.Models
{
    public class PushMailItems
    {
        public string Category { get; set; }
        public DateTime ToMail { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string className { get; set; }
        public string description { get; set; }
    }
}
