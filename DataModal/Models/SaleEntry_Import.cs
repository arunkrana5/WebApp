namespace DataModal.Models
{
    public class SaleEntry_Import
    {
        public class List
        {
            public int RowNum { get; set; }
            public string DocNo { get; set; }
            public string SaleFor { get; set; }
            public string EMPCode { get; set; }
            public string BranchCode { get; set; }
            public string DealerCode { get; set; }
            public string Date { get; set; }
            public string InvoiceNo { get; set; }
            public string InvoiceDate { get; set; }
            public string ItemCode { get; set; }
            public string Qty { get; set; }
            public string Price { get; set; }
            public string SerialNo { get; set; }

            public string InstallationNo { get; set; }
            public string PaymentMode { get; set; }
            public string IsExchange { get; set; }
            public string CustomerName { get; set; }
            public string CustomerPhone { get; set; }
            public string CustomerEmail { get; set; }

            public string CustomerCountry { get; set; }
            public string CustomerState { get; set; }
            public string CustomerCity { get; set; }
            public string CustomerLocation { get; set; }
            public string CustomerAddress { get; set; }
            public string CustomerPincode { get; set; }
            public string Remarks { get; set; }
            public string UserRemarks { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedDate { get; set; }
            public string IPAddress { get; set; }
        }
    }
}
