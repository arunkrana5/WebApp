using System.ComponentModel.DataAnnotations;

namespace DataModal.Models
{
    public class ContactUs_Query
    {
        public class List
        {
            public int RowNum { get; set; }
            public long ID { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string DocNo { get; set; }
            public string DocDate { get; set; }
            public string Category { get; set; }
            public string Subject { get; set; }
            public string Description { get; set; }
            public long MasterStatusID { get; set; }
            public string Status { get; set; }
            public string Remarks { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedDate { get; set; }
            public string ModifiedDate { get; set; }
            public string ModifiedBy { get; set; }
            public string IPAddress { get; set; }
        }

        public class AddQuery
        {
            public long? ID { get; set; }
            [Required(ErrorMessage = "Can't be Blank")]
            public string Name { get; set; }
            [Required(ErrorMessage = "Can't be Blank")]
            public string Email { get; set; }
            [Required(ErrorMessage = "Can't be Blank")]
            public string Phone { get; set; }
            [Required(ErrorMessage = "Can't be Blank")]
            public string Category { get; set; }
            [Required(ErrorMessage = "Can't be Blank")]
            public string Subject { get; set; }
            [Required(ErrorMessage = "Can't be Blank")]
            public string Description { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }

        }

        public class Approve
        {
            public long ID { get; set; }
            public long? MasterStatusID { get; set; }
            public string Remarks { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }
        }
    }
}
