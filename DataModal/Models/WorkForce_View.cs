using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModal.Models
{
    public class WorkForce_View
    {
        public Request request { get; set; }
        public List<WorkForce_Approvals> approvals { get; set; }
        public List<WorkForce_ApprovalsMaster> approvalsMasters { get; set; }
        public List<WorkForce_Attachments> attachments { get; set; }
        public SMTPMail mail { get; set; }
        public MailDetails maildetails { get; set; }
    }
    public class Request
    {
        public long WorkForceID { get; set; }
        public string DocNo { get; set; }
        public DateTime DocDate { get; set; }
        public string BranchCode { get; set; }
        public string ProjectCode { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string EmailID { get; set; }
        public string MaritalStatus { get; set; }
        public DateTime? DOJ { get; set; }
        public string AadharNo { get; set; }
        public DateTime? DOB { get; set; }
        public string LastCompany { get; set; }
        public string StateCode { get; set; }
        public decimal InhandSalary { get; set; }
        public decimal ExpectedSalary { get; set; }
        public string TshirtSize { get; set; }
        public float TotalExperience { get; set; }
        public string Qualification { get; set; }
        public string LastCompany_Designation { get; set; }
        public decimal PreviousSalary_Inhand { get; set; }
        public string Designation { get; set; }
        public string ASM_Phone { get; set; }
        public string Department { get; set; }
        public string RecruitementType { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public DateTime? InterviewDate { get; set; }
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
        public int Approved { get; set; }
        public string Status { get; set; }
        public string Approvedby { get; set; }
        public string ApprovedRemarks { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByMail { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string IPAddress { get; set; }
    }
    public class WorkForce_Approvals
    {
        public int ApprovalID { get; set; }
        public int WorkForceID { get; set; }
        public int Approved { get; set; }
        public int ApprovedLevel { get; set; }
        public string Approvedby { get; set; }
        public string ApprovedRemarks { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }

    }
    public class WorkForce_ApprovalsMaster
    {
        public int ID { get; set; }
        public int LoginID { get; set; }
        public int ApprovedLevel { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

    }
    public class WorkForce_Attachments
    {
        public long? DocID { get; set; }
        public long WorkForceID { get; set; }
        public string DocType { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }
    }
}
