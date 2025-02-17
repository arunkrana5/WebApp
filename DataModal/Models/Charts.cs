using System.Collections.Generic;

namespace DataModal.Models
{
    public class Charts
    {
        public List<SalesGraph> ChartsList { get; set; }

    }
    public class SalesGraph
    {
        public string ProductCode { get; set; }
        public int Jan { get; set; }
        public int Feb { get; set; }
        public int Mar { get; set; }
        public int Apr { get; set; }
        public int May { get; set; }
        public int Jun { get; set; }
        public int Jul { get; set; }
        public int Aug { get; set; }
        public int Sep { get; set; }
        public int Oct { get; set; }
        public int Nov { get; set; }
        public int Dec { get; set; }
        public string colors { get; set; }
    }

    public class ProductSalesGraph
    {
        public string ProductName { get; set; }
        public int Qty { get; set; }
        public string GraphColor { get; set; }
        public List<ProductSalesGraph> ProductSalesList { get; set; }
        public int RegionID { get; set; }
        public int BranchID { get; set; }
        public List<DropDownlist> RegionList { get; set; }
        public List<DropDownlist> BranchList { get; set; }
        public string InputDate { get; set; }
        public List<SalesGrowthGraph> SalesGrowthList { get; set; }
        public List<BranchWiseSalesGraph> BranchWiseSalesList { get; set; }
        public List<ISDCountGraph> ISDCountGraphList { get; set; }
    }
    public class SalesGrowthGraph
    {
        public string ProductName { get; set; }
        public string GraphColor { get; set; }
        public int Year { get; set; }
        public int Qty { get; set; } 
        
    }
    public class BranchWiseSalesGraph
    {
        public string Branch { get; set; }
        public int Qty { get; set; }
        public int Year { get; set; }

    }
    public class ISDCountGraph
    {
        public string Branch { get; set; }
        public int Qty { get; set; }
        public int Year { get; set; }

    }
}
