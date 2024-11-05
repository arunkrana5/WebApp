using Dapper;
using DataModal.CommanClass;
using DataModal.Models;
using DataModal.ModelsMasterHelper;
using ExcelDataReader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using static DataModal.Models.EMPTalentPool;

namespace DataModal.ModelsMaster
{
    public class MasterModal : IMasterHelper
    {
        string ConnectionStrings = ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString.ToString();
        public static string EntrySource = "Web";
        public List<Masters.List> GetMastersList(JqueryDatatableParam Modal)
        {

            List<Masters.List> result = new List<Masters.List>();
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
                    param.Add("@TableName", dbType: DbType.String, value: Modal.OtherValue);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetMastersList", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Masters.List>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetMastersList", "spu_GetMastersList", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }
        public Masters.Add GetMasters(GetResponse Modal)
        {

            Masters.Add result = new Masters.Add();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@MasterID", dbType: DbType.Int64, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@GroupID", dbType: DbType.Int64, value: Modal.AdditionalID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    param.Add("@TableName", dbType: DbType.String, value: ClsCommon.EnsureString(Modal.Doctype), direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetMasters", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Masters.Add>().FirstOrDefault();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetMasters", "GetMaster", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public PostResponse fnSetMasters(Masters.Add modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetMasters", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@MasterID", SqlDbType.Int).Value = modal.MasterID;
                        command.Parameters.Add("@TableName", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.TableName);
                        command.Parameters.Add("@Name", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.Name);
                        command.Parameters.Add("@Value", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.Value);
                        command.Parameters.Add("@GroupID", SqlDbType.Int).Value = modal.GroupID;
                        command.Parameters.Add("@IsActive", SqlDbType.Int).Value = modal.IsActive;
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = modal.Priority ?? 0;
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



        public List<Branch.List> GetBranchList(GetResponse Modal)
        {

            List<Branch.List> result = new List<Branch.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetBranchList", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Branch.List>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetBranchList", "spu_GetBranch", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public Branch.Add GetBranch(GetResponse Modal)
        {

            Branch.Add result = new Branch.Add();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@BranchID", dbType: DbType.Int64, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetBranch", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Branch.Add>().FirstOrDefault();
                        if (result == null)
                        {
                            result = new Branch.Add();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.StateList = reader.Read<DropDownlist>().ToList();

                        }
                        if (!reader.IsConsumed)
                        {
                            result.CityList = reader.Read<DropDownlist>().ToList();
                            if (result.CityList == null)
                            {
                                result.CityList = new List<DropDownlist>();
                            }

                        }
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetBranch", "spu_GetBranch", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }


        public PostResponse fnSetBranch(Branch.Add modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetBranch", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@BranchID", SqlDbType.Int).Value = modal.BranchID;
                        command.Parameters.Add("@MaxEMPCount", SqlDbType.Int).Value = modal.MaxEMPCount;
                        command.Parameters.Add("@BranchCode", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.BranchCode);
                        command.Parameters.Add("@BranchName", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.BranchName);
                        command.Parameters.Add("@StateID", SqlDbType.Int).Value = modal.StateID;
                        command.Parameters.Add("@IsActive", SqlDbType.Int).Value = modal.IsActive;
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = modal.Priority ?? 0;
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


        public List<Department.List> GetDepartmentList(GetResponse Modal)
        {

            List<Department.List> result = new List<Department.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@DeptID", dbType: DbType.Int64, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetDepartment", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Department.List>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetDepartmentList", "spu_GetDepartment", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public Department.Add GetDepartment(GetResponse Modal)
        {

            Department.Add result = new Department.Add();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@DeptID", dbType: DbType.Int64, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetDepartment", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Department.Add>().FirstOrDefault();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetDepartment", "spu_GetDepartment", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }


        public PostResponse fnSetDepartment(Department.Add modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetDepartment", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@DeptID", SqlDbType.Int).Value = modal.DeptID;
                        command.Parameters.Add("@DeptCode", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.DeptCode);
                        command.Parameters.Add("@DeptName", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.DeptName);
                        command.Parameters.Add("@IsActive", SqlDbType.Int).Value = modal.IsActive;
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = modal.Priority ?? 0;
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




        public List<Designation.List> GetDesignationList(GetResponse Modal)
        {

            List<Designation.List> result = new List<Designation.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@DesignID", dbType: DbType.Int64, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetDesignation", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Designation.List>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetDesignationList", "spu_GetDesignation", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public Designation.Add GetDesignation(GetResponse Modal)
        {

            Designation.Add result = new Designation.Add();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@DesignID", dbType: DbType.Int64, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetDesignation", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Designation.Add>().FirstOrDefault();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetDesignation", "spu_GetDesignation", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }


        public PostResponse fnSetDesignation(Designation.Add modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetDesignation", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@DesignID", SqlDbType.Int).Value = modal.DesignID;
                        command.Parameters.Add("@DesignCode", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.DesignCode);
                        command.Parameters.Add("@DesignName", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.DesignName);
                        command.Parameters.Add("@IsActive", SqlDbType.Int).Value = modal.IsActive;
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = modal.Priority ?? 0;
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




        public List<Brand.List> GetBrandList(GetResponse Modal)
        {

            List<Brand.List> result = new List<Brand.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@BrandID", dbType: DbType.Int64, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetBrand", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Brand.List>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetBrandList", "spu_GetBrand", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public Brand.Add GetBrand(GetResponse Modal)
        {

            Brand.Add result = new Brand.Add();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@BrandID", dbType: DbType.Int64, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetBrand", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Brand.Add>().FirstOrDefault();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetBrand", "spu_GetBrand", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }


        public PostResponse fnSetBrand(Brand.Add modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetBrand", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@BrandID", SqlDbType.Int).Value = modal.BrandID;
                        command.Parameters.Add("@BrandName", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.BrandName);
                        command.Parameters.Add("@BrandCode", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.BrandCode);
                        command.Parameters.Add("@Description", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.Description);
                        command.Parameters.Add("@IsActive", SqlDbType.Int).Value = modal.IsActive;
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = modal.Priority ?? 0;
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




        public List<Employee.List> GetEMPList(Tab.Approval Modal)
        {

            List<Employee.List> result = new List<Employee.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@Approved", dbType: DbType.Int64, value: Modal.Approved, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetEmployeeList", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Employee.List>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetEMPList", "spu_GetEmployeeList", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public Employee.Add GetEMP(GetResponse Modal)
        {

            Employee.Add result = new Employee.Add();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@EMPID", dbType: DbType.Int64, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetEmployee", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Employee.Add>().FirstOrDefault();
                        if (result == null)
                        {
                            result = new Employee.Add();
                        }
                        if (!string.IsNullOrEmpty(result.Password))
                        {
                            result.Password = ClsCommon.Decrypt(result.Password);
                        }
                        if (!reader.IsConsumed)
                        {
                            result.DepartmentList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.DesignationList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.RoleList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.CountryList = reader.Read<DropDownlist>().ToList();
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
                            result.DealerList = reader.Read<DropDownlist>().ToList();
                        }
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetEMP", "spu_GetEmployeeList", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public PostResponse fnSetEMP(Employee.Add model)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetEMP", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@EMPID", SqlDbType.Int).Value = model.EMPID ?? 0;
                        command.Parameters.Add("@EMPName", SqlDbType.NVarChar).Value = model.EMPName ?? "";
                        command.Parameters.Add("@EMPCode", SqlDbType.NVarChar).Value = model.EMPCode ?? "";
                        command.Parameters.Add("@Phone", SqlDbType.VarChar, 50).Value = model.Phone ?? "";
                        command.Parameters.Add("@EmailID", SqlDbType.VarChar, 500).Value = model.EmailID ?? "";
                        command.Parameters.Add("@FatherName", SqlDbType.NVarChar).Value = model.FatherName ?? "";
                        command.Parameters.Add("@DOB", SqlDbType.NVarChar).Value = model.DOB ?? "";
                        command.Parameters.Add("@Gender", SqlDbType.VarChar, 50).Value = model.Gender.ToString() ?? "";
                        command.Parameters.Add("@DesignID", SqlDbType.Int).Value = model.DesignID ?? 0;
                        command.Parameters.Add("@DepartID", SqlDbType.Int).Value = model.DepartID ?? 0;
                        command.Parameters.Add("@DealerID", SqlDbType.Int).Value = model.DealerID ?? 0;

                        command.Parameters.Add("@CountryID", SqlDbType.Int).Value = model.CountryID ?? 0;
                        command.Parameters.Add("@RegionID", SqlDbType.Int).Value = model.RegionID ?? 0;
                        command.Parameters.Add("@StateID", SqlDbType.Int).Value = model.StateID ?? 0;
                        command.Parameters.Add("@CityID", SqlDbType.Int).Value = model.CityID ?? 0;
                        command.Parameters.Add("@Address1", SqlDbType.NVarChar).Value = model.Address1 ?? "";
                        command.Parameters.Add("@Address2", SqlDbType.NVarChar).Value = model.Address2 ?? "";
                        command.Parameters.Add("@Location", SqlDbType.NVarChar).Value = model.Location ?? "";
                        command.Parameters.Add("@Zipcode", SqlDbType.NVarChar).Value = model.Zipcode ?? "";


                        command.Parameters.Add("@BankName", SqlDbType.NVarChar).Value = model.BankName ?? "";
                        command.Parameters.Add("@AccountNo", SqlDbType.NVarChar).Value = model.AccountNo ?? "";
                        command.Parameters.Add("@IFSCCode", SqlDbType.NVarChar).Value = model.IFSCCode ?? "";
                        command.Parameters.Add("@BankBranch", SqlDbType.NVarChar).Value = model.BankBranch ?? "";


                        command.Parameters.Add("@DOJ", SqlDbType.VarChar).Value = model.DOJ ?? "";
                        command.Parameters.Add("@PAN", SqlDbType.VarChar, 50).Value = model.PAN ?? "";
                        command.Parameters.Add("@UAN", SqlDbType.VarChar, 50).Value = model.UAN ?? "";
                        command.Parameters.Add("@ESIC", SqlDbType.VarChar, 50).Value = model.ESIC ?? "";
                        command.Parameters.Add("@PaymentMode", SqlDbType.VarChar, 50).Value = model.PaymentMode ?? "";

                        command.Parameters.Add("@UserID", SqlDbType.VarChar).Value = model.UserID;
                        command.Parameters.Add("@Password", SqlDbType.VarChar).Value = ClsCommon.Encrypt(model.Password);
                        command.Parameters.Add("@RoleID", SqlDbType.Int).Value = model.RoleID;
                        command.Parameters.Add("@AttachID", SqlDbType.Int).Value = model.AttachID;
                        command.Parameters.Add("@IsPJPAutoAssign", SqlDbType.Int).Value = model.IsPJPAutoAssign;
                        command.Parameters.Add("@IsActive", SqlDbType.Int).Value = 1;
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = model.Priority;
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



        public List<Dealer.List> GetDealerList(JqueryDatatableParam Modal)
        {

            List<Dealer.List> result = new List<Dealer.List>();
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
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetDealerList", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Dealer.List>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetDealerList", "spu_GetDealerList", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public List<Employee.List> GetEmployeeList(JqueryDatatableParam Modal)
        {

            List<Employee.List> result = new List<Employee.List>();
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
                    using (var reader = DBContext.QueryMultiple("spu_GetEmployeeList", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Employee.List>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetEmployeeList", "spu_GetEmployeeList", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public List<DealerImport.List> GetDealerImportList(GetResponse Modal)
        {
            List<DealerImport.List> result = new List<DealerImport.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetDealerImportList", commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<DealerImport.List>().ToList();
                    }

                    DBContext.Close();
                }

            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetDealerList", "spu_GetDealerList", "DataModal", Modal.LoginID, Modal.IPAddress);

            }
            return result;
        }
        public List<DealerMappingImport.List> GetDealer_Mapping_ImportList(GetResponse Modal)
        {
            List<DealerMappingImport.List> result = new List<DealerMappingImport.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetDealer_Mapping_ImportList", commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<DealerMappingImport.List>().ToList();
                    }

                    DBContext.Close();
                }

            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetDealer_Mapping_ImportList", "GetDealer_Mapping_ImportList", "DataModal", Modal.LoginID, Modal.IPAddress);

            }
            return result;
        }

        public Dealer.Add GetDealer(GetResponse Modal)
        {

            Dealer.Add result = new Dealer.Add();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@DealerID", dbType: DbType.Int64, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetDealer", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Dealer.Add>().FirstOrDefault();
                        if (result == null)
                        {
                            result = new Dealer.Add();
                        }
                        if (!reader.IsConsumed)
                        {
                            var Mapping = reader.Read<DropDownlist>().ToList();
                            if (Mapping.Count > 0)
                            {
                                string doctype = "NSM";
                                if (Mapping.Where(x => x.Name == doctype).Select(x => x.ID).ToList().Count() > 0)
                                {
                                    result.NSM = string.Join(",", Mapping.Where(x => x.Name == doctype).Select(x => x.ID).ToList());
                                }
                                doctype = "RSM";
                                if (Mapping.Where(x => x.Name == doctype).Select(x => x.ID).ToList().Count() > 0)
                                {
                                    result.RSM = string.Join(",", Mapping.Where(x => x.Name == doctype).Select(x => x.ID).ToList());
                                }
                                doctype = "BSM";
                                if (Mapping.Where(x => x.Name == doctype).Select(x => x.ID).ToList().Count() > 0)
                                {
                                    result.BSM = string.Join(",", Mapping.Where(x => x.Name == doctype).Select(x => x.ID).ToList());
                                }
                                doctype = "ASM";
                                if (Mapping.Where(x => x.Name == doctype).Select(x => x.ID).ToList().Count() > 0)
                                {
                                    result.ASM = string.Join(",", Mapping.Where(x => x.Name == doctype).Select(x => x.ID).ToList());
                                }
                                doctype = "TL";
                                if (Mapping.Where(x => x.Name == doctype).Select(x => x.ID).ToList().Count() > 0)
                                {
                                    result.TL = string.Join(",", Mapping.Where(x => x.Name == doctype).Select(x => x.ID).ToList());
                                }
                                doctype = "RMM";
                                if (Mapping.Where(x => x.Name == doctype).Select(x => x.ID).ToList().Count() > 0)
                                {
                                    result.RMM = string.Join(",", Mapping.Where(x => x.Name == doctype).Select(x => x.ID).ToList());
                                }
                                doctype = "BMM";
                                if (Mapping.Where(x => x.Name == doctype).Select(x => x.ID).ToList().Count() > 0)
                                {
                                    result.BMM = string.Join(",", Mapping.Where(x => x.Name == doctype).Select(x => x.ID).ToList());
                                }
                                doctype = "Inhouse";
                                if (Mapping.Where(x => x.Name == doctype).Select(x => x.ID).ToList().Count() > 0)
                                {
                                    result.Inhouse = string.Join(",", Mapping.Where(x => x.Name == doctype).Select(x => x.ID).ToList());
                                }
                                doctype = "Others";
                                if (Mapping.Where(x => x.Name == doctype).Select(x => x.ID).ToList().Count() > 0)
                                {
                                    result.Others = string.Join(",", Mapping.Where(x => x.Name == doctype).Select(x => x.ID).ToList());
                                }

                            }

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

                            result.AreaList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {

                            result.BranchList = reader.Read<DropDownlist>().ToList();
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

                            var AllList = reader.Read<DropDownlist>().ToList();
                            if (AllList != null)
                            {
                                result.NSMList = AllList.Where(x => x.Other == "NSM").ToList();
                                if (result.NSMList == null)
                                {
                                    result.NSMList = new List<DropDownlist>();
                                }
                                result.RSMList = AllList.Where(x => x.Other == "RSM").ToList();
                                if (result.RSMList == null)
                                {
                                    result.RSMList = new List<DropDownlist>();
                                }
                                result.BSMList = AllList.Where(x => x.Other == "BSM").ToList();
                                if (result.BSMList == null)
                                {
                                    result.BSMList = new List<DropDownlist>();
                                }
                                result.ASMList = AllList.Where(x => x.Other == "ASM").ToList();
                                if (result.ASMList == null)
                                {
                                    result.ASMList = new List<DropDownlist>();
                                }
                                result.TLList = AllList.Where(x => x.Other == "TL").ToList();
                                if (result.TLList == null)
                                {
                                    result.TLList = new List<DropDownlist>();
                                }
                                result.RMMList = AllList.Where(x => x.Other == "RMM").ToList();
                                if (result.RMMList == null)
                                {
                                    result.RMMList = new List<DropDownlist>();
                                }
                                result.BMMList = AllList.Where(x => x.Other == "BMM").ToList();
                                if (result.BMMList == null)
                                {
                                    result.BMMList = new List<DropDownlist>();
                                }
                                result.InhouseList = AllList.Where(x => x.Other == "Inhouse").ToList();
                                if (result.InhouseList == null)
                                {
                                    result.InhouseList = new List<DropDownlist>();
                                }
                                result.OtherList = AllList.Where(x => x.Other == "Others").ToList();
                                if (result.OtherList == null)
                                {
                                    result.OtherList = new List<DropDownlist>();
                                }
                            }
                            else
                            {
                                result.NSMList = new List<DropDownlist>();
                                result.RSMList = new List<DropDownlist>();
                                result.BSMList = new List<DropDownlist>();
                                result.ASMList = new List<DropDownlist>();
                                result.TLList = new List<DropDownlist>();
                                result.RMMList = new List<DropDownlist>();
                                result.BMMList = new List<DropDownlist>();
                                result.InhouseList = new List<DropDownlist>();
                            }
                        }
                        if (!reader.IsConsumed)
                        {
                            result.VisitTypeList = reader.Read<DropDownlist>().ToList();
                        }
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetDealer", "spu_GetDealer", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }


        public PostResponse fnSetDealer(Dealer.Add model)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetDealer", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@DealerID", SqlDbType.Int).Value = model.DealerID ?? 0;
                        command.Parameters.Add("@DealerCode", SqlDbType.VarChar).Value = model.DealerCode ?? "";
                        command.Parameters.Add("@DealerName", SqlDbType.VarChar).Value = model.DealerName ?? "";
                        command.Parameters.Add("@EmailID", SqlDbType.VarChar).Value = model.EmailID ?? "";
                        command.Parameters.Add("@Phone", SqlDbType.VarChar).Value = model.Phone ?? "";
                        command.Parameters.Add("@Address", SqlDbType.VarChar).Value = model.Address ?? "";
                        command.Parameters.Add("@DealerTypeID", SqlDbType.VarChar).Value = model.DealerTypeID ?? 0;
                        command.Parameters.Add("@DealerCategoryID", SqlDbType.Int).Value = model.DealerCategoryID ?? 0;
                        command.Parameters.Add("@StateID", SqlDbType.Int).Value = model.StateID ?? 0;
                        command.Parameters.Add("@BranchID", SqlDbType.Int).Value = model.BranchID ?? 0;
                        command.Parameters.Add("@CityID", SqlDbType.Int).Value = model.CityID ?? 0;
                        command.Parameters.Add("@AreaID", SqlDbType.Int).Value = model.AreaID ?? 0;
                        command.Parameters.Add("@RegionID", SqlDbType.Int).Value = model.RegionID ?? 0;
                        command.Parameters.Add("@PinCode", SqlDbType.VarChar).Value = model.PinCode ?? "";
                        command.Parameters.Add("@Latitude", SqlDbType.VarChar).Value = model.Latitude ?? "";
                        command.Parameters.Add("@Longitude", SqlDbType.VarChar).Value = model.Longitude ?? "";
                        command.Parameters.Add("@BillingCode", SqlDbType.VarChar).Value = model.BillingCode ?? "";
                        command.Parameters.Add("@BillingName", SqlDbType.VarChar).Value = model.BillingName ?? "";
                        command.Parameters.Add("@IsActive", SqlDbType.Int).Value = 1;
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = model.Priority ?? 0;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = model.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = model.IPAddress;
                        command.Parameters.Add("@RouteNumber", SqlDbType.VarChar).Value = model.RouteNumber ?? "";
                        command.Parameters.Add("@VisitType", SqlDbType.VarChar).Value = model.VisitType ?? "";
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
        public PostResponse fnSetDealerMapping(Dealer.Add model)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetDealer_Mapping", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@DealerID", SqlDbType.Int).Value = model.DealerID ?? 0;
                        command.Parameters.Add("@NSMIDs", SqlDbType.VarChar).Value = model.NSM ?? "";
                        command.Parameters.Add("@RSMIDs", SqlDbType.VarChar).Value = model.RSM ?? "";
                        command.Parameters.Add("@BSMIDs", SqlDbType.VarChar).Value = model.BSM ?? "";
                        command.Parameters.Add("@ASMIDs", SqlDbType.VarChar).Value = model.ASM ?? "";
                        command.Parameters.Add("@TLIDs", SqlDbType.VarChar).Value = model.TL ?? "";
                        command.Parameters.Add("@RMMIDs", SqlDbType.VarChar).Value = model.RMM ?? "";
                        command.Parameters.Add("@BMMIDs", SqlDbType.VarChar).Value = model.BMM ?? "";
                        command.Parameters.Add("@InhouseIDs", SqlDbType.VarChar).Value = model.Inhouse ?? "";
                        command.Parameters.Add("@OthersIDs", SqlDbType.VarChar).Value = model.Others ?? "";
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




        public PostResponse SaveDealerImportTempDetails(DataSet TempDataset, GetResponse getResponse)
        {
            PostResponse Result = new PostResponse();
            Result.SuccessMessage = "Error occurred while saving into Dealer import";
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetDealerImport", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        TempDataset.Tables[0].Rows.RemoveAt(0);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@LoginID", SqlDbType.Int).Value = getResponse.LoginID;
                        command.Parameters.Add("@MasterDealerImportType", SqlDbType.Structured).Value = TempDataset.Tables[0];
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

            //try
            //{

            //    if (TempDataset != null && TempDataset.Tables[0].Rows.Count > 0)
            //    {
            //        int count = 0;
            //        using (SqlConnection conn = new SqlConnection(ConnectionStrings))
            //        {
            //            TempDataset.Tables[0].Rows.RemoveAt(0);
            //            SqlCommand cmd = conn.CreateCommand();
            //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //            cmd.CommandText = "dbo.spu_SetDealerImport";
            //            cmd.Parameters.AddWithValue("@LoginID", getResponse.LoginID);
            //            cmd.Parameters.AddWithValue("@MasterDealerImportType", TempDataset.Tables[0]);
            //            conn.Open();
            //            cmd.ExecuteNonQuery();                        
            //        }
            //        //clsDataBaseHelper.executeArrayOfSql(SqlArrayList);
            //        Result.Status = true;
            //        Result.SuccessMessage = "data inserted";
            //    }
            //    else
            //    {
            //        Result = "NoRecordFound";
            //    }


            //}
            //catch (Exception ex)
            //{

            //}
            return Result;

        }
        public PostResponse ClearDealerImportTemp(GetResponse getResponse)
        {
            PostResponse Result = new PostResponse();
            try
            {
                string SQL = "Truncate table Master_Dealer_Import";
                clsDataBaseHelper.ExecuteNonQuery(SQL);
                Result.Status = true;
                Result.SuccessMessage = "data cleared";
            }
            catch (Exception ex)
            {
                Result.StatusCode = -1;
                Result.SuccessMessage = ex.ToString();
            }
            return Result;
        }
        public PostResponse ClearDealerMappingImportTemp(GetResponse getResponse)
        {
            PostResponse Result = new PostResponse();
            try
            {
                string SQL = "Truncate table Master_Dealer_Mapping_Import";
                clsDataBaseHelper.ExecuteNonQuery(SQL);
                Result.Status = true;
                Result.SuccessMessage = "data cleared";
            }
            catch (Exception ex)
            {
                Result.StatusCode = -1;
                Result.SuccessMessage = ex.ToString();
            }
            return Result;
        }
        public PostResponse UploadDealerImportDetailList(GetResponse getResponse)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetDealerFromDealerImport", con))
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
        public PostResponse UploadDealerMappingImportDetailList(GetResponse getResponse)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetDealerMappingFromDealerMappingImport", con))
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


        public List<AttendenceStatus.List> GetAttendenceStatusList(GetResponse Modal)
        {

            List<AttendenceStatus.List> result = new List<AttendenceStatus.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetAttendenceStatusList", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<AttendenceStatus.List>().ToList();
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

        public AttendenceStatus.Add GetAttendenceStatus(GetResponse Modal)
        {

            AttendenceStatus.Add result = new AttendenceStatus.Add();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@ID", dbType: DbType.Int32, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@Doctype", dbType: DbType.String, value: Modal.Doctype ?? "", direction: ParameterDirection.Input);
                    param.Add("@LoginId", dbType: DbType.Int32, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetAttendenceStatus", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<AttendenceStatus.Add>().FirstOrDefault();
                        if (result == null)
                        {
                            result = new AttendenceStatus.Add();
                            result.UseFor = "";
                        }
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetLeaveType", "spu_GetLeaveType", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }


        public PostResponse fnSetAttendenceStatus(AttendenceStatus.Add model)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetAttendenceStatus", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@ID", SqlDbType.Int).Value = model.ID ?? 0;
                        command.Parameters.Add("@Status_Name", SqlDbType.VarChar).Value = model.Status ?? "";
                        command.Parameters.Add("@DisplayName", SqlDbType.VarChar).Value = model.DisplayName ?? "";
                        command.Parameters.Add("@Icon", SqlDbType.VarChar).Value = model.Icon ?? "";
                        command.Parameters.Add("@Color", SqlDbType.VarChar).Value = model.Color ?? "";
                        command.Parameters.Add("@MonthlyAccrued", SqlDbType.VarChar).Value = model.MonthlyAccrued ?? 0;
                        command.Parameters.Add("@UseFor", SqlDbType.VarChar).Value = model.UseFor ?? "";
                        command.Parameters.Add("@IsActive", SqlDbType.Int).Value = 1;
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







        public List<EMPImport.List> GetEMPImportList(GetResponse Modal)
        {
            List<EMPImport.List> result = new List<EMPImport.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetEMPImportList", commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<EMPImport.List>().ToList();
                    }
                    if (result != null && result.Count > 0)
                    {
                        foreach (var item in result)
                        {
                            item.Password = ClsCommon.Decrypt(item.Password);
                        };
                    }

                    DBContext.Close();
                }

            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetDealerList", "spu_GetDealerList", "DataModal", Modal.LoginID, Modal.IPAddress);

            }
            return result;
        }


        public PostResponse UploadEMPImportDataExcelFile(HttpPostedFileBase file, string SheetName, GetResponse getResponse)
        {
            PostResponse Result = new PostResponse();
            DataSet TempDataset = default(DataSet);
            string ExcelFileName = null;
            string DirectotyPath = null;
            string FileExtension = null;
            ArrayList SqlArrayList = new ArrayList();

            try
            {
                ExcelFileName = "EMPImportData";
                DirectotyPath = ConfigurationManager.AppSettings["ApplicationPath_Physical"] + "\\Attachments\\ImportExcels";

                if (!System.IO.Directory.Exists(DirectotyPath + "\\EMPImportData"))
                {
                    System.IO.Directory.CreateDirectory(DirectotyPath + "\\EMPImportData");
                }
                FileExtension = System.IO.Path.GetExtension(file.FileName);
                if (FileExtension == ".xlsx" || FileExtension == ".xls")
                {

                    file.SaveAs(DirectotyPath + "\\EMPImportData\\" + ExcelFileName + "" + FileExtension);
                    TempDataset = clsDataBaseHelper.GetExcelDataAsDataSet(DirectotyPath + "\\EMPImportData\\" + ExcelFileName + "" + FileExtension, SheetName.Replace("'", "''").Replace("$", "") + "$");

                    if ((TempDataset != null))
                    {

                        if (TempDataset.Tables[0].Columns.Count > 0)
                        {
                            Result = SaveEMPImportTempDetails(TempDataset, getResponse);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Result;

        }

        public PostResponse SaveEMPImportTempDetails(DataSet TempDataset, GetResponse getResponse)
        {
            PostResponse Result = new PostResponse();
            Result.SuccessMessage = "Error occurred while saving into EMP import";
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetEMPImport", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        TempDataset.Tables[0].Rows.RemoveAt(0);

                        foreach (DataRow dtRow in TempDataset.Tables[0].Rows)
                        {
                            foreach (DataColumn dc in TempDataset.Tables[0].Columns)
                            {
                                if (dc.ColumnName == "F27")
                                {
                                    dtRow["F27"] = ClsCommon.Encrypt(dtRow["F27"].ToString());
                                }
                            }
                        }

                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@LoginID", SqlDbType.Int).Value = getResponse.LoginID;
                        command.Parameters.Add("@EMPImportType", SqlDbType.Structured).Value = TempDataset.Tables[0];
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
        public PostResponse ClearEMPImportTemp(GetResponse getResponse)
        {
            PostResponse Result = new PostResponse();
            try
            {
                string SQL = "Truncate table EMP_Import";
                clsDataBaseHelper.ExecuteNonQuery(SQL);
                Result.Status = true;
                Result.SuccessMessage = "data cleared";
            }
            catch (Exception ex)
            {

            }
            return Result;
        }
        public PostResponse SetEMPFromEMPImport(GetResponse getResponse)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetEMPFromEMPImport", con))
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



        public List<MastersImport.List> GetMastersImportList(GetResponse Modal)
        {
            List<MastersImport.List> result = new List<MastersImport.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetMastersImportList", commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<MastersImport.List>().ToList();
                    }

                    DBContext.Close();
                }

            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetMastersImportList", "spu_GetMastersImportList", "DataModal", Modal.LoginID, Modal.IPAddress);

            }
            return result;
        }


        public PostResponse UploadMastersImportDataExcelFile(HttpPostedFileBase file, string SheetName, GetResponse getResponse)
        {
            PostResponse Result = new PostResponse();
            DataSet TempDataset = default(DataSet);
            string ExcelFileName = null;
            string DirectotyPath = null;
            string FileExtension = null;
            ArrayList SqlArrayList = new ArrayList();

            try
            {
                ExcelFileName = "MastersImportData";
                DirectotyPath = ConfigurationManager.AppSettings["ApplicationPath_Physical"] + "\\Attachments\\ImportExcels";

                if (!System.IO.Directory.Exists(DirectotyPath + "\\MastersImportData"))
                {
                    System.IO.Directory.CreateDirectory(DirectotyPath + "\\MastersImportData");
                }
                FileExtension = System.IO.Path.GetExtension(file.FileName);
                if (FileExtension == ".xlsx" || FileExtension == ".xls")
                {

                    file.SaveAs(DirectotyPath + "\\MastersImportData\\" + ExcelFileName + "" + FileExtension);
                    TempDataset = clsDataBaseHelper.GetExcelDataAsDataSet(DirectotyPath + "\\MastersImportData\\" + ExcelFileName + "" + FileExtension, SheetName.Replace("'", "''").Replace("$", "") + "$");

                    if ((TempDataset != null))
                    {

                        if (TempDataset.Tables[0].Columns.Count > 0)
                        {
                            Result = SaveMastersImportTempDetails(TempDataset, getResponse);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Result;

        }

        public PostResponse SaveMastersImportTempDetails(DataSet TempDataset, GetResponse getResponse)
        {
            PostResponse Result = new PostResponse();
            Result.SuccessMessage = "Error occurred while saving into EMP import";
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetMastersImport", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        TempDataset.Tables[0].Rows.RemoveAt(0);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@LoginID", SqlDbType.Int).Value = getResponse.LoginID;
                        command.Parameters.Add("@Masters_ImportType", SqlDbType.Structured).Value = TempDataset.Tables[0];
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
        public PostResponse ClearMastersImportTemp(GetResponse getResponse)
        {
            PostResponse Result = new PostResponse();
            try
            {
                string SQL = "Truncate table Masters_Import";
                clsDataBaseHelper.ExecuteNonQuery(SQL);
                Result.Status = true;
                Result.SuccessMessage = "data cleared";
            }
            catch (Exception ex)
            {

            }
            return Result;
        }
        public PostResponse SetMastersFromMastersImport(GetResponse getResponse)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetMastersFromMastersImport", con))
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


        public PostResponse fnSetEMP_TalentPool(EMPTalentPool.Add model)
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
                    using (SqlCommand command = new SqlCommand("spu_SetEMP_TalentPool", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@TPID", SqlDbType.Int).Value = model.TPID ?? 0;
                        command.Parameters.Add("@Name", SqlDbType.VarChar).Value = model.Name ?? "";
                        command.Parameters.Add("@Age", SqlDbType.Int).Value = model.Age;
                        command.Parameters.Add("@CounterName", SqlDbType.VarChar).Value = model.CounterName ?? "";
                        command.Parameters.Add("@Location", SqlDbType.VarChar).Value = model.Location ?? "";
                        command.Parameters.Add("@TotalExperience", SqlDbType.VarChar).Value = model.TotalExperience ?? "";
                        command.Parameters.Add("@CurrentCompany", SqlDbType.VarChar).Value = model.CurrentCompany ?? "";
                        command.Parameters.Add("@TenureWithCompany", SqlDbType.VarChar).Value = model.TenureWithCompany ?? "";
                        command.Parameters.Add("@CurrentSalary", SqlDbType.Decimal).Value = model.CurrentSalary;
                        command.Parameters.Add("@ExpectedSalary", SqlDbType.Decimal).Value = model.ExpectedSalary;
                        command.Parameters.Add("@Pincode", SqlDbType.VarChar).Value = model.Pincode;
                        command.Parameters.Add("@Mobile", SqlDbType.VarChar).Value = model.Mobile;
                        command.Parameters.Add("@Email", SqlDbType.VarChar).Value = model.Email;
                        command.Parameters.Add("@DOB", SqlDbType.VarChar).Value = model.DOB;
                        command.Parameters.Add("@WorkProfile", SqlDbType.VarChar).Value = model.WorkProfile;
                        command.Parameters.Add("@DealerID", SqlDbType.Int).Value = model.DealerID ?? 0;
                        command.Parameters.Add("@BranchID", SqlDbType.Int).Value = model.BranchID ?? 0;
                        command.Parameters.Add("@Address", SqlDbType.VarChar).Value = model.Address ?? "";
                        command.Parameters.Add("@State", SqlDbType.Int).Value = model.State ?? 0;
                        command.Parameters.Add("@City", SqlDbType.Int).Value = model.City ?? 0;
                        command.Parameters.Add("@Trade_Experience", SqlDbType.VarChar).Value = model.Trade_Experience ?? "";
                        command.Parameters.Add("@Qualification", SqlDbType.VarChar).Value = model.Qualification ?? "";
                        command.Parameters.Add("@CW_Company", SqlDbType.VarChar).Value = model.CW_Company ?? "";
                        command.Parameters.Add("@CW_Address", SqlDbType.VarChar).Value = model.CW_Address ?? "";
                        command.Parameters.Add("@CW_State", SqlDbType.Int).Value = model.CW_State ?? 0;
                        command.Parameters.Add("@CW_City", SqlDbType.Int).Value = model.CW_City ?? 0;
                        command.Parameters.Add("@CW_Pincode", SqlDbType.VarChar).Value = model.CW_Pincode ?? "";
                        command.Parameters.Add("@CW_Salary", SqlDbType.Decimal).Value = model.CW_Salary;
                        command.Parameters.Add("@Latitude", SqlDbType.Float).Value = Latitude;
                        command.Parameters.Add("@Longitude", SqlDbType.Float).Value = Longitude;
                        command.Parameters.Add("@Error", SqlDbType.VarChar).Value = model.Error ?? "";
                        command.Parameters.Add("@EntrySource", SqlDbType.VarChar, 50).Value = EntrySource;
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

        public PostResponse fnSetBranch_Mapping(long BranchID, string LinkID, GetResponse Modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetBranch_Mapping", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@BranchID", SqlDbType.Int).Value = BranchID;
                        command.Parameters.Add("@LinkID", SqlDbType.Int).Value = LinkID;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = Modal.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = Modal.IPAddress;
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

        public List<ChallanDocuments.List> GetChallanDocumentsList(Tab.Date Modal)
        {
            List<ChallanDocuments.List> result = new List<ChallanDocuments.List>();
            try
            {
                DateTime dt;
                DateTime.TryParse(Modal.Month, out dt);
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;

                    var param = new DynamicParameters();
                    param.Add("@Date", dbType: DbType.String, value: (dt.Year > 1900 ? dt.ToString("dd-MMM-yyyy") : ""), direction: ParameterDirection.Input);
                    param.Add("@LoginId", dbType: DbType.Int32, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetChallanDocumentsList", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<ChallanDocuments.List>().ToList();
                    }

                    DBContext.Close();
                }

            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetChallanDocumentsList", "spu_GetChallanDocumentsList", "DataModal", Modal.LoginID, Modal.IPAddress);

            }
            return result;
        }
        public ChallanDocuments.Add GetChallanDocuments(GetResponse Modal)
        {

            ChallanDocuments.Add result = new ChallanDocuments.Add();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@ChallanID", dbType: DbType.Int32, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginId", dbType: DbType.Int32, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetChallanDocuments", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<ChallanDocuments.Add>().FirstOrDefault();
                        if (result == null)
                        {
                            result = new ChallanDocuments.Add();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.StateList = reader.Read<DropDownlist>().ToList();
                        }

                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetChallanDocuments", "spu_GetChallanDocuments", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }
        public PostResponse fnSetChallanDocuments(ChallanDocuments.Add Modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    DateTime dt;
                    DateTime.TryParse(Modal.Date, out dt);
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetChallanDocuments", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ChallanID", SqlDbType.Int).Value = Modal.ChallanID ?? 0;
                        command.Parameters.Add("@AttachmentID", SqlDbType.Int).Value = Modal.AttachmentID;
                        command.Parameters.Add("@MonthYear", SqlDbType.VarChar).Value = dt.ToString("dd-MMM-yyyy");
                        command.Parameters.Add("@StateID", SqlDbType.Int).Value = Modal.StateID ?? 0;
                        command.Parameters.Add("@ChallanType", SqlDbType.VarChar).Value = Modal.ChallanType;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = Modal.LoginID;
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = Modal.Priority ?? 0;
                        command.Parameters.Add("@IsActive", SqlDbType.Int).Value = 0;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = Modal.IPAddress;
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

        public List<EMPTalentPoolImport.List> GetEMPTalentPoolImportList(GetResponse Modal)
        {
            List<EMPTalentPoolImport.List> result = new List<EMPTalentPoolImport.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetEMP_TalentPoolImportList", commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<EMPTalentPoolImport.List>().ToList();
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


        public PostResponse UploadEMP_TalentPoolImportDataExcelFile(HttpPostedFileBase file, string SheetName, GetResponse getResponse)
        {
            PostResponse Result = new PostResponse();
            DataSet TempDataset = default(DataSet);
            string ExcelFileName = null;
            string DirectotyPath = null;
            string FileExtension = null;
            ArrayList SqlArrayList = new ArrayList();

            try
            {
                ExcelFileName = "EMP_TalentPoolImportData";
                DirectotyPath = ConfigurationManager.AppSettings["ApplicationPath_Physical"] + "\\Attachments\\ImportExcels";

                if (!System.IO.Directory.Exists(DirectotyPath + "\\EMP_TalentPoolImportData"))
                {
                    System.IO.Directory.CreateDirectory(DirectotyPath + "\\EMP_TalentPoolImportData");
                }
                FileExtension = System.IO.Path.GetExtension(file.FileName);
                if (FileExtension == ".xlsx" || FileExtension == ".xls")
                {

                    file.SaveAs(DirectotyPath + "\\EMP_TalentPoolImportData\\" + ExcelFileName + "" + FileExtension);
                    TempDataset = clsDataBaseHelper.GetExcelDataAsDataSet(DirectotyPath + "\\EMP_TalentPoolImportData\\" + ExcelFileName + "" + FileExtension, SheetName.Replace("'", "''").Replace("$", "") + "$");

                    if ((TempDataset != null))
                    {

                        if (TempDataset.Tables[0].Columns.Count > 0)
                        {
                            Result = SaveEMP_TalentPoolImportTempDetails(TempDataset, getResponse);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Result;

        }

        public PostResponse SaveEMP_TalentPoolImportTempDetails(DataSet TempDataset, GetResponse getResponse)
        {
            PostResponse Result = new PostResponse();
            Result.SuccessMessage = "Error occurred while saving into EMP import";
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetEMP_TalentPool_Import", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        TempDataset.Tables[0].Rows.RemoveAt(0);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@LoginID", SqlDbType.Int).Value = getResponse.LoginID;
                        command.Parameters.Add("@EMP_TalentPoolType", SqlDbType.Structured).Value = TempDataset.Tables[0];
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


        public PostResponse ClearEMP_TalentPoolImportTemp(GetResponse getResponse)
        {
            PostResponse Result = new PostResponse();
            try
            {
                string SQL = "Truncate table EMP_TalentPool_Import";
                clsDataBaseHelper.ExecuteNonQuery(SQL);
                Result.Status = true;
                Result.SuccessMessage = "data cleared";
            }
            catch (Exception ex)
            {

            }
            return Result;
        }

        public PostResponse SetEMP_TalentPoolFromImportTable(GetResponse getResponse)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetEMP_TalentPoolFromImportTable", con))
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




        public List<DealerCategory.List> GetDealerCategoryList(GetResponse Modal)
        {

            List<DealerCategory.List> result = new List<DealerCategory.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@DealerCategoryID", dbType: DbType.Int64, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetDealerCategory", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<DealerCategory.List>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetDealerCategoryList", "GetMastersList", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public DealerCategory.Add GetDealerCategory(GetResponse Modal)
        {

            DealerCategory.Add result = new DealerCategory.Add();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@DealerCategoryID", dbType: DbType.Int64, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetDealerCategory", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<DealerCategory.Add>().FirstOrDefault();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetDealerCategory", "spu_GetDealerCategory", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public PostResponse fnSetDealerCategory(DealerCategory.Add modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetDealerCategory", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@DealerCategoryID", SqlDbType.Int).Value = modal.DealerCategoryID;
                        command.Parameters.Add("@Name", SqlDbType.VarChar).Value = modal.Name ?? "";
                        command.Parameters.Add("@Code", SqlDbType.VarChar).Value = modal.Code ?? "";
                        command.Parameters.Add("@IsActive", SqlDbType.Int).Value = modal.IsActive;
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = modal.Priority ?? 0;
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



        public List<DealerType.List> GetDealerTypeList(GetResponse Modal)
        {

            List<DealerType.List> result = new List<DealerType.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@DealerTypeID", dbType: DbType.Int64, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetDealerType", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<DealerType.List>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetDealerTypeList", "GetMastersList", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public DealerType.Add GetDealerType(GetResponse Modal)
        {

            DealerType.Add result = new DealerType.Add();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@DealerTypeID", dbType: DbType.Int64, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetDealerType", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<DealerType.Add>().FirstOrDefault();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetDealerType", "spu_GetDealerType", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public PostResponse fnSetDealerType(DealerType.Add modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetDealerType", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@DealerTypeID", SqlDbType.Int).Value = modal.DealerTypeID;
                        command.Parameters.Add("@Name", SqlDbType.VarChar).Value = modal.Name ?? "";
                        command.Parameters.Add("@Code", SqlDbType.VarChar).Value = modal.Code ?? "";
                        command.Parameters.Add("@IsActive", SqlDbType.Int).Value = modal.IsActive;
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = modal.Priority ?? 0;
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


        public List<MasterCatalogue.List> GetMasterCatalogueList(Tab.Approval Modal)
        {
            List<MasterCatalogue.List> result = new List<MasterCatalogue.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;

                    var param = new DynamicParameters();
                    param.Add("@CatID", dbType: DbType.Int32, value: 0, direction: ParameterDirection.Input);
                    param.Add("@LoginId", dbType: DbType.Int32, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetMaster_Catalogue", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<MasterCatalogue.List>().ToList();
                    }

                    DBContext.Close();
                }

            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "MasterCatalogue", "spu_GetMaster_Catalogue", "DataModal", Modal.LoginID, Modal.IPAddress);

            }
            return result;
        }
        public MasterCatalogue.Add GetMasterCatalogue(GetResponse Modal)
        {

            MasterCatalogue.Add result = new MasterCatalogue.Add();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@CatID", dbType: DbType.Int32, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginId", dbType: DbType.Int32, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetMaster_Catalogue", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<MasterCatalogue.Add>().FirstOrDefault();
                        if (result == null)
                        {
                            result = new MasterCatalogue.Add();
                        }

                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "MasterCatalogue", "MasterCatalogue", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }
        public PostResponse fnSetMasterCatalogue(MasterCatalogue.Add Modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetMaster_Catalogue", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@CatID", SqlDbType.Int).Value = Modal.CatID ?? 0;
                        command.Parameters.Add("@AttachmentID", SqlDbType.Int).Value = Modal.AttachmentID ?? 0;
                        command.Parameters.Add("@ProductName", SqlDbType.VarChar).Value = Modal.ProductName;
                        command.Parameters.Add("@Description", SqlDbType.VarChar).Value = Modal.Description ?? "";
                        command.Parameters.Add("@URL", SqlDbType.VarChar).Value = Modal.URL ?? "";
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = Modal.LoginID;
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = Modal.Priority ?? 0;
                        command.Parameters.Add("@IsActive", SqlDbType.Int).Value = 0;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = Modal.IPAddress;
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


        public PostResponse DealerImport_UploadData(HttpPostedFileBase file, GetResponse getResponse)
        {
            PostResponse Response = new PostResponse();
            DataSet ResultDataSet = default(DataSet);
            try
            {
                Stream stream = file.InputStream;
                IExcelDataReader reader = null;
                if (file.FileName.EndsWith(".xls"))
                {
                    reader = ExcelReaderFactory.CreateBinaryReader(stream);
                }
                else if (file.FileName.EndsWith(".xlsx"))
                {
                    reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                }
                else if (file.FileName.EndsWith(".csv"))
                {
                    reader = ExcelReaderFactory.CreateCsvReader(stream);
                }
                ResultDataSet = reader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow = true
                    }
                });
                reader.Close();

                if (ResultDataSet != null)
                {
                    DataTable dtCloned = ResultDataSet.Tables[0].Clone();
                    DataColumnCollection columns = ResultDataSet.Tables[0].Columns;
                    dtCloned.Columns["Latitude"].DataType = typeof(float);
                    dtCloned.Columns["Longitude"].DataType = typeof(float);

                    for (int i = 0; i < dtCloned.Columns.Count; i++)
                    {
                        if (CONSTANT.DealerImport_Column[i].ToLower() != dtCloned.Columns[i].ColumnName.ToLower())
                        {
                            Response.Status = false;
                            Response.SuccessMessage = "we need " + CONSTANT.DealerImport_Column.Count + " column must be same as given template. Found error in " + dtCloned.Columns[i].ColumnName + "!";
                            return Response;
                        }
                    }
                    foreach (DataRow row in ResultDataSet.Tables[0].Rows)
                    {
                        foreach (DataColumn c in columns)
                        {
                            if (row.IsNull(c))
                            {
                                if (c.ColumnName.Contains("Latitude,Longitude"))
                                {
                                    row[c] = -1;
                                }
                                else
                                {
                                    row[c] = "";
                                }
                            }
                            else
                            {
                                if (!c.ColumnName.Contains("Latitude,Longitude"))
                                {
                                    row[c] = row[c].ToString().Trim();
                                }
                            }
                        }
                        dtCloned.ImportRow(row);
                    }
                    if (dtCloned != null)
                    {
                        dtCloned.Columns.Add("Remarks", typeof(string)).Expression = "'Draft'";
                        dtCloned.Columns.Add("CreatedBy", typeof(string)).Expression = "'" + getResponse.LoginID + "'";
                        dtCloned.Columns.Add("CreatedDate", typeof(string)).Expression = "'" + DateTime.Now + "'";
                        dtCloned.Columns.Add("IPAddress", typeof(string)).Expression = "'" + getResponse.IPAddress + "'";
                        dtCloned.AcceptChanges();
                        using (SqlConnection con = new SqlConnection(ClsCommon.ConnectionString()))
                        {
                            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                            {
                                try
                                {
                                    sqlBulkCopy.DestinationTableName = "dbo.Master_Dealer_Import";
                                    foreach (var item in CONSTANT.DealerImport_Column)
                                    {
                                        sqlBulkCopy.ColumnMappings.Add(item.ToString(), item.ToString());
                                    }
                                    sqlBulkCopy.ColumnMappings.Add("Remarks", "Remarks");
                                    sqlBulkCopy.ColumnMappings.Add("CreatedBy", "CreatedBy");
                                    sqlBulkCopy.ColumnMappings.Add("CreatedDate", "CreatedDate");
                                    sqlBulkCopy.ColumnMappings.Add("IPAddress", "IPAddress");

                                    con.Open();
                                    sqlBulkCopy.BatchSize = 50;
                                    sqlBulkCopy.BulkCopyTimeout = 1000;
                                    sqlBulkCopy.WriteToServer(dtCloned);
                                    con.Close();
                                    Response.ID = 1;
                                    Response.StatusCode = 1;
                                    Response.SuccessMessage = "Uploded Successfully";
                                    Response.Status = true;
                                }
                                catch (Exception ex)
                                {
                                    con.Close();
                                    Response.Status = false;
                                    Response.SuccessMessage = "data not found, please check excel colomn";

                                }
                                finally
                                {
                                    con.Close();
                                }


                            }
                        }
                    }
                    else
                    {
                        Response.Status = false;
                        Response.SuccessMessage = "data not found, please check excel colomn";
                        return Response;
                    }
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = -1;
                Response.SuccessMessage = ex.Message.ToString();
            }
            return Response;
        }

        public PostResponse DealerMappingImport_UploadData(HttpPostedFileBase file, GetResponse getResponse)
        {
            PostResponse Response = new PostResponse();
            DataSet ResultDataSet = default(DataSet);
            try
            {
                Stream stream = file.InputStream;
                IExcelDataReader reader = null;
                if (file.FileName.EndsWith(".xls"))
                {
                    reader = ExcelReaderFactory.CreateBinaryReader(stream);
                }
                else if (file.FileName.EndsWith(".xlsx"))
                {
                    reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                }
                else if (file.FileName.EndsWith(".csv"))
                {
                    reader = ExcelReaderFactory.CreateCsvReader(stream);
                }
                ResultDataSet = reader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow = true
                    }
                });
                reader.Close();

                if (ResultDataSet != null)
                {
                    DataTable dtCloned = ResultDataSet.Tables[0].Clone();
                    DataColumnCollection columns = ResultDataSet.Tables[0].Columns;

                    for (int i = 0; i < dtCloned.Columns.Count; i++)
                    {
                        if (CONSTANT.DealerMappingImport_Column[i].ToLower() != dtCloned.Columns[i].ColumnName.ToLower())
                        {
                            Response.Status = false;
                            Response.SuccessMessage = "we need " + CONSTANT.DealerMappingImport_Column.Count + " column must be same as given template. Found error in " + dtCloned.Columns[i].ColumnName + "!";
                            return Response;
                        }
                    }
                    foreach (DataRow row in ResultDataSet.Tables[0].Rows)
                    {
                        foreach (DataColumn c in columns)
                        {
                            if (row.IsNull(c))
                            {
                                row[c] = "";
                            }
                            else
                            {
                                row[c] = row[c].ToString().Trim();
                            }
                        }
                        dtCloned.ImportRow(row);
                    }
                    if (dtCloned != null)
                    {
                        dtCloned.Columns.Add("Remarks", typeof(string)).Expression = "'Draft'";
                        dtCloned.Columns.Add("CreatedBy", typeof(string)).Expression = "'" + getResponse.LoginID + "'";
                        dtCloned.Columns.Add("CreatedDate", typeof(string)).Expression = "'" + DateTime.Now + "'";
                        dtCloned.Columns.Add("IPAddress", typeof(string)).Expression = "'" + getResponse.IPAddress + "'";
                        dtCloned.AcceptChanges();
                        using (SqlConnection con = new SqlConnection(ClsCommon.ConnectionString()))
                        {
                            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                            {
                                try
                                {
                                    sqlBulkCopy.DestinationTableName = "dbo.Master_Dealer_Mapping_Import";
                                    foreach (var item in CONSTANT.DealerMappingImport_Column)
                                    {
                                        sqlBulkCopy.ColumnMappings.Add(item.ToString(), item.ToString());
                                    }
                                    sqlBulkCopy.ColumnMappings.Add("Remarks", "Remarks");
                                    sqlBulkCopy.ColumnMappings.Add("CreatedBy", "CreatedBy");
                                    sqlBulkCopy.ColumnMappings.Add("CreatedDate", "CreatedDate");
                                    sqlBulkCopy.ColumnMappings.Add("IPAddress", "IPAddress");

                                    con.Open();
                                    sqlBulkCopy.BatchSize = 50;
                                    sqlBulkCopy.BulkCopyTimeout = 1000;
                                    sqlBulkCopy.WriteToServer(dtCloned);
                                    con.Close();
                                    Response.ID = 1;
                                    Response.StatusCode = 1;
                                    Response.SuccessMessage = "Uploded Successfully";
                                    Response.Status = true;
                                }
                                catch (Exception ex)
                                {
                                    con.Close();
                                    Response.Status = false;
                                    Response.SuccessMessage = "data not found, please check excel colomn";

                                }
                                finally
                                {
                                    con.Close();
                                }


                            }
                        }
                    }
                    else
                    {
                        Response.Status = false;
                        Response.SuccessMessage = "data not found, please check excel colomn";
                        return Response;
                    }
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = -1;
                Response.SuccessMessage = ex.Message.ToString();
            }
            return Response;
        }

        public PostResponse EmployeeImport_UploadData(HttpPostedFileBase file, GetResponse getResponse)
        {
            PostResponse Response = new PostResponse();
            DataSet ResultDataSet = default(DataSet);
            try
            {
                Stream stream = file.InputStream;
                IExcelDataReader reader = null;
                if (file.FileName.EndsWith(".xls"))
                {
                    reader = ExcelReaderFactory.CreateBinaryReader(stream);
                }
                else if (file.FileName.EndsWith(".xlsx"))
                {
                    reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                }
                else if (file.FileName.EndsWith(".csv"))
                {
                    reader = ExcelReaderFactory.CreateCsvReader(stream);
                }
                ResultDataSet = reader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow = true
                    }
                });
                reader.Close();

                if (ResultDataSet != null)
                {
                    DataTable dtCloned = ResultDataSet.Tables[0].Clone();
                    DataColumnCollection columns = ResultDataSet.Tables[0].Columns;


                    for (int i = 0; i < dtCloned.Columns.Count; i++)
                    {
                        if (CONSTANT.EmplyeeImport_Column[i].ToLower() != dtCloned.Columns[i].ColumnName.ToLower())
                        {
                            Response.Status = false;
                            Response.SuccessMessage = "we need " + CONSTANT.DealerImport_Column.Count + " column must be same as given template. Found error in " + dtCloned.Columns[i].ColumnName + "!";
                            return Response;
                        }
                    }
                    foreach (DataRow row in ResultDataSet.Tables[0].Rows)
                    {
                        foreach (DataColumn c in columns)
                        {
                            if (row.IsNull(c))
                            {
                                row[c] = "";

                            }

                            if (c.ColumnName.ToLower().Contains("password"))
                            {
                                row[c] = ClsCommon.Encrypt(row[c].ToString());
                            }
                        }
                        dtCloned.ImportRow(row);
                    }
                    if (dtCloned != null)
                    {
                        dtCloned.Columns.Add("Remarks", typeof(string)).Expression = "'Draft'";
                        dtCloned.Columns.Add("CreatedBy", typeof(string)).Expression = "'" + getResponse.LoginID + "'";
                        dtCloned.Columns.Add("CreatedDate", typeof(string)).Expression = "'" + DateTime.Now + "'";
                        dtCloned.Columns.Add("IPAddress", typeof(string)).Expression = "'" + getResponse.IPAddress + "'";
                        dtCloned.AcceptChanges();
                        using (SqlConnection con = new SqlConnection(ClsCommon.ConnectionString()))
                        {
                            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                            {
                                try
                                {
                                    sqlBulkCopy.DestinationTableName = "dbo.EMP_Import";
                                    foreach (var item in CONSTANT.EmplyeeImport_Column)
                                    {
                                        sqlBulkCopy.ColumnMappings.Add(item.ToString(), item.ToString());
                                    }
                                    sqlBulkCopy.ColumnMappings.Add("Remarks", "Remarks");
                                    sqlBulkCopy.ColumnMappings.Add("CreatedBy", "CreatedBy");
                                    sqlBulkCopy.ColumnMappings.Add("CreatedDate", "CreatedDate");
                                    sqlBulkCopy.ColumnMappings.Add("IPAddress", "IPAddress");

                                    con.Open();
                                    sqlBulkCopy.BatchSize = 50;
                                    sqlBulkCopy.BulkCopyTimeout = 1000;
                                    sqlBulkCopy.WriteToServer(dtCloned);
                                    con.Close();
                                    Response.ID = 1;
                                    Response.StatusCode = 1;
                                    Response.SuccessMessage = "Uploded Successfully";
                                    Response.Status = true;
                                }
                                catch (Exception ex)
                                {
                                    con.Close();
                                    Response.Status = false;
                                    Response.SuccessMessage = "data not found, please check excel colomn";

                                }
                                finally
                                {
                                    con.Close();
                                }


                            }
                        }
                    }
                    else
                    {
                        Response.Status = false;
                        Response.SuccessMessage = "data not found, please check excel colomn";
                        return Response;
                    }
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = -1;
                Response.SuccessMessage = ex.Message.ToString();
            }
            return Response;
        }

        //public PostResponse EmployeeImport_UploadData(HttpPostedFileBase file, GetResponse getResponse)
        //{
        //    PostResponse Response = new PostResponse();
        //    DataSet ResultDataSet = default(DataSet);
        //    try
        //    {
        //        Stream stream = file.InputStream;
        //        IExcelDataReader reader = null;
        //        if (file.FileName.EndsWith(".xls"))
        //        {
        //            reader = ExcelReaderFactory.CreateBinaryReader(stream);
        //        }
        //        else if (file.FileName.EndsWith(".xlsx"))
        //        {
        //            reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
        //        }
        //        else if (file.FileName.EndsWith(".csv"))
        //        {
        //            reader = ExcelReaderFactory.CreateCsvReader(stream);
        //        }
        //        ResultDataSet = reader.AsDataSet(new ExcelDataSetConfiguration()
        //        {
        //            ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
        //            {
        //                UseHeaderRow = true
        //            }
        //        });
        //        reader.Close();

        //        if (ResultDataSet != null)
        //        {
        //            for (int i = 0; i < ResultDataSet.Tables[0].Columns.Count; i++)
        //            {
        //                if (CONSTANT.EmplyeeImport_Column[i].ToLower() != ResultDataSet.Tables[0].Columns[i].ColumnName.ToLower())
        //                {
        //                    Response.Status = false;
        //                    Response.SuccessMessage = "we need " + CONSTANT.DealerImport_Column.Count + " column must be same as given template. Found error in " + ResultDataSet.Tables[0].Columns[i].ColumnName + "!";
        //                    return Response;
        //                }
        //            }

        //            //foreach (DataRow dtRow in ResultDataSet.Tables[0].Rows)
        //            //{
        //            //    foreach (DataColumn dc in ResultDataSet.Tables[0].Columns)
        //            //    {
        //            //        if (dc.ColumnName.ToLower() == "password")
        //            //        {
        //            //            dtRow["password"] = ClsCommon.Encrypt(dtRow["password"].ToString());
        //            //        }
        //            //    }
        //            //}
        //        }
        //        using (SqlConnection con = new SqlConnection(ClsCommon.ConnectionString()))
        //        {
        //            try
        //            {
        //                con.Open();
        //                using (SqlCommand command = new SqlCommand("spu_SetEMPImport", con))
        //                {
        //                    SqlDataAdapter da = new SqlDataAdapter();

        //                    command.CommandType = CommandType.StoredProcedure;
        //                    command.Parameters.Add("@LoginID", SqlDbType.Int).Value = getResponse.LoginID;
        //                    command.Parameters.Add("@Data", SqlDbType.Structured).Value = ResultDataSet.Tables[0];
        //                    command.CommandTimeout = 0;
        //                    using (SqlDataReader sqlreader = command.ExecuteReader())
        //                    {
        //                        while (sqlreader.Read())
        //                        {
        //                            Response.ID = Convert.ToInt64(sqlreader["RET_ID"]);
        //                            Response.StatusCode = Convert.ToInt32(sqlreader["STATUS"]);
        //                            Response.SuccessMessage = sqlreader["MESSAGE"].ToString();
        //                            if (Response.StatusCode > 0)
        //                            {
        //                                Response.Status = true;
        //                            }
        //                        }
        //                    }

        //                }
        //                con.Close();
        //            }
        //            catch (Exception ex)
        //            {
        //                con.Close();
        //                Response.StatusCode = -1;
        //                Response.SuccessMessage = ex.Message.ToString();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.StatusCode = -1;
        //        Response.SuccessMessage = ex.Message.ToString();
        //    }
        //    return Response;
        //}

        public List<EMPTalentPool.List> GetEMP_TalentPoolList(JqueryDatatableParam Modal)
        {

            List<EMPTalentPool.List> result = new List<EMPTalentPool.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@Approved", dbType: DbType.Int32, value: Modal.Approved);
                    param.Add("@States", dbType: DbType.Int64, value: (string.IsNullOrEmpty(Modal.OtherValue) ? 0 : Convert.ToInt32(Modal.OtherValue)));
                    param.Add("@City", dbType: DbType.Int64, value: (string.IsNullOrEmpty(Modal.OtherValue1) ? 0 : Convert.ToInt32(Modal.OtherValue1)));
                    param.Add("@Pincodes", dbType: DbType.String, value: Modal.OtherValue2 ?? "");
                    param.Add("@Counters", dbType: DbType.String, value: Modal.OtherValue3 ?? "");
                    param.Add("@Location", dbType: DbType.String, value: Modal.OtherValue4 ?? "");
                    param.Add("@start", dbType: DbType.Int32, value: Modal.start);
                    param.Add("@length ", dbType: DbType.Int32, value: Modal.length);
                    param.Add("@SearchText", dbType: DbType.String, value: Modal.SearchText);
                    param.Add("@sortColumn", dbType: DbType.Int32, value: Modal.sortColumn);
                    param.Add("@sortOrder", dbType: DbType.String, value: Modal.sortOrder);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetEMP_TalentPoolList", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<EMPTalentPool.List>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetEMP_TalentPoolList", "GetEMP_TalentPoolList", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public List<EMPTalentPool.List> GetMyEMPTalentPoolList(Tab.Approval Modal)
        {

            List<EMPTalentPool.List> result = new List<EMPTalentPool.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@Date", dbType: DbType.String, value: Modal.Month);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetEMP_TalentPoolList_App", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<EMPTalentPool.List>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetEMP_TalentPoolList_App", "GetEMP_TalentPoolList", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public EMPTalentPool.Add GetEMPTalentPool(GetResponse Modal)
        {
            EMPTalentPool.Add result = new EMPTalentPool.Add();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@TPID", dbType: DbType.Int64, value: Modal.ID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetEMP_TalentPool", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<EMPTalentPool.Add>().FirstOrDefault();
                        if (result == null)
                        {
                            result = new EMPTalentPool.Add();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.AttachmentsList = reader.Read<Attachments>().ToList();
                            if (result.AttachmentsList.Count == 0)
                            {

                                result.AttachmentsList.Add(new Attachments { Attach_ID = 0, AttachPath = "", Doctype = "Resume", Upload = null });
                                result.AttachmentsList.Add(new Attachments { Attach_ID = 0, AttachPath = "", Doctype = "Photo", Upload = null });
                            }
                        }
                        if (!reader.IsConsumed)
                        {
                            result.BranchList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.DealerList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.StateList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.CityList = reader.Read<DropDownlist>().ToList();
                        }
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetMasters", "GetMastersList", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public EMPTalentPool.MyAdd GetEMPTalentPool_MyAdd(GetResponse Modal)
        {
            EMPTalentPool.MyAdd result = new EMPTalentPool.MyAdd();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@TPID", dbType: DbType.Int64, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@Doctype", dbType: DbType.String, value: Modal.Doctype, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetEMP_TalentPool", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<EMPTalentPool.MyAdd>().FirstOrDefault();
                        if (result == null)
                        {
                            result = new EMPTalentPool.MyAdd();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.AttachmentsList = reader.Read<Attachments>().ToList();
                            if (result.AttachmentsList.Count == 0)
                            {

                                result.AttachmentsList.Add(new Attachments { Attach_ID = 0, AttachPath = "", Doctype = "Resume", Upload = null });
                                result.AttachmentsList.Add(new Attachments { Attach_ID = 0, AttachPath = "", Doctype = "Photo", Upload = null });
                            }
                        }
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetMasters", "GetMastersList", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public PostResponse fnSetEMP_TalentPool_Approved(EMPTalentPool.ApprovalAction modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetEMP_TalentPool_Approved", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@TPIDs", SqlDbType.VarChar).Value = modal.TPIDs;
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

        public PostResponse fnSetEMP_TalentPool_MyAdd(EMPTalentPool.MyAdd model)
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
                    using (SqlCommand command = new SqlCommand("spu_SetEMP_TalentPool_App", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@TPID", SqlDbType.Int).Value = model.TPID ?? 0;
                        command.Parameters.Add("@Age", SqlDbType.Int).Value = model.Age;
                        command.Parameters.Add("@CurrentSalary", SqlDbType.Decimal).Value = model.CurrentSalary;
                        command.Parameters.Add("@ExpectedSalary", SqlDbType.Decimal).Value = model.ExpectedSalary;
                        command.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = model.Name ?? "";
                        command.Parameters.Add("@CounterName", SqlDbType.VarChar, 50).Value = model.CounterName ?? "";
                        command.Parameters.Add("@Location", SqlDbType.VarChar, 50).Value = model.Location ?? "";
                        command.Parameters.Add("@TotalExperience", SqlDbType.VarChar, 50).Value = model.TotalExperience ?? "";
                        command.Parameters.Add("@CurrentCompany", SqlDbType.VarChar, 50).Value = model.CurrentCompany ?? "";
                        command.Parameters.Add("@TenureWithCompany", SqlDbType.VarChar).Value = model.TenureWithCompany ?? "";
                        command.Parameters.Add("@Mobile", SqlDbType.VarChar).Value = model.Mobile;
                        command.Parameters.Add("@Pincode", SqlDbType.VarChar).Value = model.Pincode;
                        command.Parameters.Add("@EntrySource", SqlDbType.VarChar, 50).Value = EntrySource;
                        command.Parameters.Add("@Latitude", SqlDbType.Float).Value = Latitude;
                        command.Parameters.Add("@Longitude", SqlDbType.Float).Value = Longitude;
                        command.Parameters.Add("@Error", SqlDbType.VarChar).Value = model.Error ?? "";
                        command.Parameters.Add("@IsActive", SqlDbType.Int).Value = model.IsActive;
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = model.Priority;
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

        public Filter GetEMPTalentPoolFilter(GetResponse modal)
        {
            Filter result = new Filter();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.ConnectionString()))
                {
                    var param = new DynamicParameters();
                    param.Add("@Doctype", dbType: DbType.String, value: "EMPTalentPool_Filter");
                    param.Add("@Values", dbType: DbType.String, value: "");
                    param.Add("@LoginId", dbType: DbType.Int32, value: modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetDropDownList", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result.StateList = reader.Read<DropDownlist>().ToList();
                        result.CityList = new List<DropDownlist>();
                        if (!reader.IsConsumed)
                        {
                            result.PinCodeList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.CountersList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.LocationList = reader.Read<DropDownlist>().ToList();
                        }
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


        public List<DealerRequest.List> GetDealerRequests_MyList(JqueryDatatableParam Modal)
        {

            List<DealerRequest.List> result = new List<DealerRequest.List>();
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
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetDealerRequests_MyList", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<DealerRequest.List>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetDealerRequests_MyList", "spu_GetDealerRequests_MyList", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }
        public List<DealerRequest.List> GetDealerRequests_List(JqueryDatatableParam Modal)
        {

            List<DealerRequest.List> result = new List<DealerRequest.List>();
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
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetDealerRequests_List", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<DealerRequest.List>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetDealerList", "spu_GetDealerList", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public DealerRequest.Add GetDealerRequests(GetResponse Modal)
        {

            DealerRequest.Add result = new DealerRequest.Add();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@ID", dbType: DbType.Int64, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetDealerRequests", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<DealerRequest.Add>().FirstOrDefault();
                        if (result == null)
                        {
                            result = new DealerRequest.Add();
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

                            result.AreaList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {

                            result.BranchList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {

                            result.DealerCategoryList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {

                            result.DealerTypeList = reader.Read<DropDownlist>().ToList();
                        }
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetDealerRequests", "spu_GetDealerRequests", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public PostResponse fnSetDealerRequests(DealerRequest.Add model)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetDealerRequests", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = model.ID ?? 0;
                        command.Parameters.Add("@DealerCode", SqlDbType.VarChar).Value = model.DealerCode ?? "";
                        command.Parameters.Add("@DealerName", SqlDbType.VarChar).Value = model.DealerName ?? "";
                        command.Parameters.Add("@EmailID", SqlDbType.VarChar).Value = model.EmailID ?? "";
                        command.Parameters.Add("@Phone", SqlDbType.VarChar).Value = model.Phone ?? "";
                        command.Parameters.Add("@Address", SqlDbType.VarChar).Value = model.Address ?? "";
                        command.Parameters.Add("@DealerTypeID", SqlDbType.VarChar).Value = model.DealerTypeID ?? 0;
                        command.Parameters.Add("@DealerCategoryID", SqlDbType.Int).Value = model.DealerCategoryID ?? 0;
                        command.Parameters.Add("@StateID", SqlDbType.Int).Value = model.StateID ?? 0;
                        command.Parameters.Add("@BranchID", SqlDbType.Int).Value = model.BranchID ?? 0;
                        command.Parameters.Add("@CityID", SqlDbType.Int).Value = model.CityID ?? 0;
                        command.Parameters.Add("@AreaID", SqlDbType.Int).Value = model.AreaID ?? 0;
                        command.Parameters.Add("@RegionID", SqlDbType.Int).Value = model.RegionID ?? 0;
                        command.Parameters.Add("@PinCode", SqlDbType.VarChar).Value = model.PinCode ?? "";
                        command.Parameters.Add("@Latitude", SqlDbType.VarChar).Value = model.Latitude ?? "";
                        command.Parameters.Add("@Longitude", SqlDbType.VarChar).Value = model.Longitude ?? "";
                        command.Parameters.Add("@BillingCode", SqlDbType.VarChar).Value = model.BillingCode ?? "";
                        command.Parameters.Add("@BillingName", SqlDbType.VarChar).Value = model.BillingName ?? "";
                        command.Parameters.Add("@IsActive", SqlDbType.Int).Value = 1;
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = model.Priority;
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

        public DealerSearch GetDealerSearchFilter(GetResponse modal)
        {
            DealerSearch result = new DealerSearch();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.ConnectionString()))
                {
                    var param = new DynamicParameters();
                    param.Add("@Doctype", dbType: DbType.String, value: "DealerSearch_Filter");
                    param.Add("@Values", dbType: DbType.String, value: "");
                    param.Add("@LoginId", dbType: DbType.Int32, value: modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetDropDownList", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result.StateList = reader.Read<DropDownlist>().ToList();
                        result.CityList = new List<DropDownlist>();
                        if (!reader.IsConsumed)
                        {
                            result.RegionList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.BranchList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.DealerCategoryList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.DealerTypeList = reader.Read<DropDownlist>().ToList();
                        }
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
    }
}
