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
    public class ReportModal : IReportHelper
    {
        string ConnectionStrings = ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString.ToString();

        public DataSet GetSaleEntryReport(Tab.Approval Modal)
        {
            DataSet ds = new DataSet();
            try
            {
                DateTime Start, End;
                DateTime.TryParse(Modal.StartDate, out Start);
                DateTime.TryParse(Modal.EndDate, out End);
                SqlParameter[] oparam = new SqlParameter[5];
                oparam[0] = new SqlParameter("@StartDate", Start.ToString("dd-MMM-yyyy"));
                oparam[1] = new SqlParameter("@EndDate", End.ToString("dd-MMM-yyyy"));
                oparam[2] = new SqlParameter("@Approved", Modal.Approved);
                oparam[3] = new SqlParameter("@LoginID", Modal.LoginID);
                oparam[4] = new SqlParameter("@IsAutoReport", Modal.Flag1 ?? "");
                ds = clsDataBaseHelper.ExecuteDataSet("spu_GetSaleEntry_Report", oparam);
            }
            catch (Exception ex)
            {
                Common_SPU.LogError("Error during GetSaleEntryReport. The query was executed :", ex.ToString(), "spu_GetSaleEntry_Report()", "Export", "Export", Modal.LoginID, "");
            }
            return ds;

        }

        public DataSet GetSaleEntryReport_Export(Tab.Approval Modal)
        {
            DataSet ds = new DataSet();
            try
            {
                DateTime Start, End;
                DateTime.TryParse(Modal.StartDate, out Start);
                DateTime.TryParse(Modal.EndDate, out End);
                SqlParameter[] oparam = new SqlParameter[3];
                oparam[0] = new SqlParameter("@StartDate", Start.ToString("dd-MMM-yyyy"));
                oparam[1] = new SqlParameter("@EndDate", End.ToString("dd-MMM-yyyy"));
                oparam[2] = new SqlParameter("@LoginID", Modal.LoginID);
                ds = clsDataBaseHelper.ExecuteDataSet("spu_GetSaleEntry_Export", oparam);
            }
            catch (Exception ex)
            {
                Common_SPU.LogError("Error during spu_GetSaleEntry_Export. The query was executed :", ex.ToString(), "spu_GetSaleEntry_Export()", "Export", "Export", Modal.LoginID, "");
            }
            return ds;

        }


        public DataSet GetMTD_Report(Tab.Approval Modal)
        {
            DataSet ds = new DataSet();

            try
            {
                DateTime dt;
                DateTime.TryParse(Modal.Month, out dt);

                SqlParameter[] oparam = new SqlParameter[3];
                oparam[0] = new SqlParameter("@Month", dt.Month);
                oparam[1] = new SqlParameter("@Year", dt.Year);
                oparam[2] = new SqlParameter("@LoginID", Modal.LoginID);
                ds = clsDataBaseHelper.ExecuteDataSet("spu_GetMTD_Report", oparam);
            }
            catch (Exception ex)
            {
            }
            return ds;

        }



        public DataSet GetAllSSR_Hierarchy_Report(GetResponse Modal)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] oparam = new SqlParameter[1];
                oparam[0] = new SqlParameter("@LoginID", Modal.LoginID);
                ds = clsDataBaseHelper.ExecuteDataSet("spu_GetAllSSR_Hierarchy_Report", oparam);
            }
            catch (Exception ex)
            {
            }
            return ds;

        }


        public List<CounterDisplay.Report> GetCounterDisplayReport(Tab.Approval Modal)
        {

            List<CounterDisplay.Report> result = new List<CounterDisplay.Report>();
            try
            {
                DateTime Start, End;
                DateTime.TryParse(Modal.StartDate, out Start);
                DateTime.TryParse(Modal.EndDate, out End);
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.ConnectionString()))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@StartDate", dbType: DbType.String, value: Start.ToString("dd-MMM-yyyy"), direction: ParameterDirection.Input);
                    param.Add("@EndDate", dbType: DbType.String, value: End.ToString("dd-MMM-yyyy"), direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);

                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetCounterDisplay_Report", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<CounterDisplay.Report>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetCounterDisplayList", "GetCounterDisplayList", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public List<MOP.Report> GetMOPReportList(Tab.Approval Modal)
        {

            List<MOP.Report> result = new List<MOP.Report>();
            try
            {
                DateTime Start, End;
                DateTime.TryParse(Modal.StartDate, out Start);
                DateTime.TryParse(Modal.EndDate, out End);
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.ConnectionString()))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@StartDate", dbType: DbType.String, value: Start.ToString("dd-MMM-yyyy"), direction: ParameterDirection.Input);
                    param.Add("@EndDate", dbType: DbType.String, value: End.ToString("dd-MMM-yyyy"), direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetMOP_Report", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<MOP.Report>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetMOPReportList", "spu_GetMOP_Report", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }



        public DataSet GetTargetVsAchievement_Report(Tab.Approval Modal)
        {
            DataSet ds = new DataSet();
            try
            {
                DateTime dt;
                DateTime.TryParse(Modal.Month, out dt);
                SqlParameter[] oparam = new SqlParameter[2];
                oparam[0] = new SqlParameter("@Date", dt.ToString("dd-MMM-yyyy"));
                oparam[1] = new SqlParameter("@LoginID", Modal.LoginID);
                ds = clsDataBaseHelper.ExecuteDataSet("spu_GetTargetVsAchievement_Report", oparam);
            }
            catch (Exception ex)
            {
            }
            return ds;

        }



        public DailySummary.Data GetDailyReportSummaryData(Tab.Approval Modal)
        {

            DailySummary.Data result = new DailySummary.Data();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    param.Add("@Date", dbType: DbType.String, value: Modal.Month ?? "", direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetDailyReportSummary", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result.MTDList = reader.Read<DailySummary.MainList>().ToList();
                        if (!reader.IsConsumed)
                        {
                            result.SaleList = reader.Read<DailySummary.MainList>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.AttendenceList = reader.Read<DailySummary.MainList>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.TotalSSRList = reader.Read<DailySummary.MainList>().ToList();
                        }
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetDailyReportSummaryData", "spu_GetDailyReportSummary", "DataModal", 0, "");
            }
            return result;
        }

        public DataSet GetPJPEntryReport(Tab.Approval Modal)
        {
            DataSet ds = new DataSet();
            try
            {
                DateTime dt;
                DateTime.TryParse(Modal.Month, out dt);
                SqlParameter[] oparam = new SqlParameter[2];
                oparam[0] = new SqlParameter("@Date", dt.ToString("dd-MMM-yyyy"));
                oparam[1] = new SqlParameter("@LoginID", Modal.LoginID);
                ds = clsDataBaseHelper.ExecuteDataSet("spu_GetPJPEntry_Report", oparam);
            }
            catch (Exception ex)
            {
            }
            return ds;

        }



        public DataSet GetSaleEntry_WithCustomer(Tab.Approval Modal)
        {
            DataSet ds = new DataSet();
            try
            {
                DateTime Start, End;
                DateTime.TryParse(Modal.StartDate, out Start);
                DateTime.TryParse(Modal.EndDate, out End);

                SqlParameter[] oparam = new SqlParameter[3];
                oparam[0] = new SqlParameter("@StartDate", Start.ToString("dd-MMM-yyyy"));
                oparam[1] = new SqlParameter("@EndDate", End.ToString("dd-MMM-yyyy"));
                oparam[2] = new SqlParameter("@LoginID", Modal.LoginID);
                ds = clsDataBaseHelper.ExecuteDataSet("spu_GetSaleEntry_WithCustomer", oparam);
            }
            catch (Exception ex)
            {
            }
            return ds;

        }




        public List<Attendance_Log.Daily> GetAttendance_Log_Daily(Tab.Approval Modal)
        {

            List<Attendance_Log.Daily> result = new List<Attendance_Log.Daily>();
            try
            {
                DateTime dt;
                DateTime.TryParse(Modal.Month, out dt);
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.ConnectionString()))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@date", dbType: DbType.String, value: dt.ToString("dd-MMM-yyyy"), direction: ParameterDirection.Input);
                    param.Add("@RoleID", dbType: DbType.Int64, value: Modal.RoleID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetAttendance_Log_Daily", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Attendance_Log.Daily>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetAttendance_Log", "GetAttendance_LogDaily", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public List<Attendance_Log.Monthly> GetAttendance_Log_MonthlyList(Tab.Approval Modal)
        {
            DateTime dt;
            DateTime.TryParse(Modal.Month, out dt);

            DateTime baseDate = dt;
            DateTime StartDate = baseDate.AddDays(1 - baseDate.Day);
            DateTime EndDate = StartDate.AddMonths(1).AddSeconds(-1);
            List<Attendance_Log.Monthly> List = new List<Attendance_Log.Monthly>();
            Attendance_Log.Monthly obj;
            try
            {
                foreach (DataRow item in GetAttendance_Log_Monthly(Modal).Tables[0].Rows)
                {
                    obj = new Attendance_Log.Monthly();
                    obj.EMPID = Convert.ToInt64(item["EMPID"].ToString());
                    obj.EMPCode = item["EMPCode"].ToString();
                    obj.EMPImageURL = item["EMPImageURL"].ToString();
                    obj.EMPName = item["EMPName"].ToString();
                    obj.Phone = item["Phone"].ToString();
                    obj.DOJ = item["DOJ"].ToString();
                    obj.DesignName = item["DesignName"].ToString();
                    obj.DeptName = item["DeptName"].ToString();
                    obj.Region = item["Region"].ToString();
                    obj.DOJ = item["DOJ"].ToString();
                    obj.Gender = item["Gender"].ToString();
                    obj.State = item["State"].ToString();
                    obj.City = item["City"].ToString();
                    obj.Area = item["Area"].ToString();
                    obj.Role = item["Role"].ToString();
                    obj.Status = item["Status"].ToString();
                    obj.DealerCode = item["DealerCode"].ToString();
                    obj.DealerName = item["DealerName"].ToString();
                    obj.DealerType = item["DealerType"].ToString();
                    obj.DealerArea = item["Area"].ToString();
                    obj.BranchName = item["BranchName"].ToString();
                    obj.BranchCode = item["BranchCode"].ToString();
                    obj.EntrySource = item["EntrySource"].ToString();

                    obj.Days = new Dictionary<string, string>();
                    foreach (var da in ClsCommon.EachDay(StartDate, EndDate))
                    {
                        obj.Days.Add(da.ToString("dd-MMM-yyyy"), item[da.ToString("dd-MMM-yyyy")].ToString());
                    }

                    List.Add(obj);
                }


            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetAttendance_Log_MonthlyList", "GetAttendance_LogDaily", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return List;
        }

        public List<Attendance_Log.Monthly_INOUT> GetAttendance_Log_Monthly_InOutList(Tab.Approval Modal)
        {
            DateTime dt;
            DateTime.TryParse(Modal.Month, out dt);

            DateTime baseDate = dt;
            DateTime StartDate = baseDate.AddDays(1 - baseDate.Day);
            DateTime EndDate = StartDate.AddMonths(1).AddSeconds(-1);
            List<Attendance_Log.Monthly_INOUT> List = new List<Attendance_Log.Monthly_INOUT>();
            Attendance_Log.Monthly_INOUT obj;
            try
            {
                foreach (DataRow item in GetAttendance_Log_Monthly_InOut(Modal).Tables[0].Rows)
                {
                    obj = new Attendance_Log.Monthly_INOUT();
                    obj.EMPID = Convert.ToInt64(item["EMPID"].ToString());
                    obj.EMPCode = item["EMPCode"].ToString();
                    obj.EMPImageURL = item["EMPImageURL"].ToString();
                    obj.EMPName = item["EMPName"].ToString();
                    obj.Role = item["Role"].ToString();
                    obj.DOJ = item["DOJ"].ToString();
                    obj.Status = item["Status"].ToString();
                    obj.Phone = item["Phone"].ToString();
                    obj.DesignName = item["DesignName"].ToString();
                    obj.DeptName = item["DeptName"].ToString();
                    obj.Region = item["Region"].ToString();
                    obj.Gender = item["Gender"].ToString();
                    obj.State = item["State"].ToString();
                    obj.City = item["City"].ToString();
                    obj.Area = item["Area"].ToString();
                    obj.DealerCode = item["DealerCode"].ToString();
                    obj.DealerName = item["DealerName"].ToString();
                    obj.DealerType = item["DealerType"].ToString();
                    obj.DealerArea = item["DealerArea"].ToString();
                    obj.BranchName = item["BranchName"].ToString();
                    obj.BranchCode = item["BranchCode"].ToString();

                    obj.Days = new Dictionary<string, string>();
                    foreach (var da in ClsCommon.EachDay(StartDate, EndDate))
                    {
                        obj.Days.Add(da.ToString("dd-MMM-yyyy"), item[da.ToString("dd-MMM-yyyy")].ToString());
                    }

                    List.Add(obj);
                }


            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetAttendance_Log_Monthly_InOutList", "GetAttendance_LogDaily", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return List;
        }

        public DataSet GetAttendance_Log_Monthly(Tab.Approval Modal)
        {
            DataSet ds = new DataSet();
            try
            {
                DateTime dt;
                DateTime.TryParse(Modal.Month, out dt);
                SqlParameter[] oparam = new SqlParameter[4];
                oparam[0] = new SqlParameter("@Month", dt.Month);
                oparam[1] = new SqlParameter("@Year", dt.Year);
                oparam[2] = new SqlParameter("@RoleID", Modal.RoleID);
                oparam[3] = new SqlParameter("@LoginID", Modal.LoginID);
                ds = clsDataBaseHelper.ExecuteDataSet("spu_GetAttendance_Log_Monthly", oparam);
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetAttendance_Log_Monthly", "spu_GetAttendance_Log_Monthly", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return ds;

        }

        public DataSet GetAttendance_Log_Monthly_InOut(Tab.Approval Modal)
        {
            DataSet ds = new DataSet();
            try
            {
                DateTime dt;
                DateTime.TryParse(Modal.Month, out dt);
                SqlParameter[] oparam = new SqlParameter[4];
                oparam[0] = new SqlParameter("@Month", dt.Month);
                oparam[1] = new SqlParameter("@Year", dt.Year);
                oparam[2] = new SqlParameter("@RoleID", Modal.RoleID);
                oparam[3] = new SqlParameter("@LoginID", Modal.LoginID);
                ds = clsDataBaseHelper.ExecuteDataSet("spu_GetAttendance_Log_Monthly_InOut", oparam);
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetAttendance_Log_Monthly_InOut", "spu_GetAttendance_Log_Monthly_InOut", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return ds;

        }



        public DataSet GetAttendance_Final(Tab.Approval Modal)
        {
            DataSet ds = new DataSet();
            try
            {
                DateTime dt;
                DateTime.TryParse(Modal.Month, out dt);
                SqlParameter[] oparam = new SqlParameter[5];
                oparam[0] = new SqlParameter("@Month", dt.Month);
                oparam[1] = new SqlParameter("@Year", dt.Year);
                oparam[2] = new SqlParameter("@Approved", Modal.Approved);
                oparam[3] = new SqlParameter("@RoleID", Modal.RoleID);
                oparam[4] = new SqlParameter("@LoginID", Modal.LoginID);

                ds = clsDataBaseHelper.ExecuteDataSet("spu_GetAttendance", oparam);
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetAttendance_Final", "GetAttendance_Final", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return ds;

        }

        public DataSet GetAttendance(Tab.Approval Modal)
        {
            DataSet ds = new DataSet();
            try
            {
                DateTime dt;
                DateTime.TryParse(Modal.Month, out dt);
                SqlParameter[] oparam = new SqlParameter[5];
                oparam[0] = new SqlParameter("@Month", dt.Month);
                oparam[1] = new SqlParameter("@Year", dt.Year);
                oparam[2] = new SqlParameter("@Approved", Modal.Approved);
                oparam[3] = new SqlParameter("@RoleID", Modal.RoleID);
                oparam[4] = new SqlParameter("@LoginID", Modal.LoginID);

                ds = clsDataBaseHelper.ExecuteDataSet("spu_GetAttendance", oparam);
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetAttendance", "spu_GetAttendance", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return ds;

        }


        public DataSet GetAttendance_EMPWise(Tab.Approval Modal)
        {
            DataSet ds = new DataSet();
            try
            {
                DateTime dt;
                DateTime.TryParse(Modal.Month, out dt);
                SqlParameter[] oparam = new SqlParameter[3];
                oparam[0] = new SqlParameter("@Month", dt.Month);
                oparam[1] = new SqlParameter("@Year", dt.Year);
                oparam[2] = new SqlParameter("@EMPIDs", Modal.SeperatedIDs ?? "");
                ds = clsDataBaseHelper.ExecuteDataSet("spu_GetAttendance_EMPWise", oparam);
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetAttendance_EMPWise", "spu_GetAttendance_EMPWise", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return ds;

        }

        public DataSet GetTLTracker_Report(Tab.Approval Modal)
        {
            DataSet ds = new DataSet();
            try
            {
                DateTime dt;
                DateTime.TryParse(Modal.Month, out dt);
                SqlParameter[] oparam = new SqlParameter[2];
                oparam[0] = new SqlParameter("@Date", dt.ToString("dd-MMM-yyyy"));
                oparam[1] = new SqlParameter("@LoginID", Modal.LoginID);
                ds = clsDataBaseHelper.ExecuteDataSet("spu_GetTLTracker_Report", oparam);
            }
            catch (Exception ex)
            {
                Common_SPU.LogError("Error during GetTLTracker_Report. The query was executed :", ex.ToString(), "spu_TLTracker_Report()", "Export", "Export", Modal.LoginID, "");
            }
            return ds;

        }

        public PostResponse fnSetUpdateAttendance(GetResponse modal, DataTable Table)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetUpdateAttendance", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = modal.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = modal.IPAddress;
                        command.Parameters.Add("@Attendance_Type", SqlDbType.Structured).Value = Table;
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


        public DataSet GetTravel_Expenses_Report(Tab.Approval Modal)
        {
            DataSet ds = new DataSet();
            try
            {
                DateTime dt;
                DateTime.TryParse(Modal.Month, out dt);
                SqlParameter[] oparam = new SqlParameter[3];
                oparam[0] = new SqlParameter("@Month", dt.Month);
                oparam[1] = new SqlParameter("@Year", dt.Year);
                oparam[2] = new SqlParameter("@LoginID", Modal.LoginID);
                ds = clsDataBaseHelper.ExecuteDataSet("spu_GetTravel_Expenses_Report", oparam);
            }
            catch (Exception ex)
            {
            }
            return ds;

        }

        public DataSet GetTravel_Visit_Report(Tab.Approval Modal)
        {
            DataSet ds = new DataSet();
            try
            {
                DateTime dt;
                DateTime.TryParse(Modal.Month, out dt);
                SqlParameter[] oparam = new SqlParameter[3];
                oparam[0] = new SqlParameter("@Month", dt.Month);
                oparam[1] = new SqlParameter("@Year", dt.Year);
                oparam[2] = new SqlParameter("@LoginID", Modal.LoginID);
                ds = clsDataBaseHelper.ExecuteDataSet("spu_GetTravel_Visit_Report", oparam);
            }
            catch (Exception ex)
            {
            }
            return ds;

        }

        public List<AbsentTracker> GetAbsentTracker(Tab.Approval Modal)
        {

            List<AbsentTracker> result = new List<AbsentTracker>();
            try
            {
                DateTime Start;
                DateTime.TryParse(Modal.Month, out Start);
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.ConnectionString()))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@Date", dbType: DbType.String, value: Start.ToString("dd-MMM-yyyy"), direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetAbsentTracker", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<AbsentTracker>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetAbsentTracker", "spu_GetAbsentTracker", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }


        public DataSet GetEMP_Incentive(Tab.Approval Modal)
        {
            DataSet ds = new DataSet();
            try
            {
                DateTime dt;
                DateTime.TryParse(Modal.Month, out dt);
                SqlParameter[] oparam = new SqlParameter[3];
                oparam[0] = new SqlParameter("@Month", dt.Month);
                oparam[1] = new SqlParameter("@Year", dt.Year);
                oparam[2] = new SqlParameter("@LoginID", Modal.LoginID);
                ds = clsDataBaseHelper.ExecuteDataSet("spu_GetEMP_Incentive", oparam);
            }
            catch (Exception ex)
            {
            }
            return ds;

        }

        public DataSet GetDealerPerformance_Reports(Tab.Approval Modal)
        {
            DataSet ds = new DataSet();
            try
            {
                DateTime dt;
                DateTime.TryParse(Modal.Month, out dt);
                SqlParameter[] oparam = new SqlParameter[2];
                oparam[0] = new SqlParameter("@Date", dt.ToString("dd-MMM-yyyy"));
                oparam[1] = new SqlParameter("@LoginID", Modal.LoginID);
                ds = clsDataBaseHelper.ExecuteDataSet("spu_GetDealerPerformance_Reports", oparam);
            }
            catch (Exception ex)
            {
            }
            return ds;

        }

        public DataSet GetGetOnboardFor_ProHRMS(Tab.Approval Modal)
        {
            DataSet ds = new DataSet();
            try
            {
                DateTime dt;
                DateTime.TryParse(Modal.Month, out dt);
                SqlParameter[] oparam = new SqlParameter[2];
                oparam[0] = new SqlParameter("@Date", dt.ToString("dd-MMM-yyyy"));
                oparam[1] = new SqlParameter("@LoginID", Modal.LoginID);
                ds = clsDataBaseHelper.ExecuteDataSet("spu_GetOnboardFor_ProHRMS", oparam);
            }
            catch (Exception ex)
            {
            }
            return ds;

        }

        public DataSet GetAttendance_Log_Monthly_InOut_EMPWise(Tab.Approval Modal)
        {
            DataSet ds = new DataSet();
            try
            {
                DateTime dt;
                DateTime.TryParse(Modal.Month, out dt);
                SqlParameter[] oparam = new SqlParameter[2];
                oparam[0] = new SqlParameter("@Date", dt.ToString("dd-MMM-yyyy"));
                oparam[1] = new SqlParameter("@EMPID", Modal.EMPID);
                ds = clsDataBaseHelper.ExecuteDataSet("spu_GetAttendance_Log_Monthly_InOut_EMPWise", oparam);
            }
            catch (Exception ex)
            {
            }
            return ds;

        }

        public List<AttendenceChangeLog> GetAttendanceChangeLog(JqueryDatatableParam Modal)
        {

            List<AttendenceChangeLog> result = new List<AttendenceChangeLog>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@start", dbType: DbType.Int32, value: Modal.start);
                    param.Add("@length ", dbType: DbType.Int32, value: Modal.length);
                    param.Add("@SearchText", dbType: DbType.String, value: Modal.SearchText);
                    param.Add("@sortColumn", dbType: DbType.Int32, value: Modal.sortColumn);
                    param.Add("@sortOrder", dbType: DbType.String, value: Modal.sortOrder);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    param.Add("@FromDate", dbType: DbType.String, value: Modal.OtherValue1, direction: ParameterDirection.Input);
                    param.Add("@ToDate", dbType: DbType.String, value: Modal.OtherValue2, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetAttendanceChange_Log", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<AttendenceChangeLog>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetAttendanceChange_Log", "spu_GetAttendanceChange_Log", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public List<PJPEntry.List> GetPJPReport(JqueryDatatableParam Modal)
        {

            List<PJPEntry.List> result = new List<PJPEntry.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@start", dbType: DbType.Int32, value: Modal.start);
                    param.Add("@length ", dbType: DbType.Int32, value: Modal.length);
                    param.Add("@SearchText", dbType: DbType.String, value: Modal.SearchText);
                    param.Add("@sortColumn", dbType: DbType.Int32, value: Modal.sortColumn);
                    param.Add("@sortOrder", dbType: DbType.String, value: Modal.sortOrder);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    param.Add("@Month", dbType: DbType.String, value: Modal.OtherValue1, direction: ParameterDirection.Input); 
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetPJPReport_NEW", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<PJPEntry.List>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetPJPReport_NEW", "spu_GetPJPReport_NEW", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }
        public List<PJPEntries.List> GetPJPReports(JqueryDatatableParam Modal)
        {

            List<PJPEntries.List> result = new List<PJPEntries.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@start", dbType: DbType.Int32, value: Modal.start);
                    param.Add("@length ", dbType: DbType.Int32, value: Modal.length);
                    param.Add("@SearchText", dbType: DbType.String, value: Modal.SearchText);
                    param.Add("@sortColumn", dbType: DbType.Int32, value: Modal.sortColumn);
                    param.Add("@sortOrder", dbType: DbType.String, value: Modal.sortOrder);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    param.Add("@Month", dbType: DbType.String, value: Modal.OtherValue1, direction: ParameterDirection.Input); 
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetPJPReports", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<PJPEntries.List>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetPJPReports", "spu_GetPJPReports", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }
        public DataSet GetPJPEntriesReport(Tab.Approval Modal)
        {
            DataSet ds = new DataSet();
            try
            {
                DateTime dt;
                DateTime.TryParse(Modal.Month, out dt);
                SqlParameter[] oparam = new SqlParameter[2];
                oparam[0] = new SqlParameter("@Date", dt.ToString("dd-MMM-yyyy"));
                oparam[1] = new SqlParameter("@LoginID", Modal.LoginID);
                ds = clsDataBaseHelper.ExecuteDataSet("spu_GetPJPEntries_Report", oparam);
            }
            catch (Exception ex)
            {

            }
            return ds;
        }
        public DataSet spu_GetSalesSummary(Tab.Approval Modal)
        {
            DataSet ds = new DataSet();
            try
            {
                DateTime dt;
                DateTime.TryParse(Modal.Month, out dt);
                SqlParameter[] oparam = new SqlParameter[2];
                oparam[0] = new SqlParameter("@InputDate", dt.ToString("dd-MMM-yyyy"));
                oparam[1] = new SqlParameter("@LoginID", Modal.LoginID);
                ds = clsDataBaseHelper.ExecuteDataSet("spu_GetSalesSummary", oparam);
            }
            catch (Exception ex)
            {

            }
            return ds;
        }
    }
}
