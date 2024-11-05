using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using static DataModal.Models.AllEnum;

namespace DataModal.Models
{
    public class Employee
    {
        public class List
        {
            public int TotalCount { get; set; }
            public long EMPID { get; set; }
            public string EMPCode { get; set; }
            public string EMPName { get; set; }
            public string DealerName { get; set; }
            public string DealerCode { get; set; }
            public string Phone { get; set; }
            public string EmailID { get; set; }
            public string FatherName { get; set; }
            public string DOB { get; set; }
            public string DOJ { get; set; }
            public string DOL { get; set; }
            public string DOLStatus { get; set; }
            public string DOLReason { get; set; }
            public string Gender { get; set; }
            public string DeptName { get; set; }
            public string DesignName { get; set; }
            public bool IsActive { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedDate { get; set; }
            public string ModifiedDate { get; set; }
            public string ModifiedBy { get; set; }
            public string IPAddress { get; set; }
        }
        public class Add
        {
            public long? EMPID { get; set; }
            public string EMPCode { get; set; }
            [Required(ErrorMessage = "Name Can't be Blank")]
            public string EMPName { get; set; }

            [Required(ErrorMessage = "UserID Can't be Blank")]
            public string UserID { get; set; }

            [Required(ErrorMessage = "Password Can't be Blank")]
            public string Password { get; set; }

            [Required(ErrorMessage = "Role  Can't be Blank")]
            public long RoleID { get; set; }
            [Required(ErrorMessage = "Phone Can't be Blank")]
            public string Phone { get; set; }
            public string PAN { get; set; }
            [Required(ErrorMessage = "Email Can't be Blank")]
            [EmailAddress]
            public string EmailID { get; set; }

            [Required(ErrorMessage = "Father Can't be Blank")]
            public string FatherName { get; set; }
            [Required(ErrorMessage = "Date of Birth Can't be Blank")]
            public string DOB { get; set; }

            [Required(ErrorMessage = "Date of Birth Can't be Blank")]
            public string DOJ { get; set; }
            [Required(ErrorMessage = "Gender Can't be Blank")]
            public Gender? Gender { get; set; }

            [Required(ErrorMessage = "Desigantion Can't be Blank")]
            public long? DesignID { get; set; }
            [Required(ErrorMessage = "Department Can't be Blank")]
            public long? DepartID { get; set; }

            [Required(ErrorMessage = "Country Can't be Blank")]
            public long? CountryID { get; set; }

            [Required(ErrorMessage = "Region Can't be Blank")]
            public long? RegionID { get; set; }
            [Required(ErrorMessage = "State Can't be Blank")]
            public long? StateID { get; set; }
            [Required(ErrorMessage = "City Can't be Blank")]
            public long? CityID { get; set; }

            [Required(ErrorMessage = "Address Can't be Blank")]
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string Location { get; set; }
            public string Zipcode { get; set; }

            [Required(ErrorMessage = "Account No Can't be Blank")]
            public string AccountNo { get; set; }

            [Required(ErrorMessage = "Bank Name Can't be Blank")]
            public string BankName { get; set; }

            [Required(ErrorMessage = "IFSC Code Can't be Blank")]
            public string IFSCCode { get; set; }

            [Required(ErrorMessage = "Bank Branch Can't be Blank")]
            public string BankBranch { get; set; }

            [Required(ErrorMessage = "Dealer Can't be Blank")]
            public long? DealerID { get; set; }

            public bool IsPJPAutoAssign { get; set; }
            public long AttachID { get; set; }
            public int Priority { get; set; }
            public string UAN { get; set; }
            public string ESIC { get; set; }
            public string PaymentMode { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }
            public string DOL { get; set; }
            public HttpPostedFileBase Upload { get; set; }
            public List<DropDownlist> DepartmentList { get; set; }
            public List<DropDownlist> DesignationList { get; set; }
            public List<DropDownlist> CountryList { get; set; }
            public List<DropDownlist> RegionList { get; set; }
            public List<DropDownlist> StateList { get; set; }
            public List<DropDownlist> CityList { get; set; }
            public List<DropDownlist> DealerList { get; set; }
            public List<DropDownlist> RoleList { get; set; }

        }



    }


    public class BirthdayList
    {
        public string EMPName { get; set; }
        public string EMPCode { get; set; }
        public string ImageURL { get; set; }
        public string EmailID { get; set; }
    }
}
