using DataModal.Models;
using System.Collections.Generic;

namespace DataModal.ModelsMasterHelper
{
    public interface IOnboardingHelper
    {

        PostResponse fnSetOnboarding_Approval(Onboarding.ApprovalAction modal);
        List<Onboarding.List> GetOnboarding_Applications_List(JqueryDatatableParam Modal);



    }
}
