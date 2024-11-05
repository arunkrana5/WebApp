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
    public class ClientVisitModal : IClientVisitHelper
    {
        string EntrySource = "Web";
        public List<ClientVisit.List> GetClientVisitList(Tab.Approval Modal)
        {
            DateTime dt;
            DateTime.TryParse(Modal.Month, out dt);

            List<ClientVisit.List> result = new List<ClientVisit.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.ConnectionString()))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@Date", dbType: DbType.Date, value: dt.ToString("dd-MMM-yyyy"), direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetClient_Visit_List", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<ClientVisit.List>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetClient_Visit_List", "spu_GetClient_Visit_List", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }




        public PostResponse fnSetClient_Visit_CheckIn(ClientVisit.AddVisitCheckIn modal)
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
                    using (SqlCommand command = new SqlCommand("spu_SetClient_Visit_CheckIn", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@StatusID", SqlDbType.Int).Value = modal.StatusID ?? 0;
                        command.Parameters.Add("@EMPID", SqlDbType.Int).Value = modal.EMPID;
                        command.Parameters.Add("@Date", SqlDbType.VarChar).Value = modal.Date;
                        command.Parameters.Add("@DealerID", SqlDbType.Int).Value = modal.DealerID ?? 0;
                        command.Parameters.Add("@IsMasterDealer", SqlDbType.Int).Value = modal.IsMasterDealer ?? 0;
                        command.Parameters.Add("@DealerName", SqlDbType.VarChar).Value = modal.DealerName ?? "";
                        command.Parameters.Add("@PurposeOfVisit", SqlDbType.VarChar).Value = modal.PurposeOfVisit ?? "";
                        command.Parameters.Add("@Location", SqlDbType.VarChar).Value = modal.Location ?? "";
                        command.Parameters.Add("@Latitude", SqlDbType.Float).Value = Latitude;
                        command.Parameters.Add("@Longitude", SqlDbType.Float).Value = Longitude;
                        command.Parameters.Add("@Error", SqlDbType.VarChar).Value = modal.Error ?? "";
                        command.Parameters.Add("@AttachmentID", SqlDbType.Int).Value = modal.AttachmentID;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = modal.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = modal.IPAddress;
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

        public PostResponse fnSetClient_Visit_CheckOut(ClientVisit.AddVisitCheckOut modal)
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
                    using (SqlCommand command = new SqlCommand("spu_SetClient_Visit_CheckOut", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@StatusID", SqlDbType.Int).Value = modal.StatusID ?? 0;
                        command.Parameters.Add("@C_TranID", SqlDbType.Int).Value = modal.C_TranID;
                        command.Parameters.Add("@IsNoSale", SqlDbType.Int).Value = modal.IsNoSale;
                        command.Parameters.Add("@ContactPersonName", SqlDbType.VarChar).Value = modal.ContactPersonName ?? "";
                        command.Parameters.Add("@ContactPersonPhone", SqlDbType.VarChar).Value = modal.ContactPersonPhone ?? "";
                        command.Parameters.Add("@Location", SqlDbType.VarChar).Value = modal.Location ?? "";
                        command.Parameters.Add("@Latitude", SqlDbType.Float).Value = Latitude;
                        command.Parameters.Add("@Longitude", SqlDbType.Float).Value = Longitude;
                        command.Parameters.Add("@Error", SqlDbType.VarChar).Value = modal.Error ?? "";
                        command.Parameters.Add("@AttachmentID", SqlDbType.Int).Value = modal.AttachmentID;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = modal.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = modal.IPAddress;
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

        public PostResponse fnSetClient_Visit_Sales(ClientVisit.AddSales modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.ConnectionString()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetClient_Visit_Sales", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@SaleID", SqlDbType.Int).Value = modal.SaleID;
                        command.Parameters.Add("@CVisitID", SqlDbType.Int).Value = modal.CVisitID;
                        command.Parameters.Add("@C_TranID", SqlDbType.Int).Value = modal.C_TranID;
                        command.Parameters.Add("@AttachmentID", SqlDbType.Int).Value = modal.AttachmentID;
                        command.Parameters.Add("@Model", SqlDbType.VarChar).Value = modal.Model ?? "";
                        command.Parameters.Add("@Qty", SqlDbType.Int).Value = modal.Qty;
                        command.Parameters.Add("@Price", SqlDbType.Float).Value = modal.Price;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = modal.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = modal.IPAddress;
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

    }
}
