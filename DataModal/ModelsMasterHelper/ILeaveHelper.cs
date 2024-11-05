using DataModal.Models;
using System.Collections.Generic;
using System.Data;

namespace DataModal.ModelsMasterHelper
{
    public interface ILeaveHelper
    {
        List<LeaveType.List> GetMaster_LeaveTypeList(GetResponse Modal);
        LeaveType.Add GetMaster_LeaveType(GetResponse Modal);
        PostResponse fnSetMaster_LeaveType(LeaveType.Add model);
        List<LeaveLog.List> GetLeave_LogList(GetResponse Modal);

        List<LeaveLog.ApproverList> GetLeaveLogApprovalList(Tab.Approval Modal);
        LeaveLog.View GetLeaveLogTran(GetResponse Modal);

        PostResponse fnSetLeaveLog(AddLeave modal);
        //PostResponse fnSetLeaveLogTran(LeaveTran modal);
        PostResponse fnSetLeave_Log_Approve(LeaveApproval modal);
        List<AddLeaveTran> GetApplyLeaveTran(GetResponse Modal);
        AddLeave GetLeave_Apply(GetResponse Modal);
        List<LeaveBalance.List> GetLeave_BalanceList(Tab.Approval Modal);
        DataSet GetLeave_BalanceTran(GetResponse Modal);
    }
}
