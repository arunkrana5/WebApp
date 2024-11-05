using System.ComponentModel.DataAnnotations;

namespace DataModal.Models
{
    public class Bank
    {
        public long? BankID { get; set; }
        public string Doctype { get; set; }
        public long? EMPID { get; set; }
        [Required(ErrorMessage = "Bank Name Can't be Blank")]
        public string BankName { get; set; }
        [Required(ErrorMessage = "Account No Can't be Blank")]
        public string AccountNo { get; set; }
        [Required(ErrorMessage = "IFSC Code Can't be Blank")]
        public string IFSCCode { get; set; }
        [Required(ErrorMessage = "Branch Name Can't be Blank")]
        public string BranchName { get; set; }
        public long? Priority { get; set; }
        public long LoginID { get; set; }
        public string IPAddress { get; set; }

        public class List
        {
            public long? BankID { get; set; }
            public string Doctype { get; set; }
            public long? EMPID { get; set; }
            public string BankName { get; set; }
            public string AccountNo { get; set; }
            public string IFSCCode { get; set; }
            public string BranchName { get; set; }
        }
    }
}
