using System.ComponentModel.DataAnnotations;

namespace DataModal.Models
{
    public class LeaveType
    {
        public class List
        {
            public int RowNum { get; set; }
            public long LeaveTypeID { get; set; }
            public string DisplayName { get; set; }
            public string Icon { get; set; }
            public string Color { get; set; }
            public float MonthlyAccrued { get; set; }
            public bool IsActive { get; set; }
            public bool ShowInDashboard { get; set; }
            public int Priority { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedDate { get; set; }
            public string ModifiedDate { get; set; }
            public string ModifiedBy { get; set; }
            public string IPAddress { get; set; }
        }

        public class Add
        {
            public long? LeaveTypeID { get; set; }
            [Required(ErrorMessage = "Display Name Can't be Blank")]
            public string DisplayName { get; set; }
            public string Icon { get; set; }
            public string Color { get; set; }
            public float? MonthlyAccrued { get; set; }
            public bool IsActive { get; set; }
            public int? Priority { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }

        }
    }
}
