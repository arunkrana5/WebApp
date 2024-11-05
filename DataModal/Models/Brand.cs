using System.ComponentModel.DataAnnotations;

namespace DataModal.Models
{
    public class Brand
    {
        public class List
        {
            public int RowNum { get; set; }
            public long BrandID { get; set; }
            public string BrandName { get; set; }
            public string BrandCode { get; set; }

            public string Description { get; set; }
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
            public long BrandID { get; set; }
            [Required(ErrorMessage = "Brand Name Can't be Blank")]
            public string BrandName { get; set; }
            [Required(ErrorMessage = "Brand Code Can't be Blank")]
            public string BrandCode { get; set; }

            public string Description { get; set; }
            public bool IsActive { get; set; }
            public int? Priority { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }

        }
    }
}
