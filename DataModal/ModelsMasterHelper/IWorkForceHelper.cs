using DataModal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModal.ModelsMasterHelper
{
    public interface IWorkForceHelper
    {
        WorkForce.Add GetWorkForce(GetResponse Modal);
        PostResponse fnSetWorkForceRequest(WorkForce.Add model);
        List<WorkForce.List> GetWorkForceRequestList(JqueryDatatableParam Modal);
        PostResponse fnSetWorkForce_Approved(WorkForce.ApprovalAction modal);
        WorkForce_View GetWorkForce_View(Tab.Approval Modal);
    }
}
