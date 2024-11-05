using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DataModal.Models
{
    public class PJPEntry
    {
        public class List
        {
            public int RowNum { get; set; }
            public string VisitDate { get; set; }
            public long PJPEntryID { get; set; }
            public long PJPPlanID { get; set; }
            public string DealerType { get; set; }
            public string DealerName { get; set; }
            public string DealerCode { get; set; }
            public long SSR_EMPID { get; set; }
            public string SSR_Name { get; set; }
            public string SSR_Code { get; set; }

            public string Region { get; set; }
            public string State { get; set; }
            public string City { get; set; }
            public string Area { get; set; }

            public long AttachID { get; set; }
            public string AttachPath { get; set; }
            public int ProductRating { get; set; }
            public int CustomerRating { get; set; }
            public string ProductKnw { get; set; }
            public string CustomerKnw { get; set; }
            public bool IsActive { get; set; }
            public int Priority { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedDate { get; set; }
            public string ModifiedDate { get; set; }
            public string ModifiedBy { get; set; }
            public string IPAddress { get; set; }

            public string ExpenseAmt { get; set; }
            public string AttachmentPath { get; set; }
            public string ExpenseAttachmentPath { get; set; }
            public int TotalCount { get; set; }
            public string DealerAddress { get; set; }
            public string EMPName { get; set; }
            public string EMPCode { get; set; }
            public string SSRAvailability { get; set; }
            public string Status { get; set; }
            public string PlanCreatedDate { get; set; }
            public string Branch { get; set; }
        }

        public class Add
        {
            public string ContactPerson_Name { get; set; }

            public string ContactPerson_Phone { get; set; }
            public long? PJPEntryID { get; set; }
            public long? PJPPlanID { get; set; }
            [Required(ErrorMessage = "SSR Can't be Blank")]
            public long? SSR_EMPID { get; set; }
            public string SSR_Name { get; set; }
            public string SSR_Code { get; set; }
            public string AttPunchedBySSR { get; set; }
            public long? SSR_AttendenceID { get; set; }
            [Required(ErrorMessage = "Availability Can't be Blank")]
            public string SSRAvailability { get; set; }

            [Required(ErrorMessage = "Image Can't be Blank")]
            public string ImageBase64String { get; set; }
            public long? AttachmentID { get; set; }
            public string AttachmentPath { get; set; }
            [Required(ErrorMessage = "Product Rating Can't be Blank")]
            public int? ProductRating { get; set; }
            [Required(ErrorMessage = "Customer Rating Can't be Blank")]
            public int? CustomerRating { get; set; }
            public string ProductKnw { get; set; }
            public string CustomerKnw { get; set; }
            public string Location { get; set; }
            [Required(ErrorMessage = "Location not Found please check GPS Permission")]
            public string Latitude { get; set; }
            public string Longitude { get; set; }
            public string Error { get; set; }
            public string Notes { get; set; }

            [Required(ErrorMessage = "Expense Amount Can't be Blank")]
            public decimal? ExpenseAmt { get; set; }

            public HttpPostedFileBase ExpenseUpload { get; set; }

            public long? ExpenseAttachmentID { get; set; }
            public string ExpenseRemarks { get; set; }
            public string ExpenseAttachmentPath { get; set; }

            public List<PJPEntry_Brand.List> BrandEntryList { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }


            public List<DropDownlist> SSRList { get; set; }
            public List<DropDownlist> BrandList { get; set; }

        }

        public class AddWithNoSSR
        {
            [Required(ErrorMessage = "Contact Person Name Can't be Blank")]
            public string ContactPerson_Name { get; set; }
            [Required(ErrorMessage = "Contact Person Phone Can't be Blank")]
            public string ContactPerson_Phone { get; set; }
            public long? PJPEntryID { get; set; }
            public long? PJPPlanID { get; set; }

            [Required(ErrorMessage = "Image Can't be Blank")]
            public string ImageBase64String { get; set; }
            public long? AttachmentID { get; set; }
            public string AttachmentPath { get; set; }
            public string Location { get; set; }
            [Required(ErrorMessage = "Location not Found please check GPS Permission")]
            public string Latitude { get; set; }
            public string Longitude { get; set; }
            public string Error { get; set; }
            public string Notes { get; set; }

            [Required(ErrorMessage = "Expense Amount Can't be Blank")]
            public decimal? ExpenseAmt { get; set; }
            public HttpPostedFileBase ExpenseUpload { get; set; }
            public long? ExpenseAttachmentID { get; set; }
            public string ExpenseRemarks { get; set; }
            public string ExpenseAttachmentPath { get; set; }
            public List<PJPEntry_Brand.List> BrandEntryList { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }
            public List<DropDownlist> BrandList { get; set; }

            public long? SSR_EMPID { get; set; }
            public string SSRAvailability { get; set; }
            public long? SSR_AttendenceID { get; set; }

        }
        public class ApprovalList
        {
            public int RowNum { get; set; }
            public long PJPEntryID { get; set; }
            public long PJPExpID { get; set; }
            public string DocNo { get; set; }
            public string DocDate { get; set; }
            public string DealerName { get; set; }
            public string DealerCode { get; set; }
            public string DealerArea { get; set; }
            public string DealerAddress { get; set; }
            public string EMPName { get; set; }
            public string EMPCode { get; set; }
            public string VisitDate { get; set; }
            public bool IsActive { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedDate { get; set; }
            public string ModifiedDate { get; set; }
            public string ModifiedBy { get; set; }
            public string IPAddress { get; set; }
            public string RouteNumber { get; set; }
            public string VisitType { get; set; }
            public string Status { get; set; }
            public string ApprovedRemarks { get; set; }
            public int Approved { get; set; }
        }
        public class ApprovalAction
        {
            public string PJPEntryIDs { get; set; }
            public int Approved { get; set; }
            public string ApprovedRemarks { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }
        }

    }
    public class PJPEntry_Brand
    {
        public class List
        {
            public int? RowNum { get; set; }
            public long? PJPBrandID { get; set; }
            public long? PJPEntryID { get; set; }
            public long? BrandID { get; set; }
            public string BrandName { get; set; }
            public long? Qty { get; set; }
            public long? AttachID { get; set; }
            public string AttachPath { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedDate { get; set; }
            public string ModifiedDate { get; set; }
            public string ModifiedBy { get; set; }
            public string IPAddress { get; set; }
            public long LoginID { get; set; }
            public string ImageBase64String { get; set; }
        }


    }


    public class PJPExpense
    {
        public class List
        {
            public long PJPEntryID { get; set; }
            public long PJPPlanID { get; set; }
            public string Image { get; set; }
            public string VisitDate { get; set; }
            public string BranchCode { get; set; }
            public string BranchName { get; set; }
            public string BTName { get; set; }
            public string BTCode { get; set; }
            public string DealerType { get; set; }
            public string DealerName { get; set; }
            public string DealerCode { get; set; }
            public string RegionName { get; set; }
            public string StateName { get; set; }
            public string CityName { get; set; }
            public string AreaName { get; set; }
            public string PurposeVisit { get; set; }
            public string Exp_Amount { get; set; }
            public string Exp_Remarks { get; set; }
            public string SSRName { get; set; }
            public string SSRCode { get; set; }
            public string ApprovedStatus { get; set; }
            public string ApprovedRemarks { get; set; }
            public string Approvedby { get; set; }
            public string ApprovedDate { get; set; }
            public int Approved { get; set; }
            public string ContactPerson_Name { get; set; }
            public string ContactPerson_Phone { get; set; }

        }
    }
    public class PJPExpenses
    {
        public class List
        {
            public long PJPExpensesID { get; set; }
            public long PJPEntryID { get; set; }
            public long PJPPlanID { get; set; }
            public string Image { get; set; }
            public string ExpImage { get; set; }
            public string VisitDate { get; set; }
            public string BranchCode { get; set; }
            public string BranchName { get; set; }
            public string BTName { get; set; }
            public string BTCode { get; set; }
            public string DealerType { get; set; }
            public string DealerName { get; set; }
            public string DealerCode { get; set; }
            public string RegionName { get; set; }
            public string StateName { get; set; }
            public string CityName { get; set; }
            public string AreaName { get; set; }
            public string PurposeVisit { get; set; }
            public string Exp_Amount { get; set; }
            public string Exp_Remarks { get; set; }
            public string SSRName { get; set; }
            public string SSRCode { get; set; }
            public string ApprovedStatus { get; set; }
            public string ApprovedRemarks { get; set; }
            public string Approvedby { get; set; }
            public string ApprovedDate { get; set; }
            public int Approved { get; set; }
            public string ContactPerson_Name { get; set; }
            public string ContactPerson_Phone { get; set; }
            public string Description { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }
            public string DocType { get; set; }
            public string FileName { get; set; }
            public string FileExt { get; set; }
            public string VisitType { get; set; }
            public string ExpenseType { get; set; }
            public string exp_Amount { get; set; }
            public string exp_Remarks { get; set; }
        }

        public class ApprovalAction
        {
            public string PJPExpIDs { get; set; }
            public int Approved { get; set; }
            public string ApprovedRemarks { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }
        }
    }

    public class PJPEntries
    {
        public class List
        {
            public int RowNum { get; set; }
            public string VisitDate { get; set; }
            public long PJPEntryID { get; set; }
            public long PJPPlanID { get; set; }
            public string DealerType { get; set; }
            public string DealerName { get; set; }
            public string DealerCode { get; set; }
            public long SSR_EMPID { get; set; }
            public string SSR_Name { get; set; }
            public string SSR_Code { get; set; }

            public string Region { get; set; }
            public string State { get; set; }
            public string City { get; set; }
            public string Area { get; set; }

            public long AttachID { get; set; }
            public string AttachPath { get; set; }
            public int ProductRating { get; set; }
            public int CustomerRating { get; set; }
            public string ProductKnw { get; set; }
            public string CustomerKnw { get; set; }
            public bool IsActive { get; set; }
            public int Priority { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedDate { get; set; }
            public string ModifiedDate { get; set; }
            public string ModifiedBy { get; set; }
            public string IPAddress { get; set; }

            public string ExpenseAmt { get; set; }
            public string AttachmentPath { get; set; }
            public string ExpenseAttachmentPath { get; set; }
            public int TotalCount { get; set; }
            public string DealerAddress { get; set; }
            public string EMPName { get; set; }
            public string EMPCode { get; set; }
            public string SSRAvailability { get; set; }
            public string Status { get; set; }
            public string PlanCreatedDate { get; set; }
            public string Branch { get; set; }
            public string RouteNumber { get; set; }
            public string VisitType { get; set; }
            public string TotalExpAmt { get; set; }
        }

        public class Add
        {
            public string ContactPerson_Name { get; set; }

            public string ContactPerson_Phone { get; set; }
            public long? PJPEntryID { get; set; }
            public long? PJPPlanID { get; set; }
            [Required(ErrorMessage = "SSR Can't be Blank")]
            public long? SSR_EMPID { get; set; }
            public string SSR_Name { get; set; }
            public string SSR_Code { get; set; }
            public string AttPunchedBySSR { get; set; }
            public long? SSR_AttendenceID { get; set; }
            [Required(ErrorMessage = "Availability Can't be Blank")]
            public string SSRAvailability { get; set; }

            [Required(ErrorMessage = "Image Can't be Blank")]
            public string ImageBase64String { get; set; }
            public long? AttachmentID { get; set; }
            public string AttachmentPath { get; set; }
            [Required(ErrorMessage = "Product Rating Can't be Blank")]
            public int? ProductRating { get; set; }
            [Required(ErrorMessage = "Customer Rating Can't be Blank")]
            public int? CustomerRating { get; set; }
            public string ProductKnw { get; set; }
            public string CustomerKnw { get; set; }
            public string Location { get; set; }
            [Required(ErrorMessage = "Location not Found please check GPS Permission")]
            public string Latitude { get; set; }
            public string Longitude { get; set; }
            public string Error { get; set; }
            public string Notes { get; set; }

            //[Required(ErrorMessage = "Expense Amount Can't be Blank")]
            public decimal? ExpenseAmt { get; set; }

            public HttpPostedFileBase ExpenseUpload { get; set; }

            public long? ExpenseAttachmentID { get; set; }
            public string ExpenseRemarks { get; set; }
            public string ExpenseAttachmentPath { get; set; }

            public List<PJPEntry_Brand.List> BrandEntryList { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }


            public List<DropDownlist> SSRList { get; set; }
            public List<DropDownlist> BrandList { get; set; }
            [Required(ErrorMessage = "Sales Entry Can't be Blank")]
            public string LastMonthACSaleQty { get; set; }
            [Required(ErrorMessage = "Sales Entry Can't be Blank")]
            public string LastMonthSaleQty { get; set; }
            public List<PJPDocumentList> DocumentList { get; set; }

        }

        public class AddWithNoSSR
        {
            [Required(ErrorMessage = "Contact Person Name Can't be Blank")]
            public string ContactPerson_Name { get; set; }
            [Required(ErrorMessage = "Contact Person Phone Can't be Blank")]
            public string ContactPerson_Phone { get; set; }
            public long? PJPEntryID { get; set; }
            public long? PJPPlanID { get; set; }

            [Required(ErrorMessage = "Image Can't be Blank")]
            public string ImageBase64String { get; set; }
            public long? AttachmentID { get; set; }
            public string AttachmentPath { get; set; }
            public string Location { get; set; }
            [Required(ErrorMessage = "Location not Found please check GPS Permission")]
            public string Latitude { get; set; }
            public string Longitude { get; set; }
            public string Error { get; set; }
            public string Notes { get; set; }

            //[Required(ErrorMessage = "Expense Amount Can't be Blank")]
            public decimal? ExpenseAmt { get; set; }
            public HttpPostedFileBase ExpenseUpload { get; set; }
            public long? ExpenseAttachmentID { get; set; }
            public string ExpenseRemarks { get; set; }
            public string ExpenseAttachmentPath { get; set; }
            public List<PJPEntry_Brand.List> BrandEntryList { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }
            public List<DropDownlist> BrandList { get; set; }

            public long? SSR_EMPID { get; set; }
            public string SSRAvailability { get; set; }
            public long? SSR_AttendenceID { get; set; }
            [Required(ErrorMessage = "Sales Entry Can't be Blank")]
            public string LastMonthACSaleQty { get; set; }
            [Required(ErrorMessage = "Sales Entry Can't be Blank")]
            public string LastMonthSaleQty { get; set; }

        }
        public class ApprovalList
        {
            public int RowNum { get; set; }
            public long PJPEntryID { get; set; }
            public long PJPExpID { get; set; }
            public string DocNo { get; set; }
            public string DocDate { get; set; }
            public string DealerName { get; set; }
            public string DealerCode { get; set; }
            public string DealerArea { get; set; }
            public string DealerAddress { get; set; }
            public string EMPName { get; set; }
            public string EMPCode { get; set; }
            public string VisitDate { get; set; }
            public bool IsActive { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedDate { get; set; }
            public string ModifiedDate { get; set; }
            public string ModifiedBy { get; set; }
            public string IPAddress { get; set; }
            public string RouteNumber { get; set; }
            public string VisitType { get; set; }
            public string Status { get; set; }
            public string ApprovedRemarks { get; set; }
            public int Approved { get; set; }
        }
        public class ApprovalAction
        {
            public string PJPEntryIDs { get; set; }
            public int Approved { get; set; }
            public string ApprovedRemarks { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }
        }

    }
}
