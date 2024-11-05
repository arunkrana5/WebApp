namespace DataModal.Models
{
    public class TargetImport
    {
        public class List
        {
            public long ID { get; set; }
            public string EMPCode { get; set; }
            public string Month { get; set; }
            public string Year { get; set; }
            public string TargetFor { get; set; }
            public string ProductCode { get; set; }
            public long Qty { get; set; }
            public string Remarks { get; set; }

        }
    }
}
