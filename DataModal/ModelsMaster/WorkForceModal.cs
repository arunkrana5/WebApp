using Dapper;
using DataModal.CommanClass;
using DataModal.Models;
using DataModal.ModelsMasterHelper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataModal.Models.EMPTalentPool;

namespace DataModal.ModelsMaster
{
    public class WorkForceModal: IWorkForceHelper
    {
        string ConnectionStrings = ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString.ToString();
        string EntrySource = "Web";


        public WorkForce.Add GetWorkForce(GetResponse Modal)
        {
            WorkForce.Add result = new WorkForce.Add();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@WorkForceID", dbType: DbType.Int64, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetWorkForce_Request", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<WorkForce.Add>().FirstOrDefault();
                        if (result == null)
                        {
                            result = new WorkForce.Add();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.BranchList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.DepartList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.ProjectList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.TitleList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.RegionList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.StateList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.CityList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.TshirtSizeList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.ExperienceList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.QualificationList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.GradeList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.DesigList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.MWCategoryList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.GenderList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.MaritalStatusList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.RecruitmentTypeList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.WFDocumentList = reader.Read<WorkForce.WFDocuments>().ToList();
                        }
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetWorkForce_Request", "spu_GetWorkForce_Request", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public PostResponse fnSetWorkForceRequest(WorkForce.Add model)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetWorkForce_Request", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@WorkForceID", SqlDbType.Int).Value = model.WorkForceID;
                        command.Parameters.Add("@BranchCode", SqlDbType.VarChar).Value = model.BranchCode ?? "";
                        command.Parameters.Add("@ProjectCode", SqlDbType.VarChar).Value = model.ProjectCode ?? "";
                        command.Parameters.Add("@Title", SqlDbType.VarChar).Value = model.Title ?? "";
                        command.Parameters.Add("@Name", SqlDbType.VarChar).Value = model.Name ?? "";
                        command.Parameters.Add("@Phone", SqlDbType.VarChar).Value = model.Phone ?? "";
                        command.Parameters.Add("@Gender", SqlDbType.VarChar).Value = model.Gender ?? "";
                        command.Parameters.Add("@EmailID", SqlDbType.VarChar).Value = model.EmailID ?? "";
                        command.Parameters.Add("@MatrialStatus", SqlDbType.VarChar).Value = model.MaritalStatus ?? "";
                        command.Parameters.Add("@DOJ", SqlDbType.VarChar).Value = model.DOJ ?? "";
                        command.Parameters.Add("@AadharNo", SqlDbType.VarChar).Value = model.AadharNo ?? "";
                        command.Parameters.Add("@DOB", SqlDbType.VarChar).Value = model.DOB ?? "";
                        command.Parameters.Add("@LastCompany", SqlDbType.VarChar).Value = model.LastCompany ?? "";
                        command.Parameters.Add("@StateCode", SqlDbType.VarChar).Value = model.StateCode ?? "";
                        command.Parameters.Add("@InhandSalary", SqlDbType.Decimal).Value = model.InhandSalary;
                        command.Parameters.Add("@ExpectedSalary", SqlDbType.Decimal).Value = model.ExpectedSalary;
                        command.Parameters.Add("@TshirtSize", SqlDbType.VarChar).Value = model.TshirtSize ?? "";
                        command.Parameters.Add("@TotalExperience", SqlDbType.Float).Value = model.TotalExperience;
                        command.Parameters.Add("@Qualification", SqlDbType.VarChar).Value = model.Qualification ?? "";
                        command.Parameters.Add("@LastCompany_Designation", SqlDbType.VarChar).Value = model.LastCompany_Designation ?? "";
                        command.Parameters.Add("@PreviousSalary_Inhand", SqlDbType.Decimal).Value = model.PreviousSalary_Inhand;
                        command.Parameters.Add("@Designation", SqlDbType.VarChar).Value = model.Designation ?? "";
                        command.Parameters.Add("@ASM_Phone", SqlDbType.VarChar).Value = model.ASM_Phone ?? "";
                        command.Parameters.Add("@Department", SqlDbType.VarChar).Value = model.Department ?? "";
                        command.Parameters.Add("@RecruitementType", SqlDbType.VarChar).Value = model.RecruitmentType ?? "";
                        command.Parameters.Add("@Region", SqlDbType.VarChar).Value = model.Region ?? "";
                        command.Parameters.Add("@City", SqlDbType.VarChar).Value = model.City ?? "";
                        command.Parameters.Add("@InterviewDate", SqlDbType.VarChar).Value = model.InterviewDate ?? "";
                        command.Parameters.Add("@Grade", SqlDbType.VarChar).Value = model.Grade ?? "";
                        command.Parameters.Add("@ASMName", SqlDbType.VarChar).Value = model.ASMName ?? "";
                        command.Parameters.Add("@MWCategory", SqlDbType.VarChar).Value = model.MWCategory ?? "";
                        command.Parameters.Add("@StoreCode", SqlDbType.VarChar).Value = model.StoreCode ?? "";
                        command.Parameters.Add("@StoreName", SqlDbType.VarChar).Value = model.StoreName ?? "";
                        command.Parameters.Add("@StoreContact", SqlDbType.VarChar).Value = model.StoreContact ?? "";
                        command.Parameters.Add("@StoreAddress", SqlDbType.VarChar).Value = model.StoreAddress ?? "";
                        command.Parameters.Add("@StoreImagePath", SqlDbType.VarChar).Value = model.StoreImagePath ?? "";
                        command.Parameters.Add("@TotalCounterSales_Qty", SqlDbType.Int).Value = model.TotalCounterSales_Qty;
                        command.Parameters.Add("@ClientCurrentSales_Qty", SqlDbType.Int).Value = model.ClientCurrentSales_Qty;
                        command.Parameters.Add("@ProposedSales_Qty", SqlDbType.Int).Value = model.ProposedSales_Qty;
                        command.Parameters.Add("@ClientProductDisplay_Qty", SqlDbType.Int).Value = model.ClientProductDisplay_Qty;
                        command.Parameters.Add("@Sales_Last2ndMonth", SqlDbType.Int).Value = model.Sales_Last2ndMonth;
                        command.Parameters.Add("@Sales_Last1stMonth", SqlDbType.Int).Value = model.Sales_Last1stMonth;
                        command.Parameters.Add("@Sales_CurrentMonth", SqlDbType.Int).Value = model.Sales_CurrentMonth;
                        command.Parameters.Add("@Sales_CurrentInventory", SqlDbType.Int).Value = model.Sales_CurrentInventory;
                        command.Parameters.Add("@SOP_BranchName", SqlDbType.VarChar).Value = model.SOP_BranchName ?? "";
                        command.Parameters.Add("@SOP_OutletName", SqlDbType.VarChar).Value = model.SOP_OutletName ?? "";
                        command.Parameters.Add("@SOP_OutletLocation", SqlDbType.VarChar).Value = model.SOP_OutletLocation ?? "";
                        command.Parameters.Add("@SOP_CurrentMonth", SqlDbType.VarChar).Value = model.SOP_CurrentMonth ?? "";
                        command.Parameters.Add("@CMIConnectCode", SqlDbType.VarChar).Value = model.CMIConnectCode ?? "";
                        command.Parameters.Add("@SOP_NextMonth1", SqlDbType.VarChar).Value = model.SOP_NextMonth1 ?? "";
                        command.Parameters.Add("@SOP_NextMonth2", SqlDbType.VarChar).Value = model.SOP_NextMonth2 ?? "";
                        command.Parameters.Add("@SOP_NextMonth3", SqlDbType.VarChar).Value = model.SOP_NextMonth3 ?? "";
                        command.Parameters.Add("@SOP_NextMonth4", SqlDbType.VarChar).Value = model.SOP_NextMonth4 ?? "";
                        command.Parameters.Add("@SOP_NextMonth5", SqlDbType.VarChar).Value = model.SOP_NextMonth5 ?? "";
                        command.Parameters.Add("@SOP_NextMonth6", SqlDbType.VarChar).Value = model.SOP_NextMonth6 ?? "";
                        command.Parameters.Add("@SOR_Target1", SqlDbType.Int).Value = model.SOR_Target1;
                        command.Parameters.Add("@SOR_Target2", SqlDbType.Int).Value = model.SOR_Target2;
                        command.Parameters.Add("@SOR_Target3", SqlDbType.Int).Value = model.SOR_Target3;
                        command.Parameters.Add("@SOR_Target4", SqlDbType.Int).Value = model.SOR_Target4;
                        command.Parameters.Add("@SOR_Target5", SqlDbType.Int).Value = model.SOR_Target5;
                        command.Parameters.Add("@SOR_Target6", SqlDbType.Int).Value = model.SOR_Target6;
                        command.Parameters.Add("@SOR_RACSales1", SqlDbType.Int).Value = model.SOR_RACSales1;
                        command.Parameters.Add("@SOR_RACSales2", SqlDbType.Int).Value = model.SOR_RACSales2;
                        command.Parameters.Add("@SOR_RACSales3", SqlDbType.Int).Value = model.SOR_RACSales3;
                        command.Parameters.Add("@SOR_RACSales4", SqlDbType.Int).Value = model.SOR_RACSales4;
                        command.Parameters.Add("@SOR_RACSales5", SqlDbType.Int).Value = model.SOR_RACSales5;
                        command.Parameters.Add("@SOR_RACSales6", SqlDbType.Int).Value = model.SOR_RACSales6;
                        //command.Parameters.Add("@ReferralSource", SqlDbType.VarChar).Value = model.ReferralSource ?? "";
                        //command.Parameters.Add("@InterviewBy", SqlDbType.VarChar).Value = model.InterviewBy ?? "";
                        //command.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = model.Remarks ?? "";
                        //command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = model.IsActive;
                        //command.Parameters.Add("@Priority", SqlDbType.Int).Value = model.Priority;
                        //command.Parameters.Add("@Isdeleted", SqlDbType.Int).Value = model.Isdeleted;
                        //command.Parameters.Add("@DeletedBy", SqlDbType.Int).Value = model.DeletedBy;
                        //command.Parameters.Add("@DeletedDate", SqlDbType.VarChar).Value = model.DeletedDate ?? (object)DBNull.Value;
                        command.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = model.CreatedBy;
                        //command.Parameters.Add("@CreatedDate", SqlDbType.VarChar).Value = model.CreatedDate;
                        //command.Parameters.Add("@ModifiedDate", SqlDbType.VarChar).Value = model.ModifiedDate;
                        //command.Parameters.Add("@ModifiedBy", SqlDbType.Int).Value = model.ModifiedBy;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = model.IPAddress ?? "";
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

        public List<WorkForce.List> GetWorkForceRequestList(JqueryDatatableParam Modal)
        {

            List<WorkForce.List> result = new List<WorkForce.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
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
                    using (var reader = DBContext.QueryMultiple("spu_GetWorkForce_RequestList", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<WorkForce.List>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetAttendenceStatusList", "spu_GetAttendenceStatusList", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public PostResponse fnSetWorkForce_Approved(WorkForce.ApprovalAction modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetWorkForce_Approvals", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@WorkForceID", SqlDbType.Int).Value = modal.WorkForceID;
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

        public WorkForce_View GetWorkForce_View(Tab.Approval Modal)
        {
            WorkForce_View result = new WorkForce_View();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@WorkForceID", dbType: DbType.Int64, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@DocType", dbType: DbType.String, value: Modal.Doctype, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetWorkForce_RequestView", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result.request = reader.Read<Request>().FirstOrDefault();
                        if (result == null)
                        {
                            result = new WorkForce_View();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.attachments = reader.Read<WorkForce_Attachments>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.approvals = reader.Read<WorkForce_Approvals>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.approvalsMasters = reader.Read<WorkForce_ApprovalsMaster>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.mail = reader.Read<SMTPMail>().FirstOrDefault();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.maildetails = reader.Read<MailDetails>().FirstOrDefault();
                        }
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetWorkForce_View", "GetWorkForce_View", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }
    }
}
