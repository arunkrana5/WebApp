using System;

namespace DataModal.Models
{
    public class Attendence
    {
        public int RowNum { get; set; }
        public long EMPID { get; set; }
        public string Date { get; set; }
        public string EMPName { get; set; }
        public string EMPCode { get; set; }
        public long StatusID { get; set; }
        public string StatusDisplay { get; set; }
        public int Approved { get; set; }
        public string status { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string IPAddress { get; set; }
    }

    public class ApprovalAction
    {
        public string IDs { get; set; }
        public int Approved { get; set; }
        public string ApprovedRemarks { get; set; }
        public long LoginID { get; set; }
        public string IPAddress { get; set; }
        public DateTime dt { get; set; }
        public string Doctype { get; set; }


    }
}
