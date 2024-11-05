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
    public class HomeModal : IHomeHelper
    {
        public Attendance_Log.PunchStatus GetPunchTime_DateWise(GetResponse Modal)
        {
            DateTime dt;
            DateTime.TryParse(Modal.Date, out dt);
            Attendance_Log.PunchStatus result = new Attendance_Log.PunchStatus();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.ConnectionString()))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@Date", dbType: DbType.String, value: dt.ToString("dd-MMM-yyyy") ?? "", direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetPunchTime_DateWise", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Attendance_Log.PunchStatus>().FirstOrDefault();
                        if (result == null)
                        {
                            result = new Attendance_Log.PunchStatus();
                        }
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetPunchTime_DateWise", "spu_GetPunchTime_DateWise", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public List<TrgVsAch> GetTargetAchieved_MonthWise(GetResponse Modal)
        {

            List<TrgVsAch> result = new List<TrgVsAch>();
            try
            {
                DateTime dt;
                DateTime.TryParse(Modal.Date, out dt);
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.ConnectionString()))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@Month", dbType: DbType.Int32, value: dt.Month, direction: ParameterDirection.Input);
                    param.Add("@Year", dbType: DbType.Int32, value: dt.Year, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    param.Add("@Doctype", dbType: DbType.String, value: Modal.Doctype ?? "", direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetTargetAchieved_MonthWise", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<TrgVsAch>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetTargetAchieved_MonthWise", "spu_GetTargetAchieved_MonthWise", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public List<Announcement.My> GetMyAnnouncement(GetResponse modal)
        {
            List<Announcement.My> result = new List<Announcement.My>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.ConnectionString()))
                {
                    var param = new DynamicParameters();
                    DBContext.Open();
                    param.Add("@LoginID", dbType: DbType.Int32, value: modal.LoginID, direction: ParameterDirection.Input);
                    using (var reader = DBContext.QueryMultiple("spu_GetAnnouncement", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<Announcement.My>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError("Error during spu_GetAnnouncement. The query was executed :", ex.ToString(), "spu_GetAnnouncement()", "ToolsModal", "ToolsModal", modal.LoginID, modal.IPAddress);

            }
            return result;
        }


        public List<BirthdayList> GetBirthdayList(GetResponse modal)
        {
            List<BirthdayList> result = new List<BirthdayList>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.ConnectionString()))
                {
                    var param = new DynamicParameters();
                    DBContext.Open();
                    param.Add("@LoginID", dbType: DbType.Int32, value: modal.LoginID, direction: ParameterDirection.Input);
                    using (var reader = DBContext.QueryMultiple("spu_GetBirthdayList", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<BirthdayList>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError("Error during GetBirthdayList. The query was executed :", ex.ToString(), "spu_GetBirhtdayList()", "ToolsModal", "ToolsModal", modal.LoginID, modal.IPAddress);

            }
            return result;
        }
    }
}
