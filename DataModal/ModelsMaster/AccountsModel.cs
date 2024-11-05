using Dapper;
using DataModal.CommanClass;
using DataModal.Models;
using DataModal.ModelsMasterHelper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DataModal.DataModal.ModelsMaster
{
    public class AccountsModel : IAccountsHelper
    {

        string EntrySource = "Web";
        public AdminUser.Details GetLogin(AdminUser.Login Model)
        {

            AdminUser.Details result = new AdminUser.Details();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.ConnectionString()))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@UserID", dbType: DbType.String, value: ClsCommon.EnsureString(Model.UserName), direction: ParameterDirection.Input);
                    param.Add("@Password", dbType: DbType.String, value: ClsCommon.EnsureString(ClsCommon.Encrypt(Model.Password)), direction: ParameterDirection.Input);
                    param.Add("@SessionID", dbType: DbType.String, value: ClsCommon.EnsureString(Model.SessionID), direction: ParameterDirection.Input);
                    param.Add("@IPAddress", dbType: DbType.String, value: ClsCommon.EnsureString(Model.IPAddress), direction: ParameterDirection.Input);
                    param.Add("@LoginInfo", dbType: DbType.String, value: Model.LoginInfo ?? "", direction: ParameterDirection.Input);
                    param.Add("@EntrySource", dbType: DbType.String, value: EntrySource, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetLogin", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<AdminUser.Details>().FirstOrDefault();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetLogin", "spu_GetLogin", "AccountsModal", 0, "");
            }
            return result;
        }
        public AdminUser.Details GetLoginWithToken(string UserName, string Password, string SessionID, string IPAddress)
        {

            AdminUser.Details result = new AdminUser.Details();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.ConnectionString()))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@UserID", dbType: DbType.String, value: ClsCommon.EnsureString(UserName), direction: ParameterDirection.Input);
                    param.Add("@Password", dbType: DbType.String, value: ClsCommon.EnsureString(ClsCommon.Encrypt(Password)), direction: ParameterDirection.Input);
                    param.Add("@SessionID", dbType: DbType.String, value: ClsCommon.EnsureString(SessionID), direction: ParameterDirection.Input);
                    param.Add("@IPAddress", dbType: DbType.String, value: ClsCommon.EnsureString(IPAddress), direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetLogin", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<AdminUser.Details>().FirstOrDefault();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetLogin", "spu_GetLogin", "AccountsModal", 0, "");
            }
            return result;
        }
        
    }
}
