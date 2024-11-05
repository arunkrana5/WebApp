using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DataModal.Models
{
    public class SaleEntry
    {
        public class List
        {
            public string DocNo { get; set; }
            public long SaleEntryID { get; set; }
            public string EMPName { get; set; }
            public string EMPCode { get; set; }
            public string DealerName { get; set; }
            public string DealerCode { get; set; }

            public string RegionName { get; set; }
            public string StateName { get; set; }
            public string CityName { get; set; }
            public string AreaName { get; set; }
            public string DealerType { get; set; }
            public string BranchCode { get; set; }
            public string BranchName { get; set; }
            public string Date { get; set; }
            public string InvoiceDate { get; set; }
            public string InvoiceNo { get; set; }
            public string ProductName { get; set; }
            public string TranName { get; set; }
            public string ItemName { get; set; }
            public decimal Qty { get; set; }
            public decimal Price { get; set; }
            public string SerialNo { get; set; }
            public string InstallationNo { get; set; }
            public string PaymentMode { get; set; }
            public string IsExchange { get; set; }
            public string Remarks { get; set; }
            public long AttachmentID { get; set; }
            public string AttachmentPath { get; set; }
            public bool IsActive { get; set; }
            public int Priority { get; set; }

            public string CustomerName { get; set; }
            public string CustomerPhone { get; set; }
            public string CustomerEmail { get; set; }
            public int Approved { get; set; }
            public string Status { get; set; }
            public string ApprovedRemarks { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedDate { get; set; }
            public string ModifiedDate { get; set; }
            public string ModifiedBy { get; set; }
            public string IPAddress { get; set; }
        }
        public class Add
        {
            public string SaleFor { get; set; }
            public long? SaleEntryID { get; set; }
            public long EMPID { get; set; }
            public string Date { get; set; }
            [Required(ErrorMessage = "Invoice Date Can't be Blank")]
            public string InvoiceDate { get; set; }
            [Required(ErrorMessage = "Invoice No Can't be Blank")]
            public string InvoiceNo { get; set; }
            public long? ProductTypeID { get; set; }
            public long? ProductID { get; set; }
            public long? PDTranID { get; set; }
            [Required(ErrorMessage = "Item Can't be Blank")]
            public long? ItemID { get; set; }
            [Required(ErrorMessage = "Qty Can't be Blank")]
            [Range(1, 50, ErrorMessage = "Qty must between 1 to 50")]
            public decimal? Qty { get; set; }
            [Required(ErrorMessage = "Price Can't be Blank")]
            public decimal? Price { get; set; }

            public string SerialNo { get; set; }

            public string InstallationNo { get; set; }
            [Required(ErrorMessage = "Payment Mode Can't be Blank")]
            public string PaymentMode { get; set; }
            public int IsExchange { get; set; }
            public string Remarks { get; set; }
            public long AttachmentID { get; set; }


            // Customer Start
            [Required(ErrorMessage = "Name Can't be Blank")]
            public string Name { get; set; }
            [Required(ErrorMessage = "Phone Can't be Blank")]
            [DataType(DataType.PhoneNumber)]
            [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
            public string Phone { get; set; }

            public string Email { get; set; }

            // Customer ENd

            // Address Customer start
            public string Doctype { get; set; }
            public long? CountryID { get; set; }
            public long? StateID { get; set; }
            public string Location { get; set; }
            public long? CityID { get; set; }
            [Required(ErrorMessage = "Address 1 Can't be Blank")]
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string Zipcode { get; set; }
            // Address Customer End

            public int? Priority { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }
            public List<DropDownlist> StateList { get; set; }
            public List<DropDownlist> CityList { get; set; }
            public List<DropDownlist> ProductTypeList { get; set; }
            public List<DropDownlist> ProductList { get; set; }
            public List<DropDownlist> ProductTranList { get; set; }
            public List<DropDownlist> ItemsList { get; set; }
            public List<DropDownlist> PaymentModeList { get; set; }

            public List<DropDownlist> QtyList { get; set; }
            public string AttachmentPath { get; set; }

            public HttpPostedFileBase Upload { get; set; }


            public int Approved { get; set; }
            public string ApprovedRemarks { get; set; }
        }


        public class Report
        {
            public string DocNo { get; set; }
            public long SaleEntryID { get; set; }
            public string EMPName { get; set; }
            public string EMPCode { get; set; }
            public string DealerName { get; set; }
            public string DealerCode { get; set; }

            public string RegionName { get; set; }
            public string StateName { get; set; }
            public string CityName { get; set; }
            public string AreaName { get; set; }
            public string DealerType { get; set; }
            public string DealerArea { get; set; }
            public string BranchCode { get; set; }
            public string BranchName { get; set; }
            public string Date { get; set; }
            public string InvoiceDate { get; set; }
            public string InvoiceNo { get; set; }
            public string ProductName { get; set; }
            public string TranName { get; set; }
            public string ItemName { get; set; }
            public decimal Qty { get; set; }
            public decimal Price { get; set; }
            public string SerialNo { get; set; }
            public string InstallationNo { get; set; }
            public string PaymentMode { get; set; }
            public string IsExchange { get; set; }
            public string Remarks { get; set; }
            public long AttachmentID { get; set; }
            public string AttachmentPath { get; set; }
            public bool IsActive { get; set; }
            public int Priority { get; set; }
            public string CustomerEmail { get; set; }
            public string CustomerName { get; set; }
            public string CustomerPhone { get; set; }
            public int Approved { get; set; }
            public string Status { get; set; }
            public string ApprovedRemarks { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedDate { get; set; }
            public string ModifiedDate { get; set; }
            public string ModifiedBy { get; set; }
            public string IPAddress { get; set; }
        }


        public class ApprovalList
        {
            public string DocNo { get; set; }
            public long SaleEntryID { get; set; }
            public string EMPName { get; set; }
            public string EMPCode { get; set; }
            public string DealerName { get; set; }
            public string DealerCode { get; set; }

            public string RegionName { get; set; }
            public string StateName { get; set; }
            public string CityName { get; set; }
            public string AreaName { get; set; }
            public string DealerType { get; set; }
            public string DealerArea { get; set; }
            public string BranchCode { get; set; }
            public string BranchName { get; set; }
            public string Date { get; set; }
            public string InvoiceDate { get; set; }
            public string InvoiceNo { get; set; }
            public string ProductTypeName { get; set; }
            public string ProductName { get; set; }
            public string TranName { get; set; }
            public string ItemName { get; set; }
            public decimal Qty { get; set; }
            public decimal Price { get; set; }
            public string SerialNo { get; set; }
            public string InstallationNo { get; set; }
            public string PaymentMode { get; set; }
            public string IsExchange { get; set; }
            public string Remarks { get; set; }
            public long AttachmentID { get; set; }
            public string AttachmentPath { get; set; }
            public bool IsActive { get; set; }
            public int Priority { get; set; }

            public string CustomerName { get; set; }
            public string CustomerPhone { get; set; }
            public string CustomerEmail { get; set; }
            public int Approved { get; set; }
            public string Status { get; set; }
            public string ApprovedRemarks { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedDate { get; set; }
            public string ModifiedDate { get; set; }
            public string ModifiedBy { get; set; }
            public string IPAddress { get; set; }


        }

        public class ApprovalAction
        {
            public string SaleEntryIDs { get; set; }
            public int Approved { get; set; }
            public string ApprovedRemarks { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }
        }
    }
}
