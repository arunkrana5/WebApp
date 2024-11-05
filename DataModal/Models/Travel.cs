using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DataModal.Models
{
    public class Travel
    {
        public class List
        {

            public long TRID { get; set; }
            public long EMPID { get; set; }
            public string EMPName { get; set; }
            public string EMPCode { get; set; }
            public string DocNo { get; set; }
            public string DocDate { get; set; }

            public string FromCity { get; set; }
            public string ToCity { get; set; }
            public string StartDate { get; set; }
            public string EndDate { get; set; }
            public string TravelMode { get; set; }
            public string IsRequiredHotel { get; set; }
            public string Remarks { get; set; }
            public string Status { get; set; }
            public int Approved { get; set; }
            public string ApprovedRemarks { get; set; }
            public string ApprovedDate { get; set; }
            public string ApprovedBy { get; set; }
            public string EntrySource { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedDate { get; set; }
            public string ModifiedDate { get; set; }
            public string ModifiedBy { get; set; }
            public string IPAddress { get; set; }
        }
        public class Add
        {

            public long? TRID { get; set; }
            public long? EMPID { get; set; }
            public string DocNo { get; set; }
            public string DocDate { get; set; }
            [Required(ErrorMessage = "From City Can't be Blank")]
            public long? FromCity_ID { get; set; }
            [Required(ErrorMessage = "To City Can't be Blank")]
            public long? ToCity_ID { get; set; }
            [Required(ErrorMessage = "Start Date Can't be Blank")]
            public string StartDate { get; set; }
            [Required(ErrorMessage = "End Date Can't be Blank")]
            public string EndDate { get; set; }
            [Required(ErrorMessage = "Travel Mode Can't be Blank")]
            public string TravelMode { get; set; }

            [Required(ErrorMessage = "Dealer Can't be Blank")]
            public string DealerIDs { get; set; }
            public int IsRequiredHotel { get; set; }
            [Required(ErrorMessage = "Purpose Can't be Blank")]
            public string Remarks { get; set; }
            public int Approved { get; set; }
            public string ApprovedRemarks { get; set; }
            public string ApprovedDate { get; set; }
            public string ApprovedBy { get; set; }

            public long? Priority { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }

            public List<DropDownlist> CityList { get; set; }
            public List<DropDownlist> TravelModeList { get; set; }

        }

        public class ExpenseList
        {
            public long ExpenseID { get; set; }
            public long TRID { get; set; }
            public long EMPID { get; set; }
            public string EMPName { get; set; }
            public string EMPCode { get; set; }
            public string DocNo { get; set; }
            public string DocDate { get; set; }
            public string FromCity { get; set; }
            public string ToCity { get; set; }
            public string StartDate { get; set; }
            public string EndDate { get; set; }
            public string TravelMode { get; set; }
            public string IsRequiredHotel { get; set; }
            public string Remarks { get; set; }
            public string Status { get; set; }
            public decimal TravelExp_Amt { get; set; }
            public decimal VisitExp_Amt { get; set; }
            public decimal HotelExp_Amt { get; set; }
            public decimal? OtherExp_Amt { get; set; }

            public decimal? TotalExp_Amt { get; set; }
            public int Approved { get; set; }
            public string ApprovedRemarks { get; set; }
            public string ApprovedDate { get; set; }
            public string ApprovedBy { get; set; }
            public string IPAddress { get; set; }


        }
        public class Values
        {
            public long TRID { get; set; }
            public string Doctype { get; set; }
            public string VisitDate { get; set; }
            public long DealerID { get; set; }
            public long SSR_EMPID { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }

        }

        public class Dealers
        {
            public long DealerID { get; set; }
            public string DealerName { get; set; }
            public string DealerCode { get; set; }
            public string AreaName { get; set; }
            public string DealerType { get; set; }
            public string Address { get; set; }
            public string OnDemand { get; set; }
        }

    }
    public class VisitEntry
    {
        public class List
        {
            public long VisitID { get; set; }
            public string VisitDate { get; set; }
            public string DealerType { get; set; }
            public string DealerName { get; set; }
            public string DealerCode { get; set; }
            public string DealerArea { get; set; }

            public long SSR_EMPID { get; set; }
            public string SSR_Name { get; set; }
            public string SSR_Code { get; set; }
            public string SSR_Phone { get; set; }
            public string SSR_Gender { get; set; }
            public long AttachID { get; set; }
            public string AttachPath { get; set; }
            public int ProductRating { get; set; }
            public int CustomerRating { get; set; }
            public string ProductKnw { get; set; }
            public string CustomerKnw { get; set; }

            public decimal ExpenseAmt { get; set; }
            public string ExpenseRemarks { get; set; }
            public string AttachmentPath { get; set; }
            public string ExpenseAttachmentPath { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedDate { get; set; }
            public string ModifiedDate { get; set; }
            public string ModifiedBy { get; set; }
            public string IPAddress { get; set; }

        }

        public class Add
        {
            public string ContactPerson_Name { get; set; }

            public string ContactPerson_Phone { get; set; }
            public long? VisitID { get; set; }
            public long? TRID { get; set; }

            public long? SSR_AttendenceID { get; set; }
            [Required(ErrorMessage = "Dealer Can't be Blank")]
            public long? DealerID { get; set; }

            public string DealerName { get; set; }
            public string DealerCode { get; set; }
            public string DealerType { get; set; }
            [Required(ErrorMessage = "SSR Can't be Blank")]
            public long? SSR_EMPID { get; set; }
            public string SSR_Name { get; set; }
            public string SSR_Code { get; set; }
            public string AttPunchedBySSR { get; set; }
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

            public List<VisitEntry_Brand.List> BrandEntryList { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }

            public List<DropDownlist> DealerList { get; set; }
            public List<DropDownlist> SSRList { get; set; }
            public List<DropDownlist> BrandList { get; set; }


        }

        public class AddWithNoSSR
        {

            public long? VisitID { get; set; }
            public long? TRID { get; set; }
            public long? SSR_AttendenceID { get; set; }
            [Required(ErrorMessage = "Contact Person Name Can't be Blank")]
            public string ContactPerson_Name { get; set; }
            [Required(ErrorMessage = "Contact Person Phone Can't be Blank")]
            public string ContactPerson_Phone { get; set; }
            public long? DealerID { get; set; }
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
            public List<VisitEntry_Brand.List> BrandEntryList { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }
            public List<DropDownlist> DealerList { get; set; }
            public List<DropDownlist> BrandList { get; set; }
            public long? SSR_EMPID { get; set; }
            public string SSRAvailability { get; set; }


        }

        public class AddMoreDealer
        {
            public int? OnDemand { get; set; }
            public long? TRID { get; set; }
            [Required(ErrorMessage = "Dealer Can't be Blank")]
            public long? DealerID { get; set; }
            public List<DropDownlist> DealerList { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }
        }

    }
    public class VisitEntry_Brand
    {
        public class List
        {
            public long? VisitBrandID { get; set; }
            public int? RowNum { get; set; }
            public long? BrandID { get; set; }
            public long? VisitID { get; set; }
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


    public class TravelExpense
    {
        public long? ExpenseID { get; set; }
        public long? TRID { get; set; }
        [Required(ErrorMessage = "Travel Expense can't be blank")]
        public decimal? TravelExp_Amt { get; set; }

        public decimal? VisitExp_Amt { get; set; }

        public decimal? HotelExp_Amt { get; set; }

        public decimal? OtherExp_Amt { get; set; }

        public decimal? TotalExp_Amt { get; set; }
        public Travel.List TravelRequestDetails { get; set; }
        public List<Travel.Dealers> DealersAssignedList { get; set; }
        public List<VisitEntry.List> VisitEntryList { get; set; }
        public List<TravelExpAttachment> AttachmentList { get; set; }
        public List<OtherExpenseDet> OtherExpenseList { get; set; }

        public List<ExpenseApprovedSummary> ExpenseApprovedSummaryList { get; set; }
        public long LoginID { get; set; }
        public string IPAddress { get; set; }
    }
    public class TravelExpAttachment
    {
        public long? ID { get; set; }
        public string Name { get; set; }
        public string AttachmentPath { get; set; }

        public string Description { get; set; }
        public HttpPostedFileBase ExpenseUpload { get; set; }
    }
    public class OtherExpenseDet
    {

        public long? OExpenseID { get; set; }
        public decimal? Amt { get; set; }
        [Required(ErrorMessage = "Description can't be blank")]
        public string Description { get; set; }
        public long? AttachID { get; set; }
        public HttpPostedFileBase Upload { get; set; }
        public string AttachmentPath { get; set; }

    }

    public class ExpenseApprovedSummary
    {
        public string Doctype { get; set; }
        public string Status { get; set; }
        public int Approved { get; set; }
        public string ApprovedRemarks { get; set; }
        public string ApprovedDate { get; set; }
        public string ApprovedBy { get; set; }
    }




    public class TravelCompleteRequest
    {
        public string ApprovedRemarks { get; set; }
        public long Approved { get; set; }
        public long? ExpenseID { get; set; }
        public long? TRID { get; set; }
        public decimal? TravelExp_Amt { get; set; }
        public decimal? VisitExp_Amt { get; set; }
        public decimal? HotelExp_Amt { get; set; }
        public decimal? OtherExp_Amt { get; set; }
        public decimal? TotalExp_Amt { get; set; }
        public Travel.List TravelRequestDetails { get; set; }
        public List<Travel.Dealers> DealersAssignedList { get; set; }
        public List<VisitEntry.List> VisitEntryList { get; set; }
        public List<TravelExpAttachment> AttachmentList { get; set; }
        public List<OtherExpenseDet> OtherExpenseList { get; set; }
        public List<ExpenseApprovedSummary> ExpenseApprovedSummaryList { get; set; }
        public long LoginID { get; set; }
        public string IPAddress { get; set; }
    }
}
