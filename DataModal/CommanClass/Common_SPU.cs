using Dapper;
using DataModal.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DataModal.CommanClass
{
    public class Common_SPU
    {
        public static string EntrySource = "Web";


        public static MarkAttendence GetCheck_AttendenceEligibility(GetResponse modal)
        {
            MarkAttendence result = new MarkAttendence();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.ConnectionString()))
                {
                    var param = new DynamicParameters();
                    param.Add("@LoginID", dbType: DbType.Int32, value: modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetCheck_AttendenceEligibility", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<MarkAttendence>().FirstOrDefault();
                        if (result == null)
                        {
                            result = new MarkAttendence();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.AttendenceStatusList = reader.Read<DropDownlist>().ToList();
                        }
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError("Error during GetCheck_AttendenceEligibility. The query was executed :", ex.ToString(), "GetCheck_AttendenceEligibility()", "Common_SPU", "Common_SPU", modal.LoginID, modal.IPAddress);

            }
            return result;
        }


        public static EMPFlags GetEMPFlags(GetResponse modal)
        {
            EMPFlags result = new EMPFlags();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.ConnectionString()))
                {
                    var param = new DynamicParameters();
                    param.Add("@EMPID", dbType: DbType.Int32, value: modal.ID, direction: ParameterDirection.Input);
                    param.Add("@Doctype", dbType: DbType.String, value: modal.Doctype ?? "", direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetEMPFlags", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<EMPFlags>().FirstOrDefault();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError("Error during GetEMPFlags. The query was executed :", ex.ToString(), "spu_GetCheck_AttendenceEligibility()", "Common_SPU", "Common_SPU", modal.LoginID, modal.IPAddress);

            }
            return result;
        }


        public static PostResponse fnSetChangePassword(ChangePassword modal)
        {
            PostResponse result = new PostResponse();

            using (SqlConnection con = new SqlConnection(ClsCommon.ConnectionString()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetChangePassword", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@NewPassword", SqlDbType.VarChar).Value = ClsCommon.Encrypt(modal.NewPassword) ?? "";
                        command.Parameters.Add("@OldPassword", SqlDbType.VarChar).Value = ClsCommon.Encrypt(modal.OldPassword) ?? "";
                        command.Parameters.Add("@Doctype", SqlDbType.VarChar).Value = modal.Doctype ?? "";
                        command.Parameters.Add("@LoginID", SqlDbType.Int).Value = modal.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = modal.IPAddress;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = Convert.ToInt32(reader["STATUS"]);
                                result.SuccessMessage = reader["MESSAGE"].ToString();
                                if (result.StatusCode > 0)
                                {
                                    result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Common_SPU.LogError("Error during fnSetChangePassword. The query was executed :", ex.ToString(), "spu_SetChangePassword()", "Common_SPU", "Common_SPU", modal.LoginID, "");
                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;

        }


        public static PostResponse fnGetTokenExists(string Token)
        {
            PostResponse result = new PostResponse();

            using (SqlConnection con = new SqlConnection(ClsCommon.ConnectionString()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_GetTokenExists", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Token", SqlDbType.VarChar).Value = Token ?? "";
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.Status = Convert.ToBoolean(reader["STATUS"]);
                                result.SuccessMessage = reader["MESSAGE"].ToString();
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Common_SPU.LogError("Error during GetTokenExists. The query was executed :", ex.ToString(), "GetTokenExists()", "Common_SPU", "Common_SPU", 0, "");
                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;

        }



        public static PostResponse fnGetSessionExists(string SessionID, long LoginID)
        {
            PostResponse result = new PostResponse();

            using (SqlConnection con = new SqlConnection(ClsCommon.ConnectionString()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_GetSessionExists", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@SessionID", SqlDbType.VarChar).Value = SessionID ?? "";
                        command.Parameters.Add("@LoginID", SqlDbType.Int).Value = LoginID;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.Status = Convert.ToBoolean(reader["STATUS"]);
                                result.SuccessMessage = reader["MESSAGE"].ToString();
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Common_SPU.LogError("Error during fnGetSessionExists. The query was executed :", ex.ToString(), "spu_GetSessionExists()", "Common_SPU", "Common_SPU", LoginID, "");
                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;

        }







        public static void LogError(string ErrDescription, string SystemException, string ActiveFunction, string ActiveForm, string ActiveModule, long LoginID, string IPAddress)
        {
            try
            {
                SqlParameter[] oparam = new SqlParameter[7];
                oparam[0] = new SqlParameter("@ErrDescription", ClsCommon.EnsureString(ErrDescription));
                oparam[1] = new SqlParameter("@SystemException", ClsCommon.EnsureString(SystemException));
                oparam[2] = new SqlParameter("@ActiveFunction", ClsCommon.EnsureString(ActiveFunction));
                oparam[3] = new SqlParameter("@ActiveForm", ClsCommon.EnsureString(ActiveForm));
                oparam[4] = new SqlParameter("@ActiveModule", ClsCommon.EnsureString(ActiveModule));
                oparam[5] = new SqlParameter("@createdby", LoginID);
                oparam[6] = new SqlParameter("@IPAddress", IPAddress ?? "");
                DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetErrorLog", oparam);
            }
            catch (Exception ex)
            {
            }

        }

        public static List<DropDownlist> GetDropDownList(GetDropDownResponse modal)
        {
            List<DropDownlist> result = new List<DropDownlist>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.ConnectionString()))
                {
                    var param = new DynamicParameters();
                    param.Add("@Doctype", dbType: DbType.String, value: modal.Doctype ?? "", direction: ParameterDirection.Input);
                    param.Add("@Values", dbType: DbType.String, value: modal.Values ?? "", direction: ParameterDirection.Input);
                    param.Add("@LoginId", dbType: DbType.Int32, value: modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetDropDownList", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<DropDownlist>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError("Error during GetDropDownList. The query was executed :", ex.ToString(), "spu_GetDropDownList()", "Common_SPU", "Common_SPU", modal.LoginID, modal.IPAddress);

            }
            return result;
        }


        public static List<DropDownlist> GetAttendenceStatus(GetResponse modal)
        {
            List<DropDownlist> result = new List<DropDownlist>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.ConnectionString()))
                {
                    var param = new DynamicParameters();
                    param.Add("@ID", dbType: DbType.Int32, value: modal.ID, direction: ParameterDirection.Input);
                    param.Add("@Doctype", dbType: DbType.String, value: modal.Doctype ?? "", direction: ParameterDirection.Input);
                    param.Add("@LoginId", dbType: DbType.Int32, value: modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetAttendenceStatus", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<DropDownlist>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError("Error during GetAttendenceStatus. The query was executed :", ex.ToString(), "spu_GetAttendenceStatus()", "Common_SPU", "Common_SPU", modal.LoginID, modal.IPAddress);

            }
            return result;
        }

        public static PostResponse fnSetMasterAttachment(FileResponse Modal)
        {
            PostResponse result = new PostResponse();

            using (SqlConnection con = new SqlConnection(ClsCommon.ConnectionString()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetMasterAttachment", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = Modal.ID ?? 0;
                        command.Parameters.Add("@filename", SqlDbType.VarChar).Value = Modal.FileName ?? "";
                        command.Parameters.Add("@contenttype", SqlDbType.VarChar).Value = Modal.FileExt ?? "";
                        command.Parameters.Add("@tableid", SqlDbType.Int).Value = Modal.tableid ?? 0;
                        command.Parameters.Add("@TableName", SqlDbType.VarChar).Value = Modal.TableName ?? "";
                        command.Parameters.Add("@Description", SqlDbType.VarChar).Value = Modal.Description ?? "";
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = Modal.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = Modal.IPAddress;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = Convert.ToInt32(reader["STATUS"]);
                                result.SuccessMessage = reader["MESSAGE"].ToString();
                                if (result.StatusCode > 0)
                                {
                                    result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Common_SPU.LogError("Error during fnGetUpdateColumnResponse. The query was executed :", ex.ToString(), "spu_SetUpdateColumn_Common()", "Common_SPU", "Common_SPU", Modal.LoginID, Modal.IPAddress);
                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;

        }

        public static PostResponse fnSetEMP_TalentPool_Attachments(FileResponse Modal)
        {
            PostResponse result = new PostResponse();

            using (SqlConnection con = new SqlConnection(ClsCommon.ConnectionString()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetEMP_TalentPool_Attachments_App", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = Modal.ID ?? 0;
                        command.Parameters.Add("@TPID", SqlDbType.Int).Value = Modal.tableid ?? 0;
                        command.Parameters.Add("@Doctype", SqlDbType.VarChar).Value = Modal.Doctype ?? "";
                        command.Parameters.Add("@filename", SqlDbType.VarChar).Value = Modal.FileName ?? "";
                        command.Parameters.Add("@contenttype", SqlDbType.VarChar).Value = Modal.FileExt ?? "";
                        command.Parameters.Add("@Description", SqlDbType.VarChar).Value = Modal.Description ?? "";
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = Modal.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = Modal.IPAddress;
                        command.Parameters.Add("@EntrySource", SqlDbType.VarChar).Value = EntrySource;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = Convert.ToInt32(reader["STATUS"]);
                                result.SuccessMessage = reader["MESSAGE"].ToString();
                                if (result.StatusCode > 0)
                                {
                                    result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Common_SPU.LogError("Error during SetEMP_TalentPool_Attachments. The query was executed :", ex.ToString(), "SetEMP_TalentPool_Attachments()", "Common_SPU", "Common_SPU", Modal.LoginID, Modal.IPAddress);
                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;

        }


        public static PostResponse fnSetMasterAttachment_SSR(FileResponse Modal)
        {
            PostResponse result = new PostResponse();

            using (SqlConnection con = new SqlConnection(ClsCommon.ConnectionString()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetMasterAttachment_SSR", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = Modal.ID ?? 0;
                        command.Parameters.Add("@filename", SqlDbType.VarChar).Value = Modal.FileName ?? "";
                        command.Parameters.Add("@contenttype", SqlDbType.VarChar).Value = Modal.FileExt ?? "";
                        command.Parameters.Add("@tableid", SqlDbType.Int).Value = Modal.tableid ?? 0;
                        command.Parameters.Add("@TableName", SqlDbType.VarChar).Value = Modal.TableName ?? "";
                        command.Parameters.Add("@Description", SqlDbType.VarChar).Value = Modal.Description ?? "";
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = Modal.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = Modal.IPAddress;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = Convert.ToInt32(reader["STATUS"]);
                                result.SuccessMessage = reader["MESSAGE"].ToString();
                                if (result.StatusCode > 0)
                                {
                                    result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Common_SPU.LogError("Error during fnSetMasterAttachment_SSR. The query was executed :", ex.ToString(), "spu_SetUpdateColumn_Common()", "Common_SPU", "Common_SPU", Modal.LoginID, Modal.IPAddress);
                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;

        }


        public static PostResponse fnSetMasterAttachment_TL(FileResponse Modal)
        {
            PostResponse result = new PostResponse();

            using (SqlConnection con = new SqlConnection(ClsCommon.ConnectionString()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetMasterAttachment_TL", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = Modal.ID ?? 0;
                        command.Parameters.Add("@filename", SqlDbType.VarChar).Value = Modal.FileName ?? "";
                        command.Parameters.Add("@contenttype", SqlDbType.VarChar).Value = Modal.FileExt ?? "";
                        command.Parameters.Add("@tableid", SqlDbType.Int).Value = Modal.tableid ?? 0;
                        command.Parameters.Add("@TableName", SqlDbType.VarChar).Value = Modal.TableName ?? "";
                        command.Parameters.Add("@Description", SqlDbType.VarChar).Value = Modal.Description ?? "";
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = Modal.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = Modal.IPAddress;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = Convert.ToInt32(reader["STATUS"]);
                                result.SuccessMessage = reader["MESSAGE"].ToString();
                                if (result.StatusCode > 0)
                                {
                                    result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Common_SPU.LogError("Error during fnSetMasterAttachment_TL. The query was executed :", ex.ToString(), "spu_SetUpdateColumn_Common()", "Common_SPU", "Common_SPU", Modal.LoginID, Modal.IPAddress);
                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;

        }

        public static PostResponse fnSetProfilePic(FileResponse Modal)
        {
            PostResponse result = new PostResponse();

            using (SqlConnection con = new SqlConnection(ClsCommon.ConnectionString()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetProfilePic", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@filename", SqlDbType.VarChar).Value = Modal.FileName ?? "";
                        command.Parameters.Add("@contenttype", SqlDbType.VarChar).Value = Modal.FileExt ?? "";
                        command.Parameters.Add("@tableid", SqlDbType.Int).Value = Modal.tableid ?? 0;
                        command.Parameters.Add("@TableName", SqlDbType.VarChar).Value = Modal.TableName ?? "";
                        command.Parameters.Add("@Description", SqlDbType.VarChar).Value = Modal.Description ?? "";
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = Modal.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = Modal.IPAddress;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = Convert.ToInt32(reader["STATUS"]);
                                result.SuccessMessage = reader["MESSAGE"].ToString();
                                if (result.StatusCode > 0)
                                {
                                    result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Common_SPU.LogError("Error during fnSetProfilePic. The query was executed :", ex.ToString(), "spu_SetProfilePic()", "Common_SPU", "Common_SPU", Modal.LoginID, Modal.IPAddress);
                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;

        }


        public static PostResponse fnDelRecord(GetResponse Modal)
        {
            PostResponse result = new PostResponse();

            using (SqlConnection con = new SqlConnection(ClsCommon.ConnectionString()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_DelRecord", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = Modal.ID;
                        command.Parameters.Add("@Doctype", SqlDbType.VarChar).Value = Modal.Doctype ?? "";
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = Modal.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = Modal.IPAddress;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = Convert.ToInt32(reader["STATUS"]);
                                result.SuccessMessage = reader["MESSAGE"].ToString();
                                if (result.StatusCode > 0)
                                {
                                    result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Common_SPU.LogError("Error during fnGetUpdateColumnResponse. The query was executed :", ex.ToString(), "spu_SetUpdateColumn_Common()", "Common_SPU", "Common_SPU", Modal.LoginID, Modal.IPAddress);
                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;

        }

        public static PostResponse fnGetUpdateColumnResponse(GetUpdateColumnResponse Modal)
        {
            PostResponse result = new PostResponse();

            using (SqlConnection con = new SqlConnection(ClsCommon.ConnectionString()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetUpdateColumn_Common", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = Modal.ID;
                        command.Parameters.Add("@Value", SqlDbType.VarChar).Value = Modal.Value ?? "";
                        command.Parameters.Add("@IsActive_Reason", SqlDbType.VarChar).Value = Modal.Reason ?? "";
                        command.Parameters.Add("@Doctype", SqlDbType.VarChar).Value = Modal.Doctype ?? "";
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = Modal.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = Modal.IPAddress;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = Convert.ToInt32(reader["STATUS"]);
                                result.SuccessMessage = reader["MESSAGE"].ToString();
                                if (result.StatusCode > 0)
                                {
                                    result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Common_SPU.LogError("Error during fnGetUpdateColumnResponse. The query was executed :", ex.ToString(), "spu_SetUpdateColumn_Common()", "Common_SPU", "Common_SPU", Modal.LoginID, Modal.IPAddress);
                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;

        }


        public static PostResponse fnSetAddress(Address Modal)
        {
            PostResponse result = new PostResponse();

            using (SqlConnection con = new SqlConnection(ClsCommon.ConnectionString()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetAddress", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@AddressID", SqlDbType.Int).Value = Modal.AddressID ?? 0;
                        command.Parameters.Add("@Doctype", SqlDbType.VarChar).Value = Modal.Doctype ?? "";
                        command.Parameters.Add("@tableid", SqlDbType.Int).Value = Modal.TableID ?? 0;
                        command.Parameters.Add("@TableName", SqlDbType.VarChar).Value = Modal.TableName ?? "";
                        command.Parameters.Add("@CountryID", SqlDbType.Int).Value = Modal.CountryID ?? 0;
                        command.Parameters.Add("@StateID", SqlDbType.Int).Value = Modal.StateID ?? 0;
                        command.Parameters.Add("@CityID", SqlDbType.Int).Value = Modal.CityID ?? 0;
                        command.Parameters.Add("@Address1", SqlDbType.VarChar).Value = Modal.Address1 ?? "";
                        command.Parameters.Add("@Address2", SqlDbType.VarChar).Value = Modal.Address2 ?? "";
                        command.Parameters.Add("@Location", SqlDbType.VarChar).Value = Modal.Location ?? "";
                        command.Parameters.Add("@Phone", SqlDbType.VarChar).Value = Modal.Phone ?? "";
                        command.Parameters.Add("@Zipcode", SqlDbType.VarChar).Value = Modal.Zipcode ?? "";
                        command.Parameters.Add("@IsActive", SqlDbType.VarChar).Value = 1;
                        command.Parameters.Add("@Priority", SqlDbType.VarChar).Value = Modal.Priority ?? 0;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = Modal.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = Modal.IPAddress;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = Convert.ToInt32(reader["STATUS"]);
                                result.SuccessMessage = reader["MESSAGE"].ToString();
                                if (result.StatusCode > 0)
                                {
                                    result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Common_SPU.LogError("Error during fnGetUpdateColumnResponse. The query was executed :", ex.ToString(), "spu_SetUpdateColumn_Common()", "Common_SPU", "Common_SPU", Modal.LoginID, Modal.IPAddress);
                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;

        }

        public static PostResponse fnSetBank(Bank Modal)
        {
            PostResponse result = new PostResponse();

            using (SqlConnection con = new SqlConnection(ClsCommon.ConnectionString()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetBank", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@BankID", SqlDbType.Int).Value = Modal.BankID ?? 0;
                        command.Parameters.Add("@Doctype", SqlDbType.VarChar).Value = Modal.Doctype ?? "";
                        command.Parameters.Add("@EMPID", SqlDbType.Int).Value = Modal.EMPID ?? 0;
                        command.Parameters.Add("@BankName", SqlDbType.VarChar).Value = Modal.BankName ?? "";
                        command.Parameters.Add("@AccountNo", SqlDbType.VarChar).Value = Modal.AccountNo ?? "";
                        command.Parameters.Add("@IFSCCode", SqlDbType.VarChar).Value = Modal.IFSCCode ?? "";
                        command.Parameters.Add("@BranchName", SqlDbType.VarChar).Value = Modal.BranchName ?? "";
                        command.Parameters.Add("@IsActive", SqlDbType.VarChar).Value = 1;
                        command.Parameters.Add("@Priority", SqlDbType.VarChar).Value = Modal.Priority ?? 0;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = Modal.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = Modal.IPAddress;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = Convert.ToInt32(reader["STATUS"]);
                                result.SuccessMessage = reader["MESSAGE"].ToString();
                                if (result.StatusCode > 0)
                                {
                                    result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Common_SPU.LogError("Error during fnGetUpdateColumnResponse. The query was executed :", ex.ToString(), "spu_SetUpdateColumn_Common()", "Common_SPU", "Common_SPU", Modal.LoginID, Modal.IPAddress);
                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;

        }

        public static PostResponse fnGetCheckRecordExist(GetRecordExitsResponse Modal)
        {
            PostResponse result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.ConnectionString()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_GetCheckRecordExist", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = Modal.ID;
                        command.Parameters.Add("@Value", SqlDbType.VarChar).Value = Modal.Value ?? "";
                        command.Parameters.Add("@Doctype", SqlDbType.VarChar).Value = Modal.Doctype ?? "";
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = Convert.ToInt32(reader["STATUS"]);
                                result.SuccessMessage = reader["MESSAGE"].ToString();
                                if (result.StatusCode > 0)
                                {
                                    result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Common_SPU.LogError("Error during fnGetCheckRecordExist. The query was executed :", ex.ToString(), "spu_GetCheckURLExist()", "Common_SPU", "Common_SPU", Modal.LoginID, Modal.IPAddress);
                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;

        }

        public static PostResponse fnSetAttendenceLog(MarkAttendence modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.ConnectionString()))
            {
                try
                {
                    float Latitude = 0, Longitude = 0;
                    float.TryParse(modal.Latitude, out Latitude);
                    float.TryParse(modal.Longitude, out Longitude);
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetAttendence_Log", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Location", SqlDbType.VarChar).Value = modal.Location ?? "";
                        command.Parameters.Add("@Latitude", SqlDbType.Float).Value = Latitude;
                        command.Parameters.Add("@Longitude", SqlDbType.Float).Value = Longitude;
                        command.Parameters.Add("@Error", SqlDbType.VarChar).Value = modal.Error ?? "";
                        command.Parameters.Add("@Notes", SqlDbType.VarChar).Value = modal.Notes ?? "";
                        command.Parameters.Add("@Flag_Doctype", SqlDbType.VarChar).Value = modal.Flag_Doctype ?? "";
                        command.Parameters.Add("@Flag_Reason", SqlDbType.VarChar).Value = modal.Flag_Reason ?? "";
                        command.Parameters.Add("@StatusID", SqlDbType.Int).Value = modal.StatusID ?? 0;
                        command.Parameters.Add("@AttachmentID", SqlDbType.Int).Value = modal.AttachmentID;
                        command.Parameters.Add("@EMPID", SqlDbType.Int).Value = modal.EMPID;
                        command.Parameters.Add("@IsActive", SqlDbType.Int).Value = 1;
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = modal.Priority ?? 0;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = modal.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = modal.IPAddress;
                        command.Parameters.Add("@EntrySource", SqlDbType.VarChar).Value = EntrySource;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["StatusCode"]);
                                Result.Status = Convert.ToBoolean(reader["STATUS"]);
                                Result.SuccessMessage = reader["MESSAGE"].ToString();
                                Result.AdditionalMessage = reader["AdditionalMessage"].ToString();
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


        public static PostResponse fnSetCustomer(Customer Modal)
        {
            PostResponse result = new PostResponse();

            using (SqlConnection con = new SqlConnection(ClsCommon.ConnectionString()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetCustomer", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@CustomerID", SqlDbType.Int).Value = Modal.CustomerID ?? 0;
                        command.Parameters.Add("@Name", SqlDbType.VarChar).Value = Modal.Name ?? "";
                        command.Parameters.Add("@Phone", SqlDbType.VarChar).Value = Modal.Phone ?? "";
                        command.Parameters.Add("@Email", SqlDbType.VarChar).Value = Modal.Email ?? "";
                        command.Parameters.Add("@IsActive", SqlDbType.VarChar).Value = 1;
                        command.Parameters.Add("@Priority", SqlDbType.VarChar).Value = Modal.Priority ?? 0;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = Modal.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = Modal.IPAddress ?? "";
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = Convert.ToInt32(reader["STATUS"]);
                                result.SuccessMessage = reader["MESSAGE"].ToString();
                                if (result.StatusCode > 0)
                                {
                                    result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Common_SPU.LogError("Error during fnSetCustomer. The query was executed :", ex.ToString(), "spu_SetCustomer()", "Common_SPU", "Common_SPU", Modal.LoginID, Modal.IPAddress);
                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;

        }

        public static DataSet GetDashboard_TL(GetResponse Modal)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] oparam = new SqlParameter[1];
                oparam[0] = new SqlParameter("@LoginID", Modal.LoginID);
                ds = clsDataBaseHelper.ExecuteDataSet("spu_GetDashboard_TL", oparam);
            }
            catch (Exception ex)
            {
            }
            return ds;

        }

        public static DataSet GetSSRListToday(GetResponse Modal)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] oparam = new SqlParameter[2];
                oparam[0] = new SqlParameter("@LoginID", Modal.LoginID);
                oparam[1] = new SqlParameter("@Doctype", Modal.Doctype ?? "");
                ds = clsDataBaseHelper.ExecuteDataSet("spu_GetSSRList_Today", oparam);
            }
            catch (Exception ex)
            {
            }
            return ds;

        }

        public static DataSet GetSales_DateWise(GetResponse Modal)
        {
            DataSet ds = new DataSet();
            try
            {
                DateTime dt;
                DateTime.TryParse(Modal.Date, out dt);

                SqlParameter[] oparam = new SqlParameter[2];
                oparam[0] = new SqlParameter("@LoginID", Modal.LoginID);
                oparam[1] = new SqlParameter("@Date", dt.ToString("dd-MMM-yyyy"));
                ds = clsDataBaseHelper.ExecuteDataSet("spu_GetSales_DateWise", oparam);
            }
            catch (Exception ex)
            {
            }
            return ds;

        }

        public static DashboardItems.SSR GetDashboard_SSR(GetResponse Modal)
        {
            DashboardItems.SSR result = new DashboardItems.SSR();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.ConnectionString()))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetDashboard_SSR", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<DashboardItems.SSR>().FirstOrDefault();
                        if (result == null)
                        {
                            result = new DashboardItems.SSR();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.ProductList = reader.Read<DashboardItems.ProductDetails>().ToList();
                        }


                        if (!reader.IsConsumed)
                        {
                            result.PunchStatusModal = reader.Read<Attendance_Log.PunchStatus>().FirstOrDefault();
                            if (result.PunchStatusModal == null)
                            {
                                result.PunchStatusModal = new Attendance_Log.PunchStatus();
                            }
                        }
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetDashboard_SSR", "spu_GetDashboard_SSR", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }



        public static Profile GetProfile(GetResponse Modal)
        {
            Profile result = new Profile();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.ConnectionString()))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetUserProfile", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Profile>().FirstOrDefault();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetProfile", "spu_GetUserProfile", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public static List<Calender.Events> GetCalenderEvents(GetResponse modal)
        {
            List<Calender.Events> result = new List<Calender.Events>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.ConnectionString()))
                {
                    var param = new DynamicParameters();
                    param.Add("@Doctype", dbType: DbType.String, value: modal.Doctype, direction: ParameterDirection.Input);
                    param.Add("@LoginId", dbType: DbType.Int32, value: modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetCalenderEvents", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<Calender.Events>().ToList();
                    }
                    DBContext.Close();

                    if (result != null)
                    {
                        for (int i = 0; i < result.Count; i++)
                        {
                            result[i].className = result[i].title.Replace(" ", "");
                            result[i].start = result[i].date.ToString("yyyy-MM-dd");
                            result[i].end = result[i].date.ToString("yyyy-MM-dd");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError("Error during GetCalenderEvents. The query was executed :", ex.ToString(), "spu_GetCalenderEvents()", "Common_SPU", "Common_SPU", modal.LoginID, modal.IPAddress);

            }
            return result;
        }

        public static List<ConfigSetting> GetConfigSetting(GetResponse modal)
        {
            List<ConfigSetting> result = new List<ConfigSetting>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.ConnectionString()))
                {
                    var param = new DynamicParameters();
                    param.Add("@ConfigKey", dbType: DbType.String, value: modal.Doctype ?? "", direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetConfigSetting", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<ConfigSetting>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError("Error during GetConfigSetting. The query was executed :", ex.ToString(), "spu_GetConfigSetting()", "Common_SPU", "Common_SPU", modal.LoginID, modal.IPAddress);

            }
            return result;
        }

        public static PostResponse fnSetContactUs_Query(ContactUs_Query.AddQuery Modal)
        {
            PostResponse result = new PostResponse();

            using (SqlConnection con = new SqlConnection(ClsCommon.ConnectionString()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetContactUs_Query", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Name", SqlDbType.VarChar).Value = Modal.Name ?? "";
                        command.Parameters.Add("@Phone", SqlDbType.VarChar).Value = Modal.Phone ?? "";
                        command.Parameters.Add("@Email", SqlDbType.VarChar).Value = Modal.Email ?? "";
                        command.Parameters.Add("@Category", SqlDbType.VarChar).Value = Modal.Category ?? "";
                        command.Parameters.Add("@Subject", SqlDbType.VarChar).Value = Modal.Subject ?? "";
                        command.Parameters.Add("@Description", SqlDbType.VarChar).Value = Modal.Description ?? "";
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = Modal.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = Modal.IPAddress ?? "";
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = Convert.ToInt32(reader["STATUS"]);
                                result.SuccessMessage = reader["MESSAGE"].ToString();
                                if (result.StatusCode > 0)
                                {
                                    result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Common_SPU.LogError("Error during fnSetCustomer. The query was executed :", ex.ToString(), "spu_SetCustomer()", "Common_SPU", "Common_SPU", Modal.LoginID, Modal.IPAddress);
                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;

        }



        public static PostResponse fnSetAttendence(string Date, GetResponse modal)
        {
            PostResponse result = new PostResponse();

            using (SqlConnection con = new SqlConnection(ClsCommon.ConnectionString()))
            {
                try
                {
                    DateTime dt;
                    DateTime.TryParse(Date, out dt);

                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetAttendence", con))
                    {
                        command.CommandTimeout = 180;
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Date", SqlDbType.VarChar).Value = dt.ToString("dd-MMM-yyyy");
                        command.Parameters.Add("@LoginID", SqlDbType.Int).Value = modal.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = modal.IPAddress;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = Convert.ToInt32(reader["STATUS"]);
                                result.SuccessMessage = reader["MESSAGE"].ToString();
                                if (result.StatusCode > 0)
                                {
                                    result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Common_SPU.LogError("Error during fnSetAttendenceFinal. The query was executed :", ex.ToString(), "spu_SetAttendenceFinal()", "Common_SPU", "Common_SPU", modal.LoginID, "");
                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;

        }


        public static SMTPMail GetMail_Data(GetResponse Modal)
        {
            SMTPMail result = new SMTPMail();
            result.TemplateData = new Template();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.ConnectionString()))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@Doctype", dbType: DbType.String, value: Modal.Doctype ?? "", direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int32, value: Modal.LoginID, direction: ParameterDirection.Input);
                    param.Add("@Param1", dbType: DbType.String, value: Modal.Param1 ?? "", direction: ParameterDirection.Input);
                    param.Add("@Param2", dbType: DbType.String, value: Modal.Param2 ?? "", direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetMail_Data", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<SMTPMail>().FirstOrDefault();
                        if (!reader.IsConsumed)
                        {
                            result.TemplateData = reader.Read<Template>().FirstOrDefault();
                        }
                        if (result.TemplateData != null)
                        {
                            if (!reader.IsConsumed)
                            {
                                result.TemplateData.ConfigSettingList = reader.Read<ConfigSetting>().ToList();
                            }
                        }

                        if (!reader.IsConsumed)
                        {
                            result.AutoReportUsersList = reader.Read<AutoReportUsers>().ToList();
                        }


                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetProfile", "spu_GetUserProfile", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }


        public static List<AutoReportUsers> GetLinkedHierarchy_Users(GetResponse Modal)
        {
            List<AutoReportUsers> result = new List<AutoReportUsers>();

            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.ConnectionString()))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@LoginID", dbType: DbType.Int32, value: Modal.LoginID, direction: ParameterDirection.Input);
                    param.Add("@Doctype", dbType: DbType.String, value: Modal.Doctype ?? "", direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetLinkedHierarchy_Users", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<AutoReportUsers>().ToList();

                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetLinkedHierarchy_Users", "spu_GetLinkedHierarchy_Users", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }


        public static DataSet GetCustomerList(Tab.Approval Modal)
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
                ds = clsDataBaseHelper.ExecuteDataSet("spu_GetCustomerList", oparam);
            }
            catch (Exception ex)
            {
            }
            return ds;

        }
        public static DataSet GetAttendenceFinal_EMPWise(GetResponse Modal)
        {
            DataSet ds = new DataSet();
            try
            {
                DateTime dt;
                DateTime.TryParse(Modal.Date, out dt);
                SqlParameter[] oparam = new SqlParameter[4];
                oparam[0] = new SqlParameter("@EMPID", Modal.ID);
                oparam[1] = new SqlParameter("@Month", dt.Month);
                oparam[2] = new SqlParameter("@Year", dt.Year);
                oparam[3] = new SqlParameter("@LoginID", Modal.LoginID);
                ds = clsDataBaseHelper.ExecuteDataSet("spu_GetAttendenceFinal_EMPWise", oparam);
            }
            catch (Exception ex)
            {
            }
            return ds;

        }

        public static DataSet GetPJPEntry_Expense(Tab.Approval Modal)
        {
            DataSet ds = new DataSet();
            try
            {
                DateTime dt;
                DateTime.TryParse(Modal.Month, out dt);
                SqlParameter[] oparam = new SqlParameter[3];
                oparam[0] = new SqlParameter("@Date", dt.ToString("dd-MMM-yyyy"));
                oparam[1] = new SqlParameter("@LoginID", Modal.LoginID);
                oparam[2] = new SqlParameter("@Approved", Modal.Approved);
                ds = clsDataBaseHelper.ExecuteDataSet("spu_GetPJPEntry_Expense", oparam);
            }
            catch (Exception ex)
            {
            }
            return ds;

        }
        public static PostResponse fnSetTravel_Attachments(FileResponse Modal)
        {
            PostResponse result = new PostResponse();

            using (SqlConnection con = new SqlConnection(ClsCommon.ConnectionString()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetTravel_Attachments", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = Modal.ID ?? 0;
                        command.Parameters.Add("@TRID", SqlDbType.Int).Value = Modal.tableid ?? 0;
                        command.Parameters.Add("@filename", SqlDbType.VarChar).Value = Modal.FileName ?? "";
                        command.Parameters.Add("@contenttype", SqlDbType.VarChar).Value = Modal.FileExt ?? "";
                        command.Parameters.Add("@Description", SqlDbType.VarChar).Value = Modal.Description ?? "";
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = Modal.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = Modal.IPAddress;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = Convert.ToInt32(reader["STATUS"]);
                                result.SuccessMessage = reader["MESSAGE"].ToString();
                                if (result.StatusCode > 0)
                                {
                                    result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Common_SPU.LogError("Error during spu_SetTravelRequests_Attachments. The query was executed :", ex.ToString(), "spu_SetUpdateColumn_Common()", "Common_SPU", "Common_SPU", Modal.LoginID, Modal.IPAddress);
                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;

        }


        public static PostResponse fnSetEMP_Flags_Mismatch_Reason(FlagsMismatchReason.Add Modal)
        {
            PostResponse result = new PostResponse();

            using (SqlConnection con = new SqlConnection(ClsCommon.ConnectionString()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetEMP_Flags_Mismatch_Reason", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@EMPID", SqlDbType.Int).Value = Modal.EMPID;
                        command.Parameters.Add("@Doctype", SqlDbType.VarChar).Value = Modal.Doctype ?? "";
                        command.Parameters.Add("@Date", SqlDbType.VarChar).Value = Modal.Date ?? "";
                        command.Parameters.Add("@Reason", SqlDbType.VarChar).Value = Modal.Reason ?? "";

                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = Modal.Priority ?? 0;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = Modal.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = Modal.IPAddress;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.Status = Convert.ToBoolean(reader["STATUS"]);
                                result.SuccessMessage = reader["MESSAGE"].ToString();
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Common_SPU.LogError("Error during fnSetEMP_Flags_Mismatch_Reason. The query was executed :", ex.ToString(), "SetEMP_Flags_Mismatch_Reason()", "Common_SPU", "Common_SPU", 0, "");
                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;

        }

        public static PostResponse fnAutoLeaveAccrual(string Date, GetResponse modal)
        {
            PostResponse result = new PostResponse();

            using (SqlConnection con = new SqlConnection(ClsCommon.ConnectionString()))
            {
                try
                {
                    DateTime dt;
                    DateTime.TryParse(Date, out dt);

                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_AutoLeaveAccrual", con))
                    {
                        command.CommandTimeout = 180;
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Date", SqlDbType.VarChar).Value = dt.ToString("dd-MMM-yyyy");
                        command.Parameters.Add("@LoginID", SqlDbType.Int).Value = modal.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = modal.IPAddress;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = Convert.ToInt32(reader["STATUS"]);
                                result.SuccessMessage = reader["MESSAGE"].ToString();
                                if (result.StatusCode > 0)
                                {
                                    result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Common_SPU.LogError("Error during fnAutoLeaveAccrual. The query was executed :", ex.ToString(), "spu_AutoLeaveAccrual()", "Common_SPU", "Common_SPU", modal.LoginID, "");
                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;

        }

        public static DataSet GetCompetitionSummary_Report(Tab.Approval Modal)
        {
            DataSet ds = new DataSet();
            try
            {
                DateTime dt;
                DateTime.TryParse(Modal.Month, out dt);
                SqlParameter[] oparam = new SqlParameter[2];
                oparam[0] = new SqlParameter("@Date", dt.ToString("dd-MMM-yyyy"));
                oparam[1] = new SqlParameter("@LoginID", Modal.LoginID);
                ds = clsDataBaseHelper.ExecuteDataSet("spu_GetCompetitionSummary_Report", oparam);
            }
            catch (Exception ex)
            {
            }
            return ds;

        }


        public static List<DashboardItems.HeaderCount> GetDashboard_Headers(GetResponse Modal)
        {
            List<DashboardItems.HeaderCount> result = new List<DashboardItems.HeaderCount>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.ConnectionString()))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetDashboard_Headers", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<DashboardItems.HeaderCount>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetDashboard_Headers", "spu_GetDashboard_Headers", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public static DataSet GetDateConfigrationList(Tab.Approval Modal)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] oparam = new SqlParameter[2];
                oparam[0] = new SqlParameter("@FinID", Modal.ID ?? 0);
                oparam[1] = new SqlParameter("@LoginID", Modal.LoginID);
                ds = clsDataBaseHelper.ExecuteDataSet("spu_GetDateConfigrationList", oparam);
            }
            catch (Exception ex)
            {
            }
            return ds;

        }

        public static PostResponse fnSetUserCurrent_Location(UserCurrent_Location modal)
        {
            PostResponse result = new PostResponse();

            using (SqlConnection con = new SqlConnection(ClsCommon.ConnectionString()))
            {
                try
                {
                    float Latitude = 0, Longitude = 0;
                    float.TryParse(modal.Latitude, out Latitude);
                    float.TryParse(modal.Longitude, out Longitude);
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetUserCurrent_Location", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@EMPID", SqlDbType.Float).Value = modal.EMPID;
                        command.Parameters.Add("@Location", SqlDbType.VarChar).Value = modal.Location ?? "";
                        command.Parameters.Add("@Latitude", SqlDbType.Float).Value = Latitude;
                        command.Parameters.Add("@Longitude", SqlDbType.Float).Value = Longitude;
                        command.Parameters.Add("@Error", SqlDbType.VarChar).Value = modal.Error ?? "";
                        command.Parameters.Add("@Notes", SqlDbType.VarChar).Value = modal.Notes ?? "";
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = modal.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = modal.IPAddress;
                        command.Parameters.Add("@EntrySource", SqlDbType.VarChar).Value = EntrySource;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = Convert.ToInt32(reader["StatusCode"]);
                                result.Status = Convert.ToBoolean(reader["STATUS"]);
                                result.SuccessMessage = reader["MESSAGE"].ToString();
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Common_SPU.LogError("Error during spu_SetUserCurrent_Location. The query was executed :", ex.ToString(), "spu_SetUserCurrent_Location()", "Common_SPU", "Common_SPU", modal.LoginID, "");
                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;

        }

        public static DataSet GetAuto_ReportData(Tab.Approval Modal)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] oparam = new SqlParameter[2];
                oparam[0] = new SqlParameter("@Date", Modal.Month);
                oparam[1] = new SqlParameter("@LoginID", Modal.LoginID);
                ds = clsDataBaseHelper.ExecuteDataSet("spu_GetAuto_ReportData", oparam);
            }
            catch (Exception ex)
            {
            }
            return ds;

        }

        public static List<AutoReport.User> GetAutoMail_UsersList()
        {
            List<AutoReport.User> result = new List<AutoReport.User>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.ConnectionString()))
                {
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetAutoMail_Users", commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<AutoReport.User>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError("Error during GetAutoReport_LogList. The query was executed :", ex.ToString(), "spu_GetAnnouncementList()", "ToolsModal", "ToolsModal", 0, "");

            }
            return result;
        }

        public static PostResponse fnSetAutoReport_Log(long LoginID, string Date, string EmailID, string Error, int MailFlag, string StatusCode, long createdby, string IPAddress)
        {
            PostResponse result = new PostResponse();

            using (SqlConnection con = new SqlConnection(ClsCommon.ConnectionString()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetAutoReport_Log", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@LoginID", SqlDbType.Int).Value = LoginID;
                        command.Parameters.Add("@Date", SqlDbType.VarChar).Value = Date;
                        command.Parameters.Add("@EmailID", SqlDbType.VarChar).Value = EmailID;
                        command.Parameters.Add("@Error", SqlDbType.VarChar).Value = Error;
                        command.Parameters.Add("@MailFlag", SqlDbType.Int).Value = MailFlag;
                        command.Parameters.Add("@StatusCode", SqlDbType.VarChar).Value = StatusCode;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = createdby;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = IPAddress;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = Convert.ToInt32(reader["StatusCode"]);
                                result.Status = Convert.ToBoolean(reader["STATUS"]);
                                result.SuccessMessage = reader["MESSAGE"].ToString();
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Common_SPU.LogError("Error during spu_SetUserCurrent_Location. The query was executed :", ex.ToString(), "spu_SetUserCurrent_Location()", "Common_SPU", "Common_SPU", LoginID, "");
                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;

        }


        public static List<SalesGraph> GetSales_Graph(GetResponse Modal)
        {
            List<SalesGraph> result = new List<SalesGraph>();

            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.ConnectionString()))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@FinID", dbType: DbType.Int32, value: Modal.ID);
                    param.Add("@Region", dbType: DbType.String, value: Modal.Param1 ?? "");
                    param.Add("@Branch", dbType: DbType.String, value: Modal.Param2 ?? "");
                    param.Add("@LoginID", dbType: DbType.Int32, value: Modal.LoginID);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetSales_Graph", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<SalesGraph>().ToList();

                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetSales_Graph", "spu_GetLinkedHierarchy_Users", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public static DataSet GetUserDeviceList(Tab.Approval Modal)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] oparam = new SqlParameter[1];
                oparam[0] = new SqlParameter("@LoginID", Modal.LoginID);
                ds = clsDataBaseHelper.ExecuteDataSet("spu_GetUserDeviceList", oparam);
            }
            catch (Exception ex)
            {
            }
            return ds;

        }

        public static PostResponse fnSetClearUserDevice(GetResponse modal)
        {
            PostResponse result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.ConnectionString()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetClearUserDevice", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@LoginID", SqlDbType.VarChar).Value = modal.Param1;
                        command.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = modal.Param2 ?? "";
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = modal.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = modal.IPAddress;
                        command.Parameters.Add("@EntrySource", SqlDbType.VarChar).Value = EntrySource;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.Status = Convert.ToBoolean(reader["STATUS"]);
                                result.SuccessMessage = reader["MESSAGE"].ToString();
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Common_SPU.LogError("Error during fnSetClearUserDevice. The query was executed :", ex.ToString(), "spu_SetClearUserDevice()", "Common_SPU", "Common_SPU", modal.LoginID, "");
                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;

        }

        public static DataSet GetMiscellaneousReports(Tab.Approval Modal)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] oparam = new SqlParameter[2];
                oparam[0] = new SqlParameter("@ID", Modal.ID);
                oparam[1] = new SqlParameter("@LoginID", Modal.LoginID);
                ds = clsDataBaseHelper.ExecuteDataSet("spu_GetMiscellaneousReports", oparam);
            }
            catch (Exception ex)
            {
            }
            return ds;

        }

        public static DataSet GetCompetitionEntry_Report(Tab.Approval Modal)
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
                ds = clsDataBaseHelper.ExecuteDataSet("spu_GetCompetitionEntry_Report", oparam);
            }
            catch (Exception ex)
            {
            }
            return ds;

        }

        public PostResponse fnSetEMP_Incentive(Tab.Approval modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.ConnectionString()))
            {
                try
                {
                    DateTime dt;
                    DateTime.TryParse(modal.Month, out dt);
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetEMP_Incentive", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Date", SqlDbType.VarChar).Value = dt.ToString("dd-MMM-yyyy");
                        command.Parameters.Add("@LoginID", SqlDbType.Int).Value = modal.LoginID;
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

        public static Dealer.List GetDealerByID(long DealerID)
        {

            Dealer.List result = new Dealer.List();

            try
            {
                using (var con = new SqlConnection(ClsCommon.ConnectionString()))
                {
                    return con.Query<Dealer.List>("SELECT * FROM Dealer_View WHERE DealerID=@DealerID",
                        param: new { DealerID }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetDealerList", "spu_GetDealerList", "DataModal", 0, "");
            }
            return result;
        }
        public static PostResponse fnSetOnboard_Attachment(FileResponse Modal)
        {
            PostResponse result = new PostResponse();

            using (SqlConnection con = new SqlConnection(ClsCommon.ConnectionString()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetOnboarding_Attachment", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Token", SqlDbType.VarChar).Value = Modal.Token ?? "";
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = Modal.ID ?? 0;
                        command.Parameters.Add("@filename", SqlDbType.VarChar).Value = Modal.FileName ?? "";
                        command.Parameters.Add("@contenttype", SqlDbType.VarChar).Value = Modal.FileExt ?? "";
                        command.Parameters.Add("@Description", SqlDbType.VarChar).Value = Modal.Description ?? "";
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = Modal.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = Modal.IPAddress;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = Convert.ToInt32(reader["STATUS"]);
                                result.SuccessMessage = reader["MESSAGE"].ToString();
                                if (result.StatusCode > 0)
                                {
                                    result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Common_SPU.LogError("Error during fnGetUpdateColumnResponse. The query was executed :", ex.ToString(), "spu_SetUpdateColumn_Common()", "Common_SPU", "Common_SPU", Modal.LoginID, Modal.IPAddress);
                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;

        }


        public static DataSet GetDealer_Export(Tab.Approval Modal)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] oparam = new SqlParameter[1];
                oparam[0] = new SqlParameter("@LoginID", Modal.LoginID);
                ds = clsDataBaseHelper.ExecuteDataSet("spu_GetDealer_Export", oparam);
            }
            catch (Exception ex)
            {
            }
            return ds;

        }

        public static DataSet GetEmployee_Export(Tab.Approval Modal)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] oparam = new SqlParameter[1];
                oparam[0] = new SqlParameter("@LoginID", Modal.LoginID);
                ds = clsDataBaseHelper.ExecuteDataSet("spu_GetEmployee_Export", oparam);
            }
            catch (Exception ex)
            {
            }
            return ds;

        }

        public static DataSet GetAutoReport_CompEntry(Tab.Approval Modal)
        {
            DataSet ds = new DataSet();
            try
            {
                DateTime Date;
                DateTime.TryParse(Modal.Month, out Date);

                SqlParameter[] oparam = new SqlParameter[2];
                oparam[0] = new SqlParameter("@Date", Date.ToString("dd-MMM-yyyy"));
                oparam[1] = new SqlParameter("@LoginID", Modal.LoginID);
                ds = clsDataBaseHelper.ExecuteDataSet("spu_AutoReport_CompEntry", oparam);
            }
            catch (Exception ex)
            {
            }
            return ds;

        }

        public static DataSet GetTradeWiseData(Tab.Approval Modal)
        {
            DataSet ds = new DataSet();
            try
            {
                DateTime Date;
                DateTime.TryParse(Modal.Month, out Date);

                SqlParameter[] oparam = new SqlParameter[2];
                oparam[0] = new SqlParameter("@Date", Date.ToString("dd-MMM-yyyy"));
                oparam[1] = new SqlParameter("@LoginID", Modal.LoginID);
                ds = clsDataBaseHelper.ExecuteDataSet("spu_GetTradeWiseData", oparam);
            }
            catch (Exception ex)
            {
            }
            return ds;

        }

        public static PostResponse fnSetEMPDocuments(FileResponse Modal)
        {
            PostResponse result = new PostResponse();

            using (SqlConnection con = new SqlConnection(ClsCommon.ConnectionString()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetEMPDocuments", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = Modal.ID ?? 0;
                        command.Parameters.Add("@filename", SqlDbType.VarChar).Value = Modal.FileName ?? "";
                        command.Parameters.Add("@contenttype", SqlDbType.VarChar).Value = Modal.FileExt ?? "";
                        command.Parameters.Add("@EMPID", SqlDbType.Int).Value = Modal.tableid ?? 0;
                        command.Parameters.Add("@Description", SqlDbType.VarChar).Value = Modal.Description ?? "";
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = Modal.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = Modal.IPAddress;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = Convert.ToInt32(reader["STATUS"]);
                                result.SuccessMessage = reader["MESSAGE"].ToString();
                                if (result.StatusCode > 0)
                                {
                                    result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Common_SPU.LogError("Error during fnSetEMPDocuments. The query was executed :", ex.ToString(), "spu_SetUpdateColumn_Common()", "Common_SPU", "Common_SPU", Modal.LoginID, Modal.IPAddress);
                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;

        }

        public static List<EMPDocumentList> GetEmployeeDocuments(GetResponse modal)
        {
            List<EMPDocumentList> result = new List<EMPDocumentList>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.ConnectionString()))
                {
                    var param = new DynamicParameters();
                    param.Add("@EMPID", dbType: DbType.String, value: modal.ID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetEMP_Documents", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<EMPDocumentList>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError("Error during GetEmployeeDocumentsList. The query was executed :", ex.ToString(), "spu_GetDropDownList()", "Common_SPU", "Common_SPU", modal.LoginID, modal.IPAddress);

            }
            return result;
        }

        public static DataSet GetMasterDetails(GetResponse Modal)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] oparam = new SqlParameter[3];
                oparam[0] = new SqlParameter("@ID", Modal.ID);
                oparam[1] = new SqlParameter("@Doctype", Modal.Doctype);
                oparam[2] = new SqlParameter("@LoginID", Modal.LoginID);
                ds = clsDataBaseHelper.ExecuteDataSet("spu_GetMasterDetails", oparam);
            }
            catch (Exception ex)
            {
            }
            return ds;

        }


        public static List<EMPDocumentList> GetEMPDocuments_List(Tab.Approval Modal)
        {
            List<EMPDocumentList> result = new List<EMPDocumentList>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.ConnectionString()))
                {
                    var param = new DynamicParameters();
                    param.Add("@EMPIDs", dbType: DbType.String, value: Modal.SeperatedIDs, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.String, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetEMPDocuments_List", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<EMPDocumentList>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError("Error during GetEmployeeDocumentsList. The query was executed :", ex.ToString(), "spu_GetDropDownList()", "Common_SPU", "Common_SPU", Modal.LoginID, Modal.IPAddress);

            }
            return result;
        }


        public static PostResponse fnSetMaster_Attachment_ClientVisit(FileResponse Modal)
        {
            PostResponse result = new PostResponse();

            using (SqlConnection con = new SqlConnection(ClsCommon.ConnectionString()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetMaster_Attachment_ClientVisit", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = Modal.ID ?? 0;
                        command.Parameters.Add("@filename", SqlDbType.VarChar).Value = Modal.FileName ?? "";
                        command.Parameters.Add("@contenttype", SqlDbType.VarChar).Value = Modal.FileExt ?? "";
                        command.Parameters.Add("@tableid", SqlDbType.Int).Value = Modal.tableid ?? 0;
                        command.Parameters.Add("@TableName", SqlDbType.VarChar).Value = Modal.TableName ?? "";
                        command.Parameters.Add("@Description", SqlDbType.VarChar).Value = Modal.Description ?? "";
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = Modal.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = Modal.IPAddress;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = Convert.ToInt32(reader["STATUS"]);
                                result.SuccessMessage = reader["MESSAGE"].ToString();
                                if (result.StatusCode > 0)
                                {
                                    result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Common_SPU.LogError("Error during SetMaster_Attachment_ClientVisit. The query was executed :", ex.ToString(), "SetMaster_Attachment_ClientVisit()", "Common_SPU", "Common_SPU", Modal.LoginID, Modal.IPAddress);
                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;

        }


        public static DataSet GetISDSummaryForMail(GetResponse Modal)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] oparam = new SqlParameter[1];
                oparam[0] = new SqlParameter("@Date", Modal.Date);
                ds = clsDataBaseHelper.ExecuteDataSet("spu_GetISDSummaryForMail", oparam);
            }
            catch (Exception ex)
            {
                Common_SPU.LogError("Error during GetISDDataForMail. The query was executed :", ex.ToString(), "spu_GetISDDataForMail()", "Common_SPU", "Common_SPU", 0, "");

            }
            return ds;

        }

        public static PostResponse fnAuto_SetPJPPlan()
        {
            PostResponse result = new PostResponse();

            using (SqlConnection con = new SqlConnection(ClsCommon.ConnectionString()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_Auto_SetPJPPlan", con))
                    {
                        command.CommandTimeout = 180;
                        command.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = Convert.ToInt32(reader["STATUS"]);
                                result.SuccessMessage = reader["MESSAGE"].ToString();
                                if (result.StatusCode > 0)
                                {
                                    result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Common_SPU.LogError("Error during spu_Auto_SetPJPPlan. The query was executed :", ex.ToString(), "spu_Auto_SetPJPPlan()", "Common_SPU", "Common_SPU", 0, "");
                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;

        }

        public static List<DropDownlist> GetDealerSearchList(DealerSearch modal)
        {
            List<DropDownlist> result = new List<DropDownlist>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.ConnectionString()))
                {
                    var param = new DynamicParameters();
                    param.Add("@DealerTypeID", dbType: DbType.Int64, value: modal.DealerTypeID, direction: ParameterDirection.Input);
                    param.Add("@DealerCategoryID", dbType: DbType.Int64, value: modal.DealerCategoryID, direction: ParameterDirection.Input);
                    param.Add("@RegionID", dbType: DbType.Int64, value: modal.RegionID, direction: ParameterDirection.Input);
                    param.Add("@StateID", dbType: DbType.Int64, value: modal.StateID, direction: ParameterDirection.Input);
                    param.Add("@CityID", dbType: DbType.Int64, value: modal.CityID, direction: ParameterDirection.Input);
                    param.Add("@AreaID", dbType: DbType.Int64, value: modal.AreaID, direction: ParameterDirection.Input);
                    param.Add("@LoginId", dbType: DbType.Int64, value: modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetDealerSearch", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<DropDownlist>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError("Error during GetDropDownList. The query was executed :", ex.ToString(), "spu_GetDropDownList()", "Common_SPU", "Common_SPU", modal.LoginID, modal.IPAddress);

            }
            return result;
        }
        public static DataSet GetAttendanceLog_Change_Export(Tab.Approval Modal)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] oparam = new SqlParameter[3];
                oparam[0] = new SqlParameter("@LoginID", Modal.LoginID);
                oparam[1] = new SqlParameter("@FromDate", Modal.StartDate);
                oparam[2] = new SqlParameter("@ToDate", Modal.EndDate);
                ds = clsDataBaseHelper.ExecuteDataSet("spu_GetAttendanceChange_Log_Export", oparam);
            }
            catch (Exception ex)
            {
            }
            return ds;

        }
        public static DataSet GetPJPReport_Export(Tab.Approval Modal)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] oparam = new SqlParameter[2];
                oparam[0] = new SqlParameter("@LoginID", Modal.LoginID);
                oparam[1] = new SqlParameter("@Month", Modal.StartDate); 
                ds = clsDataBaseHelper.ExecuteDataSet("spu_GetPJPReport_Export", oparam);
            }
            catch (Exception ex)
            {

            }
            return ds;

        }

        public static List<DropDownlist> GetDealerByRouteNumber(DealerSearchNew modal)
        {
            List<DropDownlist> result = new List<DropDownlist>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.ConnectionString()))
                {
                    var param = new DynamicParameters();
                    param.Add("@RouteNumber", dbType: DbType.String, value: modal.RouteNumber, direction: ParameterDirection.Input);
                    param.Add("@LoginId", dbType: DbType.Int64, value: modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetDealerByRouteNumber", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<DropDownlist>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError("Error during GetDropDownList. The query was executed :", ex.ToString(), "spu_GetDropDownList()", "Common_SPU", "Common_SPU", modal.LoginID, modal.IPAddress);

            }
            return result;
        }
        public static DataSet GetMyPJPPlanLists_Export(Tab.Approval Modal)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] oparam = new SqlParameter[2];
                oparam[0] = new SqlParameter("@LoginID", Modal.LoginID);
                oparam[1] = new SqlParameter("@date", Modal.StartDate);
                ds = clsDataBaseHelper.ExecuteDataSet("spu_GetMyPJPPlanLists_Export", oparam);
            }
            catch (Exception ex)
            {

            }
            return ds;

        }
        public static DataSet GetPJPPlanLists_Export(Tab.Approval Modal)
        {
            DataSet ds = new DataSet();
            try
            {
                DateTime dt;
                DateTime.TryParse(Modal.Month, out dt);
                SqlParameter[] oparam = new SqlParameter[4];
                oparam[0] = new SqlParameter("@LoginID", Modal.LoginID);
                oparam[1] = new SqlParameter("@Month", dt.Month);
                oparam[2] = new SqlParameter("@Year", dt.Year);
                oparam[3] = new SqlParameter("@RoleID", Modal.RoleID);
                ds = clsDataBaseHelper.ExecuteDataSet("spu_GetPJPPlanLists_Export", oparam);
            }
            catch (Exception ex)
            {

            }
            return ds;

        }

        public static List<PJPDocumentList> GetPJPDocuments(GetResponse modal)
        {
            List<PJPDocumentList> result = new List<PJPDocumentList>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.ConnectionString()))
                {
                    var param = new DynamicParameters();
                    param.Add("@PJPEntryID", dbType: DbType.String, value: modal.ID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetPJPExp_Documents", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<PJPDocumentList>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError("Error during GetEmployeeDocumentsList. The query was executed :", ex.ToString(), "spu_GetDropDownList()", "Common_SPU", "Common_SPU", modal.LoginID, modal.IPAddress);

            }
            return result;
        }
        public static PostResponse fnSetPJPDocuments(FileResponse Modal)
        {
            PostResponse result = new PostResponse();

            using (SqlConnection con = new SqlConnection(ClsCommon.ConnectionString()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetPJPDocuments", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = Modal.ID ?? 0;
                        command.Parameters.Add("@filename", SqlDbType.VarChar).Value = Modal.FileName ?? "";
                        command.Parameters.Add("@ExpenseType", SqlDbType.VarChar).Value = Modal.ExpenseType ?? "";
                        command.Parameters.Add("@contenttype", SqlDbType.VarChar).Value = Modal.FileExt ?? "";
                        command.Parameters.Add("@PJPEntryID", SqlDbType.Int).Value = Modal.LoginID;
                        command.Parameters.Add("@Amount", SqlDbType.VarChar).Value = Modal.Message ?? "";
                        command.Parameters.Add("@Description", SqlDbType.VarChar).Value = Modal.Description ?? "";
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = Modal.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = Modal.IPAddress;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = Convert.ToInt32(reader["STATUS"]);
                                result.SuccessMessage = reader["MESSAGE"].ToString();
                                if (result.StatusCode > 0)
                                {
                                    result.Status = true;
                                }
                            }
                        }
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Common_SPU.LogError("Error during fnSetEMPDocuments. The query was executed :", ex.ToString(), "spu_SetUpdateColumn_Common()", "Common_SPU", "Common_SPU", Modal.LoginID, Modal.IPAddress);
                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;
        }


        public static PostResponse fnSetPJPLocalExp(PJPExpenses.List Modal)
        {
            PostResponse result = new PostResponse();

            using (SqlConnection con = new SqlConnection(ClsCommon.ConnectionString()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetPJPDocuments", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = Convert.ToInt32(Modal.PJPExpensesID);
                        command.Parameters.Add("@PJPEntryID", SqlDbType.Int).Value = Modal.PJPEntryID;
                        command.Parameters.Add("@Amount", SqlDbType.VarChar).Value = Modal.Exp_Amount ?? "";
                        command.Parameters.Add("@Description", SqlDbType.VarChar).Value = Modal.Description ?? "";
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = Modal.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = Modal.IPAddress;
                        command.Parameters.Add("@DocType", SqlDbType.VarChar).Value = Modal.DocType;
                        command.Parameters.Add("@filename", SqlDbType.VarChar).Value = Modal.FileName ?? "";
                        command.Parameters.Add("@ExpenseType", SqlDbType.VarChar).Value = Modal.ExpenseType ?? "";
                        command.Parameters.Add("@contenttype", SqlDbType.VarChar).Value = Modal.FileExt ?? "";
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = Convert.ToInt32(reader["STATUS"]);
                                result.SuccessMessage = reader["MESSAGE"].ToString();
                                if (result.StatusCode > 0)
                                {
                                    result.Status = true;
                                }
                            }
                        }
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Common_SPU.LogError("Error during fnSetEMPDocuments. The query was executed :", ex.ToString(), "spu_SetUpdateColumn_Common()", "Common_SPU", "Common_SPU", Modal.LoginID, Modal.IPAddress);
                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;
        }

        public static DataSet GetPJPEntriesList_Export(Tab.Approval Modal)
        {
            DataSet ds = new DataSet();
            try
            {
                DateTime dt;
                DateTime.TryParse(Modal.Month, out dt);
                SqlParameter[] oparam = new SqlParameter[4];
                oparam[0] = new SqlParameter("@LoginID", Modal.LoginID);
                oparam[1] = new SqlParameter("@Month", dt.Month);
                oparam[2] = new SqlParameter("@Year", dt.Year);
                oparam[3] = new SqlParameter("@RoleID", Modal.RoleID);
                ds = clsDataBaseHelper.ExecuteDataSet("spu_GetPJPEntriesList_Export", oparam);
            }
            catch (Exception ex)
            {

            }
            return ds;
        }
        public static DataSet GetPJPEntries_Expense(Tab.Approval Modal)
        {
            DataSet ds = new DataSet();
            try
            {
                DateTime dt;
                DateTime.TryParse(Modal.Month, out dt);
                SqlParameter[] oparam = new SqlParameter[5];
                oparam[0] = new SqlParameter("@Month", dt.Month);
                oparam[1] = new SqlParameter("@Year", dt.Year);
                oparam[2] = new SqlParameter("@LoginID", Modal.LoginID);
                oparam[3] = new SqlParameter("@RoleID", Modal.RoleID);
                oparam[4] = new SqlParameter("@Approved", Modal.Approved);
                ds = clsDataBaseHelper.ExecuteDataSet("spu_GetPJPEntries_Expense", oparam);
            }
            catch (Exception ex)
            {
            }
            return ds;

        }
        public static DataSet GetPJPExpense_Export(Tab.Approval Modal)
        {
            DataSet ds = new DataSet();
            try
            {
                DateTime dt;
                DateTime.TryParse(Modal.Month, out dt);
                SqlParameter[] oparam = new SqlParameter[5];
                oparam[0] = new SqlParameter("@Month", dt.Month);
                oparam[1] = new SqlParameter("@Year", dt.Year);
                oparam[2] = new SqlParameter("@LoginID", Modal.LoginID);
                oparam[3] = new SqlParameter("@RoleID", Modal.RoleID);
                oparam[4] = new SqlParameter("@Approved", Modal.Approved);
                ds = clsDataBaseHelper.ExecuteDataSet("spu_GetPJPExpenses_Export", oparam);
            }
            catch (Exception ex)
            {
            }
            return ds;

        }

        public static DataSet GetPJPReports_Export(Tab.Approval Modal)
        {
            DataSet ds = new DataSet();
            try
            {
                DateTime dt;
                DateTime.TryParse(Modal.Month, out dt);
                SqlParameter[] oparam = new SqlParameter[3];
                oparam[0] = new SqlParameter("@LoginID", Modal.LoginID);
                oparam[1] = new SqlParameter("@Month", dt.Month);
                oparam[2] = new SqlParameter("@Year", dt.Year);
                ds = clsDataBaseHelper.ExecuteDataSet("spu_GetPJPReports_Export", oparam);
            }
            catch (Exception ex)
            {

            }
            return ds;

        }
    }
}
