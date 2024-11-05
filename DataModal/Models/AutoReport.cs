namespace DataModal.Models
{
    public class AutoReport
    {
        public class Log
        {
            public long ID { get; set; }
            public long LoginID { get; set; }
            public string UserType { get; set; }
            public string EMPName { get; set; }
            public int MailCount { get; set; }
            public string Date { get; set; }
            public string MailStatus { get; set; }
            public string EmailID { get; set; }
            public string Error { get; set; }
            public int MailFlag { get; set; }
            public string StatusCode { get; set; }
            public string ModifiedDate { get; set; }
            public string IPAddress { get; set; }
        }

        public class User
        {
            public long LoginID { get; set; }
            public string UserType { get; set; }
            public string EMPName { get; set; }
            public string EmailID { get; set; }
            public string UserID { get; set; }
        }
    }
}
