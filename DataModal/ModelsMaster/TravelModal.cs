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
    public class TravelModal : ITravelHelper
    {
        string ConnectionStrings = ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString.ToString();
        string EntrySource = "Web";
        public List<Travel.List> GetTravel_Request_List(Tab.Approval Modal)
        {

            List<Travel.List> result = new List<Travel.List>();
            try
            {
                DateTime dt;
                DateTime.TryParse(Modal.Month, out dt);
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@Month", dbType: DbType.Int64, value: dt.Month, direction: ParameterDirection.Input);
                    param.Add("@Year", dbType: DbType.Int64, value: dt.Year, direction: ParameterDirection.Input);
                    param.Add("@Approved", dbType: DbType.Int64, value: Modal.Approved, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    param.Add("@Doctype", dbType: DbType.String, value: Modal.Doctype ?? "", direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetTravel_Request_List", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Travel.List>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetMyTravelRequest_List", "GetRFCApprovalRequestList", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public Travel.Add GetTravel_Request(GetResponse Modal)
        {

            Travel.Add result = new Travel.Add();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@TRID", dbType: DbType.String, value: Modal.ID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetTravel_Request", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Travel.Add>().FirstOrDefault();
                        if (result == null)
                        {
                            result = new Travel.Add();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.CityList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.TravelModeList = reader.Read<DropDownlist>().ToList();
                        }
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetTravel_Request", "spu_GetTravel_Request", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }


        public PostResponse fnSetTravel_Request(Travel.Add model)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetTravel_Request", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@TRID", SqlDbType.Int).Value = model.TRID ?? 0;
                        command.Parameters.Add("@EMPID", SqlDbType.Int).Value = model.EMPID ?? 0;
                        command.Parameters.Add("@FromCity_ID", SqlDbType.Int).Value = model.FromCity_ID ?? 0;
                        command.Parameters.Add("@ToCity_ID", SqlDbType.Int).Value = model.ToCity_ID ?? 0;
                        command.Parameters.Add("@StartDate", SqlDbType.VarChar).Value = model.StartDate ?? "";
                        command.Parameters.Add("@EndDate", SqlDbType.VarChar).Value = model.EndDate ?? "";
                        command.Parameters.Add("@TravelMode", SqlDbType.VarChar).Value = model.TravelMode ?? "";
                        command.Parameters.Add("@IsRequiredHotel", SqlDbType.VarChar).Value = model.IsRequiredHotel;
                        command.Parameters.Add("@DealerIDs", SqlDbType.VarChar).Value = model.DealerIDs;
                        command.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = model.Remarks ?? "";
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

        public List<Travel.List> GetTravel_ApprovedRequestList(Tab.Approval Modal)
        {

            List<Travel.List> result = new List<Travel.List>();
            try
            {
                DateTime dt;
                DateTime.TryParse(Modal.Month, out dt);
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();

                    param.Add("@Month", dbType: DbType.String, value: dt.Month, direction: ParameterDirection.Input);
                    param.Add("@Year", dbType: DbType.String, value: dt.Year, direction: ParameterDirection.Input);
                    param.Add("@Approved", dbType: DbType.Int64, value: Modal.Approved, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetTravel_ApprovedRequestList", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Travel.List>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetTravel_Request_ApprovedList", "spu_GetTravel_Request_ApprovedList", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public List<VisitEntry.List> GetTravel_VisitEntry_List(Tab.Approval Modal)
        {

            List<VisitEntry.List> result = new List<VisitEntry.List>();
            try
            {
                DateTime dt;
                DateTime.TryParse(Modal.Month, out dt);
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@TRID", dbType: DbType.Int64, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@Date", dbType: DbType.String, value: (dt.Year > 1900 ? dt.ToString("dd-MMM-yyyy") : ""), direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetTravel_VisitEntry_List", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<VisitEntry.List>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetTravel_VisitEntry_List", "spu_GetTravel_VisitEntry_List", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public VisitEntry.Add GetTravel_Values(Travel.Values Modal)
        {
            VisitEntry.Add result = new VisitEntry.Add();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@TRID", dbType: DbType.Int64, value: Modal.TRID, direction: ParameterDirection.Input);
                    param.Add("@Doctype", dbType: DbType.String, value: Modal.Doctype ?? "", direction: ParameterDirection.Input);
                    param.Add("@visitDate", dbType: DbType.String, value: Modal.VisitDate, direction: ParameterDirection.Input);
                    param.Add("@DealerID", dbType: DbType.Int64, value: Modal.DealerID, direction: ParameterDirection.Input);
                    param.Add("@SSR_EMPID", dbType: DbType.Int64, value: Modal.SSR_EMPID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetTravel_Values", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<VisitEntry.Add>().FirstOrDefault();
                        if (result == null)
                        {
                            result = new VisitEntry.Add();
                        }
                        result.TRID = Modal.TRID;
                        result.DealerID = Modal.DealerID;
                        result.SSR_EMPID = Modal.SSR_EMPID;

                        if (!reader.IsConsumed)
                        {
                            result.DealerList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.SSRList = reader.Read<DropDownlist>().ToList();
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
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetTravel_Values", "spu_GetTravel_Values", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }


        public VisitEntry.Add GetTravel_VisitEntry(GetResponse Modal)
        {
            VisitEntry.Add result = new VisitEntry.Add();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@VisitID", dbType: DbType.Int64, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetTravel_VisitEntry", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<VisitEntry.Add>().FirstOrDefault();
                        if (!reader.IsConsumed)
                        {
                            result.BrandEntryList = reader.Read<VisitEntry_Brand.List>().ToList();
                        }
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetTravel_VisitEntry", "spu_GetTravel_VisitEntry", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public PostResponse fnSetTravel_VisitEntry(VisitEntry.Add model)
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
                    using (SqlCommand command = new SqlCommand("spu_SetTravel_VisitEntry", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@VisitID", SqlDbType.Int).Value = model.VisitID ?? 0;
                        command.Parameters.Add("@TRID", SqlDbType.Int).Value = model.TRID ?? 0;
                        command.Parameters.Add("@DealerID", SqlDbType.Int).Value = model.DealerID ?? 0;
                        command.Parameters.Add("@SSR_EMPID", SqlDbType.Int).Value = model.SSR_EMPID ?? 0;
                        command.Parameters.Add("@AttachID", SqlDbType.Int).Value = model.AttachmentID ?? 0;
                        command.Parameters.Add("@ProductRating", SqlDbType.VarChar).Value = model.ProductRating ?? 0;
                        command.Parameters.Add("@CustomerRating", SqlDbType.VarChar).Value = model.CustomerRating ?? 0;
                        command.Parameters.Add("@ProductKnw", SqlDbType.VarChar).Value = model.ProductKnw ?? "";
                        command.Parameters.Add("@CustomerKnw", SqlDbType.VarChar).Value = model.CustomerKnw ?? "";
                        command.Parameters.Add("@SSRAvailability", SqlDbType.VarChar).Value = model.SSRAvailability ?? "";
                        command.Parameters.Add("@Location", SqlDbType.VarChar).Value = model.Location ?? "";
                        command.Parameters.Add("@Latitude", SqlDbType.VarChar).Value = Latitude;
                        command.Parameters.Add("@Longitude", SqlDbType.VarChar).Value = Longitude;
                        command.Parameters.Add("@Error", SqlDbType.VarChar).Value = model.Error ?? "";
                        command.Parameters.Add("@Notes", SqlDbType.VarChar).Value = model.Notes ?? "";
                        command.Parameters.Add("@SSR_AttendenceID", SqlDbType.Int).Value = model.SSR_AttendenceID ?? 0;
                        command.Parameters.Add("@ExpenseAttachmentID", SqlDbType.Int).Value = model.ExpenseAttachmentID ?? 0;
                        command.Parameters.Add("@ExpenseAmt", SqlDbType.Decimal).Value = model.ExpenseAmt ?? 0;
                        command.Parameters.Add("@ExpenseRemarks", SqlDbType.VarChar).Value = model.ExpenseRemarks ?? "";

                        command.Parameters.Add("@ContactPerson_Name", SqlDbType.VarChar).Value = model.ContactPerson_Name ?? "";
                        command.Parameters.Add("@ContactPerson_Phone", SqlDbType.VarChar).Value = model.ContactPerson_Phone ?? "";

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
        public PostResponse fnSetTravel_VisitEntryWithNoSSR(VisitEntry.AddWithNoSSR model)
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
                    using (SqlCommand command = new SqlCommand("spu_SetTravel_VisitEntry", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@VisitID", SqlDbType.Int).Value = model.VisitID ?? 0;
                        command.Parameters.Add("@TRID", SqlDbType.Int).Value = model.TRID ?? 0;
                        command.Parameters.Add("@DealerID", SqlDbType.Int).Value = model.DealerID ?? 0;
                        command.Parameters.Add("@SSR_EMPID", SqlDbType.Int).Value = model.SSR_EMPID ?? 0;
                        command.Parameters.Add("@AttachID", SqlDbType.Int).Value = model.AttachmentID ?? 0;
                        command.Parameters.Add("@ProductRating", SqlDbType.VarChar).Value = 0;
                        command.Parameters.Add("@CustomerRating", SqlDbType.VarChar).Value = 0;
                        command.Parameters.Add("@ProductKnw", SqlDbType.VarChar).Value = "";
                        command.Parameters.Add("@CustomerKnw", SqlDbType.VarChar).Value = "";
                        command.Parameters.Add("@SSRAvailability", SqlDbType.VarChar).Value = model.SSRAvailability ?? "";
                        command.Parameters.Add("@Location", SqlDbType.VarChar).Value = model.Location ?? "";
                        command.Parameters.Add("@Latitude", SqlDbType.VarChar).Value = Latitude;
                        command.Parameters.Add("@Longitude", SqlDbType.VarChar).Value = Longitude;
                        command.Parameters.Add("@Error", SqlDbType.VarChar).Value = model.Error ?? "";
                        command.Parameters.Add("@Notes", SqlDbType.VarChar).Value = model.Notes ?? "";
                        command.Parameters.Add("@SSR_AttendenceID", SqlDbType.Int).Value = model.SSR_AttendenceID ?? 0;
                        command.Parameters.Add("@ExpenseAttachmentID", SqlDbType.Int).Value = model.ExpenseAttachmentID ?? 0;
                        command.Parameters.Add("@ExpenseAmt", SqlDbType.Decimal).Value = model.ExpenseAmt ?? 0;
                        command.Parameters.Add("@ExpenseRemarks", SqlDbType.VarChar).Value = model.ExpenseRemarks ?? "";
                        command.Parameters.Add("@ContactPerson_Name", SqlDbType.VarChar).Value = model.ContactPerson_Name ?? "";
                        command.Parameters.Add("@ContactPerson_Phone", SqlDbType.VarChar).Value = model.ContactPerson_Phone ?? "";

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

        public PostResponse fnSetTravel_VisitEntry_Brand(VisitEntry_Brand.List model)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetTravel_VisitEntry_Brand", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@VisitBrandID", SqlDbType.Int).Value = model.VisitBrandID ?? 0;
                        command.Parameters.Add("@VisitID", SqlDbType.Int).Value = model.VisitID ?? 0;
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


        public List<Travel.ExpenseList> GetTravel_MyExpenseList(Tab.Approval Modal)
        {

            List<Travel.ExpenseList> result = new List<Travel.ExpenseList>();
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
                    param.Add("@Doctype", dbType: DbType.String, value: Modal.Doctype ?? "", direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetTravel_MyExpenseList", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Travel.ExpenseList>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetTravel_MyExpenseList", "spu_GetTravel_MyExpenseList", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }




        public PostResponse fnSetTravel_Expense(TravelExpense Modal)
        {
            PostResponse result = new PostResponse();

            using (SqlConnection con = new SqlConnection(ClsCommon.ConnectionString()))
            {
                try
                {
                    string[] RemoveColomnIndex = { "Upload", "AttachmentPath" };
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetTravel_Expense", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ExpenseID", SqlDbType.Int).Value = Modal.ExpenseID ?? 0;
                        command.Parameters.Add("@TRID", SqlDbType.Int).Value = Modal.TRID ?? 0;
                        command.Parameters.Add("@TravelExp_Amt", SqlDbType.Decimal).Value = Modal.TravelExp_Amt ?? 0;
                        command.Parameters.Add("@VisitExp_Amt", SqlDbType.Decimal).Value = Modal.VisitExp_Amt ?? 0;
                        command.Parameters.Add("@HotelExp_Amt", SqlDbType.Decimal).Value = Modal.HotelExp_Amt ?? 0;
                        command.Parameters.Add("@TravelExpenseOther_Type", SqlDbType.Structured).Value = ClsCommon.ToDataTable(Modal.OtherExpenseList, RemoveColomnIndex);
                        command.Parameters.Add("@OtherExp_Amt", SqlDbType.Decimal).Value = Modal.OtherExp_Amt ?? 0;
                        command.Parameters.Add("@EntrySource", SqlDbType.VarChar).Value = EntrySource;
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
                    Common_SPU.LogError("Error during spu_SetTravel_Expense. The query was executed :", ex.ToString(), "spu_SetTravel_Expense()", "Common_SPU", "Common_SPU", Modal.LoginID, Modal.IPAddress);
                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;

        }


        public TravelExpense GetTravel_ExpenseSummary(GetResponse Modal)
        {
            TravelExpense result = new TravelExpense();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@TRID", dbType: DbType.Int64, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    param.Add("@Doctype", dbType: DbType.String, value: Modal.Doctype, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetTravel_ExpenseSummary", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<TravelExpense>().FirstOrDefault();
                        if (result == null)
                        {
                            result = new TravelExpense();
                            result.TRID = Modal.ID;
                        }
                        if (!reader.IsConsumed)
                        {
                            result.TravelRequestDetails = reader.Read<Travel.List>().FirstOrDefault();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.DealersAssignedList = reader.Read<Travel.Dealers>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.VisitEntryList = reader.Read<VisitEntry.List>().ToList();
                            if (result.VisitEntryList != null && (result.ExpenseID ?? 0) == 0)
                            {
                                result.VisitExp_Amt = result.VisitEntryList.Sum(x => x.ExpenseAmt);
                                result.TotalExp_Amt = result.VisitExp_Amt;
                            }
                        }
                        if (!reader.IsConsumed)
                        {
                            result.OtherExpenseList = reader.Read<OtherExpenseDet>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.AttachmentList = reader.Read<TravelExpAttachment>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.ExpenseApprovedSummaryList = reader.Read<ExpenseApprovedSummary>().ToList();
                        }


                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetTravel_ExpenseSummary", "spu_GetTravel_ExpenseSummary", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public PostResponse fnSetTravel_Requests_Approved(ApprovalAction modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetTravel_Requests_Approved", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@TRIDs", SqlDbType.VarChar).Value = modal.IDs;
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


        public List<ExpenseApprovedSummary> GetTravel_Expense_ApprovalSummary(GetResponse Modal)
        {

            List<ExpenseApprovedSummary> result = new List<ExpenseApprovedSummary>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@TRID", dbType: DbType.Int32, value: Modal.ID, direction: ParameterDirection.Input);

                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);

                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetTravel_Expense_ApprovalSummary", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<ExpenseApprovedSummary>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetTravel_Expense_ApprovalSummary", "spu_GetTravel_Expense_ApprovalSummary", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public List<Travel.ExpenseList> GetTravel_ExpenseList(Tab.Approval Modal)
        {

            List<Travel.ExpenseList> result = new List<Travel.ExpenseList>();
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
                    param.Add("@Doctype", dbType: DbType.String, value: Modal.Doctype ?? "", direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetTravel_ExpenseList", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Travel.ExpenseList>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetTravel_ExpenseList", "GetTravel_ExpenseList", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }



        public TravelCompleteRequest GetTravelCompleteRequest(GetResponse Modal)
        {
            TravelCompleteRequest result = new TravelCompleteRequest();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@TRID", dbType: DbType.Int64, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    param.Add("@Doctype", dbType: DbType.String, value: Modal.Doctype, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetTravel_ExpenseSummary", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<TravelCompleteRequest>().FirstOrDefault();
                        if (result == null)
                        {
                            result = new TravelCompleteRequest();
                            result.TRID = Modal.ID;
                        }
                        if (!reader.IsConsumed)
                        {
                            result.TravelRequestDetails = reader.Read<Travel.List>().FirstOrDefault();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.DealersAssignedList = reader.Read<Travel.Dealers>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.VisitEntryList = reader.Read<VisitEntry.List>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.OtherExpenseList = reader.Read<OtherExpenseDet>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.AttachmentList = reader.Read<TravelExpAttachment>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.ExpenseApprovedSummaryList = reader.Read<ExpenseApprovedSummary>().ToList();
                        }

                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetTravel_ExpenseSummary", "spu_GetTravel_ExpenseSummary", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }


        public PostResponse fnSetTravelExpense_Approve(ApprovalAction modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetTravel_Expense_Approved", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@TRID", SqlDbType.VarChar).Value = modal.IDs;
                        command.Parameters.Add("@Approved", SqlDbType.Int).Value = modal.Approved;
                        command.Parameters.Add("@Doctype", SqlDbType.VarChar).Value = modal.Doctype;
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


        public PostResponse fnSetTravel_Dealers(VisitEntry.AddMoreDealer modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetTravel_Dealers", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@TRID", SqlDbType.VarChar).Value = modal.TRID ?? 0;
                        command.Parameters.Add("@DealerID", SqlDbType.Int).Value = modal.DealerID ?? 0;
                        command.Parameters.Add("@OnDemand", SqlDbType.VarChar).Value = modal.OnDemand;
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


    }
}
