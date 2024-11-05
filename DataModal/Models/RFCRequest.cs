using System.Collections.Generic;

namespace DataModal.Models
{
    public class RFCRequest
    {
        public class List
        {
            public long? RFCID { get; set; }
            public long? EMPID { get; set; }
            public string EMPName { get; set; }
            public string EMPCode { get; set; }
            public string DocDate { get; set; }
            public string RFCDate { get; set; }
            public long LogID { get; set; }
            public long Log_StatusID { get; set; }
            public string Log_StatusName { get; set; }
            public long RFC_StatusID { get; set; }
            public string RFC_StatusName { get; set; }
            public string Reason { get; set; }
            public bool IsActive { get; set; }
            public int Priority { get; set; }
            public int Approved { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedDate { get; set; }
            public string ModifiedDate { get; set; }
            public string ModifiedBy { get; set; }
            public string IPAddress { get; set; }
        }
        public class Add
        {
            public long? RFCID { get; set; }
            public long? EMPID { get; set; }
            public string DocDate { get; set; }
            public string RFCDate { get; set; }
            public long LogID { get; set; }
            public long RFC_StatusID { get; set; }
            public string Reason { get; set; }
            public bool IsActive { get; set; }
            public int? Priority { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }

            public List<DropDownlist> AttendenceLogList { get; set; }
            public List<DropDownlist> AttendenceStatusList { get; set; }
        }
    }

    public class RFCApproval
    {
        public class List
        {
            public long? RFCID { get; set; }
            public long? EMPID { get; set; }
            public string EMPName { get; set; }
            public string EMPCode { get; set; }
            public string DocDate { get; set; }
            public string RFCDate { get; set; }
            public string status { get; set; }
            public string Log_StatusName { get; set; }

            public string RFC_StatusName { get; set; }
            public string Reason { get; set; }
            public int Approved { get; set; }
            public string ApprovedRemarks { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedDate { get; set; }
            public string ModifiedDate { get; set; }
            public string ModifiedBy { get; set; }
            public string IPAddress { get; set; }
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
