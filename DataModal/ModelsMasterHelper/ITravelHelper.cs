using DataModal.Models;
using System.Collections.Generic;

namespace DataModal.ModelsMasterHelper
{
    public interface ITravelHelper
    {
        List<Travel.List> GetTravel_Request_List(Tab.Approval Modal);
        Travel.Add GetTravel_Request(GetResponse Modal);
        PostResponse fnSetTravel_Request(Travel.Add model);
        List<Travel.List> GetTravel_ApprovedRequestList(Tab.Approval Modal);
        List<VisitEntry.List> GetTravel_VisitEntry_List(Tab.Approval Modal);
        VisitEntry.Add GetTravel_Values(Travel.Values Modal);
        VisitEntry.Add GetTravel_VisitEntry(GetResponse Modal);
        PostResponse fnSetTravel_VisitEntry(VisitEntry.Add model);
        PostResponse fnSetTravel_VisitEntryWithNoSSR(VisitEntry.AddWithNoSSR model);
        PostResponse fnSetTravel_VisitEntry_Brand(VisitEntry_Brand.List model);
        List<Travel.ExpenseList> GetTravel_MyExpenseList(Tab.Approval Modal);

        PostResponse fnSetTravel_Expense(TravelExpense Modal);
        TravelExpense GetTravel_ExpenseSummary(GetResponse Modal);
        PostResponse fnSetTravel_Requests_Approved(ApprovalAction modal);

        List<ExpenseApprovedSummary> GetTravel_Expense_ApprovalSummary(GetResponse Modal);
        List<Travel.ExpenseList> GetTravel_ExpenseList(Tab.Approval Modal);
        TravelCompleteRequest GetTravelCompleteRequest(GetResponse Modal);
        PostResponse fnSetTravelExpense_Approve(ApprovalAction modal);
        PostResponse fnSetTravel_Dealers(VisitEntry.AddMoreDealer modal);
    }
}
