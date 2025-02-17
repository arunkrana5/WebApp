using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataModal.Models
{
    public class ManageActivities
    {
        public class Sale
        {
            [Required(ErrorMessage = "Sale No Can't be Blank")]
            [StringLength(3999)]
            public string SaleNo { get; set; }
            public int? Approved { get; set; }
            [Required(ErrorMessage = "Remarks Can't be Blank")]
            public string Remarks { get; set; }

            [Required(ErrorMessage = "Doctype Can't be Blank")]
            public string Doctype { get; set; }
            public string Date { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }
        }

        public class Attendance
        {
            [Required(ErrorMessage = "EMP Code Can't be Blank")]
            public string EMPCode { get; set; }

            [Required(ErrorMessage = "Date Can't be Blank")]
            public DateTime Date { get; set; }
            [Required(ErrorMessage = "Status Can't be Blank")]
            public long? StatusID { get; set; }
            public string Notes { get; set; }
            public List<DropDownlist> AttendenceStatusList { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }
        }

        public class EMPDOL
        {
            [Required(ErrorMessage = "EMP Code Can't be Blank")]
            public string EMPCode { get; set; }
            public string DOL { get; set; }

            [Required(ErrorMessage = "Reason Can't be Blank")]
            public string DOLReason { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }
        }
        public class ImportLeaveBalance
        { 
            public string EMPCode { get; set; }
            public int LeaveBalance { get; set; } 
            public int Month { get; set; }
            public int Year { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }
            public string CreatedDate { get; set; }
            public string CreatedBy { get; set; }
        }
        public class PendingLeave
        {
            [Required(ErrorMessage = "Doc No Can't be Blank")]
            public string DocNo { get; set; }

            [Required(ErrorMessage = "Reason Can't be Blank")]
            public string Reason { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }
        }
        public class AssignTo
        {
            [Required(ErrorMessage = "Request No Can't be Blank")]
            public string RequestNos { get; set; }
            [Required(ErrorMessage = "Assign To Can't be Blank")]
            public int AssignToID { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }
        }
    }
}
