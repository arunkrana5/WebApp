using DataModal.Models;
using System.Collections.Generic;

namespace DataModal.ModelsMasterHelper
{
    public interface IApprovalsHelper
    {
        List<RFCEntry.List> GetRFCApprovalList(Tab.Approval Modal);
        PostResponse fnSetRFCApproved(RFCEntry.Action modal);
        List<SaleEntry.ApprovalList> GetSaleEntryApprovalList(Tab.Approval Modal);
        PostResponse fnSetSaleEntry_Approved(SaleEntry.ApprovalAction modal);
        PostResponse fnSetAttendenceApproved(ApprovalAction modal);
        List<PJPExpense.List> GetPJPEntry_Expense_ApprovalList(Tab.Approval Modal);
        PostResponse fnSetPJPEntry_Approved(ApprovalAction modal);
        List<PJPPlan.ApprovalList> GetPJPPlanApprovalList(Tab.Approval Modal);

        PostResponse fnSetPJPPlan_Approved(PJPPlan.ApprovalAction modal);

        List<PJPExpenses.List> GetPJPExpApprovalList(Tab.Approval Modal);

        PostResponse fnSetPJPExp_Approved(PJPExpenses.ApprovalAction modal);
    }
}
