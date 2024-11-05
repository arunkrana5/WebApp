namespace DataModal.Models
{
    public class Items_Import
    {
        public class List
        {
            public int RowNum { get; set; }
            public long ID { get; set; }
            public string ProductCode { get; set; }
            public string ProductTranCode { get; set; }
            public string ItemName { get; set; }
            public string ItemCode { get; set; }
            public string Remarks { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedDate { get; set; }
            public string IPAddress { get; set; }
        }
    }
}
