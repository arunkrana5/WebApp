using DataModal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModal.ModelsMasterHelper
{
    public interface IDealerHelper
    {
        List<DealerChangeRequest.List> GetDealerChangeRequestList(JqueryDatatableParam Modal);
        List<DealerChangeRequest.List> GetDealerChangeRequestList_All(JqueryDatatableParam Modal);
        DealerChangeRequest.Add GetDealerChangeRequest(GetResponse Modal);
        PostResponse fnSetDealerChangeRequest(DealerChangeRequest.Add model);
        PostResponse fnSetDealerChangeRequest_Approved(DealerChangeRequest.ApprovalAction modal);
        List<TerminateRequest.List> GetTerminationRequestList(JqueryDatatableParam Modal);
        List<TerminateRequest.List> GetTerminationRequest_MyList(JqueryDatatableParam Modal);
        PostResponse fnSetTerminationRequest_Approved(TerminateRequest.ApprovalAction modal);
        PostResponse fnSetTerminationRequest(TerminateRequest.Add model);
    }
}
