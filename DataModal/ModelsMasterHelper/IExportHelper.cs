using DataModal.Models;
using NPOI.SS.UserModel;
using System.Data;

namespace DataModal.ModelsMasterHelper
{
    public interface IExportHelper
    {
        ISheet GetWorkbookSheet(IWorkbook workbook, DataTable result, string Doctype);
        ISheet GetWorkbookSheet_ISDSummary(IWorkbook workbook, DataTable result, string Doctype);
        IWorkbook GetDataTable_Workbook_Common(DataTable result, string Doctype);
        ISheet GetWorkbookSheet_Common(IWorkbook workbook, DataTable result, string Doctype);
        IWorkbook GetDataTable_Workbook(DataTable result, string Doctype);
        IWorkbook GetSaleEntry_Workbook(Tab.Approval modal);
        IWorkbook GetTargetVsAchievement_Workbook(Tab.Approval modal);
        IWorkbook GetMTDReport_Workbook(Tab.Approval modal);
        IWorkbook GetPJPExpense_Workbook(Tab.Approval modal);
        IWorkbook GetAttendanceLog_Day_Workbook(Tab.Approval modal);
        IWorkbook GetAttendanceLog_Monthly_Workbook(Tab.Approval modal);
        IWorkbook GetAttendanceLog_Monthly_InOut_Workbook(Tab.Approval modal);
        IWorkbook GetAttendance_Final_Workbook(Tab.Approval modal);
        IWorkbook GetAttendance_Workbook(Tab.Approval modal);
        ISheet GetTLTrackerSheet(IWorkbook workbook, DataTable result, string Doctype);
        IWorkbook GetTLTracker_Workbook(Tab.Approval modal);
        IWorkbook CompetitionSummary_Workbook(Tab.Approval modal);
        IWorkbook SaleEntryWithCustomerReport_Workbook(Tab.Approval modal);
        IWorkbook GetTravel_Expenses_Workbook(Tab.Approval modal);
        IWorkbook GetTravel_Visit_Report_Workbook(Tab.Approval modal);
        IWorkbook CounterDisplayReport_Workbook(Tab.Approval modal);
        IWorkbook GetIncentiveCalculator_Workbook(Tab.Approval modal);
        IWorkbook GetDealerPerformance_Workbook(Tab.Approval modal);
        ISheet GetDealerSheet(IWorkbook workbook, DataTable result, string Doctype);
        ISheet GetEmployeeSheet(IWorkbook workbook, DataTable result, string Doctype);
        ISheet GetCommonSheet(IWorkbook workbook, DataTable result, string Doctype);
        DataSet GetBrandDisplay_Export(GetResponse Modal);
        DataSet GetOnboarding_Applications_Export(GetResponse Modal);
        ISheet GetOnboardingSheet(IWorkbook workbook, DataTable result, string Doctype);
        DataSet GetEMP_TalentPool_Export(GetResponse Modal);
        DataSet GetLogin_Users_Export(GetResponse Modal);
        ISheet GetWorkbookSheet_Shorted(IWorkbook workbook, DataTable result, string Doctype);

    }

}

