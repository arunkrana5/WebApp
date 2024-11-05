using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataModal.Models
{
    public class MarkAttendence
    {
        public long? LogID { get; set; }
        public long EMPID { get; set; }



        [Required(ErrorMessage = "Can't be Blank")]
        public long? StatusID { get; set; }

        public string Location { get; set; }
        [Required(ErrorMessage = "Location not Found please check GPS Permission")]
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Error { get; set; }
        public string Notes { get; set; }
        public long? Priority { get; set; }
        public long LoginID { get; set; }
        public string IPAddress { get; set; }
        public long AttachmentID { get; set; }
        public string ImageBase64String { get; set; }

        public string Flag_Doctype { get; set; }
        public string Flag_Reason { get; set; }
        //public bool Stop_OutPunch { get; set; }
        //public bool Stop_InPunch { get; set; }
        //public bool CounterDisplay { get; set; }
        public List<DropDownlist> AttendenceStatusList { get; set; }


    }
}
