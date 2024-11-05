using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TaxEncryptDecrypt;
using Xamarin.Forms.Xaml;

namespace ePayEssMvc.Repository
{
    public class FlexiRepo
    {


        public List<Flexi.EMPList> GetFlexi_EMPList(string Doctype)
        {
            string Company_Code = SessionInfo.Loginuser.COMPANY_CODE;
            string EMPID = SessionInfo.Loginuser.EMP_ID;
            int commandTimeout = 0;
            List<Flexi.EMPList> result = new List<Flexi.EMPList>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString()))
                {
                    var param = new DynamicParameters();
                    param.Add("@EMPID", dbType: DbType.Int64, value: EMPID, direction: ParameterDirection.Input);
                    param.Add("@Doctype", dbType: DbType.String, value: Doctype, direction: ParameterDirection.Input);
                    param.Add("@COMPANYCODE", dbType: DbType.String, value: Company_Code, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetEMPList", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Flexi.EMPList>().ToList();
                    }
                    DBContext.Close();
                    if (result.Count > 0)
                    {
                        for (int i = 0; i < result.Count; i++)
                        {
                            result[i].EMP_Name = clsEncryptDecrypt.fnDecrypt(result[i].EMP_Name);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Sahaj.LogError(ex.Message.ToString(), ex.ToString(), "GetFlexi_VendorList", "FlexiRepo", "", "", Company_Code, string.IsNullOrEmpty(SessionInfo.Loginuser.EMP_ID) ? "" : SessionInfo.Loginuser.EMP_ID);
            }
            return result;
        }


        public List<Flexi.VendorList> GetFlexi_VendorList(long EDPS_ID)
        {
            string Company_Code = SessionInfo.Loginuser.COMPANY_CODE;
            int commandTimeout = 0;
            List<Flexi.VendorList> result = new List<Flexi.VendorList>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString()))
                {
                    var param = new DynamicParameters();
                    param.Add("@EDPS_ID", dbType: DbType.Int64, value: EDPS_ID, direction: ParameterDirection.Input);
                    param.Add("@COMPANY_CODE", dbType: DbType.String, value: Company_Code, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetFlexi_VendorList", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Flexi.VendorList>().ToList();
                    }
                    DBContext.Close();
                    if (result.Count > 0)
                    {
                        for (int i = 0; i < result.Count; i++)
                        {
                            result[i].VENDOR_CODE = clsEncryptDecrypt.fnDecrypt(result[i].VENDOR_CODE);
                            result[i].VENDOR_NAME = clsEncryptDecrypt.fnDecrypt(result[i].VENDOR_NAME);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Sahaj.LogError(ex.Message.ToString(), ex.ToString(), "GetFlexi_VendorList", "FlexiRepo", "", "", Company_Code, string.IsNullOrEmpty(SessionInfo.Loginuser.EMP_ID) ? "" : SessionInfo.Loginuser.EMP_ID);
            }
            return result;
        }

        public List<Flexi.FinYear> GetFinYearList(long ID)
        {
            string Company_Code = SessionInfo.Loginuser.COMPANY_CODE;
            int commandTimeout = 0;
            List<Flexi.FinYear> result = new List<Flexi.FinYear>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString()))
                {
                    var param = new DynamicParameters();
                    param.Add("@id", dbType: DbType.Int64, value: ID, direction: ParameterDirection.Input);
                    param.Add("@company_code", dbType: DbType.String, value: Company_Code, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetFinancialYear", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Flexi.FinYear>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Sahaj.LogError(ex.Message.ToString(), ex.ToString(), "GetFinYearList", "FlexiRepo", "", "", Company_Code, string.IsNullOrEmpty(SessionInfo.Loginuser.EMP_ID) ? "" : SessionInfo.Loginuser.EMP_ID);
            }
            return result;
        }


        public List<Flexi.ClaimEntry.OptedEDPSList> GetOptedEDPS(DateTime date,long EMPID)
        {
            string Company_Code = SessionInfo.Loginuser.COMPANY_CODE;
            int commandTimeout = 0;
            List<Flexi.ClaimEntry.OptedEDPSList> result = new List<Flexi.ClaimEntry.OptedEDPSList>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString()))
                {
                    var param = new DynamicParameters();
                    param.Add("@CompanyCode", dbType: DbType.String, value: Company_Code, direction: ParameterDirection.Input);
                    param.Add("@date", dbType: DbType.DateTime, value: date, direction: ParameterDirection.Input);
                    param.Add("@EMPID", dbType: DbType.Int32, value: EMPID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetFlexi_OptedEDPS", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Flexi.ClaimEntry.OptedEDPSList>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Sahaj.LogError(ex.Message.ToString(), ex.ToString(), "GetOptedEDPS", "FlexiRepo", "", "", Company_Code, string.IsNullOrEmpty(SessionInfo.Loginuser.EMP_ID) ? "" : SessionInfo.Loginuser.EMP_ID);
            }
            return result;
        }



        public List<Flexi.EDPSConfig> GetEDPSConfigList(int Approved,long FinID)
        {
            string Company_Code = SessionInfo.Loginuser.COMPANY_CODE;
            int commandTimeout = 0;
            List<Flexi.EDPSConfig> result = new List<Flexi.EDPSConfig>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString()))
                {
                    var param = new DynamicParameters();
                    param.Add("@company_code", dbType: DbType.String, value: Company_Code, direction: ParameterDirection.Input);
                    param.Add("@Approved", dbType: DbType.Int32, value: Approved, direction: ParameterDirection.Input);
                    param.Add("@FinID", dbType: DbType.Int64, value: FinID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetFLEXI_EDPS", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Flexi.EDPSConfig>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Sahaj.LogError(ex.Message.ToString(), ex.ToString(), "GetEDPSConfigList", "FlexiRepo", "", "", Company_Code, string.IsNullOrEmpty(SessionInfo.Loginuser.EMP_ID) ? "" : SessionInfo.Loginuser.EMP_ID);
            }
            return result;
        }

        public PostJSonResult SetFlexiEDPS(Flexi.EDPSConfig model)
        {
            PostJSonResult result = new PostJSonResult();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString()))
            {
                try
                {
                    DataTable dt = new DataTable();
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetFlexiEDPS", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = model.ID;
                        command.Parameters.Add("@ADDITIONAL_INFO", SqlDbType.VarChar).Value = Sahaj.EnsureString(model.ADDITIONAL_INFO);
                        command.Parameters.Add("@CLAIM_INFO", SqlDbType.VarChar).Value = Sahaj.EnsureString(model.CLAIM_INFO);
                        command.Parameters.Add("@REMARKS_REQUIRED", SqlDbType.Int).Value = (string.IsNullOrEmpty(model.REMARKS_REQUIRED) ? 0 : 1);
                        command.Parameters.Add("@EDPS_OPTED", SqlDbType.VarChar, 255).Value = Sahaj.EnsureString(model.EDPS_OPTED);
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
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
                    Sahaj.LogError(ex.Message.ToString(), ex.ToString(), "SetFlexiEDPS", "FlexiRepo", "", "", "", SessionInfo.Loginuser.LOGINID);
                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;
        }


        public List<Flexi.DeclarationPeriod> GetDeclarationPeriodList(long ID, long finyearid)
        {
            string Company_Code = SessionInfo.Loginuser.COMPANY_CODE;
            int commandTimeout = 0;
            List<Flexi.DeclarationPeriod> result = new List<Flexi.DeclarationPeriod>();

            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString()))
                {
                    var param = new DynamicParameters();
                    param.Add("@id", dbType: DbType.Int64, value: ID, direction: ParameterDirection.Input);
                    param.Add("@company_code", dbType: DbType.String, value: Company_Code, direction: ParameterDirection.Input);
                    param.Add("@finyearid", dbType: DbType.Int64, value: finyearid, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetFlexi_DeclarationPeriodList", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Flexi.DeclarationPeriod>().ToList();
                    }
                    if (result.Count > 0)
                    {
                        for (int i = 0; i < result.Count; i++)
                        {
                            result[i].EMP_CODE = clsEncryptDecrypt.fnDecrypt(result[i].EMP_CODE);
                            result[i].EMP_NAME = clsEncryptDecrypt.fnDecrypt(result[i].EMP_NAME);
                            result[i].DESIG_NAME = clsEncryptDecrypt.fnDecrypt(result[i].DESIG_NAME);

                        }
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Sahaj.LogError(ex.Message.ToString(), ex.ToString(), "GetDeclarationPeriodList", "FlexiRepo", "", "", Company_Code, string.IsNullOrEmpty(SessionInfo.Loginuser.EMP_ID) ? "" : SessionInfo.Loginuser.EMP_ID);
            }
            return result;
        }

        public List<Flexi.ClaimPeriod> GetClaimPeriodList(long ID, long finyearid)
        {
            string Company_Code = SessionInfo.Loginuser.COMPANY_CODE;
            int commandTimeout = 0;
            List<Flexi.ClaimPeriod> result = new List<Flexi.ClaimPeriod>();

            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString()))
                {
                    var param = new DynamicParameters();
                    param.Add("@id", dbType: DbType.Int64, value: ID, direction: ParameterDirection.Input);
                    param.Add("@company_code", dbType: DbType.String, value: Company_Code, direction: ParameterDirection.Input);
                    param.Add("@finyearid", dbType: DbType.Int64, value: finyearid, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetFlexi_ClaimPeriod", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Flexi.ClaimPeriod>().ToList();
                    }
                    if (result.Count > 0)
                    {
                        for (int i = 0; i < result.Count; i++)
                        {
                            result[i].EMP_CODE = clsEncryptDecrypt.fnDecrypt(result[i].EMP_CODE);
                            result[i].EMP_NAME = clsEncryptDecrypt.fnDecrypt(result[i].EMP_NAME);
                            result[i].DESIG_NAME = clsEncryptDecrypt.fnDecrypt(result[i].DESIG_NAME);

                        }
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Sahaj.LogError(ex.Message.ToString(), ex.ToString(), "GetClaimPeriodList", "FlexiRepo", "", "", Company_Code, string.IsNullOrEmpty(SessionInfo.Loginuser.EMP_ID) ? "" : SessionInfo.Loginuser.EMP_ID);
            }
            return result;
        }


        public Flexi.DeclarationEntry.Opendates GetFlexi_DeclarationOpenDate(long finyearid)
        {
            string Company_Code = SessionInfo.Loginuser.COMPANY_CODE;
            string EMPID = SessionInfo.Loginuser.EMP_ID;
            int commandTimeout = 0;
            Flexi.DeclarationEntry.Opendates result = new Flexi.DeclarationEntry.Opendates();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString()))
                {
                    var param = new DynamicParameters();
                    param.Add("@EMPID", dbType: DbType.Int64, value: EMPID, direction: ParameterDirection.Input);
                    param.Add("@company_code", dbType: DbType.String, value: Company_Code, direction: ParameterDirection.Input);
                    param.Add("@finyearid", dbType: DbType.Int64, value: finyearid, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetFlexi_DeclarationOpenDate", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Flexi.DeclarationEntry.Opendates>().FirstOrDefault();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Sahaj.LogError(ex.Message.ToString(), ex.ToString(), "GetFlexi_DeclarationOpenDate", "FlexiRepo", "", "", Company_Code, string.IsNullOrEmpty(SessionInfo.Loginuser.EMP_ID) ? "" : SessionInfo.Loginuser.EMP_ID);
            }
            return result;
        }

        public List<Flexi.DeclarationEntry.List> GetDeclarationEntryList(long finyearid,int Approved)
        {
            string Company_Code = SessionInfo.Loginuser.COMPANY_CODE;
            string EMPID = SessionInfo.Loginuser.EMP_ID;
            int commandTimeout = 0;
            List<Flexi.DeclarationEntry.List> result = new List<Flexi.DeclarationEntry.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString()))
                {
                    var param = new DynamicParameters();
                    param.Add("@EMPID", dbType: DbType.Int64, value: EMPID, direction: ParameterDirection.Input);
                    param.Add("@company_code", dbType: DbType.String, value: Company_Code, direction: ParameterDirection.Input);
                    param.Add("@finyearid", dbType: DbType.Int64, value: finyearid, direction: ParameterDirection.Input);
                    param.Add("@Approved", dbType: DbType.Int32, value: Approved, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetFlexi_DeclarationEntryList", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Flexi.DeclarationEntry.List>().ToList();
                    }
                   
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Sahaj.LogError(ex.Message.ToString(), ex.ToString(), "GetClaimPeriodList", "FlexiRepo", "", "", Company_Code, string.IsNullOrEmpty(SessionInfo.Loginuser.EMP_ID) ? "" : SessionInfo.Loginuser.EMP_ID);
            }
            return result;
        }

        public List<Flexi.ClaimEntry.List> GetClaimEntryList(DateTime date, int Approved)
        {
            string Company_Code = SessionInfo.Loginuser.COMPANY_CODE;
            string EMPID = SessionInfo.Loginuser.EMP_ID;
            int commandTimeout = 0;
            List<Flexi.ClaimEntry.List> result = new List<Flexi.ClaimEntry.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString()))
                {
                    var param = new DynamicParameters();
                    param.Add("@EMPID", dbType: DbType.Int64, value: EMPID, direction: ParameterDirection.Input);
                    param.Add("@company_code", dbType: DbType.String, value: Company_Code, direction: ParameterDirection.Input);
                    param.Add("@date", dbType: DbType.DateTime, value: date, direction: ParameterDirection.Input);
                    param.Add("@Approved", dbType: DbType.Int64, value: Approved, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetFlexi_ClaimEntryList", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Flexi.ClaimEntry.List>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Sahaj.LogError(ex.Message.ToString(), ex.ToString(), "GetClaimPeriodList", "FlexiRepo", "", "", Company_Code, string.IsNullOrEmpty(SessionInfo.Loginuser.EMP_ID) ? "" : SessionInfo.Loginuser.EMP_ID);
            }
            return result;
        }


        public Flexi.DeclarationEntry.Add GetFlexi_DeclarationEntry(long ID, long FinYearID)
        {
            string Company_Code = SessionInfo.Loginuser.COMPANY_CODE;
            long EMPID = 0;
            long.TryParse(SessionInfo.Loginuser.EMP_ID, out EMPID);
            int commandTimeout = 0;
            Flexi.DeclarationEntry.Add result = new Flexi.DeclarationEntry.Add();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString()))
                {
                    var param = new DynamicParameters();
                    param.Add("@ID", dbType: DbType.Int64, value: ID, direction: ParameterDirection.Input);
                    param.Add("@EMPID", dbType: DbType.Int64, value: EMPID, direction: ParameterDirection.Input);
                    param.Add("@company_code", dbType: DbType.String, value: Company_Code, direction: ParameterDirection.Input);
                    param.Add("@FinYearID", dbType: DbType.Int64, value: FinYearID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetFlexi_DeclarationEntry", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Flexi.DeclarationEntry.Add>().FirstOrDefault();
                        if (result==null)
                        {
                            result = new Flexi.DeclarationEntry.Add();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.BFDetails = reader.Read<Flexi.DeclarationEntry.FixedEDPS>().FirstOrDefault();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.FixedEDPS= reader.Read<Flexi.DeclarationEntry.FixedEDPS>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.FlexiEDPS = reader.Read<Flexi.DeclarationEntry.FlexiEDPS>().ToList();
                            if(result.FlexiEDPS!=null)
                            {
                                foreach (var item in result.FlexiEDPS)
                                {
                                    item.FLEXI_NPS_PRAN= clsEncryptDecrypt.fnDecrypt(item.FLEXI_NPS_PRAN);
                                    item.FLEXI_NPS_IFSC = clsEncryptDecrypt.fnDecrypt(item.FLEXI_NPS_IFSC);
                                    item.Vendor_Name = clsEncryptDecrypt.fnDecrypt(item.Vendor_Name);

                                }
                            }
                        }
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Sahaj.LogError(ex.Message.ToString(), ex.ToString(), "GetFlexi_DeclarationEntry", "FlexiRepo", "", "", Company_Code, string.IsNullOrEmpty(SessionInfo.Loginuser.EMP_ID) ? "" : SessionInfo.Loginuser.EMP_ID);
            }
            return result;
        }


        public PostJSonResult SetFlexiEmpConfig(long ID, long Finyearid,decimal ANUAL_CTC,decimal PROP_CTC)
        {
            PostJSonResult result = new PostJSonResult();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString()))
            {
                try
                {
                    string Company_Code = SessionInfo.Loginuser.COMPANY_CODE;

                    long EMPID = 0, createdby = 0;
                    long.TryParse(SessionInfo.Loginuser.EMP_ID, out EMPID);
                    long.TryParse(SessionInfo.Loginuser.LOGINID, out createdby);

                    DataTable dt = new DataTable();
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetFlexiEmpConfig", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
                        command.Parameters.Add("@Empid", SqlDbType.Int).Value = EMPID;
                        command.Parameters.Add("@CompanyCode", SqlDbType.VarChar).Value = Company_Code;
                        command.Parameters.Add("@Finyearid", SqlDbType.Int).Value = Finyearid;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = createdby;

                        command.Parameters.Add("@ANUAL_CTC", SqlDbType.Decimal).Value = ANUAL_CTC;
                        command.Parameters.Add("@PROP_CTC", SqlDbType.Decimal).Value = PROP_CTC;

                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
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
                    Sahaj.LogError(ex.Message.ToString(), ex.ToString(), "SetFlexiEDPS", "FlexiRepo", "", "", "", SessionInfo.Loginuser.LOGINID);
                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;

        }


        public PostJSonResult SetFlexiEmpConfig_det(long configid,long Con_DetID,long edpsid,decimal Amount,int opted,decimal optedamt,decimal ApprovedClaimed_Amt, string Remarks)
        {
            PostJSonResult result = new PostJSonResult();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString()))
            {
                try
                {
                    string Company_Code = SessionInfo.Loginuser.COMPANY_CODE;
                    long EMPID = 0, createdby = 0;
                    long.TryParse(SessionInfo.Loginuser.EMP_ID, out EMPID);
                    long.TryParse(SessionInfo.Loginuser.LOGINID, out createdby);
                    DataTable dt = new DataTable();
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetFlexiEmpConfigDet", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = Con_DetID;
                        command.Parameters.Add("@configid", SqlDbType.Int).Value = configid;
                        command.Parameters.Add("@edpsid", SqlDbType.Int).Value = edpsid;
                        command.Parameters.Add("@Amount", SqlDbType.Decimal).Value = Amount;
                        command.Parameters.Add("@opted", SqlDbType.Int).Value =opted;
                        command.Parameters.Add("@optedamt", SqlDbType.Decimal).Value = optedamt;
                        command.Parameters.Add("@ApprovedClaimed_Amt", SqlDbType.Decimal).Value = ApprovedClaimed_Amt;  
                        command.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = Sahaj.EnsureString(Remarks);
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = createdby;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
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
                    Sahaj.LogError(ex.Message.ToString(), ex.ToString(), "SetFlexiEDPS", "FlexiRepo", "", "", "", SessionInfo.Loginuser.LOGINID);
                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;

        }

        public List<Flexi.Verification.DecList> GetFlexi_DeclarationVerList(long Approved, long finyearid)
        {
            string Company_Code = SessionInfo.Loginuser.COMPANY_CODE;
            int commandTimeout = 0;
            List<Flexi.Verification.DecList> result = new List<Flexi.Verification.DecList>();

            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString()))
                {
                    var param = new DynamicParameters();
                    param.Add("@Approved", dbType: DbType.Int64, value: Approved, direction: ParameterDirection.Input);
                    param.Add("@company_code", dbType: DbType.String, value: Company_Code, direction: ParameterDirection.Input);
                    param.Add("@finyearid", dbType: DbType.Int64, value: finyearid, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetFlexi_DeclarationVerList", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Flexi.Verification.DecList>().ToList();
                    }
                    if (result.Count > 0)
                    {
                        for (int i = 0; i < result.Count; i++)
                        {
                            result[i].EMP_CODE = clsEncryptDecrypt.fnDecrypt(result[i].EMP_CODE);
                            result[i].EMP_NAME = clsEncryptDecrypt.fnDecrypt(result[i].EMP_NAME);
                            result[i].DESIG_NAME = clsEncryptDecrypt.fnDecrypt(result[i].DESIG_NAME);
                            result[i].src = clsEncryptDecrypt.fnEncrypt(result[i].ID.ToString() + "*" + result[i].finyearid+"*"+ result[i].Empid);

                        }
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Sahaj.LogError(ex.Message.ToString(), ex.ToString(), "GetDeclarationPeriodList", "FlexiRepo", "", "", Company_Code, string.IsNullOrEmpty(SessionInfo.Loginuser.EMP_ID) ? "" : SessionInfo.Loginuser.EMP_ID);
            }
            return result;
        }



        public PostJSonResult SetFlexi_Declaration_Approval(long ID, int Approved, string Remarks)
        {
            PostJSonResult result = new PostJSonResult();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString()))
            {
                try
                {
                    string Company_Code = SessionInfo.Loginuser.COMPANY_CODE;
                    long EMPID = 0, createdby = 0;
                    long.TryParse(SessionInfo.Loginuser.EMP_ID, out EMPID);
                    long.TryParse(SessionInfo.Loginuser.LOGINID, out createdby);
                    DataTable dt = new DataTable();
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetFlexi_Declaration_Approval", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
                        command.Parameters.Add("@Approved", SqlDbType.Int).Value = Approved;
                        command.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = Sahaj.EnsureString(Remarks);
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = createdby;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
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
                    Sahaj.LogError(ex.Message.ToString(), ex.ToString(), "SetFlexiEDPS", "FlexiRepo", "", "", "", SessionInfo.Loginuser.LOGINID);
                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;

        }


        public Flexi.Verification.DecView GetFlexi_Declaration_View(long ID, long FinYearID, long EMPID)
        {
            string Company_Code = SessionInfo.Loginuser.COMPANY_CODE;
            
            int commandTimeout = 0;
           Flexi.Verification.DecView result = new Flexi.Verification.DecView();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString()))
                {
                    var param = new DynamicParameters();
                    param.Add("@ID", dbType: DbType.Int64, value: ID, direction: ParameterDirection.Input);
                    param.Add("@EMPID", dbType: DbType.Int64, value: EMPID, direction: ParameterDirection.Input);
                    param.Add("@company_code", dbType: DbType.String, value: Company_Code, direction: ParameterDirection.Input);
                    param.Add("@FinYearID", dbType: DbType.Int64, value: FinYearID, direction: ParameterDirection.Input);
                    DBContext.Open();
                  
                    using (var reader = DBContext.QueryMultiple("spu_GetFlexi_DeclarationEntry", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Flexi.Verification.DecView>().FirstOrDefault();
                        if (result == null)
                        {
                            result = new Flexi.Verification.DecView();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.BFDetails = reader.Read<Flexi.Verification.FixedEDPS>().FirstOrDefault();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.FixedEDPS = reader.Read<Flexi.Verification.FixedEDPS>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.FlexiEDPS = reader.Read<Flexi.Verification.FlexiEDPS>().ToList();
                            if (result.FlexiEDPS != null)
                            {
                                foreach (var item in result.FlexiEDPS)
                                {
                                    item.FLEXI_NPS_PRAN = clsEncryptDecrypt.fnDecrypt(item.FLEXI_NPS_PRAN);
                                    item.FLEXI_NPS_IFSC = clsEncryptDecrypt.fnDecrypt(item.FLEXI_NPS_IFSC);

                                }
                            }
                        }
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Sahaj.LogError(ex.Message.ToString(), ex.ToString(), "GetFlexi_Declaration_View", "FlexiRepo", "", "", Company_Code, string.IsNullOrEmpty(SessionInfo.Loginuser.EMP_ID) ? "" : SessionInfo.Loginuser.EMP_ID);
            }
            return result;
        }



        public Flexi.ClaimEntry.Add GetClaimEntry(long ID,long EDPSID,long EMPID)
        {
            string Company_Code = SessionInfo.Loginuser.COMPANY_CODE;
            int commandTimeout = 0;
            Flexi.ClaimEntry.Add result = new Flexi.ClaimEntry.Add();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString()))
                {
                    var param = new DynamicParameters();
                    param.Add("@ID", dbType: DbType.Int32, value: ID, direction: ParameterDirection.Input);
                    param.Add("@EMPID", dbType: DbType.Int64, value: EMPID, direction: ParameterDirection.Input);
                    param.Add("@EDPSID", dbType: DbType.Int64, value: EDPSID, direction: ParameterDirection.Input);
                    param.Add("@Company_Code", dbType: DbType.String, value: Company_Code, direction: ParameterDirection.Input);
                    
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetFlexi_ClaimEntry", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Flexi.ClaimEntry.Add>().ToList().FirstOrDefault();
                    }

                    if (result==null)
                    {
                        result = new Flexi.ClaimEntry.Add();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Sahaj.LogError(ex.Message.ToString(), ex.ToString(), "GetClaimEntry", "FlexiRepo", "", "", Company_Code, string.IsNullOrEmpty(SessionInfo.Loginuser.EMP_ID) ? "" : SessionInfo.Loginuser.EMP_ID);
            }
            return result;
        }


        public PostJSonResult SetFlexi_ClaimEntry(Flexi.ClaimEntry.Add modal,DateTime date)
        {
            PostJSonResult result = new PostJSonResult();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString()))
            {
                try
                {
                    string Company_Code = SessionInfo.Loginuser.COMPANY_CODE;
                     long createdby = 0;
                    long.TryParse(SessionInfo.Loginuser.LOGINID, out createdby);
                    DataTable dt = new DataTable();
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetFlexiClaim", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = modal.ID;
                        command.Parameters.Add("@Date", SqlDbType.DateTime).Value = date.Date;
                        command.Parameters.Add("@CompanyCode", SqlDbType.VarChar).Value = Company_Code;
                        command.Parameters.Add("@Empid", SqlDbType.Int).Value = modal.Empid;
                        command.Parameters.Add("@Edpsid", SqlDbType.Int).Value = modal.Edpsid;
                        command.Parameters.Add("@DOJ", SqlDbType.VarChar).Value = Sahaj.EnsureString(modal.DOJ);
                        command.Parameters.Add("@AddInformation", SqlDbType.VarChar).Value = Sahaj.EnsureString(modal.AddInformation);
                        command.Parameters.Add("@Billno", SqlDbType.VarChar).Value = Sahaj.EnsureString(modal.Billno);
                        command.Parameters.Add("@Billdate", SqlDbType.VarChar).Value = Sahaj.EnsureString(modal.Billdate);
                        command.Parameters.Add("@BillAmt", SqlDbType.Decimal).Value = modal.BillAmt ?? 0;
                        command.Parameters.Add("@AttachmentID", SqlDbType.Int).Value = modal.AttachmentID;
                        command.Parameters.Add("@VendorID", SqlDbType.Int).Value = modal.VendorID ?? 0;
                        command.Parameters.Add("@Claimamt", SqlDbType.VarChar).Value = modal.Claimamt ?? 0;
                        command.Parameters.Add("@Remark", SqlDbType.VarChar).Value = Sahaj.EnsureString(modal.Remark);
                        command.Parameters.Add("@EntitlementValue", SqlDbType.Decimal).Value = modal.EntitlementValue??0;
                        command.Parameters.Add("@YearEligibility", SqlDbType.Decimal).Value = modal.YearEligibility??0;
                        command.Parameters.Add("@LeasingPeriod", SqlDbType.Int).Value = modal.LeasingPeriod??0;
                        command.Parameters.Add("@ProcValue", SqlDbType.Decimal).Value = modal.ProcValue ?? 0;
                        command.Parameters.Add("@Recovery_Monthly", SqlDbType.Decimal).Value = modal.Recovery_Monthly ?? 0;
                        command.Parameters.Add("@YearAdjustment", SqlDbType.Decimal).Value = modal.YearAdjustment ?? 0;
                        command.Parameters.Add("@MoreThanEntitlement", SqlDbType.Int).Value =modal.MoreThanEntitlement;
                        command.Parameters.Add("@Monthly_Limit", SqlDbType.Decimal).Value = modal.Monthly_Limit ?? 0;
                        command.Parameters.Add("@AlreadyClaimed_Amt", SqlDbType.Decimal).Value = modal.AlreadyClaimed_Amt ?? 0;
                        command.Parameters.Add("@YearlyOpt_Amt", SqlDbType.Decimal).Value = modal.YearlyOpt_Amt ?? 0;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = createdby;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
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
                    Sahaj.LogError(ex.Message.ToString(), ex.ToString(), "SetFlexiEDPS", "FlexiRepo", "", "", "", SessionInfo.Loginuser.LOGINID);
                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;

        }

        public List<Flexi.Verification.ClaimList> GetClaimVerificationList(DateTime date, int Approved)
        {
            string Company_Code = SessionInfo.Loginuser.COMPANY_CODE;
            string EMPID = SessionInfo.Loginuser.EMP_ID;
            int commandTimeout = 0;
            List<Flexi.Verification.ClaimList> result = new List<Flexi.Verification.ClaimList>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString()))
                {
                    var param = new DynamicParameters();
                    param.Add("@EMPID", dbType: DbType.Int64, value: EMPID, direction: ParameterDirection.Input);
                    param.Add("@company_code", dbType: DbType.String, value: Company_Code, direction: ParameterDirection.Input);
                    param.Add("@date", dbType: DbType.DateTime, value: date, direction: ParameterDirection.Input);
                    param.Add("@Approved", dbType: DbType.Int64, value: Approved, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetFlexi_ClaimEntryList", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Flexi.Verification.ClaimList>().ToList();
                    }
                    if (result.Count > 0)
                    {
                        for (int i = 0; i < result.Count; i++)
                        {
                            result[i].EMP_CODE = clsEncryptDecrypt.fnDecrypt(result[i].EMP_CODE);
                            result[i].EMP_NAME = clsEncryptDecrypt.fnDecrypt(result[i].EMP_NAME);
                            result[i].DESIG_NAME = clsEncryptDecrypt.fnDecrypt(result[i].DESIG_NAME);

                        }
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Sahaj.LogError(ex.Message.ToString(), ex.ToString(), "GetClaimVerificationList", "FlexiRepo", "", "", Company_Code, string.IsNullOrEmpty(SessionInfo.Loginuser.EMP_ID) ? "" : SessionInfo.Loginuser.EMP_ID);
            }
            return result;
        }

        public Flexi.Verification.ClaimView GetClaimEntry_View(long ID)
        {
            string Company_Code = SessionInfo.Loginuser.COMPANY_CODE;
            string EMPID = SessionInfo.Loginuser.EMP_ID;
            int commandTimeout = 0;
            Flexi.Verification.ClaimView result = new Flexi.Verification.ClaimView();
            DateTime date = DateTime.Now.Date;
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString()))
                {
                    var param = new DynamicParameters();
                    param.Add("@ID", dbType: DbType.Int32, value: ID, direction: ParameterDirection.Input);
                    param.Add("@EMPID", dbType: DbType.Int64, value: EMPID, direction: ParameterDirection.Input);
                    param.Add("@EDPSID", dbType: DbType.Int64, value: 0, direction: ParameterDirection.Input);
                    param.Add("@Company_Code", dbType: DbType.String, value: Company_Code, direction: ParameterDirection.Input);
                    
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetFlexi_ClaimEntry", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {

                        result = reader.Read<Flexi.Verification.ClaimView>().ToList().FirstOrDefault();
                    }
                    if (result != null)
                    {

                        result.EMP_Code = clsEncryptDecrypt.fnDecrypt(result.EMP_Code);
                        result.EMP_NAME = clsEncryptDecrypt.fnDecrypt(result.EMP_NAME);
                        result.DESIG_NAME = clsEncryptDecrypt.fnDecrypt(result.DESIG_NAME);
                        result.Vendor_Code = clsEncryptDecrypt.fnDecrypt(result.Vendor_Code);
                        result.Vendor_Name = clsEncryptDecrypt.fnDecrypt(result.Vendor_Name);


                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Sahaj.LogError(ex.Message.ToString(), ex.ToString(), "GetClaimEntry", "FlexiRepo", "", "", Company_Code, string.IsNullOrEmpty(SessionInfo.Loginuser.EMP_ID) ? "" : SessionInfo.Loginuser.EMP_ID);
            }
            return result;
        }


        public PostJSonResult SetFlexi_Claim_Approval(long ID, int Approved, string Remarks)
        {
            PostJSonResult result = new PostJSonResult();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString()))
            {
                try
                {
                    string Company_Code = SessionInfo.Loginuser.COMPANY_CODE;
                    long EMPID = 0, createdby = 0;
                    long.TryParse(SessionInfo.Loginuser.EMP_ID, out EMPID);
                    long.TryParse(SessionInfo.Loginuser.LOGINID, out createdby);
                    DataTable dt = new DataTable();
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetFlexi_Claim_Approval", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
                        command.Parameters.Add("@Approved", SqlDbType.Int).Value = Approved;
                        command.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = Sahaj.EnsureString(Remarks);
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = createdby;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
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
                    Sahaj.LogError(ex.Message.ToString(), ex.ToString(), "SetFlexiEDPS", "FlexiRepo", "", "", "", SessionInfo.Loginuser.LOGINID);
                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;

        }


        public List< Flexi.LTA.List> GetFlexiLTA_List(DateTime date, int Approved)
        {
            string Company_Code = SessionInfo.Loginuser.COMPANY_CODE;
            string EMPID = SessionInfo.Loginuser.EMP_ID;
            int commandTimeout = 0;
            List<Flexi.LTA.List> result = new List<Flexi.LTA.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString()))
                {
                    var param = new DynamicParameters();
                    param.Add("@EMPID", dbType: DbType.Int64, value: EMPID, direction: ParameterDirection.Input);
                    param.Add("@company_code", dbType: DbType.String, value: Company_Code, direction: ParameterDirection.Input);
                    param.Add("@date", dbType: DbType.DateTime, value: date, direction: ParameterDirection.Input);
                    param.Add("@Approved", dbType: DbType.Int64, value: Approved, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetFlexi_LTA_List", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Flexi.LTA.List>().ToList().ToList(); 
                    }
                   
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Sahaj.LogError(ex.Message.ToString(), ex.ToString(), "GetFlexiLTA_List", "FlexiRepo", "", "", Company_Code, string.IsNullOrEmpty(SessionInfo.Loginuser.EMP_ID) ? "" : SessionInfo.Loginuser.EMP_ID);
            }
            return result;
        }


        public PostJSonResult SetFlexi_LTA(Flexi.LTA.Add modal,DateTime date)
        {
            PostJSonResult result = new PostJSonResult();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString()))
            {
                try
                {
                    string Company_Code = SessionInfo.Loginuser.COMPANY_CODE;
                    long EMPID = 0, createdby = 0;
                    long.TryParse(SessionInfo.Loginuser.EMP_ID, out EMPID);
                    long.TryParse(SessionInfo.Loginuser.LOGINID, out createdby);
                    DataTable dt = new DataTable();
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetFlexi_LTA", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = modal.ID;
                        command.Parameters.Add("@Date", SqlDbType.DateTime).Value = modal.DocDate;
                        command.Parameters.Add("@CompanyCode", SqlDbType.VarChar).Value = Company_Code;
                        command.Parameters.Add("@ClaimPrevYr", SqlDbType.Int).Value = (string.IsNullOrEmpty(modal.ClaimPrevYr)?0:1);
                        command.Parameters.Add("@Empid", SqlDbType.Int).Value = modal.EMPID;
                        command.Parameters.Add("@Edpsid", SqlDbType.Int).Value = modal.EDPSID;
                        command.Parameters.Add("@AdditionalInfo", SqlDbType.VarChar).Value = Sahaj.EnsureString(modal.AdditionalInfo);
                        command.Parameters.Add("@OptedAmt", SqlDbType.Decimal).Value = modal.OptedAmt??0;
                        command.Parameters.Add("@Previous_ClaimedAmt", SqlDbType.Decimal).Value = modal.Previous_ClaimedAmt??0;
                        command.Parameters.Add("@ClaimAmt", SqlDbType.Decimal).Value = modal.ClaimAmt ?? 0;
                        command.Parameters.Add("@BalanceAmt", SqlDbType.Decimal).Value = modal.BalanceAmt??0;
                        command.Parameters.Add("@Travel_Mode", SqlDbType.VarChar).Value = Sahaj.EnsureString(modal.Travel_Mode);
                        command.Parameters.Add("@Route_Det", SqlDbType.VarChar).Value = Sahaj.EnsureString(modal.Route_Det);
                        command.Parameters.Add("@Travel_StartDate", SqlDbType.VarChar).Value = Sahaj.EnsureString(modal.Travel_StartDate);
                        command.Parameters.Add("@Travel_EndDate", SqlDbType.VarChar).Value = Sahaj.EnsureString(modal.Travel_EndDate);
                        command.Parameters.Add("@All_Attachments", SqlDbType.VarChar).Value = Sahaj.EnsureString(modal.All_Attachments);
                        command.Parameters.Add("Remarks", SqlDbType.VarChar).Value = Sahaj.EnsureString(modal.Remarks);
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = createdby;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
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
                    Sahaj.LogError(ex.Message.ToString(), ex.ToString(), "spu_SetFlexi_LTA", "FlexiRepo", "", "", "", SessionInfo.Loginuser.LOGINID);
                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;

        }



        public Flexi.ClaimEntry.Opendates GetFlexi_ClaimOpenDate(DateTime Date,long EMPID)
        {
            string Company_Code = SessionInfo.Loginuser.COMPANY_CODE;
            
            int commandTimeout = 0;
            Flexi.ClaimEntry.Opendates result = new Flexi.ClaimEntry.Opendates();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString()))
                {
                    var param = new DynamicParameters();
                    param.Add("@EMPID", dbType: DbType.Int64, value: EMPID, direction: ParameterDirection.Input);
                    param.Add("@company_code", dbType: DbType.String, value: Company_Code, direction: ParameterDirection.Input);
                    param.Add("@Date", dbType: DbType.DateTime, value: Date, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetFlexi_ClaimOpenDate", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Flexi.ClaimEntry.Opendates>().FirstOrDefault();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Sahaj.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetFlexi_ClaimOpenDate", "FlexiRepo", "", "", Company_Code, string.IsNullOrEmpty(SessionInfo.Loginuser.EMP_ID) ? "" : SessionInfo.Loginuser.EMP_ID);
            }
            return result;
        }



        public List<Flexi.ClaimHistory> GetFlexi_Claim_History(long EDPS_ID,DateTime Date,long EMPID)
        {
            string Company_Code = SessionInfo.Loginuser.COMPANY_CODE;
            int commandTimeout = 0;
            List<Flexi.ClaimHistory> result = new List<Flexi.ClaimHistory>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString()))
                {
                    var param = new DynamicParameters();
                    param.Add("@EMPID", dbType: DbType.Int64, value: EMPID, direction: ParameterDirection.Input);
                    param.Add("@EDPSID", dbType: DbType.Int64, value: EDPS_ID, direction: ParameterDirection.Input);
                    param.Add("@Date", dbType: DbType.DateTime, value: Date, direction: ParameterDirection.Input);
                    param.Add("@COMPANY_CODE", dbType: DbType.String, value: Company_Code, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetFlexi_Claim_History", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Flexi.ClaimHistory>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Sahaj.LogError(ex.Message.ToString(), ex.ToString(), "GetFlexi_Claim_History", "FlexiRepo", "", "", Company_Code, string.IsNullOrEmpty(SessionInfo.Loginuser.EMP_ID) ? "" : SessionInfo.Loginuser.EMP_ID);
            }
            return result;
        }

        public Flexi.LTA.Add GetFlexiLTA(long ID, DateTime date,long EMPID)
        {
            string Company_Code = SessionInfo.Loginuser.COMPANY_CODE;
            
            int commandTimeout = 0;
            Flexi.LTA.Add result = new Flexi.LTA.Add();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString()))
                {
                    var param = new DynamicParameters();
                    param.Add("@ID", dbType: DbType.Int32, value: ID, direction: ParameterDirection.Input);
                    param.Add("@EMPID", dbType: DbType.Int64, value: EMPID, direction: ParameterDirection.Input);
                    param.Add("@date", dbType: DbType.DateTime, value: date, direction: ParameterDirection.Input);
                    param.Add("@Company_Code", dbType: DbType.String, value: Company_Code, direction: ParameterDirection.Input);

                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetFlexi_LTA", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Flexi.LTA.Add>().ToList().FirstOrDefault();
                        if (result == null)
                        {
                            result = new Flexi.LTA.Add();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.EDPSList = reader.Read<Flexi.LTA.EDPSList>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.FamilyList = reader.Read<Flexi.LTA.FamilyInfo>().ToList();
                           
                            foreach (var item in result.FamilyList)
                            {
                                item.Name = clsEncryptDecrypt.fnDecrypt(item.Name);
                            }

                            if (result.FamilyList == null || result.FamilyList.Count == 0)
                            {
                                Flexi.LTA.FamilyInfo Tobj = new Flexi.LTA.FamilyInfo();
                                List<Flexi.LTA.FamilyInfo> TList = new List<Flexi.LTA.FamilyInfo>();
                                TList.Add(Tobj);
                                result.FamilyList = TList;
                            }
                        }
                        if (!reader.IsConsumed)
                        {
                            result.AttachmentList = reader.Read<Flexi.LTA.Attachment>().ToList();
                            
                        }
                        if (result !=null)
                        {
                            result.EMP_NAME = clsEncryptDecrypt.fnDecrypt(result.EMP_NAME);
                          
                        }
                    }

                   
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Sahaj.LogError(ex.Message.ToString(), ex.ToString(), "GetFlexiLTA", "FlexiRepo", "", "", Company_Code, string.IsNullOrEmpty(SessionInfo.Loginuser.EMP_ID) ? "" : SessionInfo.Loginuser.EMP_ID);
            }
            return result;
        }



        public PostJSonResult SetFlexi_LTA_Family(Flexi.LTA.FamilyInfo Modal)
        {
            PostJSonResult result = new PostJSonResult();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString()))
            {
                try
                {
                    string Company_Code = SessionInfo.Loginuser.COMPANY_CODE;
                    long EMPID = 0, createdby = 0;
                    long.TryParse(SessionInfo.Loginuser.EMP_ID, out EMPID);
                    long.TryParse(SessionInfo.Loginuser.LOGINID, out createdby);
                    DataTable dt = new DataTable();
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetFlexi_LTA_Family", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@FamilyID", SqlDbType.Int).Value = Modal.FamilyID??0;
                        command.Parameters.Add("@LTAID", SqlDbType.Int).Value = Modal.LTAID;
                        command.Parameters.Add("@Name", SqlDbType.VarChar).Value = clsEncryptDecrypt.fnDecrypt(Sahaj.EnsureString(Modal.Name));
                        command.Parameters.Add("@Relation", SqlDbType.VarChar).Value = Sahaj.EnsureString(Modal.Relation);
                        command.Parameters.Add("@Company_Code", SqlDbType.VarChar).Value = Company_Code;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
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
                    Sahaj.LogError(ex.Message.ToString(), ex.ToString(), "SetFlexiEDPS", "FlexiRepo", "", "", "", SessionInfo.Loginuser.LOGINID);
                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;

        }


        public List<Flexi.LTA.Verification.List> GetFlexiLTAVerification_List(DateTime date, int Approved)
        {
            string Company_Code = SessionInfo.Loginuser.COMPANY_CODE;
            int commandTimeout = 0;
            List<Flexi.LTA.Verification.List> result = new List<Flexi.LTA.Verification.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString()))
                {
                    var param = new DynamicParameters();
                    param.Add("@EMPID", dbType: DbType.Int64, value: 0, direction: ParameterDirection.Input);
                    param.Add("@company_code", dbType: DbType.String, value: Company_Code, direction: ParameterDirection.Input);
                    param.Add("@date", dbType: DbType.DateTime, value: date, direction: ParameterDirection.Input);
                    param.Add("@Approved", dbType: DbType.Int64, value: Approved, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetFlexi_LTA_List", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Flexi.LTA.Verification.List>().ToList().ToList();
                    }

                    DBContext.Close();
                    if (result.Count > 0)
                    {
                        for (int i = 0; i < result.Count; i++)
                        {
                            result[i].EMP_CODE = clsEncryptDecrypt.fnDecrypt(result[i].EMP_CODE);
                            result[i].EMP_Name = clsEncryptDecrypt.fnDecrypt(result[i].EMP_Name);
                            result[i].DESIG_NAME = clsEncryptDecrypt.fnDecrypt(result[i].DESIG_NAME);
                            result[i].DEPT_NAME = clsEncryptDecrypt.fnDecrypt(result[i].DEPT_NAME);


                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Sahaj.LogError(ex.Message.ToString(), ex.ToString(), "GetFlexiLTA_List", "FlexiRepo", "", "", Company_Code, string.IsNullOrEmpty(SessionInfo.Loginuser.EMP_ID) ? "" : SessionInfo.Loginuser.EMP_ID);
            }
            return result;
        }



        public Flexi.LTA.Verification.View GetFlexiLTA_View(long ID)
        {
            string Company_Code = SessionInfo.Loginuser.COMPANY_CODE;
            
            int commandTimeout = 0;
            Flexi.LTA.Verification.View result = new Flexi.LTA.Verification.View();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString()))
                {
                    var param = new DynamicParameters();
                    param.Add("@ID", dbType: DbType.Int32, value: ID, direction: ParameterDirection.Input);
                    param.Add("@Company_Code", dbType: DbType.String, value: Company_Code, direction: ParameterDirection.Input);

                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetFlexi_LTA_View", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Flexi.LTA.Verification.View>().ToList().FirstOrDefault();
                        if (result == null)
                        {
                            result = new Flexi.LTA.Verification.View();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.FamilyList = reader.Read<Flexi.LTA.FamilyInfo>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.AttachmentList = reader.Read<Flexi.LTA.Attachment>().ToList();
                        }
                    }


                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Sahaj.LogError(ex.Message.ToString(), ex.ToString(), "GetFlexiLTA_View", "FlexiRepo", "", "", Company_Code, string.IsNullOrEmpty(SessionInfo.Loginuser.EMP_ID) ? "" : SessionInfo.Loginuser.EMP_ID);
            }
            return result;
        }

        public PostJSonResult SetFlexi_LTA_Approval(long ID, int Approved, string Remarks,decimal Exemption_Amt,decimal Sanction_Amt)
        {
            PostJSonResult result = new PostJSonResult();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString()))
            {
                try
                {
                    string Company_Code = SessionInfo.Loginuser.COMPANY_CODE;
                    long EMPID = 0, createdby = 0;
                    long.TryParse(SessionInfo.Loginuser.EMP_ID, out EMPID);
                    long.TryParse(SessionInfo.Loginuser.LOGINID, out createdby);
                    DataTable dt = new DataTable();
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetFlexi_LTA_Approval", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
                        command.Parameters.Add("@Approved", SqlDbType.Int).Value = Approved;
                        command.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = Sahaj.EnsureString(Remarks);
                        command.Parameters.Add("@Exemption_Amt", SqlDbType.Decimal).Value = Exemption_Amt;
                        command.Parameters.Add("@Sanction_Amt", SqlDbType.Decimal).Value = Sanction_Amt;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = createdby;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
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
                    Sahaj.LogError(ex.Message.ToString(), ex.ToString(), "SetFlexiEDPS", "FlexiRepo", "", "", "", SessionInfo.Loginuser.LOGINID);
                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;

        }



        public Flexi.EDPSinfo GetFlexi_EDPSinfo(long EDPS_ID,string Doctype,DateTime Date,long EMPID)
        {
            string Company_Code = SessionInfo.Loginuser.COMPANY_CODE;
            int commandTimeout = 0;
            Flexi.EDPSinfo result = new Flexi.EDPSinfo();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString()))
                {
                    var param = new DynamicParameters();
                    param.Add("@EDPS_ID", dbType: DbType.Int32, value: EDPS_ID, direction: ParameterDirection.Input);
                    param.Add("@Company_Code", dbType: DbType.String, value: Company_Code, direction: ParameterDirection.Input);
                    param.Add("@Doctype", dbType: DbType.String, value: Doctype, direction: ParameterDirection.Input);
                    param.Add("@EMPID", dbType: DbType.Int32, value: EMPID, direction: ParameterDirection.Input);
                    param.Add("@Date", dbType: DbType.DateTime, value: Date, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetFlexi_EDPS_info", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Flexi.EDPSinfo>().FirstOrDefault();
                    }


                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Sahaj.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetFlexi_EDPS_info", "GetFlexi_EDPSinfo", "", "", Company_Code, string.IsNullOrEmpty(SessionInfo.Loginuser.EMP_ID) ? "" : SessionInfo.Loginuser.EMP_ID);
            }
            return result;
        }



        public List<Flexi.LTA.History> GetFlexi_LTA_History(long EDPS_ID, DateTime Date, long EMPID)
        {
            string Company_Code = SessionInfo.Loginuser.COMPANY_CODE;
            int commandTimeout = 0;
            List<Flexi.LTA.History> result = new List<Flexi.LTA.History>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString()))
                {
                    var param = new DynamicParameters();
                    param.Add("@EMPID", dbType: DbType.Int64, value: EMPID, direction: ParameterDirection.Input);
                    param.Add("@EDPSID", dbType: DbType.Int64, value: EDPS_ID, direction: ParameterDirection.Input);
                    param.Add("@Date", dbType: DbType.DateTime, value: Date, direction: ParameterDirection.Input);
                    param.Add("@COMPANY_CODE", dbType: DbType.String, value: Company_Code, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetFlexi_LTA_History", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Flexi.LTA.History>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Sahaj.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetFlexi_LTA_History", "FlexiRepo", "", "", Company_Code, string.IsNullOrEmpty(SessionInfo.Loginuser.EMP_ID) ? "" : SessionInfo.Loginuser.EMP_ID);
            }
            return result;
        }


        public List<Flexi.LTA.EMP_LTA> GetFlexi_LTA_EMPList(long EMPID)
        {
            string Company_Code = SessionInfo.Loginuser.COMPANY_CODE;
            int commandTimeout = 0;
            List<Flexi.LTA.EMP_LTA> result = new List<Flexi.LTA.EMP_LTA>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString()))
                {
                    var param = new DynamicParameters();
                    param.Add("@EMPID", dbType: DbType.Int64, value: EMPID, direction: ParameterDirection.Input);
                    param.Add("@CompanyCode", dbType: DbType.String, value: Company_Code, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetFlexi_EMP_LTA", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Flexi.LTA.EMP_LTA>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Sahaj.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetFlexi_LTA_History", "FlexiRepo", "", "", Company_Code, string.IsNullOrEmpty(SessionInfo.Loginuser.EMP_ID) ? "" : SessionInfo.Loginuser.EMP_ID);
            }
            return result;
        }


        public PostJSonResult SetFlexi_EMP_LTA(Flexi.LTA.EMP_LTA modal)
        {
            PostJSonResult result = new PostJSonResult();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString()))
            {
                try
                {
                    string Company_Code = SessionInfo.Loginuser.COMPANY_CODE;
                    long  createdby = 0;
                    long.TryParse(SessionInfo.Loginuser.LOGINID, out createdby);
                    DataTable dt = new DataTable();
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetFlexi_EMP_LTA", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = modal.ID;
                        command.Parameters.Add("@Company_Code", SqlDbType.VarChar).Value = Company_Code;
                        command.Parameters.Add("@NO_OF_CLAIM", SqlDbType.Int).Value = modal.NO_OF_CLAIM;
                        command.Parameters.Add("@PAMOUNT", SqlDbType.Decimal).Value = modal.PAMOUNT;
                        command.Parameters.Add("@EAMOUNT", SqlDbType.Decimal).Value = modal.EAMOUNT;
                        command.Parameters.Add("@CLAIM_PREV_YEAR", SqlDbType.Int).Value = (string.IsNullOrEmpty(modal.chk) ? 0 : 1);
                        command.Parameters.Add("@REMARKS", SqlDbType.VarChar).Value = Sahaj.EnsureString(modal.REMARKS);
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = createdby;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
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
                    Sahaj.LogError(ex.Message.ToString(), ex.ToString(), "spu_SetFlexi_EMP_LTA", "FlexiRepo", "", "", "", SessionInfo.Loginuser.LOGINID);
                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;

        }



        public DataSet GetFlexi_Declaration_Report(long EMPID, long FinID)
        {
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@EMPID", EMPID);
            oparam[1] = new SqlParameter("@FYID", FinID);
            oparam[2] = new SqlParameter("@Company_Code", SessionInfo.Loginuser.COMPANY_CODE);
            return Common.ExecuteDataSet("spu_GetFlexi_Declaration_Report", oparam);
        }


      

    }
}