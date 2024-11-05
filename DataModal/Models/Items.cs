using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataModal.Models
{
    public class Items
    {
        public class List
        {
            public int RowNum { get; set; }
            public long ItemID { get; set; }
            public string ItemCode { get; set; }
            public string ItemName { get; set; }
            public string ProductName { get; set; }
            public string ProductCode { get; set; }
            public string TranName { get; set; }
            public string TranCode { get; set; }
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
            public long? ItemID { get; set; }

            [Required(ErrorMessage = "Product Can't be Blank")]
            public long? ProductID { get; set; }

            [Required(ErrorMessage = "Product Tran Can't be Blank")]
            public long? PDTranID { get; set; }

            [Required(ErrorMessage = "Code Can't be Blank")]
            public string ItemCode { get; set; }
            [Required(ErrorMessage = "Name Can't be Blank")]
            public string ItemName { get; set; }
            public bool IsActive { get; set; }
            public int? Priority { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }

            public List<DropDownlist> ProductList { get; set; }
            public List<DropDownlist> ProductTranList { get; set; }
            public List<IncentiveRules> IncentiveRulesList { get; set; }

        }

        public class IncentiveRules
        {

            public int? Qty_From { get; set; }

            public int? Qty_To { get; set; }

            public decimal? Price { get; set; }
            public string Description { get; set; }
        }
    }
}
