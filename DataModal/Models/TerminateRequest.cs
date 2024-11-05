using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModal.Models
{
    public class TerminateRequest
    {
        public class List
        {
            public int TotalCount { get; set; }
            public string DocNo { get; set; }
            public string DocDate { get; set; }
            public string ApprovedStatus { get; set; }
            public int TerminateID { get; set; }
            public int EMPID { get; set; }
            public string Reason { get; set; }
            public string EMPName { get; set; }
            public string EMPCode { get; set; }
            public string Category { get; set; }
            public string IsNPCompleted {  get; set; }
            public string SalaryDeductionConfirmation { get; set; }
            public string LastWorkingDay { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedDate { get; set; }
            public string ApprovedRemarks { get; set; }
            public string ApprovedBy { get; set; }
            public string ApprovedDate { get; set; }
            public string ModifiedDate { get; set; }
            public string ModifiedBy { get; set; }
            public string IPAddress { get; set; }
            public string EntrySource { get; set; }

        }
        public class Add
        {
            public long? TerminateID { get; set; }
            [Required(ErrorMessage = "Employee Can't be Blank")]
            public long EMPID { get; set; }
            [Required(ErrorMessage = "Reason Can't be Blank")]
            public string Reason { get; set; }
            [Required(ErrorMessage = "Category Can't be Blank")]
            public string Category { get; set; }
            [Required(ErrorMessage = "Last Working Day Can't be Blank")]
            public string LastWorkingDay { get; set; }
            public bool SalaryDeductionConfirmation { get; set; }
            public string IsNPCompleted { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }
            public List<DropDownlist> EmployeeList { get; set; }
        }
        public class ApprovalAction
        {
            public string TerminateIDs { get; set; }
            public int Approved { get; set; }
            public string ApprovedRemarks { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }
        }
    }
}
