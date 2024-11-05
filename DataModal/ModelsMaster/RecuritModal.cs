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
    public class RecuritModal : IRecuritHelper
    {
        public static string EntrySource = "Web";

        public List<Requirement.BranchWiseEMP_Dashboard> GetBranchWiseEMP_Max_Active(GetResponse Modal)
        {

            List<Requirement.BranchWiseEMP_Dashboard> result = new List<Requirement.BranchWiseEMP_Dashboard>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.ConnectionString()))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetBranchWiseEMP_Max_Active", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Requirement.BranchWiseEMP_Dashboard>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetBranchWiseEMP_Max_Active", "spu_GetBranchWiseEMP_Max_Active", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }
        public List<Requirement.RequestList> GetRequirement_MyRequest(Tab.Approval Modal)
        {

            List<Requirement.RequestList> result = new List<Requirement.RequestList>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.ConnectionString()))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@Approved", dbType: DbType.Int64, value: Modal.Approved, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetRequirement_MyRequest", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Requirement.RequestList>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetRequirementRequestList", "spu_RequirementRequestList", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public Requirement.AddRequest GetRequirement_Request(GetResponse Modal)
        {

            Requirement.AddRequest result = new Requirement.AddRequest();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.ConnectionString()))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@REQID", dbType: DbType.Int64, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetRequirement_Request", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Requirement.AddRequest>().FirstOrDefault();
                        if (result == null)
                        {
                            result = new Requirement.AddRequest();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.RegionList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.ProductTypeList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.REC_TargetList = reader.Read<Requirement.REC_Target>().ToList();
                        }
                        result.StateList = new List<DropDownlist>();
                        result.CityList = new List<DropDownlist>();
                        result.BranchList = new List<DropDownlist>();
                        result.DealerList = new List<DropDownlist>();
                        if (result.ProductTypeList.Count > 0 && result.REC_TargetList.Count == 0)
                        {
                            List<Requirement.REC_Target> Lst = new List<Requirement.REC_Target>();

                            foreach (var item in result.ProductTypeList)
                            {
                                Lst.Add(new Requirement.REC_Target { Qty = 0, TargetForID = item.ID, ProductType = item.Name });
                            }
                            result.REC_TargetList = Lst;
                        }
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetRequirementRequestList", "spu_RequirementRequestList", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public PostResponse fnSetRequirement_Request(Requirement.AddRequest modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.ConnectionString()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetRequirement_Request", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ReqID", SqlDbType.Int).Value = modal.ReqID ?? 0;
                        command.Parameters.Add("@HiredBy", SqlDbType.VarChar).Value = modal.HiredBy ?? "";
                        command.Parameters.Add("@RequirementType", SqlDbType.VarChar).Value = modal.RequirementType ?? "";
                        command.Parameters.Add("@RegionID", SqlDbType.Int).Value = modal.RegionID ?? 0;
                        command.Parameters.Add("@StateID", SqlDbType.Int).Value = modal.StateID ?? 0;
                        command.Parameters.Add("@CityID", SqlDbType.Int).Value = modal.CityID ?? 0;
                        command.Parameters.Add("@BranchID", SqlDbType.Int).Value = modal.BranchID ?? 0;
                        command.Parameters.Add("@DealerID", SqlDbType.Int).Value = modal.DealerID ?? 0;
                        command.Parameters.Add("@Potential", SqlDbType.Int).Value = modal.Potential ?? 0;
                        command.Parameters.Add("@CandidateName", SqlDbType.VarChar).Value = modal.CandidateName ?? "";
                        command.Parameters.Add("@CandidatePhone", SqlDbType.VarChar).Value = modal.CandidatePhone ?? "";
                        command.Parameters.Add("@CandidateEmail", SqlDbType.VarChar).Value = modal.CandidateEmail ?? "";
                        command.Parameters.Add("@EntrySource", SqlDbType.VarChar).Value = EntrySource;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = modal.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = modal.IPAddress;

                        string[] RemoveColomn = { "ProductType" };
                        command.Parameters.Add("@RequirementTarget", SqlDbType.Structured).Value = ClsCommon.ToDataTable(modal.REC_TargetList, RemoveColomn);
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


        public List<Requirement.RequestList> GetRequirement_RequestList(Tab.Approval Modal)
        {

            List<Requirement.RequestList> result = new List<Requirement.RequestList>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.ConnectionString()))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@Approved", dbType: DbType.Int64, value: Modal.Approved, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetRequirement_RequestList", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Requirement.RequestList>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetRequirement_RequestList", "spu_GetRequirement_RequestList", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }


        public PostResponse fnSetRequirement_Application(Requirement.Application.Add modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.ConnectionString()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetRequirement_Application", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ApplicationID", SqlDbType.Int).Value = modal.ApplicationID;
                        command.Parameters.Add("@ReqID", SqlDbType.Int).Value = modal.ReqID;
                        command.Parameters.Add("@Name", SqlDbType.VarChar).Value = modal.Name ?? "";
                        command.Parameters.Add("@Phone", SqlDbType.VarChar).Value = modal.Phone ?? "";
                        command.Parameters.Add("@EmailID", SqlDbType.VarChar).Value = modal.EmailID ?? "";
                        command.Parameters.Add("@Qualification", SqlDbType.VarChar).Value = modal.Qualification ?? "";
                        command.Parameters.Add("@PreviousCompany", SqlDbType.VarChar).Value = modal.PreviousCompany ?? "";
                        command.Parameters.Add("@Experience", SqlDbType.VarChar).Value = modal.Experience ?? "";
                        command.Parameters.Add("@Salary", SqlDbType.Decimal).Value = modal.Salary ?? 0;
                        command.Parameters.Add("@AttachID", SqlDbType.Int).Value = modal.AttachID;
                        command.Parameters.Add("@EntrySource", SqlDbType.VarChar).Value = EntrySource;
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

        public List<Requirement.Application.List> GetRequirement_ApplicationList(GetResponse Modal)
        {

            List<Requirement.Application.List> result = new List<Requirement.Application.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.ConnectionString()))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@ReqID", dbType: DbType.Int64, value: Modal.ID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetRequirement_ApplicationList", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Requirement.Application.List>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetRequirement_RequestList", "spu_GetRequirement_RequestList", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public Requirement.FullView GetRequirement_FullView(GetResponse Modal)
        {

            Requirement.FullView result = new Requirement.FullView();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.ConnectionString()))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@ReqID", dbType: DbType.Int64, value: Modal.ID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetRequirement_FullView", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Requirement.FullView>().FirstOrDefault();
                        if (!reader.IsConsumed)
                        {
                            result.TargetList = reader.Read<Requirement.REC_Target>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.ApplicationList = reader.Read<Requirement.Application.List>().ToList();
                        }
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetRequirement_FullView", "GetRequirement_FullView", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public PostResponse fnSetRequirementApplication_Approved(long ReqID, long ApplicationID, int Approved, string ApprovedRemarks, string FinalRemarks, int IsFinalSubmit, long LoginID, string IPAddress)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.ConnectionString()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetRequirement_Application_Approved", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ReqID", SqlDbType.VarChar).Value = ReqID;
                        command.Parameters.Add("@ApplicationID", SqlDbType.VarChar).Value = @ApplicationID;
                        command.Parameters.Add("@Approved", SqlDbType.Int).Value = Approved;
                        command.Parameters.Add("@ApprovedRemarks", SqlDbType.VarChar).Value = ApprovedRemarks ?? "";
                        command.Parameters.Add("@FinalRemarks", SqlDbType.VarChar).Value = FinalRemarks ?? "";
                        command.Parameters.Add("@IsFinalSubmit", SqlDbType.Int).Value = IsFinalSubmit;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = IPAddress;
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
