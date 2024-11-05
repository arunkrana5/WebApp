namespace DataModal.Models
{
    public class MastersImport
    {
        public class List
        {
            public int RowNum { get; set; }
            public long ID { get; set; }
            public string TableName { get; set; }
            public string Name { get; set; }
            public string Value { get; set; }
            public string GroupName { get; set; }
            public string Remarks { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedDate { get; set; }
            public string IPAddress { get; set; }
        }
    }
}
