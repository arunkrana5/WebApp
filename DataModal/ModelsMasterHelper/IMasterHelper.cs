using DataModal.Models;
using System.Collections.Generic;
using System.Data;
using System.Web;
using static DataModal.Models.EMPTalentPool;

namespace DataModal.ModelsMasterHelper
{
    public interface IMasterHelper
    {
        List<Masters.List> GetMastersList(JqueryDatatableParam Modal);
        Masters.Add GetMasters(GetResponse Modal);
        PostResponse fnSetMasters(Masters.Add modal);

        List<Branch.List> GetBranchList(GetResponse Modal);
        Branch.Add GetBranch(GetResponse Modal);
        PostResponse fnSetBranch(Branch.Add modal);

        List<Department.List> GetDepartmentList(GetResponse Modal);
        Department.Add GetDepartment(GetResponse Modal);
        PostResponse fnSetDepartment(Department.Add modal);

        List<Designation.List> GetDesignationList(GetResponse Modal);
        Designation.Add GetDesignation(GetResponse Modal);
        PostResponse fnSetDesignation(Designation.Add modal);

        List<Brand.List> GetBrandList(GetResponse Modal);
        Brand.Add GetBrand(GetResponse Modal);

        PostResponse fnSetBrand(Brand.Add modal);



        List<Employee.List> GetEMPList(Tab.Approval Modal);
        Employee.Add GetEMP(GetResponse Modal);
        PostResponse fnSetEMP(Employee.Add modal);
        List<Dealer.List> GetDealerList(JqueryDatatableParam Modal);
        List<Employee.List> GetEmployeeList(JqueryDatatableParam Modal);
        List<DealerImport.List> GetDealerImportList(GetResponse Modal);
        List<DealerMappingImport.List> GetDealer_Mapping_ImportList(GetResponse Modal);
        Dealer.Add GetDealer(GetResponse Modal);
        PostResponse fnSetDealer(Dealer.Add model);
        PostResponse fnSetDealerMapping(Dealer.Add model);



        PostResponse ClearDealerImportTemp(GetResponse getResponse);
        PostResponse ClearDealerMappingImportTemp(GetResponse getResponse);
        PostResponse UploadDealerImportDetailList(GetResponse getResponse);
        PostResponse UploadDealerMappingImportDetailList(GetResponse getResponse);


        List<AttendenceStatus.List> GetAttendenceStatusList(GetResponse Modal);
        AttendenceStatus.Add GetAttendenceStatus(GetResponse Modal);
        PostResponse fnSetAttendenceStatus(AttendenceStatus.Add model);

        List<EMPImport.List> GetEMPImportList(GetResponse Modal);
        PostResponse SaveEMPImportTempDetails(DataSet TempDataset, GetResponse getResponse);
        PostResponse ClearEMPImportTemp(GetResponse getResponse);
        PostResponse SetEMPFromEMPImport(GetResponse getResponse);
        PostResponse UploadEMPImportDataExcelFile(HttpPostedFileBase file, string SheetName, GetResponse getResponse);


        List<MastersImport.List> GetMastersImportList(GetResponse Modal);
        PostResponse SaveMastersImportTempDetails(DataSet TempDataset, GetResponse getResponse);
        PostResponse ClearMastersImportTemp(GetResponse getResponse);
        PostResponse SetMastersFromMastersImport(GetResponse getResponse);
        PostResponse UploadMastersImportDataExcelFile(HttpPostedFileBase file, string SheetName, GetResponse getResponse);
        PostResponse fnSetEMP_TalentPool(EMPTalentPool.Add model);
        PostResponse fnSetBranch_Mapping(long BranchID, string LinkID, GetResponse Modal);
        List<ChallanDocuments.List> GetChallanDocumentsList(Tab.Date Modal);
        ChallanDocuments.Add GetChallanDocuments(GetResponse Modal);
        PostResponse fnSetChallanDocuments(ChallanDocuments.Add Modal);

        List<EMPTalentPoolImport.List> GetEMPTalentPoolImportList(GetResponse Modal);

        PostResponse UploadEMP_TalentPoolImportDataExcelFile(HttpPostedFileBase file, string SheetName, GetResponse getResponse);
        PostResponse SaveEMP_TalentPoolImportTempDetails(DataSet TempDataset, GetResponse getResponse);
        PostResponse ClearEMP_TalentPoolImportTemp(GetResponse getResponse);
        PostResponse SetEMP_TalentPoolFromImportTable(GetResponse getResponse);
   
        List<DealerCategory.List> GetDealerCategoryList(GetResponse Modal);
        DealerCategory.Add GetDealerCategory(GetResponse Modal);
        PostResponse fnSetDealerCategory(DealerCategory.Add modal);

        List<DealerType.List> GetDealerTypeList(GetResponse Modal);
        DealerType.Add GetDealerType(GetResponse Modal);
        PostResponse fnSetDealerType(DealerType.Add modal);

        List<MasterCatalogue.List> GetMasterCatalogueList(Tab.Approval Modal);
        MasterCatalogue.Add GetMasterCatalogue(GetResponse Modal);
        PostResponse fnSetMasterCatalogue(MasterCatalogue.Add Modal);

        PostResponse DealerImport_UploadData(HttpPostedFileBase file, GetResponse getResponse);
        PostResponse DealerMappingImport_UploadData(HttpPostedFileBase file, GetResponse getResponse);
        PostResponse EmployeeImport_UploadData(HttpPostedFileBase file, GetResponse getResponse);
        List<EMPTalentPool.List> GetEMP_TalentPoolList(JqueryDatatableParam Modal);
        List<EMPTalentPool.List> GetMyEMPTalentPoolList(Tab.Approval Modal);
        EMPTalentPool.Add GetEMPTalentPool(GetResponse Modal);
        PostResponse fnSetEMP_TalentPool_Approved(EMPTalentPool.ApprovalAction modal);
        PostResponse fnSetEMP_TalentPool_MyAdd(EMPTalentPool.MyAdd model);
        EMPTalentPool.MyAdd GetEMPTalentPool_MyAdd(GetResponse Modal);
        Filter GetEMPTalentPoolFilter(GetResponse modal);
        List<DealerRequest.List> GetDealerRequests_MyList(JqueryDatatableParam Modal);
        List<DealerRequest.List> GetDealerRequests_List(JqueryDatatableParam Modal);
        DealerRequest.Add GetDealerRequests(GetResponse Modal);
        PostResponse fnSetDealerRequests(DealerRequest.Add model);
        DealerSearch GetDealerSearchFilter(GetResponse modal);
        List<Banner.List> GetBannerList(GetResponse Modal);
        Banner.Add GetBanner(GetResponse Modal);
        PostResponse fnSetBanner(Banner.Add modal);
        ProductSalesGraph GetSalesGraphData(GetGraphResponse Modal);
        ProductSalesGraph GetSalesGrowthData(GetGraphResponse Modal);
        ProductSalesGraph GetBranchWiseSales(GetGraphResponse Modal);
        ProductSalesGraph GetISDCountGraph(GetGraphResponse Modal);
        ProductSalesGraph GetGraphFilterData(GetDropDownResponse Modal);
    }
}
