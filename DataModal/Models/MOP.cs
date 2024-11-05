using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataModal.Models
{
    public class MOP
    {
        public class List
        {
            public int RowNum { get; set; }
            public long MOPID { get; set; }

            public long EMPID { get; set; }
            public string EMPName { get; set; }
            public string EMPCode { get; set; }
            public string Date { get; set; }
            public string BrandName { get; set; }
            public string ProductName { get; set; }
            public string ProductCode { get; set; }
            public string TranName { get; set; }
            public string TranCode { get; set; }
            public string ModelNo { get; set; }
            public decimal Price { get; set; }
            public string Remarks { get; set; }
            public int Rating { get; set; }
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
            public long EMPID { get; set; }
            public long? MOPID { get; set; }

            public long TourPlanID { get; set; }
            public string Date { get; set; }
            [Required(ErrorMessage = "Brand Can't be Blank")]
            public long? BrandID { get; set; }
            [Required(ErrorMessage = "Product Can't be Blank")]
            public long? ProductID { get; set; }

            [Required(ErrorMessage = "Sub Product Can't be Blank")]
            public long? PDTranID { get; set; }

            [Required(ErrorMessage = "Model No Can't be Blank")]
            public string ModelNo { get; set; }

            [Required(ErrorMessage = "Price Can't be Blank")]
            public decimal? Price { get; set; }

            [Required(ErrorMessage = "Rating Can't be Blank")]
            public int? Rating { get; set; }
            public string Remarks { get; set; }
            public bool IsActive { get; set; }
            public int? Priority { get; set; }

            public long LoginID { get; set; }
            public string IPAddress { get; set; }

            public List<DropDownlist> BrandList { get; set; }
            public List<DropDownlist> ProductList { get; set; }
            public List<DropDownlist> ProductTranList { get; set; }
            public List<DropDownlist> RatingList { get; set; }
        }

        public class Report
        {
            public int RowNum { get; set; }
            public long MOPID { get; set; }

            public long EMPID { get; set; }
            public string EMPName { get; set; }
            public string EMPCode { get; set; }
            public string Date { get; set; }
            public string BrandName { get; set; }
            public string ProductName { get; set; }
            public string ProductCode { get; set; }
            public string TranName { get; set; }
            public string TranCode { get; set; }
            public string ModelNo { get; set; }
            public decimal Price { get; set; }
            public string Remarks { get; set; }
            public string Rating { get; set; }
            public string DealerType { get; set; }
            public string DealerCode { get; set; }
            public string DealerName { get; set; }
            public string DealerArea { get; set; }
            public string BranchCode { get; set; }
            public string BranchName { get; set; }
            public string RegionName { get; set; }
            public string CityName { get; set; }
            public string StateName { get; set; }
            public string AreaName { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedDate { get; set; }
            public string ModifiedDate { get; set; }
            public string ModifiedBy { get; set; }
            public string IPAddress { get; set; }

        }
    }
}
