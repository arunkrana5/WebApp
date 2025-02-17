using MathNet.Numerics.RootFinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DataModal.Models
{
    public class WorkForce
    {
        public class Add
        {
            public int WorkForceID { get; set; }
            public string DocNo { get; set; }
            public string DocDate { get; set; }
            [Required(ErrorMessage ="Branch Can'be blank")]
            public string BranchCode { get; set; }
            [Required(ErrorMessage = "Project Can'be blank")]
            public string ProjectCode { get; set; }
            [Required(ErrorMessage = "Title Can'be blank")]
            public string Title { get; set; }
            [Required(ErrorMessage = "Name Can'be blank")]
            public string Name { get; set; }
            [Required(ErrorMessage = "Phone Can'be blank")]
            public string Phone { get; set; }
            [Required(ErrorMessage = "Gender Can'be blank")]
            public string Gender { get; set; }
            [Required(ErrorMessage = "Email Can'be blank")]
            public string EmailID { get; set; }
            [Required(ErrorMessage = "MaritalStatus Can'be blank")]
            public string MaritalStatus { get; set; }
            [Required(ErrorMessage = "DOJ Can'be blank")]
            public string DOJ { get; set; }
            [Required(ErrorMessage = "Aadhar No Can'be blank")]
            public string AadharNo { get; set; }
            [Required(ErrorMessage = "DOB Can'be blank")]
            public string DOB { get; set; }
            [Required(ErrorMessage = "Last Company Can'be blank")]
            public string LastCompany { get; set; }
            [Required(ErrorMessage = "State Can'be blank")]
            public string StateCode { get; set; }
            [Required(ErrorMessage = "Inhand Salary Can'be blank")]
            public decimal InhandSalary { get; set; }
            [Required(ErrorMessage = "Expected Salary Can'be blank")]
            public decimal ExpectedSalary { get; set; }
            [Required(ErrorMessage = "T Shirt Size Can'be blank")]
            public string TshirtSize { get; set; }
            [Required(ErrorMessage = "Total Experience Can'be blank")]
            public double TotalExperience { get; set; }
            [Required(ErrorMessage = "Qualification Can'be blank")]
            public string Qualification { get; set; }
            [Required(ErrorMessage = "Last Company Designation Can'be blank")]
            public string LastCompany_Designation { get; set; }
            [Required(ErrorMessage = "Last Salary Can'be blank")]
            public decimal PreviousSalary_Inhand { get; set; }
            [Required(ErrorMessage = "Designation Can'be blank")]
            public string Designation { get; set; }
            [Required(ErrorMessage = "ASM Phone Can'be blank")]
            public string ASM_Phone { get; set; }
            [Required(ErrorMessage = "Department Can'be blank")]
            public string Department { get; set; }
            [Required(ErrorMessage = "Recruitment Type Can'be blank")]
            public string RecruitmentType { get; set; }
            [Required(ErrorMessage = "Region Can'be blank")]
            public string Region { get; set; }
            [Required(ErrorMessage = "City Can'be blank")]
            public string City { get; set; }
            [Required(ErrorMessage = "Interview Date Can'be blank")]
            public string InterviewDate { get; set; }
            [Required(ErrorMessage = "Grade Can'be blank")]
            public string Grade { get; set; }
            [Required(ErrorMessage = "ASM Name Can'be blank")]
            public string ASMName { get; set; }
            [Required(ErrorMessage = "MW Category Can'be blank")]
            public string MWCategory { get; set; }
            public string StoreCode { get; set; }
            public string StoreName { get; set; }
            public string StoreContact { get; set; }
            public string StoreAddress { get; set; }
            public string StoreImagePath { get; set; }
            public int TotalCounterSales_Qty { get; set; }
            public int ClientCurrentSales_Qty { get; set; }
            public int ProposedSales_Qty { get; set; }
            public int ClientProductDisplay_Qty { get; set; }
            public int Sales_Last2ndMonth { get; set; }
            public int Sales_Last1stMonth { get; set; }
            public int Sales_CurrentMonth { get; set; }
            public int Sales_CurrentInventory { get; set; }
            public string SOP_BranchName { get; set; }
            public string SOP_OutletName { get; set; }
            public string SOP_OutletLocation { get; set; }
            public string SOP_CurrentMonth { get; set; }
            public string CMIConnectCode { get; set; }
            public string SOP_NextMonth1 { get; set; }
            public string SOP_NextMonth2 { get; set; }
            public string SOP_NextMonth3 { get; set; }
            public string SOP_NextMonth4 { get; set; }
            public string SOP_NextMonth5 { get; set; }
            public string SOP_NextMonth6 { get; set; }
            public int SOR_Target1 { get; set; }
            public int SOR_Target2 { get; set; }
            public int SOR_Target3 { get; set; }
            public int SOR_Target4 { get; set; }
            public int SOR_Target5 { get; set; }
            public int SOR_Target6 { get; set; }
            public int SOR_RACSales1 { get; set; }
            public int SOR_RACSales2 { get; set; }
            public int SOR_RACSales3 { get; set; }
            public int SOR_RACSales4 { get; set; }
            public int SOR_RACSales5 { get; set; }
            public int SOR_RACSales6 { get; set; }
            public string ReferralSource { get; set; }
            public string InterviewBy { get; set; }
            public string Remarks { get; set; }
            public bool IsActive { get; set; }
            public int Priority { get; set; }
            public int Isdeleted { get; set; }
            public int DeletedBy { get; set; }
            public DateTime? DeletedDate { get; set; }
            public int CreatedBy { get; set; }
            public DateTime CreatedDate { get; set; }
            public DateTime ModifiedDate { get; set; }
            public int ModifiedBy { get; set; }
            public string IPAddress { get; set; }
            public List<DropDownlist> BranchList { get; set; }
            public List<DropDownlist> DepartList { get; set; }
            public List<DropDownlist> ProjectList { get; set; }
            public List<DropDownlist> TitleList { get; set; }
            public List<DropDownlist> RegionList { get; set; }
            public List<DropDownlist> StateList { get; set; }
            public List<DropDownlist> CityList { get; set; }
            public List<DropDownlist> TshirtSizeList { get; set; }
            public List<DropDownlist> ExperienceList { get; set; }
            public List<DropDownlist> QualificationList { get; set; }
            public List<DropDownlist> GradeList { get; set; }
            public List<DropDownlist> DesigList { get; set; }
            public List<DropDownlist> MWCategoryList { get; set; }
            public List<WFDocuments> WFDocumentList { get; set; }
            public List<DropDownlist> GenderList { get; set; }
            public List<DropDownlist> MaritalStatusList { get; set; }
            public List<DropDownlist> RecruitmentTypeList { get; set; }
            public int TotalTgt { get; set; }
            public int TotalAch { get; set; }
            public int AchPercentage { get; set; }
        }
        public class WFDocuments
        {
            public long? Attach_ID { get; set; }
            public long EMPID { get; set; }
            public string FileName { get; set; }
            public string EMPName { get; set; }
            public string EMPCode { get; set; }
            public string FilePath { get; set; }
            public string ContentType { get; set; }
            public string Description { get; set; }
            public HttpPostedFileBase upload { get; set; }
        }
        public class List
        {
            public int WorkForceID { get; set; }
            public string DocNo { get; set; }
            public string DocDate { get; set; }
            public string BranchCode { get; set; }
            public string ProjectCode { get; set; }
            public string Title { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public string Gender { get; set; }
            public string EmailID { get; set; }
            public string MaritalStatus { get; set; }
            public string DOJ { get; set; }
            public string AadharNo { get; set; }
            public string DOB { get; set; }
            public string LastCompany { get; set; }
            public string StateCode { get; set; }
            public decimal InhandSalary { get; set; }
            public decimal ExpectedSalary { get; set; }
            public string TshirtSize { get; set; }
            public double TotalExperience { get; set; }
            public string Qualification { get; set; }
            public string LastCompany_Designation { get; set; }
            public decimal PreviousSalary_Inhand { get; set; }
            public string Designation { get; set; }
            public string ASM_Phone { get; set; }
            public string Department { get; set; }
            public string RecruitmentType { get; set; }
            public string Region { get; set; }
            public string City { get; set; }
            public string InterviewDate { get; set; }
            public string Grade { get; set; }
            public string ASMName { get; set; }
            public string MWCategory { get; set; }
            public string StoreCode { get; set; }
            public string StoreName { get; set; }
            public string StoreContact { get; set; }
            public string StoreAddress { get; set; }
            public string StoreImagePath { get; set; }
            public int TotalCounterSales_Qty { get; set; }
            public int ClientCurrentSales_Qty { get; set; }
            public int ProposedSales_Qty { get; set; }
            public int ClientProductDisplay_Qty { get; set; }
            public int Sales_Last2ndMonth { get; set; }
            public int Sales_Last1stMonth { get; set; }
            public int Sales_CurrentMonth { get; set; }
            public int Sales_CurrentInventory { get; set; }
            public string SOP_BranchName { get; set; }
            public string SOP_OutletName { get; set; }
            public string SOP_OutletLocation { get; set; }
            public string SOP_CurrentMonth { get; set; }
            public string CMIConnectCode { get; set; }
            public string SOP_NextMonth1 { get; set; }
            public string SOP_NextMonth2 { get; set; }
            public string SOP_NextMonth3 { get; set; }
            public string SOP_NextMonth4 { get; set; }
            public string SOP_NextMonth5 { get; set; }
            public string SOP_NextMonth6 { get; set; }
            public int SOR_Target1 { get; set; }
            public int SOR_Target2 { get; set; }
            public int SOR_Target3 { get; set; }
            public int SOR_Target4 { get; set; }
            public int SOR_Target5 { get; set; }
            public int SOR_Target6 { get; set; }
            public int SOR_RACSales1 { get; set; }
            public int SOR_RACSales2 { get; set; }
            public int SOR_RACSales3 { get; set; }
            public int SOR_RACSales4 { get; set; }
            public int SOR_RACSales5 { get; set; }
            public int SOR_RACSales6 { get; set; }
            public string ReferralSource { get; set; }
            public string InterviewBy { get; set; }
            public string Remarks { get; set; }
            public int TotalCount { get; set; }
            public int Approved { get; set; }
            public int ShowApprovalBtn { get; set; }
        }
        public class ApprovalAction
        {
            public int WorkForceID { get; set; }
            public int Approved { get; set; }
            public int ApprovedLevel { get; set; }
            public string ApprovedRemarks { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }
        }

    }
}
