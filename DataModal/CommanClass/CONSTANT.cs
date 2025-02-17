using System.Collections.Generic;

namespace DataModal.CommanClass
{
    public static class CONSTANT
    {
        public static IList<string> DealerImport_Column = new List<string>
        {
            "DealerCode", "DealerName", "Region", "State", "Branch", "City", "Area", "PinCode", "Address", "Email", "Phone",
            "DealerType", "DealerCategory", "Latitude", "Longitude", "BillingCode", "BillingName","RouteNumber","VisitType","IsHiringOpen"
        };
        public static IList<string> DealerMappingImport_Column = new List<string>
        {
            "DealerCode","NSM", "RSM", "BSM", "ASM", "TL", "BMM", "RMM","Inhouse","Others"
        };

        public static IList<string> EmplyeeImport_Column = new List<string>
        {
            "EMPCode","EMPName","Phone","EmailID","FatherName","DOB","Gender","Design","Depart","DOJ","PAN","UAN","ESIC","PaymentMode","Country","Region","State",
            "City","Address1","Address2","Location","Zipcode","BankName","AccountNo","IFSCCode","BankBranch","DealerCode","UserID","Password","RoleName"
        };
        public static IList<string> LeaveBalanceImport_Column = new List<string>
        {
            "EMPCode", "LeaveBalance", "Month", "Year"
        };
    }
}
