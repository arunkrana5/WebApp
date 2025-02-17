using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DataModal.Models
{
    public class Requirement
    {
        public class RequestList
        {
            public long ReqID { get; set; }
            public int ApplicationOffered { get; set; }
            public string RequestNo { get; set; }
            public string RequestDate { get; set; }
            public string HiredBy { get; set; }
            public string HiredFor { get; set; }
            public string RequirementType { get; set; }
            public string Region { get; set; }
            public string State { get; set; }
            public string City { get; set; }
            public string Branch { get; set; }
            public string Status { get; set; }
            public string Dealer { get; set; }
            public string DealerName { get; set; }
            public string DealerCode { get; set; }
            public string DealerAddress { get; set; }
            public string Category { get; set; }
            public string Channel { get; set; }
            public int Potential { get; set; }
            public string CandidateName { get; set; }
            public string CandidatePhone { get; set; }
            public string CandidateEmail { get; set; }
            public int Approved { get; set; }
            public string AssignTo { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedDate { get; set; }
            public int TotalCount { get; set; }
            public int RowNum { get; set; }
        }
        public class AddRequest
        {
            public string RequestNo { get; set; }
            public string RequestDate { get; set; }
            public long? ReqID { get; set; }
            [Required(ErrorMessage = "Hired For Can't be Blank")]
            public string HiredFor { get; set; } = "";
            [Required(ErrorMessage = "Hired By Can't be Blank")]
            public string HiredBy { get; set; }
            [Required(ErrorMessage = "Requirement Type Can't be Blank")]
            public string RequirementType { get; set; }
            [Required(ErrorMessage = "Branch Code Can't be Blank")]
            public string BranchCode { get; set; }
            [Required(ErrorMessage = "Dealer ID Can't be Blank")]
            public string DealerCode { get; set; }
            public string DealerName { get; set; } 
            public string DealerAddress { get; set; }
            public string DealerType { get; set; }
            public string DealerCategory { get; set; }
            public int? AssignToID { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }
            public List<DropDownlist> DealerList { get; set; }
            public List<DropDownlist> ProductTypeList { get; set; }
            public List<REC_Target> REC_TargetList { get; set; }
            public List<DropDownlist> HiredForList { get; set; }
            public List<DropDownlist> AssignToList { get; set; }
        }

        public class REC_Target
        {
            public long TargetForID { get; set; }
            public string ProductType { get; set; }
            public int? Qty { get; set; }
        }

        public class Application
        {
            public class List
            {
                public long ApplicationID { get; set; }
                public int ApplicationOffered { get; set; }
                public long ReqID { get; set; }
                public long LoginID { get; set; }
                public string Name { get; set; }
                [DataType(DataType.PhoneNumber)]
                [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
                public string Phone { get; set; }
                public string EmailID { get; set; }
                public string Qualification { get; set; }
                public string Experience { get; set; }
                public decimal Salary { get; set; }
                public int Approved { get; set; }
                public string ApprovedRemarks { get; set; }
                public string ApprovedDate { get; set; }
                public string ApprovedBy { get; set; }
                public string ApprovedStatus { get; set; }
                public string FilePath { get; set; }
                public string FileName { get; set; }
                public string IPAddress { get; set; }
                public string IsChecked { get; set; }
                public string CreatedDate { get; set; }
                public decimal ExpectedSalary { get; set; }
                public string CurrentCompany { get; set; }
                public decimal NetPay { get; set; }
                public string DOJ { get; set; }
            }
            public class Add
            {
                public long ApplicationID { get; set; }
                public long? ReqID { get; set; }

                [Required(ErrorMessage = "Name Can't be Blank")]
                public string Name { get; set; }
                [Required(ErrorMessage = "Phone Number Can't be Blank")]
                [DataType(DataType.PhoneNumber)]
                [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
                public string Phone { get; set; }
                //[Required(ErrorMessage = "Email Can't be Blank")]
                [EmailAddress]
                public string EmailID { get; set; }
                //[Required(ErrorMessage = "Qualification Can't be Blank")]
                public string Qualification { get; set; }
                [Required(ErrorMessage = "Experience Can't be Blank")]
                public string Experience { get; set; }

                [Required(ErrorMessage = "Salary Can't be Blank")]
                public decimal? Salary { get; set; }

                [Required(ErrorMessage = "Previous Company Can't be Blank")]
                public string PreviousCompany { get; set; }

                //[Required(ErrorMessage = "Upload Can't be Blank")]
                public HttpPostedFileBase Upload { get; set; }

                public string FileName { get; set; }
                public long LoginID { get; set; }
                public string IPAddress { get; set; }
                [Required(ErrorMessage = "Expected Salary Can't be Blank")]
                public decimal? ExpectedSalary { get; set; }
                public string FilePath { get; set; }
            }
        }


        public class FullView
        {
            public long ReqID { get; set; }
            public int ApplicationOffered { get; set; }
            public string RequestNo { get; set; }
            public string RequestDate { get; set; }
            public string HiredBy { get; set; }
            public string RequirementType { get; set; }
            public string Region { get; set; }
            public string State { get; set; }
            public string City { get; set; }
            public string Branch { get; set; }
            public string Status { get; set; }
            public string Dealer { get; set; }
            public string DealerName { get; set; }
            public string DealerCode { get; set; }
            public string DealerAddress { get; set; }
            public string DealerCategory { get; set; }
            public string DealerType { get; set; }
            public int Potential { get; set; }
            public string CandidateName { get; set; }
            public string CandidatePhone { get; set; }
            public string CandidateEmail { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedDate { get; set; }
            public string ModifiedDate { get; set; }
            public string ModifiedBy { get; set; }
            public string IPAddress { get; set; }
            public long LoginID { get; set; }
            public int Approved { get; set; }
            public string ApprovedRemarks { get; set; }
            public string ApprovedDate { get; set; }
            public string ApprovedBy { get; set; }
            public string ApprovedStatus { get; set; }
            public List<REC_Target> TargetList { get; set; }
            public List<Application.List> ApplicationList { get; set; }
            public int SelectedCount { get; set; }
            public int NotInterviewedCount { get; set; }
            public int RejectedCount { get; set; }
        }

        public class BranchWiseEMP_Dashboard
        {
            public long BranchID { get; set; }
            public string BranchCode { get; set; }
            public string BranchName { get; set; }
            public long MaxEMPCount { get; set; }
            public long ActiveEMP { get; set; }
            public long PendingCount { get; set; }
        }
    }
}
