using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DataModal.Models
{
    public class EMPTalentPool
    {
        public class List
        {
            public long TPID { get; set; }
            public string DocNo { get; set; }
            public string DocDate { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string DOB { get; set; }
            public string Age { get; set; }         
            public string CounterName { get; set; }
            public string StateName { get; set; }
            public string Location { get; set; }
            public string TotalExperience { get; set; }
            public string CurrentCompany { get; set; }
            public string TenureWithCompany { get; set; }
            public string Mobile { get; set; }
            public string CurrentSalary { get; set; }
            public string ExpectedSalary { get; set; }
            public string Pincode { get; set; }
            public string Latitude { get; set; }
            public string Longitude { get; set; }
            public string EntrySource { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedDate { get; set; }
            public string ModifiedDate { get; set; }
            public string ModifiedBy { get; set; }
            public string IPAddress { get; set; }
            public long TotalCount { get; set; }

            public string ApprovedRemarks { get; set; }
        }

        public class MyAdd
        {
            public long? TPID { get; set; }
            [Required(ErrorMessage = "Name Can't be Blank")]
            public string Name { get; set; }

            [Required(ErrorMessage = "Age Can't be Blank")]
            public int? Age { get; set; }

            [Required(ErrorMessage = "CounterName Can't be Blank")]
            public string CounterName { get; set; }

            [Required(ErrorMessage = "Location Can't be Blank")]
            public string Location { get; set; }

            [Required(ErrorMessage = "TotalExperience Can't be Blank")]
            public string TotalExperience { get; set; }
            public string Address { get; set; }

            [Required(ErrorMessage = "Current Company Can't be Blank")]
            public string CurrentCompany { get; set; }

            [Required(ErrorMessage = "TenureWithCompany Can't be Blank")]
            public string TenureWithCompany { get; set; }

            public string Mobile { get; set; }
            public decimal? CurrentSalary { get; set; }
            public decimal? ExpectedSalary { get; set; }
            public string Pincode { get; set; }
            public int IsActive { get; set; }
            public int Priority { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }

            [Required(ErrorMessage = "Location not Found please check GPS Permission")]
            public string Latitude { get; set; }
            public string Longitude { get; set; }
            public string Error { get; set; }

            public List<Attachments> AttachmentsList { get; set; }
        }
        public class Add
        {
            public long? TPID { get; set; }
            [Required(ErrorMessage = "Name Can't be Blank")]
            public string Name { get; set; }

            [Required(ErrorMessage = "Age Can't be Blank")]
            public int? Age { get; set; }

            [Required(ErrorMessage = "CounterName Can't be Blank")]
            public string CounterName { get; set; }

            [Required(ErrorMessage = "Location Can't be Blank")]
            public string Location { get; set; }

            [Required(ErrorMessage = "TotalExperience Can't be Blank")]
            public string TotalExperience { get; set; }
            public string Address { get; set; }

            [Required(ErrorMessage = "Current Company Can't be Blank")]
            public string CurrentCompany { get; set; }

            [Required(ErrorMessage = "TenureWithCompany Can't be Blank")]
            public string TenureWithCompany { get; set; }

            public string Mobile { get; set; }
            public decimal? CurrentSalary { get; set; }
            public decimal? ExpectedSalary { get; set; }
            public string Pincode { get; set; }
            public int IsActive { get; set; }
            public int Priority { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }

            [Required(ErrorMessage = "Location not Found please check GPS Permission")]
            public string Latitude { get; set; }
            public string Longitude { get; set; }
            public string Error { get; set; }
            // Extra fields
            [Required(ErrorMessage = "Email Can't be Blank")]
            [EmailAddress]
            public string Email { get; set; }
            [Required(ErrorMessage = "DOB Can't be Blank")]
            public string DOB { get; set; }
            [Required(ErrorMessage = "Work Profile Can't be Blank")]
            public string WorkProfile { get; set; }
            public int? DealerID { get; set; }
            public int?  BranchID { get; set; }
            [Required(ErrorMessage = "State Can't be Blank")]
            public int? State { get; set; }
            [Required(ErrorMessage = "City Can't be Blank")]
            public int? City { get; set; }
            public string Trade_Experience { get; set; }
            public string Qualification { get; set; }
            public string CW_Company { get; set; }
            public string CW_Address { get; set; }
            public int? CW_State { get; set; }
            public int? CW_City { get; set; }
            public string CW_Pincode { get; set; }
            public decimal CW_Salary { get; set; }
            public List<Attachments> AttachmentsList { get; set; }
            public List<DropDownlist> BranchList { get; set; }
            public List<DropDownlist> DealerList { get; set; }
            public List<DropDownlist> StateList { get; set; }
            public List<DropDownlist> CityList { get; set; }
        }
        public class Attachments
        {
            public long? Attach_ID { get; set; }
            public string AttachPath { get; set; }
            public string Doctype { get; set; }
            public HttpPostedFileBase Upload { get; set; }
        }

        public class ApprovalAction
        {
            public string TPIDs { get; set; }
            public int Approved { get; set; }
            public string ApprovedRemarks { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }
        }

        public class Filter
        {
            public string States { get; set; }
            public string City { get; set; }
            public string Pincodes { get; set; }
            public string Counters { get; set; }
            public string Location { get; set; }
            public int Approved { get; set; }
            public string Month { get; set; }
            public List<DropDownlist> StateList { get; set; }
            public List<DropDownlist> CityList { get; set; }
            public List<DropDownlist> LocationList { get; set; }
            public List<DropDownlist> CountersList { get; set; }
            public List<DropDownlist> PinCodeList { get; set; }
        }
    }
}
