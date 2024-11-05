using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataModal.Models
{
    public class CompetitionEntry
    {
        public class List
        {
            public int RowNum { get; set; }
            public long CompetitionID { get; set; }
            public string DocDate { get; set; }
            public long EMPID { get; set; }
            public string EMPName { get; set; }
            public string EMPCode { get; set; }
            public long BrandID { get; set; }
            public string BrandName { get; set; }
            public string BrandCode { get; set; }
            public string ProductTypeName { get; set; }
            public string Category { get; set; }
            public string SubCategory { get; set; }
            public int Qty { get; set; }
            public string Price { get; set; }
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
            public long? CompetitionID { get; set; }
            public long EMPID { get; set; }
            public long? BrandID { get; set; }

            public int? Qty { get; set; }
            public decimal? Price { get; set; }

            [Required(ErrorMessage = "Product Type Can't be Blank")]
            public long? ProductTypeID { get; set; }
            public string Category { get; set; }
            public int? StarRating { get; set; }
            public string ModalNo { get; set; }
            public string SubCategory { get; set; }
            public string Remarks { get; set; }
            public bool IsActive { get; set; }
            public int? Priority { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }

            public List<DropDownlist> BrandList { get; set; }
            public List<DropDownlist> ProductTypeList { get; set; }
            public List<DropDownlist> QtyList { get; set; }

            public List<DropDownlist> CategoryList { get; set; }
            public List<DropDownlist> SubCategoryList { get; set; }
        }
    }
}
