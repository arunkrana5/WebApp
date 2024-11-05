using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataModal.Models
{
    public class Branch
    {
        public class List
        {
            public int RowNum { get; set; }
            public long BranchID { get; set; }
            public string BranchCode { get; set; }
            public string BranchName { get; set; }
            public long StateID { get; set; }
            public string StateName { get; set; }
            public string CityIDs { get; set; }
            public string CityNames { get; set; }
            public int? MaxEMPCount { get; set; }

            public int AllocatedEMPCount { get; set; }
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
            public long BranchID { get; set; }
            [Required(ErrorMessage = "Branch Code Can't be Blank")]
            public string BranchCode { get; set; }
            [Required(ErrorMessage = "Branch Name Can't be Blank")]
            public string BranchName { get; set; }

            [Required(ErrorMessage = "State Can't be Blank")]
            public long? StateID { get; set; }

            [Required(ErrorMessage = "City Can't be Blank")]
            public string CityIDs { get; set; }

            [Required(ErrorMessage = "Can't be Blank")]
            public int? MaxEMPCount { get; set; }
            public string StateName { get; set; }
            public bool IsActive { get; set; }
            public int? Priority { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }

            public List<DropDownlist> StateList { get; set; }
            public List<DropDownlist> CityList { get; set; }

        }
    }
}
