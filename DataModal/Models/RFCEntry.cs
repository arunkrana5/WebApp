using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataModal.Models
{
    public class RFCEntry
    {
        public class List
        {
            public long ID { get; set; }
            public long? EMPID { get; set; }
            public string EMPName { get; set; }
            public string EMPCode { get; set; }
            public string DocNo { get; set; }
            public string Category { get; set; }
            public string DocDate { get; set; }
            public string AttendenceDate { get; set; }
            public long Old_StatusID { get; set; }
            public long New_StatusID { get; set; }
            public string OldStatus { get; set; }
            public string NewStatus { get; set; }

            public string DealerName { get; set; }
            public string DealerCode { get; set; }
            public string AreaName { get; set; }
            public string DealerAddress { get; set; }
            public string BranchCode { get; set; }
            public string BranchName { get; set; }
            public string Reason { get; set; }
            public int Approved { get; set; }
            public string Status { get; set; }
            public string ApprovedRemarks { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedDate { get; set; }
            public string ModifiedDate { get; set; }
            public string ModifiedBy { get; set; }
            public string IPAddress { get; set; }
        }
        public class Add
        {
            public long? ID { get; set; }
            public long? EMPID { get; set; }
            public string DocNo { get; set; }
            public string DocDate { get; set; }
            [Required(ErrorMessage = "Category Can't be Blank")]
            public string Category { get; set; }
            public string AttendenceDate { get; set; }

            [Required(ErrorMessage = "Old Status Can't be Blank")]
            public long Old_StatusID { get; set; }
            [Required(ErrorMessage = "New Status Can't be Blank")]
            public long New_StatusID { get; set; }
            [Required(ErrorMessage = "Reason Can't be Blank")]
            public string Reason { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }
            public List<DropDownlist> OldList { get; set; }
            public List<DropDownlist> NewStatusList { get; set; }
            public List<DropDownlist> RFCCategoryList { get; set; }
        }

        public class Action
        {
            public string RFCIDs { get; set; }
            public int Approved { get; set; }
            public string ApprovedRemarks { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }
        }


    }
}
