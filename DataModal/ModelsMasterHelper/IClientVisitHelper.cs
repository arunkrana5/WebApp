using DataModal.Models;
using System.Collections.Generic;

namespace DataModal.ModelsMasterHelper
{
    public interface IClientVisitHelper
    {
        List<ClientVisit.List> GetClientVisitList(Tab.Approval Modal);
        PostResponse fnSetClient_Visit_CheckIn(ClientVisit.AddVisitCheckIn modal);
        PostResponse fnSetClient_Visit_CheckOut(ClientVisit.AddVisitCheckOut modal);
        PostResponse fnSetClient_Visit_Sales(ClientVisit.AddSales modal);
    }
}
