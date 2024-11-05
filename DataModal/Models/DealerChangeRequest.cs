using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModal.Models
{
    public class DealerChangeRequest
    {
        public class List
        {
            public int TotalCount { get; set; }
            public int RowNum { get; set; }
            public long ChangeID { get; set; }
            public string DocNo { get; set; }
            public string DocDate { get; set; }
            public int EMPID { get; set; }
            public string EMPCode { get; set; }
            public string EMPName { get; set; }
            public long OldDealerID { get; set; }
            public string OldDealerName { get; set; }
            public string OldDealerCode { get; set; }
            public long NewDealerID { get; set; }
            public string NewDealerName { get; set; }
            public string NewDealerCode { get; set; }
            public string ChangeDate { get;set; }
            public string Reason { get; set; }
            public string ApprovedStatus { get; set; }
            public string ApprovedRemarks { get; set; }
            public string ApprovedDate { get; set; }
            public string ApprovedBy { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedDate { get; set; }
            public string ModifiedDate { get; set; }
            public string ModifiedBy { get; set; }
            public string IPAddress { get; set; }
            public string EntrySource { get; set; }
        }
        public class Add
        {
            public long? ChangeID { get; set; }

            [Required(ErrorMessage = "Employee Can't be Blank")]
            public long? EMPID { get; set; }

            [Required(ErrorMessage = "Dealer Can't be Blank")]
            public long? NewDealerID { get; set; }

            [Required(ErrorMessage = "Reason Can't be Blank")]
            public string Reason { get; set; }
            [Required(ErrorMessage = "Change Date Can't be Blank")]
            public string ChangeDate { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }
            public long DealerTypeID { get; set; }
            public long DealerCategoryID { get; set; }
            public long RegionID { get; set; }
            public long StateID { get; set; }
            public long CityID { get; set; }
            public long AreaID { get; set; }
            public List<DropDownlist> DealerList { get; set; } = new List<DropDownlist>();
            public List<DropDownlist> EMPList { get; set; } = new List<DropDownlist>();
            public List<DropDownlist> RegionList { get; set; } = new List<DropDownlist>();
            public List<DropDownlist> StateList { get; set; } = new List<DropDownlist>();
            public List<DropDownlist> CityList { get; set; } = new List<DropDownlist>();
            public List<DropDownlist> AreaList { get; set; } = new List<DropDownlist>();
            public List<DropDownlist> DealerTypeList { get; set; } = new List<DropDownlist>();
            public List<DropDownlist> DealerCategoryList { get; set; } = new List<DropDownlist>();
        }

        public class ApprovalAction
        {
            public string ChangeIDs { get; set; }
            public int Approved { get; set; }
            public string ApprovedRemarks { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }
        }
    }
}
