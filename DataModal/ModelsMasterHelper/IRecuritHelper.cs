using DataModal.Models;
using System.Collections.Generic;

namespace DataModal.ModelsMasterHelper
{
    public interface IRecuritHelper
    {
        List<Requirement.BranchWiseEMP_Dashboard> GetBranchWiseEMP_Max_Active(GetResponse Modal);
        List<Requirement.RequestList> GetRequirement_MyRequest(JqueryDatatableParam Modal);
        PostResponse fnSetRequirement_Request(Requirement.AddRequest modal);
        Requirement.AddRequest GetRequirement_Request(GetResponse Modal);
        List<Requirement.RequestList> GetRequirement_RequestList(JqueryDatatableParam Modal);
        PostResponse fnSetRequirement_Application(Requirement.Application.Add modal);
        List<Requirement.Application.List> GetRequirement_ApplicationList(GetResponse Modal);
        Requirement.FullView GetRequirement_FullView(GetResponse Modal);
        PostResponse fnSetRequirementApplication_Approved(long ReqID, long ApplicationID, int Approved, string ApprovedRemarks, string Phone, string EmailID, decimal NetPay, string DOJ, string FinalRemarks, int IsFinalSubmit, long LoginID, string IPAddress);
    }
}
