using DataModal.Models;

namespace DataModal.ModelsMasterHelper
{
    public interface IAccountsHelper
    {
        AdminUser.Details GetLogin(AdminUser.Login Model);
        AdminUser.Details GetLoginWithToken(string UserName, string Password, string SessionID, string IPAddress);
        
    }
}
