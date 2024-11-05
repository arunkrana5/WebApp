using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataModal.Models
{
    public class LeaveLog
    {
        public class List
        {
            public int RowNum { get; set; }
            public long LogID { get; set; }
            public long FinYearID { get; set; }
            public string FinYear { get; set; }
            public string TotalHours { get; set; }
            public string DocNo { get; set; }
            public string DocDate { get; set; }
            public long EMPID { get; set; }
            public string EMPName { get; set; }
            public string EMPCode { get; set; }
            public long LeaveTypeID { get; set; }
            public string LeaveType { get; set; }
            public string StartDate { get; set; }
            public string EndDate { get; set; }
            public string Reason { get; set; }
            public string ApprovedRemarks { get; set; }
            public int Approved { get; set; }
            public string Status { get; set; }
            public float TotalDays { get; set; }
        }
        public class ApproverList
        {
            public int RowNum { get; set; }
            public string DocNo { get; set; }
            public string DocDate { get; set; }
            public string FinYear { get; set; }
            public long EMPID { get; set; }
            public string EMPName { get; set; }
            public string EMPCode { get; set; }
            public long LogID { get; set; }
            public long LeaveTypeID { get; set; }
            public string LeaveType { get; set; }
            public string StartDate { get; set; }
            public string EndDate { get; set; }
            public string Status { get; set; }
            public string Reason { get; set; }
            public string ApprovedRemarks { get; set; }
            public int Approved { get; set; }
            public string TotalHours { get; set; }
            public string TotalDays { get; set; }
        }


        public class View
        {
            public List LeaveLog { get; set; }
            public List<LeaveTran_View> TranDetails { get; set; }
            public List<LeaveBal> LeaveBalList { get; set; }
        }
    }

    public class AddLeave
    {
        public long? LogID { get; set; }
        public long? EMPID { get; set; }

        public long? LeaveTypeID { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }
        [Required(ErrorMessage = "Reason Can't be Blank")]
        public string Reason { get; set; }

        public long LoginID { get; set; }
        public string IPAddress { get; set; }
        public List<DropDownlist> LeaveTypeList { get; set; }
        public List<AddLeaveTran> LeaveTranList { get; set; }
        public List<LeaveOption> LeaveOption { get; set; }
        public List<LeaveBal> LeaveBalList { get; set; }

    }
    public class AddLeaveTran
    {
        [Required(ErrorMessage = "Leave Date Can't be Blank")]
        public string Date { get; set; }
        [Required(ErrorMessage = "Leave Type Can't be Blank")]
        public long LeaveTypeID { get; set; }
        [Required(ErrorMessage = "Hours Can't be Blank")]
        public float Hours { get; set; }
    }

    public class LeaveTran_View
    {

        public string LeaveDate { get; set; }

        public string LeaveType { get; set; }

        public float Days { get; set; }

    }

    public class LeaveBal
    {
        public string MonthName { get; set; }
        public string Opening { get; set; }
        public string Availed { get; set; }
        public string Earned { get; set; }
        public string TotalEarned { get; set; }
    }
    public class LeaveApproval
    {
        public long LogID { get; set; }
        public int Approved { get; set; }
        public string ApprovedRemarks { get; set; }
        public long LoginID { get; set; }
        public string IPAddress { get; set; }
    }

    public class LeaveOption
    {
        public string Name { get; set; }
        public float Hours { get; set; }
    }

    public class LeaveBalance
    {
        public class List
        {
            public long LeaveBalance_ID { get; set; }
            public string EMPCode { get; set; }
            public string EMPName { get; set; }
            public string FinancialYear { get; set; }
            public string TotalAvailed { get; set; }
            public string TotalEarned { get; set; }
            public string Balance { get; set; }
        }
    }
}
