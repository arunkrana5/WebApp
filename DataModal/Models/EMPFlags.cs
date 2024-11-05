using System.ComponentModel.DataAnnotations;

namespace DataModal.Models
{
    public class EMPFlags
    {
        public bool Stop_OutPunch { get; set; }
        public bool Stop_InPunch { get; set; }
    }

    public class FlagsMismatchReason
    {
        public class Add
        {
            public long EMPID { get; set; }
            public string Doctype { get; set; }
            public string Date { get; set; }
            [Required(ErrorMessage = "Reason Can't be Blank")]
            public string Reason { get; set; }
            public bool IsActive { get; set; }
            public int? Priority { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }
        }

    }
}
