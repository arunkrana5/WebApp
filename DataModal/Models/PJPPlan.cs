using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataModal.Models
{
    public class PJPPlan
    {
        public class List
        {
            public int RowNum { get; set; }
            public long PJPID { get; set; }
            public string DocNo { get; set; }
            public string DocDate { get; set; }
            public string DealerName { get; set; }
            public string DealerCode { get; set; }
            public string DealerArea { get; set; }

            public string DealerAddress { get; set; }
            public string EMPName { get; set; }
            public string EMPCode { get; set; }
            public string VisitDate { get; set; }
            public bool IsActive { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedDate { get; set; }
            public string ModifiedDate { get; set; }
            public string ModifiedBy { get; set; }
            public string IPAddress { get; set; }
            public string RouteNumber { get; set; }
            public string VisitType { get; set; }
            public string Status { get; set; }
        }
        public class Add
        {
            public int? OnDemand { get; set; }
            public long? PJPID { get; set; }

            [Required(ErrorMessage = "Dealer Can't be Blank")]
            public long?[] DealerID { get; set; }

            [Required(ErrorMessage = "EMP Can't be Blank")]
            public long? EMPID { get; set; }

            [Required(ErrorMessage = "Visit Date Can't be Blank")]
            public string VisitDate { get; set; }
            public int? Priority { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }

            public string DocNo { get; set; }
            public string DocDate { get; set; }
            public List<DropDownlist> DealerList { get; set; }
            public List<DropDownlist> EMPList { get; set; }
            public long DealerTypeID { get; set; }
            public long DealerCategoryID { get; set; }
            public long RegionID { get; set; }
            public long StateID { get; set; }
            public long CityID { get; set; }
            public long? AreaID { get; set; } 
            public List<DropDownlist> RegionList { get; set; } = new List<DropDownlist>();
            public List<DropDownlist> StateList { get; set; } = new List<DropDownlist>();
            public List<DropDownlist> CityList { get; set; } = new List<DropDownlist>();
            public List<DropDownlist> AreaList { get; set; } = new List<DropDownlist>();
            public List<DropDownlist> DealerTypeList { get; set; } = new List<DropDownlist>();
            public List<DropDownlist> DealerCategoryList { get; set; } = new List<DropDownlist>();
            [Required(ErrorMessage = "Dealer Can't be Blank")]
            public long?[] NewDealerID { get; set; }
           
            public string RouteNumber { get; set; }
            public List<DropDownlist> RouteNumberList { get; set; } = new List<DropDownlist>();
        }

        public class ApprovalList
        {
            public int RowNum { get; set; }
            public long PJPID { get; set; }
            public string DocNo { get; set; }
            public string DocDate { get; set; }
            public string DealerName { get; set; }
            public string DealerCode { get; set; }
            public string DealerArea { get; set; }
            public string DealerAddress { get; set; }
            public string EMPName { get; set; }
            public string EMPCode { get; set; }
            public string VisitDate { get; set; }
            public bool IsActive { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedDate { get; set; }
            public string ModifiedDate { get; set; }
            public string ModifiedBy { get; set; }
            public string IPAddress { get; set; }
            public string RouteNumber { get; set; }
            public string VisitType { get; set; }
            public string Status { get; set; }
            public string ApprovedRemarks { get; set; }
            public int Approved { get; set; }
        }
        public class ApprovalAction
        {
            public string PJPIDs { get; set; }
            public int Approved { get; set; }
            public string ApprovedRemarks { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }
        }
    }
}
