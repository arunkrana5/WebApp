using System;

namespace DataModal.Models
{
    public class AttendenceChangeLog
    {
        public int TotalCount { get; set; }
        public int RowNum { get; set; }
        public long EMPID { get; set; }
        public string EMPName { get; set; }
        public string EMPCode { get; set; }
        public string Date { get; set; }
        public string FinalAttendance { get; set; }
        public string AttStatus { get; set; }
        public string ApprovedBy { get; set; }
        public string ApprovedDate { get; set; }
        public string ApprovedRemarks { get; set; }
        public string RFC { get; set; }
        public string OLDRFCStatus { get; set; }
        public string NewRFCStatus { get; set; }
        public string Reason { get; set; }
        public string RFCApprovedBy { get; set; }
        public string RFCApprovedDate { get; set; }
        public string History { get; set; }
        public string ChangeMadeBy { get; set; }
        public string ChangeDate { get; set; }
        public long StatusID { get; set; }
        public string StatusDisplay { get; set; }
        public int Approved { get; set; }
        public string status { get; set; }
        public string RFCStatus { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string IPAddress { get; set; }
    } 
}
