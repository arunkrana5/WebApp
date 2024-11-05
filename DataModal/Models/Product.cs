using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace DataModal.Models
{
    public class Product
    {
        public class List
        {
            public long ProductID { get; set; }
            public string ProductName { get; set; }
            public string ProductCode { get; set; }

            public int RowNum { get; set; }
            public long PDTypeID { get; set; }
            public string TypeName { get; set; }
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
            public long ProductID { get; set; }
            [Required(ErrorMessage = "Name Can't be Blank")]
            public string ProductName { get; set; }
            [Required(ErrorMessage = "Code Can't be Blank")]
            public string ProductCode { get; set; }

            [Required(ErrorMessage = "Product Type Can't be Blank")]
            public long PDTypeID { get; set; }

            public bool IsActive { get; set; }
            public int? Priority { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }

            public List<DropDownlist> ProductTypeList { get; set; }
        }
    }

    public class ProductType
    {
        public class List
        {
            public int RowNum { get; set; }
            public long PDTypeID { get; set; }
            public string TypeName { get; set; }
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
            public long PDTypeID { get; set; }
            [Required(ErrorMessage = "Type Name Can't be Blank")]
            public string TypeName { get; set; }
            public string Description { get; set; }
            public bool IsActive { get; set; }
            public int? Priority { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }

        }
    }


    public class ProductTran
    {
        public class List
        {
            public int RowNum { get; set; }
            public long PDTranID { get; set; }
            public string TranCode { get; set; }
            public string TranName { get; set; }
            public string ProductName { get; set; }
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
            public long? PDTranID { get; set; }
            [Required(ErrorMessage = "Brand Code Can't be Blank")]
            public string TranCode { get; set; }
            [Required(ErrorMessage = "Brand Code Can't be Blank")]
            public string TranName { get; set; }
            public long ProductID { get; set; }
            public bool IsActive { get; set; }
            public int? Priority { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }



        }
    }
}
