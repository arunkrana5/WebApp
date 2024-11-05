using Dapper;
using DataModal.CommanClass;
using DataModal.Models;
using DataModal.ModelsMasterHelper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DataModal.ModelsMaster
{
    public class LeaveModal : ILeaveHelper
    {
        string ConnectionStrings = ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString.ToString();
        string EntrySource = "Web";

        public List<LeaveType.List> GetMaster_LeaveTypeList(GetResponse Modal)
        {

            List<LeaveType.List> result = new List<LeaveType.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetMaster_LeaveTypeList", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<LeaveType.List>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetMaster_LeaveTypeList", "spu_GetMaster_LeaveTypeList", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public LeaveType.Add GetMaster_LeaveType(GetResponse Modal)
        {

            LeaveType.Add result = new LeaveType.Add();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@LeaveTYpeID", dbType: DbType.Int32, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginId", dbType: DbType.Int32, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetMaster_LeaveType", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<LeaveType.Add>().FirstOrDefault();
                        if (result == null)
                        {
                            result = new LeaveType.Add();
                        }
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetMaster_LeaveType", "spu_GetMaster_LeaveType", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }



        public PostResponse fnSetMaster_LeaveType(LeaveType.Add model)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetMaster_LeaveType", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@LeaveTypeID", SqlDbType.Int).Value = model.LeaveTypeID ?? 0;
                        command.Parameters.Add("@DisplayName", SqlDbType.VarChar).Value = model.DisplayName ?? "";
                        command.Parameters.Add("@Icon", SqlDbType.VarChar).Value = model.Icon ?? "";
                        command.Parameters.Add("@Color", SqlDbType.VarChar).Value = model.Color ?? "";
                        command.Parameters.Add("@IsActive", SqlDbType.Int).Value = 1;
                        command.Parameters.Add("@MonthlyAccrued", SqlDbType.Float).Value = model.MonthlyAccrued ?? 0;
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = model.Priority ?? 0;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = model.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = model.IPAddress;
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


        public List<LeaveLog.List> GetLeave_LogList(GetResponse Modal)
        {

            List<LeaveLog.List> result = new List<LeaveLog.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@Approved", dbType: DbType.Int64, value: Modal.Approved, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetLeave_LogList", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<LeaveLog.List>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetLeave_LogList", "spu_GetLeave_LogList", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }




        public List<LeaveLog.ApproverList> GetLeaveLogApprovalList(Tab.Approval Modal)
        {

            List<LeaveLog.ApproverList> result = new List<LeaveLog.ApproverList>();
            try
            {
                DateTime dt;
                DateTime.TryParse(Modal.Month, out dt);
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@Month", dbType: DbType.Int32, value: dt.Month, direction: ParameterDirection.Input);
                    param.Add("@Year", dbType: DbType.Int32, value: dt.Year, direction: ParameterDirection.Input);
                    param.Add("@Approved", dbType: DbType.Int64, value: Modal.Approved, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    param.Add("@UserType", dbType: DbType.String, value: Modal.Doctype ?? "", direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetLeave_LogApprovalList", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<LeaveLog.ApproverList>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetLeaveLogApprovalList", "GetLeaveLogApprovalList", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }


        public LeaveLog.View GetLeaveLogTran(GetResponse Modal)
        {

            LeaveLog.View result = new LeaveLog.View();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@LogID", dbType: DbType.Int64, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetLeave_Log_Tran", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result.LeaveLog = reader.Read<LeaveLog.List>().FirstOrDefault();
                        if (!reader.IsConsumed)
                        {
                            result.TranDetails = reader.Read<LeaveTran_View>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.LeaveBalList = reader.Read<LeaveBal>().ToList();
                        }
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetLeaveLogTran", "spu_GetLeaveLogTran", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }


        public PostResponse fnSetLeaveLog(AddLeave modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetLeave_Log", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@LogID", SqlDbType.Int).Value = modal.LogID ?? 0;
                        command.Parameters.Add("@EMPID", SqlDbType.Int).Value = modal.EMPID ?? 0;
                        command.Parameters.Add("@Reason", SqlDbType.VarChar).Value = modal.Reason ?? "";
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = modal.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = modal.IPAddress;
                        command.Parameters.Add("@EntrySource", SqlDbType.VarChar).Value = EntrySource;
                        command.Parameters.Add("@LeaveTranType", SqlDbType.Structured).Value = ClsCommon.ToDataTable(modal.LeaveTranList.OrderBy(z => Convert.ToDateTime(z.Date)).ToList());
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

        //public PostResponse fnSetLeaveLogTran(LeaveTran modal)
        //{
        //    PostResponse Result = new PostResponse();
        //    using (SqlConnection con = new SqlConnection(ConnectionStrings))
        //    {
        //        try
        //        {
        //            con.Open();
        //            using (SqlCommand command = new SqlCommand("spu_SetLeaveLogTran", con))
        //            {
        //                SqlDataAdapter da = new SqlDataAdapter();
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.Parameters.Add("@LogDetID", SqlDbType.Int).Value = modal.LogDetID ?? 0;
        //                command.Parameters.Add("@LogID", SqlDbType.Int).Value = modal.LogID ?? 0;
        //                command.Parameters.Add("@Date", SqlDbType.VarChar).Value = modal.LeaveDate ?? "";
        //                command.Parameters.Add("@LeaveTypeID", SqlDbType.Int).Value = modal.LeaveTypeID;
        //                command.Parameters.Add("@Hours", SqlDbType.Int).Value = modal.Hours;
        //                command.Parameters.Add("@createdby", SqlDbType.Int).Value = modal.LoginID;
        //                command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = modal.IPAddress;
        //                command.CommandTimeout = 0;
        //                using (SqlDataReader reader = command.ExecuteReader())
        //                {
        //                    while (reader.Read())
        //                    {
        //                        Result.ID = Convert.ToInt64(reader["RET_ID"]);
        //                        Result.StatusCode = Convert.ToInt32(reader["STATUS"]);
        //                        Result.SuccessMessage = reader["MESSAGE"].ToString();
        //                        if (Result.StatusCode > 0)
        //                        {
        //                            Result.Status = true;
        //                        }
        //                    }
        //                }

        //            }
        //            con.Close();
        //        }
        //        catch (Exception ex)
        //        {
        //            con.Close();
        //            Result.StatusCode = -1;
        //            Result.SuccessMessage = ex.Message.ToString();
        //        }
        //    }
        //    return Result;
        //}

        public PostResponse fnSetLeave_Log_Approve(LeaveApproval modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetLeave_Log_Approve", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@LogID", SqlDbType.Int).Value = modal.LogID;
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


        public AddLeave GetLeave_Apply(GetResponse Modal)
        {
            AddLeave result = new AddLeave();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetLeave_Apply", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result.LeaveTypeList = reader.Read<DropDownlist>().ToList();
                        if (!reader.IsConsumed)
                        {
                            result.LeaveOption = reader.Read<LeaveOption>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.LeaveBalList = reader.Read<LeaveBal>().ToList();
                        }

                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetLeave_Apply", "spu_GetApplyLeaveTran", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public List<AddLeaveTran> GetApplyLeaveTran(GetResponse Modal)
        {
            List<LeaveOption> LeaveOption = new List<LeaveOption>();
            List<AddLeaveTran> result = new List<AddLeaveTran>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@FromDate", dbType: DbType.String, value: Modal.Param1 ?? "", direction: ParameterDirection.Input);
                    param.Add("@ToDate", dbType: DbType.String, value: Modal.Param2 ?? "", direction: ParameterDirection.Input);
                    param.Add("@LeaveTypeID", dbType: DbType.Int64, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetApplyLeaveTran", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<AddLeaveTran>().ToList();
                    }

                    DBContext.Close();
                }
                if (result != null)
                {
                    DateTime dt;
                    foreach (var item in result)
                    {
                        DateTime.TryParse(item.Date, out dt);
                        item.Date = dt.ToString("yyyy-MM-dd");
                    }
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetApplyLeaveTran", "spu_GetApplyLeaveTran", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public List<LeaveBalance.List> GetLeave_BalanceList(Tab.Approval Modal)
        {

            List<LeaveBalance.List> result = new List<LeaveBalance.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@FinYearID", dbType: DbType.Int64, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@RoleID", dbType: DbType.Int64, value: Modal.RoleID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetLeave_BalanceList", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<LeaveBalance.List>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetLeave_BalanceList", "spu_GetLeave_BalanceList", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public DataSet GetLeave_BalanceTran(GetResponse Modal)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] oparam = new SqlParameter[1];
                oparam[0] = new SqlParameter("@LeaveBalance_ID", Modal.ID);
                ds = clsDataBaseHelper.ExecuteDataSet("spu_GetLeave_BalanceTran", oparam);
            }
            catch (Exception ex)
            {
                Common_SPU.LogError("Error during GetLeave_BalanceTran. The query was executed :", ex.ToString(), "GetLeave_BalanceTran()", "Common_SPU", "Common_SPU", 0, "");

            }
            return ds;

        }


    }
}
