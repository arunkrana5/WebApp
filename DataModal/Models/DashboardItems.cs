using System.Collections.Generic;

namespace DataModal.Models
{
    public class DashboardItems
    {
        public class ForAll
        {
            public int TotalSSR { get; set; }
            public int TotalSales_CM { get; set; }
            public int Present { get; set; }
            public int Leave { get; set; }
            public int LWP { get; set; }
            public int WO { get; set; }
            public int IAMSafe { get; set; }
            public int Absent { get; set; }
            public int Holiday { get; set; }
        }
        public class SSR
        {
            public int Present { get; set; }
            public int HD { get; set; }
            public int Leave { get; set; }
            public int LWP { get; set; }
            public int WO { get; set; }
            public int IAMSafe { get; set; }
            public int Absent { get; set; }
            public int Holiday { get; set; }
            public int TotalSales_Year { get; set; }
            public int TotalSales_CM { get; set; }
            public int TotalSales_LM { get; set; }


            public List<ProductDetails> ProductList { get; set; }



            public Attendance_Log.PunchStatus PunchStatusModal { get; set; }

        }


        public class SSRList
        {
            public string Code { get; set; }
            public string Name { get; set; }
            public string Gender { get; set; }
            public string BranchCode { get; set; }
            public string BranchName { get; set; }
            public string DealerName { get; set; }
            public string DealerCode { get; set; }
            public string Area { get; set; }
            public string Title { get; set; }
            public long StatusID { get; set; }
        }




        public class ProductDetails
        {
            public long ProductID { get; set; }
            public string ProductName { get; set; }
            public string ProductCode { get; set; }
            public int TotalSale { get; set; }

        }


        public class HeaderCount
        {
            public int StatusID { get; set; }
            public int LeaveTypeID { get; set; }
            public string Name { get; set; }
            public long Count { get; set; }
            public int Priority { get; set; }
            public string Doctype { get; set; }
            public string UserType { get; set; }
        }

    }


}
