using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataModal.Models
{
    public class Targets
    {
        public class List
        {
            public long TID { get; set; }
            public long EMPID { get; set; }
            public int Year { get; set; }
            public int Month { get; set; }
            public string MonthName { get; set; }
            public string EMPName { get; set; }
            public string EMPCode { get; set; }
            public string RoleName { get; set; }
            public string DeptName { get; set; }
            public string DesignName { get; set; }
            public string EmailID { get; set; }
            public string Phone { get; set; }
            public string ImageURL { get; set; }
            public string Gender { get; set; }
            public bool IsActive { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedDate { get; set; }
            public string ModifiedDate { get; set; }
            public string ModifiedBy { get; set; }
            public string IPAddress { get; set; }
        }

        public class Add
        {
            public long TID { get; set; }

            public string Doctype { get; set; }
            [Required(ErrorMessage = "Month Can't be Blank")]
            public string TargetDate { get; set; }

            [Required(ErrorMessage = "Qty Can't be Blank")]
            public long? Qty { get; set; }

            [Required(ErrorMessage = "Employee Can't be Blank")]
            public long? EMPID { get; set; }

            [Required(ErrorMessage = "Can't be Blank")]
            public long? TargetFor_ID { get; set; }

            public long LoginID { get; set; }
            public string IPAddress { get; set; }
            public List<DropDownlist> EMPList { get; set; }
            public List<DropDownlist> TargetForList { get; set; }
        }

        public class TranList
        {
            public List<TargetsTran> TargetTranList { get; set; }
            public List TargetDetails { get; set; }

        }
    }
    public class TargetsTran
    {
        public long TID { get; set; }
        public long TTID { get; set; }
        public string Doctype { get; set; }
        [Required(ErrorMessage = "Qty Can't be Blank")]
        public long? Qty { get; set; }

        public string TargetFor { get; set; }
    }

    public class TrgVsAch
    {
        public long SNo { get; set; }
        public string ProductType { get; set; }
        public float Target { get; set; }
        public float Achievement { get; set; }
        public float Percent { get; set; }
    }
}
