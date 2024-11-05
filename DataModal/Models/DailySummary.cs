using System.Collections.Generic;

namespace DataModal.Models
{
    public class DailySummary
    {
        public class Data
        {
            public List<MainList> MTDList { get; set; }
            public List<MainList> SaleList { get; set; }
            public List<MainList> AttendenceList { get; set; }
            public List<MainList> TotalSSRList { get; set; }
        }
        public class MainList
        {
            public long EMPID { get; set; }
            public string RegionName { get; set; }
            public string BranchName { get; set; }
            public string BranchCode { get; set; }
            public string ProductType { get; set; }
            public string DealerType { get; set; }
            public long Qty { get; set; }
            public string Status { get; set; }
        }
    }
}
