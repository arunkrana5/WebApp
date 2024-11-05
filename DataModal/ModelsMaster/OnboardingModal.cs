using Dapper;
using DataModal.CommanClass;
using DataModal.Models;
using DataModal.ModelsMasterHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DataModal.ModelsMaster
{
    public class OnboardingModal : IOnboardingHelper
    {

        public PostResponse fnSetOnboarding_Approval(Onboarding.ApprovalAction modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.ConnectionString()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetOnboarding_Approval", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@OnBoardIDs", SqlDbType.VarChar).Value = modal.IDs;
                        command.Parameters.Add("@Approved", SqlDbType.Int).Value = modal.Approved;
                        command.Parameters.Add("@ApprovedRemarks", SqlDbType.VarChar).Value = modal.ApprovedRemarks ?? "";
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = modal.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = modal.IPAddress;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["STATUS"]);
                                Result.SuccessMessage = reader["MESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }
            return Result;
        }

        public List<Onboarding.List> GetOnboarding_Applications_List(JqueryDatatableParam Modal)
        {

            List<Onboarding.List> result = new List<Onboarding.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(clsDataBaseHelper.getConnectionStr()))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@Approved", dbType: DbType.Int32, value: Modal.Approved);
                    param.Add("@start", dbType: DbType.Int32, value: Modal.start);
                    param.Add("@length ", dbType: DbType.Int32, value: Modal.length);
                    param.Add("@SearchText", dbType: DbType.String, value: Modal.SearchText);
                    param.Add("@sortColumn", dbType: DbType.Int32, value: Modal.sortColumn);
                    param.Add("@sortOrder", dbType: DbType.String, value: Modal.sortOrder);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetOnboarding_Applications_List", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Onboarding.List>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetDealerList", "spu_GetDealerList", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }


    }
}
