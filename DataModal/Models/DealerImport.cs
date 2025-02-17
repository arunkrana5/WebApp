namespace DataModal.Models
{
    public class DealerImport
    {
        public class List
        {
            public int RowNum { get; set; }
            public long DealerImportID { get; set; }
            public string DealerCode { get; set; }
            public string DealerName { get; set; }
            public string Region { get; set; }
            public string State { get; set; }
            public string Branch { get; set; }
            public string City { get; set; }
            public string Area { get; set; }
            public string PinCode { get; set; }
            public string Address { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string DealerType { get; set; }
            public string Latitude { get; set; }
            public string Longitude { get; set; }
            public string BillingCode { get; set; }
            public string BillingName { get; set; }
            public string Remarks { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedDate { get; set; }
            public string IPAddress { get; set; }
            public string RouteNumber { get; set; }
            public string VisitType { get; set; }
            public string IsHiringOpen { get; set; }
        }
    }

    public class DealerMappingImport
    {
        public class List
        {
            public string DealerCode { get; set; }
            public string NSM { get; set; }
            public string RSM { get; set; }
            public string BSM { get; set; }
            public string ASM { get; set; }
            public string TL { get; set; }
            public string BMM { get; set; }
            public string RMM { get; set; }
            public string Inhouse { get; set; }
            public string Others { get; set; }
            public string Remarks { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedDate { get; set; }
            public string IPAddress { get; set; }
        }
    }
}
