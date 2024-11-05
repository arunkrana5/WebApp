using DataModal.Models;
using System.Collections.Generic;
using System.Data;
using System.Web;

namespace DataModal.ModelsMasterHelper
{
    public interface ITransactionHelper
    {

        List<PJPPlan.List> GetPJPPlanList(Tab.Approval Modal);
        PJPPlan.Add GetPJPPlan(GetResponse Modal);
        PostResponse fnSetPJPPlan(PJPPlan.Add modal);
        List<SaleEntry.List> GetSaleEntryList(Tab.Approval Modal);
        SaleEntry.Add GetSaleEntry(GetResponse Modal);
        PostResponse fnSetSaleEntry(SaleEntry.Add model);

        List<CounterDisplay.List> GetCounterDisplayList(Tab.Date Modal);
        CounterDisplay.Add GetCounterDisplay(GetResponse Modal);
        PostResponse fnSetCounterDisplay(CounterDisplay.Add model);


        List<MOP.List> GetMOPList(Tab.Date Modal);
        MOP.Add GetMOP(GetResponse Modal);
        PostResponse fnSetMOP(MOP.Add model);
        List<RFCEntry.List> GetRFCEntryList(Tab.Approval Modal);
        RFCEntry.Add GetRFCEntry(GetResponse Modal);
        PostResponse fnSetRFCEntry(RFCEntry.Add model);

        List<CompetitionEntry.List> GetCompetitionEntryList(Tab.Date Modal);
        CompetitionEntry.Add GetCompetitionEntry(GetResponse Modal);
        PostResponse fnSetCompetitionEntry(CompetitionEntry.Add model);

        List<TargetImport.List> GetTargets_ImportList(GetResponse Modal);
        PostResponse UploadEMPTargetImportExcelFile(HttpPostedFileBase file, string SheetName, GetResponse getResponse);
        PostResponse SetTargets_Import(DataSet TempDataset, GetResponse getResponse);
        PostResponse ClearEMPTargetImportTemp(GetResponse getResponse);
        PostResponse SetTarget_FromTargetImport(GetResponse getResponse);
        List<PJPPlan.List> GetMyPJPPlanList(Tab.Approval Modal);
        List<PJPEntry.List> GetPlanWisePJPEntryList(GetResponse Modal);
        PJPEntry.Add GetPJPEntryAdd(GetResponse Modal, long PJPPlanID, long PJPEntryID, long SSRTourPlanID);
        PostResponse fnSetPJPEntry(PJPEntry.Add model);
        PostResponse fnSetPJPEntryWithNoSSR(PJPEntry.AddWithNoSSR model);
        PostResponse fnSetPJPEntry_Brand(PJPEntry_Brand.List model);


        List<SaleEntry_Import.List> GetSaleEntry_ImportList(GetResponse Modal);
        PostResponse SetSaleEntry_Import(DataSet TempDataset, GetResponse getResponse);
        PostResponse UploadSaleEntryImportExcelFile(HttpPostedFileBase file, string SheetName, GetResponse getResponse);
        PostResponse ClearSaleEntryImportTemp(GetResponse getResponse);
        PostResponse SetSaleEntryFromSaleEntryImport(GetResponse getResponse);


        List<PJPPlan_Import.List> GetPJPPlan_ImportList(GetResponse Modal);
        PostResponse SetPJPPlan_Import(DataSet TempDataset, GetResponse getResponse);
        PostResponse UploadPJPPlanImportExcelFile(HttpPostedFileBase file, string SheetName, GetResponse getResponse);
        PostResponse ClearPJPPlanImportTemp(GetResponse getResponse);
        PostResponse SetPJPPlanFromPJPPlanImport(GetResponse getResponse);
        DataSet GetMyPJPEntryList(Tab.Approval Modal);
        List<Targets.List> GetTargets_List(Tab.Approval Modal);
        Targets.Add GetTargets_Add(GetResponse Modal);
        PostResponse fnSetTarget(Targets.Add model);
        Targets.TranList GetTargetsTran_List(GetResponse Modal);

        DataSet GetTourPlan_History(GetResponse Modal);

        List<BrandDisplay.List> GetBrandDisplayList(Tab.Date Modal);
        BrandDisplay.Add GetBrandDisplay(GetResponse Modal);
        PostResponse fnSetBrandDisplay(BrandDisplay.Add model);

        PJPPlan.Add GetPJPPlans(GetResponse Modal);
        PostResponse fnSetPJPPlans(PJPPlan.Add modal);
        List<PJPPlan.List> GetMyPJPPlanLists(Tab.Approval Modal);
        List<PJPEntries.List> GetPlanWisePJPEntriesList(GetResponse Modal);
        PJPEntries.Add GetPJPEntriesAdd(GetResponse Modal, long PJPPlanID, long PJPEntryID, long SSRTourPlanID);
        PostResponse fnSetPJPEntries(PJPEntries.Add model);
        PostResponse fnSetPJPEntriesWithNoSSR(PJPEntries.AddWithNoSSR model);
        PostResponse fnSetPJPEntries_Brand(PJPEntry_Brand.List model);
    }
}
