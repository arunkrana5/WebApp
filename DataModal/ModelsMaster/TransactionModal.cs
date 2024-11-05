using Dapper;
using DataModal.CommanClass;
using DataModal.Models;
using DataModal.ModelsMasterHelper;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DataModal.ModelsMaster
{
    public class TransactionModal : ITransactionHelper
    {
        string ConnectionStrings = ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString.ToString();
        string EntrySource = "Web";


        public List<PJPPlan.List> GetPJPPlanList(Tab.Approval Modal)
        {

            List<PJPPlan.List> result = new List<PJPPlan.List>();
            try
            {
                DateTime dt;
                DateTime.TryParse(Modal.Month, out dt);
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    param.Add("@Date", dbType: DbType.DateTime, value: dt, direction: ParameterDirection.Input);

                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetPJP_PlanList", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<PJPPlan.List>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetPJPList", "spu_GetPJPList", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public PJPPlan.Add GetPJPPlan(GetResponse Modal)
        {

            PJPPlan.Add result = new PJPPlan.Add();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@PJPID", dbType: DbType.Int64, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);

                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetPJP_Plan", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<PJPPlan.Add>().FirstOrDefault();
                        if (result == null)
                        {
                            result = new PJPPlan.Add();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.DealerList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.RegionList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.DealerCategoryList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.DealerTypeList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.EMPList = reader.Read<DropDownlist>().ToList();
                        }

                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetPJP", "spu_GetPJP", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public PostResponse fnSetPJPPlan(PJPPlan.Add modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetPJP_Plan", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@PJPID", SqlDbType.Int).Value = modal.PJPID ?? 0;
                        command.Parameters.Add("@DealerID", SqlDbType.VarChar).Value = string.Join(",", modal.DealerID);
                        command.Parameters.Add("@EMPID", SqlDbType.Int).Value = modal.EMPID ?? 0;
                        command.Parameters.Add("@VisitDate", SqlDbType.VarChar).Value = modal.VisitDate ?? "";
                        command.Parameters.Add("@IsActive", SqlDbType.Int).Value = 1;
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = modal.Priority ?? 0;
                        command.Parameters.Add("@OnDemand", SqlDbType.Int).Value = modal.OnDemand ?? 0;
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


        public List<SaleEntry.List> GetSaleEntryList(Tab.Approval Modal)
        {
            List<SaleEntry.List> result = new List<SaleEntry.List>();
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
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetSaleEntryList", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<SaleEntry.List>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetSaleEntryList", "GetSaleEntryList", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public SaleEntry.Add GetSaleEntry(GetResponse Modal)
        {

            SaleEntry.Add result = new SaleEntry.Add();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@SaleEntryID", dbType: DbType.Int64, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);

                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetSaleEntry", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<SaleEntry.Add>().FirstOrDefault();
                        if (result == null)
                        {
                            result = new SaleEntry.Add();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.ProductTypeList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.StateList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.PaymentModeList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.ItemsList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.QtyList = reader.Read<DropDownlist>().ToList();
                        }
                        result.ProductList = new List<DropDownlist>();
                        result.ProductTranList = new List<DropDownlist>();
                        result.CityList = new List<DropDownlist>();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetSaleEntry", "spu_GetSaleEntry", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public PostResponse fnSetSaleEntry(SaleEntry.Add model)
        {

            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetSaleEntry", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@SaleEntryID", SqlDbType.Int).Value = model.SaleEntryID ?? 0;
                        command.Parameters.Add("@EMPID", SqlDbType.Int).Value = model.EMPID;
                        command.Parameters.Add("@InvoiceDate", SqlDbType.VarChar).Value = model.InvoiceDate ?? "";
                        command.Parameters.Add("@InvoiceNo", SqlDbType.VarChar).Value = model.InvoiceNo ?? "";
                        command.Parameters.Add("@ItemID", SqlDbType.Int).Value = model.ItemID ?? 0;
                        command.Parameters.Add("@Qty", SqlDbType.Decimal).Value = model.Qty ?? 0;
                        command.Parameters.Add("@Price", SqlDbType.Decimal).Value = model.Price ?? 0;
                        command.Parameters.Add("@SerialNo", SqlDbType.VarChar, 500).Value = model.SerialNo ?? "";
                        command.Parameters.Add("@SaleFor", SqlDbType.VarChar, 500).Value = model.SaleFor ?? "";
                        command.Parameters.Add("@InstallationNo", SqlDbType.VarChar, 500).Value = model.InstallationNo ?? "";
                        command.Parameters.Add("@PaymentMode", SqlDbType.VarChar, 50).Value = model.PaymentMode ?? "";
                        command.Parameters.Add("@IsExchange", SqlDbType.Int).Value = model.IsExchange;
                        command.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = model.Remarks ?? "";
                        command.Parameters.Add("@AttachmentID", SqlDbType.Int).Value = model.AttachmentID;
                        command.Parameters.Add("@Name", SqlDbType.VarChar).Value = model.Name ?? "";
                        command.Parameters.Add("@Phone", SqlDbType.VarChar).Value = model.Phone ?? "";
                        command.Parameters.Add("@Email", SqlDbType.VarChar).Value = model.Email ?? "";
                        command.Parameters.Add("@CountryID", SqlDbType.VarChar).Value = model.CountryID ?? 0;
                        command.Parameters.Add("@StateID", SqlDbType.VarChar).Value = model.StateID ?? 0;
                        command.Parameters.Add("@CityID", SqlDbType.VarChar).Value = model.CityID ?? 0;
                        command.Parameters.Add("@Address1", SqlDbType.VarChar).Value = model.Address1 ?? "";
                        command.Parameters.Add("@Address2", SqlDbType.VarChar).Value = model.Address2 ?? "";
                        command.Parameters.Add("@Location", SqlDbType.VarChar).Value = model.Location ?? "";
                        command.Parameters.Add("@Zipcode", SqlDbType.VarChar).Value = model.Zipcode ?? "";
                        command.Parameters.Add("@IsActive", SqlDbType.Int).Value = 1;
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = model.Priority ?? 0;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = model.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = model.IPAddress;
                        command.Parameters.Add("@EntrySource", SqlDbType.VarChar).Value = EntrySource;
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


        public List<CounterDisplay.List> GetCounterDisplayList(Tab.Date Modal)
        {

            List<CounterDisplay.List> result = new List<CounterDisplay.List>();
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
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);

                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetCounterDisplayList", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<CounterDisplay.List>().ToList();
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


        public CounterDisplay.Add GetCounterDisplay(GetResponse Modal)
        {

            CounterDisplay.Add result = new CounterDisplay.Add();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@CounterID", dbType: DbType.Int64, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);

                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetCounterDisplay", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<CounterDisplay.Add>().FirstOrDefault();
                        if (result == null)
                        {
                            result = new CounterDisplay.Add();
                            result.Date = DateTime.Now.ToString("dd-MMM-yyyy");
                        }
                        if (!reader.IsConsumed)
                        {
                            result.BrandList = reader.Read<DropDownlist>().ToList();
                        }
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetCounterDisplay", "spu_GetCounterDisplay", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public PostResponse fnSetCounterDisplay(CounterDisplay.Add model)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetCounterDisplay", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@CounterID", SqlDbType.Int).Value = model.CounterID ?? 0;

                        command.Parameters.Add("@EMPID", SqlDbType.Int).Value = model.EMPID;
                        command.Parameters.Add("@BrandID", SqlDbType.Int).Value = model.BrandID ?? 0;
                        command.Parameters.Add("@Qty", SqlDbType.Decimal).Value = model.Qty ?? 0;
                        command.Parameters.Add("@AttachmentID", SqlDbType.Int).Value = model.AttachmentID;
                        command.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = model.Remarks ?? "";
                        command.Parameters.Add("@IsActive", SqlDbType.Int).Value = 1;
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = model.Priority ?? 0;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = model.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = model.IPAddress;
                        command.Parameters.Add("@EntrySource", SqlDbType.VarChar).Value = EntrySource;
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


        public List<MOP.List> GetMOPList(Tab.Date Modal)
        {

            List<MOP.List> result = new List<MOP.List>();
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
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetMOPList", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<MOP.List>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetMOPList", "spu_GetMOPList", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public MOP.Add GetMOP(GetResponse Modal)
        {

            MOP.Add result = new MOP.Add();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@MOPID", dbType: DbType.Int64, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);

                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetMOP", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<MOP.Add>().FirstOrDefault();
                        if (result == null)
                        {
                            result = new MOP.Add();
                            result.Date = DateTime.Now.ToString("dd-MMM-yyyy");
                        }
                        if (!reader.IsConsumed)
                        {
                            result.BrandList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.ProductList = reader.Read<DropDownlist>().ToList();
                        }
                        result.ProductTranList = new List<DropDownlist>();
                        if (!reader.IsConsumed)
                        {
                            result.RatingList = reader.Read<DropDownlist>().ToList();
                        }
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetCounterDisplay", "spu_GetCounterDisplay", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }
        public PostResponse fnSetMOP(MOP.Add model)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetMOP", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@MOPID", SqlDbType.Int).Value = model.MOPID ?? 0;
                        command.Parameters.Add("@EMPID", SqlDbType.Int).Value = model.EMPID;
                        command.Parameters.Add("@BrandID", SqlDbType.Int).Value = model.BrandID ?? 0;
                        command.Parameters.Add("@ProductID", SqlDbType.Int).Value = model.ProductID ?? 0;
                        command.Parameters.Add("@PDTranID", SqlDbType.Int).Value = model.PDTranID ?? 0;
                        command.Parameters.Add("@ModelNo", SqlDbType.VarChar).Value = model.ModelNo ?? "";
                        command.Parameters.Add("@Price", SqlDbType.Decimal).Value = model.Price ?? 0;
                        command.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = model.Remarks ?? "";
                        command.Parameters.Add("@IsActive", SqlDbType.Int).Value = 1;
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = model.Priority ?? 0;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = model.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = model.IPAddress;
                        command.Parameters.Add("@EntrySource", SqlDbType.VarChar).Value = EntrySource;
                        command.Parameters.Add("@Rating", SqlDbType.Int).Value = model.Rating ?? 0;
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


        public List<RFCEntry.List> GetRFCEntryList(Tab.Approval Modal)
        {

            List<RFCEntry.List> result = new List<RFCEntry.List>();
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
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetRFCEntryList", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<RFCEntry.List>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetRFCEntryList", "spu_GetRFCEntryList", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }



        public PostResponse fnSetRFCEntry(RFCEntry.Add model)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetRFCEntry", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@EMPID", SqlDbType.Int).Value = model.EMPID;
                        command.Parameters.Add("@AttendenceDate", SqlDbType.VarChar).Value = model.AttendenceDate ?? "";
                        command.Parameters.Add("@Old_StatusID", SqlDbType.Int).Value = model.Old_StatusID;
                        command.Parameters.Add("@New_StatusID", SqlDbType.Int).Value = model.New_StatusID;
                        command.Parameters.Add("@Reason", SqlDbType.VarChar).Value = model.Reason ?? "";
                        command.Parameters.Add("@Category", SqlDbType.VarChar).Value = model.Category ?? "";
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = model.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = model.IPAddress;
                        command.Parameters.Add("@EntrySource", SqlDbType.VarChar).Value = EntrySource;
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




        public List<CompetitionEntry.List> GetCompetitionEntryList(Tab.Date Modal)
        {

            List<CompetitionEntry.List> result = new List<CompetitionEntry.List>();
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
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetCompetitionEntryList", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<CompetitionEntry.List>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetCompetitionEntryList", "spu_GetCompetitionEntryList", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public CompetitionEntry.Add GetCompetitionEntry(GetResponse Modal)
        {

            CompetitionEntry.Add result = new CompetitionEntry.Add();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@CompetitionID", dbType: DbType.Int64, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetCompetitionEntry", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<CompetitionEntry.Add>().FirstOrDefault();
                        if (result == null)
                        {
                            result = new CompetitionEntry.Add();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.BrandList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.ProductTypeList = reader.Read<DropDownlist>().ToList();
                        }

                        if (!reader.IsConsumed)
                        {
                            result.QtyList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.CategoryList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.SubCategoryList = reader.Read<DropDownlist>().ToList();
                            if (result.SubCategoryList == null)
                            {
                                result.SubCategoryList = new List<DropDownlist>();
                            }
                        }
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetCompetitionEntry", "spu_GetCompetitionEntry", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public PostResponse fnSetCompetitionEntry(CompetitionEntry.Add model)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetCompetitionEntry", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@CompetitionID", SqlDbType.Int).Value = model.CompetitionID ?? 0;
                        command.Parameters.Add("@EMPID", SqlDbType.Int).Value = model.EMPID;
                        command.Parameters.Add("@BrandID", SqlDbType.Int).Value = model.BrandID ?? 0;
                        command.Parameters.Add("@Qty", SqlDbType.Int).Value = model.Qty ?? 0;
                        command.Parameters.Add("@Price", SqlDbType.Int).Value = model.Price ?? 0;
                        command.Parameters.Add("@Category", SqlDbType.VarChar).Value = model.Category ?? "";
                        command.Parameters.Add("@SubCategory", SqlDbType.VarChar).Value = model.SubCategory ?? "";
                        command.Parameters.Add("@ProductTypeID", SqlDbType.VarChar).Value = model.ProductTypeID ?? 0;
                        command.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = model.Remarks ?? "";
                        command.Parameters.Add("@ModalNo", SqlDbType.VarChar).Value = model.ModalNo ?? "";
                        command.Parameters.Add("@StarRating", SqlDbType.VarChar).Value = model.StarRating ?? 0;
                        command.Parameters.Add("@IsActive", SqlDbType.Int).Value = 1;
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = model.Priority ?? 0;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = model.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = model.IPAddress;
                        command.Parameters.Add("@EntrySource", SqlDbType.VarChar).Value = EntrySource;
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

        public List<TargetImport.List> GetTargets_ImportList(GetResponse Modal)
        {
            List<TargetImport.List> result = new List<TargetImport.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetTargets_ImportList", commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<TargetImport.List>().ToList();
                    }

                    DBContext.Close();
                }

            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetEMP_TalentPoolImportList", "GetEMPTalentPoolImportList", "DataModal", Modal.LoginID, Modal.IPAddress);

            }
            return result;
        }

        public PostResponse SetTargets_Import(DataSet TempDataset, GetResponse getResponse)
        {
            PostResponse Result = new PostResponse();
            Result.SuccessMessage = "Error occurred while saving into EMP import";
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetTargets_Import", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        TempDataset.Tables[0].Rows.RemoveAt(0);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@LoginID", SqlDbType.Int).Value = getResponse.LoginID;
                        command.Parameters.Add("@Targets_ImportType", SqlDbType.Structured).Value = TempDataset.Tables[0];
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

        public PostResponse UploadEMPTargetImportExcelFile(HttpPostedFileBase file, string SheetName, GetResponse getResponse)
        {
            PostResponse Result = new PostResponse();
            DataSet TempDataset = default(DataSet);
            string ExcelFileName = null;
            string DirectotyPath = null;
            string FileExtension = null;
            ArrayList SqlArrayList = new ArrayList();

            try
            {
                ExcelFileName = "EMPTargetImportData";
                DirectotyPath = ConfigurationManager.AppSettings["ApplicationPath_Physical"] + "\\Attachments\\ImportExcels";

                if (!System.IO.Directory.Exists(DirectotyPath + "\\EMPTargetImportData"))
                {
                    System.IO.Directory.CreateDirectory(DirectotyPath + "\\EMPTargetImportData");
                }
                FileExtension = System.IO.Path.GetExtension(file.FileName);
                if (FileExtension == ".xlsx" || FileExtension == ".xls")
                {

                    file.SaveAs(DirectotyPath + "\\EMPTargetImportData\\" + ExcelFileName + "" + FileExtension);
                    TempDataset = clsDataBaseHelper.GetExcelDataAsDataSet(DirectotyPath + "\\EMPTargetImportData\\" + ExcelFileName + "" + FileExtension, SheetName.Replace("'", "''").Replace("$", "") + "$");

                    if ((TempDataset != null))
                    {

                        if (TempDataset.Tables[0].Columns.Count > 0)
                        {
                            Result = SetTargets_Import(TempDataset, getResponse);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Result;

        }

        public PostResponse ClearEMPTargetImportTemp(GetResponse getResponse)
        {
            PostResponse Result = new PostResponse();
            try
            {
                string SQL = "Truncate table Targets_Import";
                clsDataBaseHelper.ExecuteNonQuery(SQL);
                Result.Status = true;
                Result.SuccessMessage = "data cleared";
            }
            catch (Exception ex)
            {

            }
            return Result;
        }

        public PostResponse SetTarget_FromTargetImport(GetResponse getResponse)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetTarget_FromTargetImport", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = getResponse.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = getResponse.IPAddress;
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

        public List<PJPPlan.List> GetMyPJPPlanList(Tab.Approval Modal)
        {

            List<PJPPlan.List> result = new List<PJPPlan.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    param.Add("@Date", dbType: DbType.String, value: Modal.Month ?? "", direction: ParameterDirection.Input);

                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetMyPJPPlanList", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<PJPPlan.List>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetMyPJPPlanList", "GetMyPJPPlanList", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }


        public List<PJPEntry.List> GetPlanWisePJPEntryList(GetResponse Modal)
        {

            List<PJPEntry.List> result = new List<PJPEntry.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@PJPPlanID", dbType: DbType.Int32, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetPlanWisePJPEntryList", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<PJPEntry.List>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetPlanWisePJPEntryList", "spu_GetPlanWisePJPEntryList", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public PJPEntry.Add GetPJPEntryAdd(GetResponse Modal, long PJPPlanID, long PJPEntryID, long SSR_EMPID)
        {
            PJPEntry.Add result = new PJPEntry.Add();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@PJPPlanID", dbType: DbType.Int64, value: PJPPlanID, direction: ParameterDirection.Input);
                    param.Add("@PJPEntryID", dbType: DbType.Int64, value: PJPEntryID, direction: ParameterDirection.Input);
                    param.Add("@SSR_EMPID", dbType: DbType.Int64, value: SSR_EMPID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);

                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetPJPEntry", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<PJPEntry.Add>().FirstOrDefault();
                        if (result == null)
                        {
                            result = new PJPEntry.Add();
                            result.PJPPlanID = PJPPlanID;
                            result.PJPEntryID = PJPEntryID;
                        }
                        if (!reader.IsConsumed)
                        {
                            result.BrandEntryList = reader.Read<PJPEntry_Brand.List>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.BrandList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.SSRList = reader.Read<DropDownlist>().ToList();
                        }
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetPJPEntryAdd", "GetTourPlanList", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }


        public PostResponse fnSetPJPEntry(PJPEntry.Add model)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    float Latitude = 0, Longitude = 0;
                    float.TryParse(model.Latitude, out Latitude);
                    float.TryParse(model.Longitude, out Longitude);
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetPJPEntry", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@ContactPerson_Name", SqlDbType.VarChar).Value = model.ContactPerson_Name ?? "";
                        command.Parameters.Add("@ContactPerson_Phone", SqlDbType.VarChar).Value = model.ContactPerson_Phone ?? "";


                        command.Parameters.Add("@PJPEntryID", SqlDbType.Int).Value = model.PJPEntryID ?? 0;
                        command.Parameters.Add("@PJPPlanID", SqlDbType.Int).Value = model.PJPPlanID ?? 0;
                        command.Parameters.Add("@SSR_EMPID", SqlDbType.Int).Value = model.SSR_EMPID ?? 0;
                        command.Parameters.Add("@AttachID", SqlDbType.Int).Value = model.AttachmentID ?? 0;
                        command.Parameters.Add("@ProductRating", SqlDbType.VarChar).Value = model.ProductRating ?? 0;
                        command.Parameters.Add("@CustomerRating", SqlDbType.VarChar).Value = model.CustomerRating ?? 0;
                        command.Parameters.Add("@ProductKnw", SqlDbType.VarChar).Value = model.ProductKnw ?? "";
                        command.Parameters.Add("@CustomerKnw", SqlDbType.VarChar).Value = model.CustomerKnw ?? "";
                        command.Parameters.Add("@SSRAvailability", SqlDbType.VarChar).Value = model.SSRAvailability ?? "";
                        command.Parameters.Add("@SSR_AttendenceID", SqlDbType.VarChar).Value = model.SSR_AttendenceID ?? 0;
                        command.Parameters.Add("@Location", SqlDbType.VarChar).Value = model.Location ?? "";
                        command.Parameters.Add("@Latitude", SqlDbType.VarChar).Value = Latitude;
                        command.Parameters.Add("@Longitude", SqlDbType.VarChar).Value = Longitude;
                        command.Parameters.Add("@Error", SqlDbType.VarChar).Value = model.Error ?? "";
                        command.Parameters.Add("@Notes", SqlDbType.VarChar).Value = model.Notes ?? "";

                        command.Parameters.Add("@ExpenseAttachmentID", SqlDbType.Int).Value = model.ExpenseAttachmentID ?? 0;
                        command.Parameters.Add("@ExpenseAmt", SqlDbType.Decimal).Value = model.ExpenseAmt ?? 0;
                        command.Parameters.Add("@ExpenseRemarks", SqlDbType.VarChar).Value = model.ExpenseRemarks ?? "";

                        command.Parameters.Add("@IsActive", SqlDbType.Int).Value = 1;
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = 0;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = model.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = model.IPAddress;
                        command.Parameters.Add("@EntrySource", SqlDbType.VarChar).Value = EntrySource;

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

        public PostResponse fnSetPJPEntryWithNoSSR(PJPEntry.AddWithNoSSR model)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    float Latitude = 0, Longitude = 0;
                    float.TryParse(model.Latitude, out Latitude);
                    float.TryParse(model.Longitude, out Longitude);
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetPJPEntry", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@ContactPerson_Name", SqlDbType.VarChar).Value = model.ContactPerson_Name ?? "";
                        command.Parameters.Add("@ContactPerson_Phone", SqlDbType.VarChar).Value = model.ContactPerson_Phone ?? "";

                        command.Parameters.Add("@PJPEntryID", SqlDbType.Int).Value = model.PJPEntryID ?? 0;
                        command.Parameters.Add("@PJPPlanID", SqlDbType.Int).Value = model.PJPPlanID ?? 0;
                        command.Parameters.Add("@SSR_EMPID", SqlDbType.Int).Value = model.SSR_EMPID ?? 0;
                        command.Parameters.Add("@AttachID", SqlDbType.Int).Value = model.AttachmentID ?? 0;
                        command.Parameters.Add("@ProductRating", SqlDbType.VarChar).Value = 0;
                        command.Parameters.Add("@CustomerRating", SqlDbType.VarChar).Value = 0;
                        command.Parameters.Add("@ProductKnw", SqlDbType.VarChar).Value = "";
                        command.Parameters.Add("@CustomerKnw", SqlDbType.VarChar).Value = "";
                        command.Parameters.Add("@SSRAvailability", SqlDbType.VarChar).Value = model.SSRAvailability ?? "";
                        command.Parameters.Add("@SSR_AttendenceID", SqlDbType.VarChar).Value = model.SSR_AttendenceID ?? 0;
                        command.Parameters.Add("@Location", SqlDbType.VarChar).Value = model.Location ?? "";
                        command.Parameters.Add("@Latitude", SqlDbType.VarChar).Value = Latitude;
                        command.Parameters.Add("@Longitude", SqlDbType.VarChar).Value = Longitude;
                        command.Parameters.Add("@Error", SqlDbType.VarChar).Value = model.Error ?? "";
                        command.Parameters.Add("@Notes", SqlDbType.VarChar).Value = model.Notes ?? "";

                        command.Parameters.Add("@ExpenseAttachmentID", SqlDbType.Int).Value = model.ExpenseAttachmentID ?? 0;
                        command.Parameters.Add("@ExpenseAmt", SqlDbType.Decimal).Value = model.ExpenseAmt ?? 0;
                        command.Parameters.Add("@ExpenseRemarks", SqlDbType.VarChar).Value = model.ExpenseRemarks ?? "";

                        command.Parameters.Add("@IsActive", SqlDbType.Int).Value = 1;
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = 0;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = model.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = model.IPAddress;
                        command.Parameters.Add("@EntrySource", SqlDbType.VarChar).Value = EntrySource;

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


        public PostResponse fnSetPJPEntry_Brand(PJPEntry_Brand.List model)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetPJPEntry_Brand", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@PJPBrandID", SqlDbType.Int).Value = model.PJPBrandID ?? 0;
                        command.Parameters.Add("@PJPEntryID", SqlDbType.Int).Value = model.PJPEntryID ?? 0;
                        command.Parameters.Add("@BrandID", SqlDbType.Int).Value = model.BrandID ?? 0;
                        command.Parameters.Add("@Qty", SqlDbType.Int).Value = model.Qty ?? 0;
                        command.Parameters.Add("@AttachID", SqlDbType.VarChar).Value = model.AttachID ?? 0;
                        command.Parameters.Add("@IsActive", SqlDbType.Int).Value = 1;
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = 0;
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



        public List<SaleEntry_Import.List> GetSaleEntry_ImportList(GetResponse Modal)
        {
            List<SaleEntry_Import.List> result = new List<SaleEntry_Import.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetSaleEntryImportList", commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<SaleEntry_Import.List>().ToList();
                    }

                    DBContext.Close();
                }

            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetTourPlanImportList", "spu_GetTourPlanImportList", "DataModal", Modal.LoginID, Modal.IPAddress);

            }
            return result;
        }

        public PostResponse SetSaleEntry_Import(DataSet TempDataset, GetResponse getResponse)
        {
            PostResponse Result = new PostResponse();
            Result.SuccessMessage = "Error occurred while saving into Sale Entry import";
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetSaleEntry_Import", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        TempDataset.Tables[0].Rows.RemoveAt(0);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@LoginID", SqlDbType.Int).Value = getResponse.LoginID;
                        command.Parameters.Add("@SaleEntry_Type", SqlDbType.Structured).Value = TempDataset.Tables[0];
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

        public PostResponse UploadSaleEntryImportExcelFile(HttpPostedFileBase file, string SheetName, GetResponse getResponse)
        {
            PostResponse Result = new PostResponse();
            DataSet TempDataset = default(DataSet);
            string ExcelFileName = null;
            string DirectotyPath = null;
            string FileExtension = null;
            ArrayList SqlArrayList = new ArrayList();

            try
            {
                ExcelFileName = "SaleEntryImportData";
                DirectotyPath = ConfigurationManager.AppSettings["ApplicationPath_Physical"] + "\\Attachments\\ImportExcels";

                if (!System.IO.Directory.Exists(DirectotyPath + "\\SaleEntryImportData"))
                {
                    System.IO.Directory.CreateDirectory(DirectotyPath + "\\SaleEntryImportData");
                }
                FileExtension = System.IO.Path.GetExtension(file.FileName);
                if (FileExtension == ".xlsx" || FileExtension == ".xls")
                {

                    file.SaveAs(DirectotyPath + "\\SaleEntryImportData\\" + ExcelFileName + "" + FileExtension);
                    TempDataset = clsDataBaseHelper.GetExcelDataAsDataSet(DirectotyPath + "\\SaleEntryImportData\\" + ExcelFileName + "" + FileExtension, SheetName.Replace("'", "''").Replace("$", "") + "$");

                    if ((TempDataset != null))
                    {

                        if (TempDataset.Tables[0].Columns.Count > 0)
                        {
                            Result = SetSaleEntry_Import(TempDataset, getResponse);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Result;

        }

        public PostResponse ClearSaleEntryImportTemp(GetResponse getResponse)
        {
            PostResponse Result = new PostResponse();
            try
            {
                string SQL = "Truncate table SalesEntry_Import";
                clsDataBaseHelper.ExecuteNonQuery(SQL);
                Result.Status = true;
                Result.SuccessMessage = "data cleared";
            }
            catch (Exception ex)
            {

            }
            return Result;
        }

        public PostResponse SetSaleEntryFromSaleEntryImport(GetResponse getResponse)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetSaleEntryFromSaleEntryImport", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = getResponse.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = getResponse.IPAddress;
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






        public List<PJPPlan_Import.List> GetPJPPlan_ImportList(GetResponse Modal)
        {
            List<PJPPlan_Import.List> result = new List<PJPPlan_Import.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetPJPPlanImportList", commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<PJPPlan_Import.List>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetPJPPlanImportList", "GetPJPPlan_ImportList", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public PostResponse SetPJPPlan_Import(DataSet TempDataset, GetResponse getResponse)
        {
            PostResponse Result = new PostResponse();
            Result.SuccessMessage = "Error occurred while saving into PJP Plan import";
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetPJPPlan_Import", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        TempDataset.Tables[0].Rows.RemoveAt(0);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@LoginID", SqlDbType.Int).Value = getResponse.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = getResponse.IPAddress;
                        command.Parameters.Add("@PJPPlan_ImportType", SqlDbType.Structured).Value = TempDataset.Tables[0];
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

        public PostResponse UploadPJPPlanImportExcelFile(HttpPostedFileBase file, string SheetName, GetResponse getResponse)
        {
            PostResponse Result = new PostResponse();
            DataSet TempDataset = default(DataSet);
            string ExcelFileName = null;
            string DirectotyPath = null;
            string FileExtension = null;
            ArrayList SqlArrayList = new ArrayList();

            try
            {
                ExcelFileName = "PJPPlanImportData";
                DirectotyPath = ConfigurationManager.AppSettings["ApplicationPath_Physical"] + "\\Attachments\\ImportExcels";

                if (!System.IO.Directory.Exists(DirectotyPath + "\\PJPPlanImportData"))
                {
                    System.IO.Directory.CreateDirectory(DirectotyPath + "\\PJPPlanImportData");
                }
                FileExtension = System.IO.Path.GetExtension(file.FileName);
                if (FileExtension == ".xlsx" || FileExtension == ".xls")
                {

                    file.SaveAs(DirectotyPath + "\\PJPPlanImportData\\" + ExcelFileName + "" + FileExtension);
                    TempDataset = clsDataBaseHelper.GetExcelDataAsDataSet(DirectotyPath + "\\PJPPlanImportData\\" + ExcelFileName + "" + FileExtension, SheetName.Replace("'", "''").Replace("$", "") + "$");

                    if ((TempDataset != null))
                    {

                        if (TempDataset.Tables[0].Columns.Count > 0)
                        {
                            Result = SetPJPPlan_Import(TempDataset, getResponse);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Result;

        }

        public PostResponse ClearPJPPlanImportTemp(GetResponse getResponse)
        {
            PostResponse Result = new PostResponse();
            try
            {
                string SQL = "Truncate table PJP_Plan_Import";
                clsDataBaseHelper.ExecuteNonQuery(SQL);
                Result.Status = true;
                Result.SuccessMessage = "data cleared";
            }
            catch (Exception ex)
            {

            }
            return Result;
        }

        public PostResponse SetPJPPlanFromPJPPlanImport(GetResponse getResponse)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetPJPPlanFromPJPPlanImport", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = getResponse.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = getResponse.IPAddress;
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


        public DataSet GetMyPJPEntryList(Tab.Approval Modal)
        {
            DataSet ds = new DataSet();
            try
            {
                DateTime dt;
                DateTime.TryParse(Modal.Month, out dt);
                SqlParameter[] oparam = new SqlParameter[2];
                oparam[0] = new SqlParameter("@Date", dt.ToString("dd-MMM-yyyy"));
                oparam[1] = new SqlParameter("@LoginID", Modal.LoginID);
                ds = clsDataBaseHelper.ExecuteDataSet("spu_GetMyPJPEntryList", oparam);
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetMyPJPEntryList", "spu_GetMyPJPEntryList", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return ds;

        }

        public List<Targets.List> GetTargets_List(Tab.Approval Modal)
        {

            List<Targets.List> result = new List<Targets.List>();
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
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetTargets_List", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Targets.List>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetTargets_List", "spu_GetTargets_List", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }


        public Targets.Add GetTargets_Add(GetResponse Modal)
        {
            Targets.Add result = new Targets.Add();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@TID", dbType: DbType.Int64, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);

                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetTargets_Add", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Targets.Add>().FirstOrDefault();
                        if (result == null)
                        {
                            result = new Targets.Add();
                            result.Doctype = "";
                        }
                        if (!reader.IsConsumed)
                        {
                            result.EMPList = reader.Read<DropDownlist>().ToList();
                        }

                        if (!reader.IsConsumed)
                        {
                            result.TargetForList = reader.Read<DropDownlist>().ToList();
                        }


                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetTargets_Add", "spu_GetTargets_Add", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }
        public PostResponse fnSetTarget(Targets.Add model)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    DateTime dt;
                    DateTime.TryParse(model.TargetDate, out dt);
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetTarget", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@EMPID", SqlDbType.Int).Value = model.EMPID ?? 0;
                        command.Parameters.Add("@Month", SqlDbType.Int).Value = dt.Month;
                        command.Parameters.Add("@Year", SqlDbType.Int).Value = dt.Year;
                        command.Parameters.Add("@Qty", SqlDbType.Int).Value = model.Qty ?? 0;
                        command.Parameters.Add("@TargetFor_ID", SqlDbType.Int).Value = model.TargetFor_ID ?? 0;
                        command.Parameters.Add("@EntrySource", SqlDbType.VarChar).Value = EntrySource;
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

        public Targets.TranList GetTargetsTran_List(GetResponse Modal)
        {

            Targets.TranList result = new Targets.TranList();
            try
            {

                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@TID", dbType: DbType.Int32, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetTargetsTran_List", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result.TargetTranList = reader.Read<TargetsTran>().ToList();
                        if (!reader.IsConsumed)
                        {
                            result.TargetDetails = reader.Read<Targets.List>().FirstOrDefault();
                        }

                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetTargetsTran_List", "spu_GetTargets_List", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }


        public DataSet GetTourPlan_History(GetResponse Modal)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] oparam = new SqlParameter[2];
                oparam[0] = new SqlParameter("@TourPlanID", Modal.ID);
                oparam[1] = new SqlParameter("@LoginID", Modal.LoginID);
                ds = clsDataBaseHelper.ExecuteDataSet("spu_GetTourPlan_History", oparam);
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetTourPlan_History", "GetTourPlan_History", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return ds;

        }

        public List<BrandDisplay.List> GetBrandDisplayList(Tab.Date Modal)
        {

            List<BrandDisplay.List> result = new List<BrandDisplay.List>();
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
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetBrandDisplayList", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<BrandDisplay.List>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetBrandDisplayList", "spu_GetBrandDisplayList", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public BrandDisplay.Add GetBrandDisplay(GetResponse Modal)
        {

            BrandDisplay.Add result = new BrandDisplay.Add();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@BrandID", dbType: DbType.Int64, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetBrandDisplay", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<BrandDisplay.Add>().FirstOrDefault();
                        if (result == null)
                        {
                            result = new BrandDisplay.Add();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.ItemList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.QtyList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.DealerList = reader.Read<DropDownlist>().ToList();
                        }
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetBrandDisplay", "spu_GetBrandDisplay", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public PostResponse fnSetBrandDisplay(BrandDisplay.Add model)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetBrandDisplay", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@BrandID", SqlDbType.Int).Value = model.BrandID ?? 0;
                        command.Parameters.Add("@EMPID", SqlDbType.Int).Value = model.EMPID;
                        command.Parameters.Add("@DealerID", SqlDbType.Int).Value = model.DealerID ?? 0;
                        command.Parameters.Add("@ItemID", SqlDbType.Int).Value = model.ItemID ?? 0;
                        command.Parameters.Add("@Qty", SqlDbType.Int).Value = model.Qty ?? 0;
                        command.Parameters.Add("@AttachmentID", SqlDbType.Int).Value = model.AttachmentID ?? 0;
                        command.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = model.Remarks ?? "";
                        command.Parameters.Add("@IsActive", SqlDbType.Int).Value = 1;
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = model.Priority ?? 0;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = model.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = model.IPAddress;
                        command.Parameters.Add("@EntrySource", SqlDbType.VarChar).Value = EntrySource;
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

        public RFCEntry.Add GetRFCEntry(GetResponse Modal)
        {

            RFCEntry.Add result = new RFCEntry.Add();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@RFCID", dbType: DbType.Int64, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetRFCEntry", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<RFCEntry.Add>().FirstOrDefault();
                        if (result == null)
                        {
                            result = new RFCEntry.Add();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.NewStatusList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.RFCCategoryList = reader.Read<DropDownlist>().ToList();
                        }
                        result.OldList = new List<DropDownlist>();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetCompetitionEntry", "spu_GetCompetitionEntry", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public PJPPlan.Add GetPJPPlans(GetResponse Modal)
        {

            PJPPlan.Add result = new PJPPlan.Add();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@PJPID", dbType: DbType.Int64, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);

                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetPJP_Plans", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<PJPPlan.Add>().FirstOrDefault();
                        if (result == null)
                        {
                            result = new PJPPlan.Add();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.DealerList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.EMPList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.RouteNumberList = reader.Read<DropDownlist>().ToList();
                        }
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetPJP", "spu_GetPJP", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }
        public PostResponse fnSetPJPPlans(PJPPlan.Add modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetPJP_Plans", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@PJPID", SqlDbType.Int).Value = modal.PJPID ?? 0;
                        command.Parameters.Add("@DealerID", SqlDbType.VarChar).Value = string.Join(",", modal.DealerID);
                        command.Parameters.Add("@EMPID", SqlDbType.Int).Value = modal.EMPID ?? 0;
                        command.Parameters.Add("@VisitDate", SqlDbType.VarChar).Value = modal.VisitDate ?? "";
                        command.Parameters.Add("@IsActive", SqlDbType.Int).Value = 1;
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = modal.Priority ?? 0;
                        command.Parameters.Add("@OnDemand", SqlDbType.Int).Value = modal.OnDemand ?? 0;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = modal.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = modal.IPAddress;
                        command.Parameters.Add("@RouteNumber", SqlDbType.VarChar).Value = modal.RouteNumber ?? "";
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
        public List<PJPPlan.List> GetMyPJPPlanLists(Tab.Approval Modal)
        {

            List<PJPPlan.List> result = new List<PJPPlan.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    param.Add("@Approved", dbType: DbType.Int64, value: Modal.Approved, direction: ParameterDirection.Input);
                    param.Add("@Date", dbType: DbType.String, value: Modal.Month ?? "", direction: ParameterDirection.Input);

                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetMyPJPPlanLists", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<PJPPlan.List>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetMyPJPPlanLists", "GetMyPJPPlanLists", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }
        public List<PJPEntries.List> GetPlanWisePJPEntriesList(GetResponse Modal)
        {

            List<PJPEntries.List> result = new List<PJPEntries.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@PJPPlanID", dbType: DbType.Int32, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetPlanWisePJPEntriesList", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<PJPEntries.List>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetPlanWisePJPEntriesList", "spu_GetPlanWisePJPEntriesList", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public PJPEntries.Add GetPJPEntriesAdd(GetResponse Modal, long PJPPlanID, long PJPEntryID, long SSR_EMPID)
        {
            PJPEntries.Add result = new PJPEntries.Add();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@PJPPlanID", dbType: DbType.Int64, value: PJPPlanID, direction: ParameterDirection.Input);
                    param.Add("@PJPEntryID", dbType: DbType.Int64, value: PJPEntryID, direction: ParameterDirection.Input);
                    param.Add("@SSR_EMPID", dbType: DbType.Int64, value: SSR_EMPID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);

                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetPJPEntries", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<PJPEntries.Add>().FirstOrDefault();
                        if (result == null)
                        {
                            result = new PJPEntries.Add();
                            result.PJPPlanID = PJPPlanID;
                            result.PJPEntryID = PJPEntryID;
                        }
                        if (!reader.IsConsumed)
                        {
                            result.BrandEntryList = reader.Read<PJPEntry_Brand.List>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.BrandList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.SSRList = reader.Read<DropDownlist>().ToList();
                        }
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetPJPEntriesAdd", "GetTourPlanLists", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }


        public PostResponse fnSetPJPEntries(PJPEntries.Add model)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    float Latitude = 0, Longitude = 0;
                    float.TryParse(model.Latitude, out Latitude);
                    float.TryParse(model.Longitude, out Longitude);
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetPJPEntries", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@ContactPerson_Name", SqlDbType.VarChar).Value = model.ContactPerson_Name ?? "";
                        command.Parameters.Add("@ContactPerson_Phone", SqlDbType.VarChar).Value = model.ContactPerson_Phone ?? "";


                        command.Parameters.Add("@PJPEntryID", SqlDbType.Int).Value = model.PJPEntryID ?? 0;
                        command.Parameters.Add("@PJPPlanID", SqlDbType.Int).Value = model.PJPPlanID ?? 0;
                        command.Parameters.Add("@SSR_EMPID", SqlDbType.Int).Value = model.SSR_EMPID ?? 0;
                        command.Parameters.Add("@AttachID", SqlDbType.Int).Value = model.AttachmentID ?? 0;
                        command.Parameters.Add("@ProductRating", SqlDbType.VarChar).Value = model.ProductRating ?? 0;
                        command.Parameters.Add("@CustomerRating", SqlDbType.VarChar).Value = model.CustomerRating ?? 0;
                        command.Parameters.Add("@ProductKnw", SqlDbType.VarChar).Value = model.ProductKnw ?? "";
                        command.Parameters.Add("@CustomerKnw", SqlDbType.VarChar).Value = model.CustomerKnw ?? "";
                        command.Parameters.Add("@SSRAvailability", SqlDbType.VarChar).Value = model.SSRAvailability ?? "";
                        command.Parameters.Add("@SSR_AttendenceID", SqlDbType.VarChar).Value = model.SSR_AttendenceID ?? 0;
                        command.Parameters.Add("@Location", SqlDbType.VarChar).Value = model.Location ?? "";
                        command.Parameters.Add("@Latitude", SqlDbType.VarChar).Value = Latitude;
                        command.Parameters.Add("@Longitude", SqlDbType.VarChar).Value = Longitude;
                        command.Parameters.Add("@Error", SqlDbType.VarChar).Value = model.Error ?? "";
                        command.Parameters.Add("@Notes", SqlDbType.VarChar).Value = model.Notes ?? "";
                        command.Parameters.Add("@LastMonthACSaleQty", SqlDbType.VarChar).Value = model.LastMonthACSaleQty ?? "";
                        command.Parameters.Add("@LastMonthSaleQty", SqlDbType.VarChar).Value = model.LastMonthSaleQty ?? "";

                        //command.Parameters.Add("@ExpenseAttachmentID", SqlDbType.Int).Value = model.ExpenseAttachmentID ?? 0;
                        //command.Parameters.Add("@ExpenseAmt", SqlDbType.Decimal).Value = model.ExpenseAmt ?? 0;
                        //command.Parameters.Add("@ExpenseRemarks", SqlDbType.VarChar).Value = model.ExpenseRemarks ?? "";

                        command.Parameters.Add("@IsActive", SqlDbType.Int).Value = 1;
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = 0;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = model.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = model.IPAddress;
                        command.Parameters.Add("@EntrySource", SqlDbType.VarChar).Value = EntrySource;

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

        public PostResponse fnSetPJPEntriesWithNoSSR(PJPEntries.AddWithNoSSR model)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    float Latitude = 0, Longitude = 0;
                    float.TryParse(model.Latitude, out Latitude);
                    float.TryParse(model.Longitude, out Longitude);
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetPJPEntries", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@ContactPerson_Name", SqlDbType.VarChar).Value = model.ContactPerson_Name ?? "";
                        command.Parameters.Add("@ContactPerson_Phone", SqlDbType.VarChar).Value = model.ContactPerson_Phone ?? "";

                        command.Parameters.Add("@PJPEntryID", SqlDbType.Int).Value = model.PJPEntryID ?? 0;
                        command.Parameters.Add("@PJPPlanID", SqlDbType.Int).Value = model.PJPPlanID ?? 0;
                        command.Parameters.Add("@SSR_EMPID", SqlDbType.Int).Value = model.SSR_EMPID ?? 0;
                        command.Parameters.Add("@AttachID", SqlDbType.Int).Value = model.AttachmentID ?? 0;
                        command.Parameters.Add("@ProductRating", SqlDbType.VarChar).Value = 0;
                        command.Parameters.Add("@CustomerRating", SqlDbType.VarChar).Value = 0;
                        command.Parameters.Add("@ProductKnw", SqlDbType.VarChar).Value = "";
                        command.Parameters.Add("@CustomerKnw", SqlDbType.VarChar).Value = "";
                        command.Parameters.Add("@SSRAvailability", SqlDbType.VarChar).Value = model.SSRAvailability ?? "";
                        command.Parameters.Add("@SSR_AttendenceID", SqlDbType.VarChar).Value = model.SSR_AttendenceID ?? 0;
                        command.Parameters.Add("@Location", SqlDbType.VarChar).Value = model.Location ?? "";
                        command.Parameters.Add("@Latitude", SqlDbType.VarChar).Value = Latitude;
                        command.Parameters.Add("@Longitude", SqlDbType.VarChar).Value = Longitude;
                        command.Parameters.Add("@Error", SqlDbType.VarChar).Value = model.Error ?? "";
                        command.Parameters.Add("@Notes", SqlDbType.VarChar).Value = model.Notes ?? "";
                        command.Parameters.Add("@LastMonthACSaleQty", SqlDbType.VarChar).Value = model.LastMonthACSaleQty ?? "";
                        command.Parameters.Add("@LastMonthSaleQty", SqlDbType.VarChar).Value = model.LastMonthSaleQty ?? "";

                        //command.Parameters.Add("@ExpenseAttachmentID", SqlDbType.Int).Value = model.ExpenseAttachmentID ?? 0;
                        //command.Parameters.Add("@ExpenseAmt", SqlDbType.Decimal).Value = model.ExpenseAmt ?? 0;
                        //command.Parameters.Add("@ExpenseRemarks", SqlDbType.VarChar).Value = model.ExpenseRemarks ?? "";

                        command.Parameters.Add("@IsActive", SqlDbType.Int).Value = 1;
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = 0;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = model.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = model.IPAddress;
                        command.Parameters.Add("@EntrySource", SqlDbType.VarChar).Value = EntrySource;

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


        public PostResponse fnSetPJPEntries_Brand(PJPEntry_Brand.List model)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetPJPEntries_Brand", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@PJPBrandID", SqlDbType.Int).Value = model.PJPBrandID ?? 0;
                        command.Parameters.Add("@PJPEntryID", SqlDbType.Int).Value = model.PJPEntryID ?? 0;
                        command.Parameters.Add("@BrandID", SqlDbType.Int).Value = model.BrandID ?? 0;
                        command.Parameters.Add("@Qty", SqlDbType.Int).Value = model.Qty ?? 0;
                        command.Parameters.Add("@AttachID", SqlDbType.VarChar).Value = model.AttachID ?? 0;
                        command.Parameters.Add("@IsActive", SqlDbType.Int).Value = 1;
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = 0;
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
    }
}
