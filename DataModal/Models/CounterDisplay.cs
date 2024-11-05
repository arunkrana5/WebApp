using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataModal.Models
{
    public class CounterDisplay
    {
        public class List
        {
            public int RowNum { get; set; }
            public long CounterID { get; set; }
            public string BrandName { get; set; }
            public decimal Qty { get; set; }
            public long AttachmentID { get; set; }
            public string AttachmentURL { get; set; }

            public long EMPID { get; set; }
            public string Date { get; set; }
            public string EMPName { get; set; }
            public string EMPCode { get; set; }
            public string Remarks { get; set; }
            public bool IsActive { get; set; }
            public int? Priority { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedDate { get; set; }
            public string ModifiedDate { get; set; }
            public string ModifiedBy { get; set; }
            public string IPAddress { get; set; }
        }
        public class Add
        {
            public long? CounterID { get; set; }
            public long TourPlanID { get; set; }
            public long EMPID { get; set; }
            public string Date { get; set; }
            [Required(ErrorMessage = "Brand Can't be Blank")]
            public long? BrandID { get; set; }
            [Required(ErrorMessage = "Quantity Can't be Blank")]
            public decimal? Qty { get; set; }
            public long AttachmentID { get; set; }
            public string AttachmentURL { get; set; }
            public string Remarks { get; set; }
            public bool IsActive { get; set; }
            public int? Priority { get; set; }

            public long LoginID { get; set; }
            public string IPAddress { get; set; }

            public List<DropDownlist> BrandList { get; set; }
            [Required(ErrorMessage = "Image Can't be Blank")]
            public string ImageBase64String { get; set; }

        }

        public class Report
        {
            public int RowNum { get; set; }
            public string Date { get; set; }
            public long CounterID { get; set; }
            public string BrandName { get; set; }
            public decimal Qty { get; set; }
            public long AttachmentID { get; set; }
            public string AttachmentURL { get; set; }

            public long EMPID { get; set; }

            public string EMPName { get; set; }
            public string EMPCode { get; set; }
            public string Remarks { get; set; }
            public bool IsActive { get; set; }
            public int? Priority { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedDate { get; set; }
            public string ModifiedDate { get; set; }
            public string ModifiedBy { get; set; }
            public string IPAddress { get; set; }
        }
    }
}
