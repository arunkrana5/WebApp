using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataModal.Models
{
    public class Announcement
    {
        public class List
        {
            public int RowNum { get; set; }
            public long ID { get; set; }
            public string Heading { get; set; }
            public string Message { get; set; }

            public string StartDate { get; set; }

            public string EndDate { get; set; }
            public string Roles { get; set; }
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
            public long? ID { get; set; }
            [Required(ErrorMessage = "Heading  Can't be Blank")]
            public string Heading { get; set; }
            [Required(ErrorMessage = "Message Can't be Blank")]
            public string Message { get; set; }

            [Required(ErrorMessage = "Start Date Can't be Blank")]
            public string StartDate { get; set; }

            [Required(ErrorMessage = "End Date Can't be Blank")]
            public string EndDate { get; set; }

            [Required(ErrorMessage = "Role Can't be Blank")]
            public string Roles { get; set; }
            public bool IsActive { get; set; }
            public int? Priority { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }

            public List<DropDownlist> RoleList { get; set; }

        }

        public class My
        {
            public long ID { get; set; }
            public string Heading { get; set; }
            public string Message { get; set; }
        }
    }
}
