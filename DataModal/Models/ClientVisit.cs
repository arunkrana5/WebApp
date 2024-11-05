using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataModal.Models
{
    public class ClientVisit
    {
        public class List
        {
            public int IsVisitCompleted { get; set; }
            public long CVisitID { get; set; }
            public long C_TranID { get; set; }
            public int SalesCount { get; set; }
            public string DocNo { get; set; }
            public long EMPID { get; set; }
            public string EMPName { get; set; }
            public string EMPCode { get; set; }
            public string DealerName { get; set; }
            public string IsMasterDealer { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedDate { get; set; }
            public string ModifiedDate { get; set; }
            public string ModifiedBy { get; set; }
            public string IPAddress { get; set; }
        }
        public class AddVisitCheckIn
        {
            public long? C_TranID { get; set; }
            public long? CVisitID { get; set; }
            public long BrandID { get; set; }
            public long EMPID { get; set; }
            public string Date { get; set; }

            public long? DealerID { get; set; }

            [Required(ErrorMessage = "please choose it Can't be Blank")]
            public long? IsMasterDealer { get; set; }

            [Required(ErrorMessage = "Dealer Name Can't be Blank")]
            public string DealerName { get; set; }

            [Required(ErrorMessage = "Purpose Of Visit Can't be Blank")]
            public string PurposeOfVisit { get; set; }
            public string ContactPersonName { get; set; }
            public string ContactPersonPhone { get; set; }

            [Required(ErrorMessage = "Can't be Blank")]
            public long? StatusID { get; set; }

            public string Location { get; set; }
            [Required(ErrorMessage = "Location not Found please check GPS Permission")]
            public string Latitude { get; set; }
            public string Longitude { get; set; }
            public string Error { get; set; }

            public long AttachmentID { get; set; }
            public string ImageBase64String { get; set; }
            public List<DropDownlist> AttendenceStatusList { get; set; }
            public List<DropDownlist> DealerList { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }
        }

        public class AddVisitCheckOut
        {
            public long? C_TranID { get; set; }
            public long? CVisitID { get; set; }
            public int IsNoSale { get; set; }

            [Required(ErrorMessage = "Name Can't be Blank")]
            public string ContactPersonName { get; set; }
            [Required(ErrorMessage = "Phone Can't be Blank")]
            public string ContactPersonPhone { get; set; }

            [Required(ErrorMessage = "Can't be Blank")]
            public long? StatusID { get; set; }

            public string Location { get; set; }
            [Required(ErrorMessage = "Location not Found please check GPS Permission")]
            public string Latitude { get; set; }
            public string Longitude { get; set; }
            public string Error { get; set; }

            public long AttachmentID { get; set; }
            public string ImageBase64String { get; set; }
            public List<DropDownlist> AttendenceStatusList { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }
        }


        public class SalesList
        {
            public List<AddSales> Sales { get; set; }
        }
        public class AddSales
        {
            public long SaleID { get; set; }
            public long? CVisitID { get; set; }
            public long? C_TranID { get; set; }
            [Required(ErrorMessage = "Can't be Blank")]
            public string Model { get; set; }
            [Required(ErrorMessage = "Can't be Blank")]
            public int? Qty { get; set; }
            [Required(ErrorMessage = "Can't be Blank")]
            public float? Price { get; set; }
            public long? AttachmentID { get; set; }
            [Required(ErrorMessage = "Image Can't be Blank")]
            public string ImageBase64String { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }

        }
    }
}
