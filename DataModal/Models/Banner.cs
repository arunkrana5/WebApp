using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DataModal.Models
{
    public class Banner
    {
        public class List
        {
            public long BannerID { get; set; }
            public string RoleNames { get; set; }
            public string Doctype { get; set; }
            public string ImagePath { get; set; }
            public string Heading { get; set; }
            public string SubHeading { get; set; }
            public string Description { get; set; }
            public bool IsActive { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedDate { get; set; }
            public string ModifiedDate { get; set; }
            public string ModifiedBy { get; set; }
            public string IPAddress { get; set; }

        }
        public class Add
        {
            public long BannerID { get; set; }
            public string[] RoleIDs { get; set; }
            [Required(ErrorMessage = "Doctype Can't be Blank")]
            public string Doctype { get; set; }
            [Required(ErrorMessage = "Heading Can't be Blank")]
            public string Heading { get; set; }
            public string SubHeading { get; set; }

            public HttpPostedFileBase Upload { get; set; }
            public string Description { get; set; }
            public string FileName {  get; set; }
            public string ImagePath { get; set; }
            public bool IsActive { get; set; }
            public int? Priority { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }
            public List<DropDownlist> RoleList { get; set; }
            public List<DropDownlist> DoctypeList { get; set; }
        }
    }
}
