using System;
using System.Collections.Generic;

namespace DataModal.Models
{
    public class Attendance_Log
    {
        public class Daily
        {
            public int RowNum { get; set; }
            public long EMPID { get; set; }
            public string EMPCode { get; set; }
            public string EMPName { get; set; }


            public string Gender { get; set; }
            public string Phone { get; set; }
            public string DOJ { get; set; }
            public string Role { get; set; }
            public string Status { get; set; }
            public string DesignName { get; set; }
            public string DeptName { get; set; }
            public string Region { get; set; }
            public string State { get; set; }
            public string City { get; set; }
            public string Area { get; set; }
            public string DealerCode { get; set; }
            public string DealerName { get; set; }
            public string DealerType { get; set; }
            public string DealerArea { get; set; }
            public string BranchName { get; set; }
            public string BranchCode { get; set; }
            public string EMPImageURL { get; set; }
            public string Title { get; set; }
            public TimeSpan Working { get; set; }
            public string In_Image { get; set; }
            public TimeSpan In_Time { get; set; }
            public string In_Notes { get; set; }
            public string In_PunchDistance { get; set; }

            public string Out_Image { get; set; }
            public string In_Image_Physical { get; set; }
            public string Out_Image_Physical { get; set; }
            public TimeSpan Out_Time { get; set; }
            public string Out_Notes { get; set; }
            public string Out_PunchDistance { get; set; }
            public string EntrySource { get; set; }
        }

        public class PunchStatus
        {
            public string Title { get; set; }
            public string Date { get; set; }
            public TimeSpan InTime { get; set; }
            public TimeSpan OutTime { get; set; }
            public string Working { get; set; }
            public string InStatus { get; set; }
            public string OutStatus { get; set; }

        }


        public class Monthly
        {
            public long EMPID { get; set; }
            public string EMPImageURL { get; set; }
            public string EMPCode { get; set; }
            public string EMPName { get; set; }
            public string Phone { get; set; }
            public string DesignName { get; set; }
            public string DOJ { get; set; }
            public string DeptName { get; set; }
            public string Region { get; set; }
            public string Gender { get; set; }
            public string State { get; set; }
            public string City { get; set; }
            public string Area { get; set; }
            public string DealerCode { get; set; }
            public string DealerName { get; set; }
            public string DealerType { get; set; }
            public string DealerArea { get; set; }
            public string BranchName { get; set; }
            public string BranchCode { get; set; }
            public string Role { get; set; }
            public string Status { get; set; }
            public string EntrySource { get; set; }

            public Dictionary<string, string> Days { get; set; }
        }


        public class Monthly_INOUT
        {
            public long EMPID { get; set; }
            public string EMPImageURL { get; set; }
            public string EMPCode { get; set; }
            public string EMPName { get; set; }
            public string Phone { get; set; }
            public string DesignName { get; set; }
            public string DeptName { get; set; }
            public string Region { get; set; }
            public string Gender { get; set; }
            public string State { get; set; }
            public string City { get; set; }
            public string Area { get; set; }
            public string DealerCode { get; set; }
            public string DealerName { get; set; }
            public string DealerType { get; set; }
            public string DealerArea { get; set; }
            public string BranchName { get; set; }
            public string BranchCode { get; set; }
            public string Role { get; set; }
            public string DOJ { get; set; }
            public string Status { get; set; }

            public Dictionary<string, string> Days { get; set; }
        }


    }
}
