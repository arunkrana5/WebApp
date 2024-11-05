using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DataModal.Models
{
    public class MasterCatalogue
    {
        public class List
        {
            public int RowNum { get; set; }
            public long CatID { get; set; }
            public string ProductName { get; set; }
            public string Description { get; set; }
            public long AttachmentID { get; set; }
            public string AttachmentPath { get; set; }
            public string URL { get; set; }
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

            public long? CatID { get; set; }
            [Required(ErrorMessage = "Product Name Can't be Blank")]
            public string ProductName { get; set; }
            [Required(ErrorMessage = "Description Can't be Blank")]
            public string Description { get; set; }
            public long? AttachmentID { get; set; }
            public string AttachmentPath { get; set; }
            public string URL { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }
            public HttpPostedFileBase Upload { get; set; }
            public int? Priority { get; set; }

        }
    }
}
