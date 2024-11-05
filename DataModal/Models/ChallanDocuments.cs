using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DataModal.Models
{
    public class ChallanDocuments
    {
        public class List
        {
            public int RowNum { get; set; }
            public long ChallanID { get; set; }
            public string Date { get; set; }
            public long StateID { get; set; }
            public string StateName { get; set; }
            public string ChallanType { get; set; }
            public long AttachmentID { get; set; }
            public string AttachmentPath { get; set; }
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

            public long? ChallanID { get; set; }
            [Required(ErrorMessage = "Date Can't be Blank")]
            public string Date { get; set; }
            public long? StateID { get; set; }
            [Required(ErrorMessage = "Challan Type Can't be Blank")]
            public string ChallanType { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }
            public long AttachmentID { get; set; }
            public HttpPostedFileBase Upload { get; set; }
            public List<DropDownlist> StateList { get; set; }
            public int? Priority { get; set; }

        }
    }
}
