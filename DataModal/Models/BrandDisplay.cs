using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataModal.Models
{
    public class BrandDisplay
    {
        public class List
        {
            public int RowNum { get; set; }
            public long BrandID { get; set; }
            public string DocDate { get; set; }
            public long EMPID { get; set; }
            public string EMPName { get; set; }
            public string EMPCode { get; set; }
            public long ItemID { get; set; }
            public string ItemName { get; set; }
            public string ItemCode { get; set; }

            public string DealerCode { get; set; }
            public string DealerName { get; set; }
            public int Qty { get; set; }
            public string Price { get; set; }
            public string ImageURL { get; set; }
            public string Remarks { get; set; }
            public bool IsActive { get; set; }
            public int Priority { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedDate { get; set; }
            public string ModifiedDate { get; set; }
            public string ModifiedBy { get; set; }
            public string IPAddress { get; set; }

        }
        public class Add
        {
            public string DocDate { get; set; }
            public long? BrandID { get; set; }
            public long EMPID { get; set; }
            [Required(ErrorMessage = "Item Can't be Blank")]
            public long? ItemID { get; set; }

            [Required(ErrorMessage = "Qty Can't be Blank")]
            public int? Qty { get; set; }
            public long? DealerID { get; set; }
            public long? AttachmentID { get; set; }
            public string Remarks { get; set; }
            public bool IsActive { get; set; }
            public int? Priority { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }

            public string ImageBase64String { get; set; }
            public List<DropDownlist> ItemList { get; set; }
            public List<DropDownlist> QtyList { get; set; }
            public List<DropDownlist> DealerList { get; set; }
        }
    }
}
