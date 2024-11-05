namespace DataModal.Models
{
    public class AbsentTracker
    {
        public long? LoginID { get; set; }
        public string EmailID { get; set; }
        public string EMPCode { get; set; }
        public string EMPName { get; set; }
        public string UserID { get; set; }
        public string DeptName { get; set; }
        public string ImageURL { get; set; }
        public string DOB { get; set; }
        public string DOJ { get; set; }
        public string UserType { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public int AbsentDays { get; set; }
        public long StatusID { get; set; }
        public string CurrentStatus { get; set; }
    }
}
