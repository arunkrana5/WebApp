using SixLabors.ImageSharp.ColorSpaces.Companding;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataModal.Models
{
    public class Dealer
    {
        public class List
        {
            public int RowNum { get; set; }

            public long DealerID { get; set; }
            public string DealerCode { get; set; }
            public string DealerName { get; set; }
            public string EmailID { get; set; }
            public string Phone { get; set; }
            public string DealerType { get; set; }
            public string DealerCategory { get; set; }
            public string StateName { get; set; }
            public string BranchName { get; set; }
            public string BranchCode { get; set; }
            public string CityName { get; set; }
            public string AreaName { get; set; }
            public string Address { get; set; }
            public string RegionName { get; set; }
            public string BillingCode { get; set; }
            public string BillingName { get; set; }
            public string PinCode { get; set; }

            public string Latitude { get; set; }
            public string Longitude { get; set; }

            public bool IsActive { get; set; }
            public int? Priority { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedDate { get; set; }
            public string ModifiedDate { get; set; }
            public string ModifiedBy { get; set; }
            public string IPAddress { get; set; }
            public string RouteNumber { get; set; }
            public string VisitType { get; set; }

            public int TotalCount { get; set; }
        }
        public class Add
        {

            public long? DealerID { get; set; }
            public string DealerCode { get; set; }
            [Required(ErrorMessage = "Name Can't be Blank")]
            public string DealerName { get; set; }
            [EmailAddress]
            public string EmailID { get; set; }

            [DataType(DataType.PhoneNumber)]
            [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
            public string Phone { get; set; }

            [Required(ErrorMessage = "Branch Can't be Blank")]
            public long? BranchID { get; set; }
            [Required(ErrorMessage = "State Can't be Blank")]
            public long? StateID { get; set; }
            [Required(ErrorMessage = "City Can't be Blank")]
            public long? CityID { get; set; }
            [Required(ErrorMessage = "Area Can't be Blank")]
            public long? AreaID { get; set; }
            [Required(ErrorMessage = "Region Can't be Blank")]
            public long? RegionID { get; set; }

            [Required(ErrorMessage = "Address Can't be Blank")]
            public string Address { get; set; }

            [Required(ErrorMessage = "PinCode is Required")]
            [RegularExpression(@"^\d{6}(-\d{4})?$", ErrorMessage = "Invalid PinCode")]
            public string PinCode { get; set; }
            [Required(ErrorMessage = "Latitude Can't be Blank")]
            public string Latitude { get; set; }
            [Required(ErrorMessage = "Longitude Can't be Blank")]
            public string Longitude { get; set; }

            [Required(ErrorMessage = "Billing Code Can't be Blank")]
            public string BillingCode { get; set; }
            [Required(ErrorMessage = "Billing Name Can't be Blank")]
            public string BillingName { get; set; }

            [Required(ErrorMessage = "Dealer Type Can't be Blank")]
            public long? DealerTypeID { get; set; }
            public long? DealerCategoryID { get; set; }
            public bool IsActive { get; set; }
            public int? Priority { get; set; }

            public long LoginID { get; set; }
            public string IPAddress { get; set; }
            public string NSM { get; set; }
            public string RSM { get; set; }
            public string BSM { get; set; }
            public string ASM { get; set; }
            public string TL { get; set; }
            public string RMM { get; set; }
            public string BMM { get; set; }
            public string Inhouse { get; set; }
            public string Others { get; set; }

            public List<DropDownlist> RegionList { get; set; }
            public List<DropDownlist> StateList { get; set; }
            public List<DropDownlist> BranchList { get; set; }

            public List<DropDownlist> CityList { get; set; }
            public List<DropDownlist> AreaList { get; set; }

            public List<DropDownlist> DealerCategoryList { get; set; }
            public List<DropDownlist> DealerTypeList { get; set; }

            public List<DropDownlist> NSMList { get; set; }
            public List<DropDownlist> RSMList { get; set; }
            public List<DropDownlist> BSMList { get; set; }
            public List<DropDownlist> ASMList { get; set; }
            public List<DropDownlist> TLList { get; set; }

            public List<DropDownlist> BMMList { get; set; }
            public List<DropDownlist> RMMList { get; set; }

            public List<DropDownlist> InhouseList { get; set; }
            public List<DropDownlist> OtherList { get; set; }
            public string RouteNumber { get; set; }
            public string VisitType { get; set; }
            public List<DropDownlist> VisitTypeList { get; set; }
        }
    }

    public class DealerCategory
    {
        public class List
        {
            public int RowNum { get; set; }
            public long DealerCategoryID { get; set; }
            public string Name { get; set; }
            public string Code { get; set; }

            public bool IsActive { get; set; }
            public int Priority { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedDate { get; set; }
            public string ModifiedDate { get; set; }
            public string ModifiedBy { get; set; }
            public string IPAddress { get; set; }
        }
        public class Add
        {
            public long DealerCategoryID { get; set; }

            [Required(ErrorMessage = "Name Can't be Blank")]
            public string Name { get; set; }
            [Required(ErrorMessage = "Code Can't be Blank")]
            public string Code { get; set; }
            public bool IsActive { get; set; }
            public int? Priority { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }

        }
    }

    public class DealerType
    {
        public class List
        {
            public int RowNum { get; set; }
            public long DealerTypeID { get; set; }
            public string Name { get; set; }
            public string Code { get; set; }

            public bool IsActive { get; set; }
            public int Priority { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedDate { get; set; }
            public string ModifiedDate { get; set; }
            public string ModifiedBy { get; set; }
            public string IPAddress { get; set; }
        }
        public class Add
        {
            public long DealerTypeID { get; set; }

            [Required(ErrorMessage = "Name Can't be Blank")]
            public string Name { get; set; }
            [Required(ErrorMessage = "Code Can't be Blank")]
            public string Code { get; set; }
            public bool IsActive { get; set; }
            public int? Priority { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }

        }
    }


    public class DealerSearch
    {
        public string DealerCode { get; set; }
        public string DealerName { get; set; }
        public long DealerTypeID  { get; set;}
        public long DealerCategoryID { get; set; }
        public long RegionID { get; set; }
        public long StateID { get; set; }
        public long BranchID { get; set; }
        public long CityID { get; set; }
        public long AreaID {  get; set; }
        public string PinCode { get; set; }
        public long LoginID { get; set; }
        public string IPAddress { get; set; }
        public List<DropDownlist> RegionList { get; set; } = new List<DropDownlist>();
        public List<DropDownlist> BranchList { get; set; } = new List<DropDownlist>();
        public List<DropDownlist> StateList { get; set; } = new List<DropDownlist>();
        public List<DropDownlist> CityList { get; set; } = new List<DropDownlist>();
        public List<DropDownlist> AreaList { get; set; } = new List<DropDownlist>();
        public List<DropDownlist> DealerTypeList { get; set; } = new List<DropDownlist>();
        public List<DropDownlist> DealerCategoryList { get; set; } = new List<DropDownlist>();
    }
    public class DealerSearchNew
    {
        public string DealerCode { get; set; }
        public string DealerName { get; set; }
        public long LoginID { get; set; }
        public string IPAddress { get; set; }
        public string RouteNumber { get; set; }
        public List<DropDownlist> RouteNumberList { get; set; } = new List<DropDownlist>();
    }
}
