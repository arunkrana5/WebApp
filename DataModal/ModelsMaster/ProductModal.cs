using Dapper;
using DataModal.CommanClass;
using DataModal.Models;
using DataModal.ModelsMasterHelper;

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
    public class ProductModal : IProductHelper
    {
        string ConnectionStrings = ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString.ToString();
        public List<ProductType.List> GetProductTypeList(GetResponse Modal)
        {

            List<ProductType.List> result = new List<ProductType.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@PDTypeID", dbType: DbType.Int64, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetProductType", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<ProductType.List>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetProductTypeList", "spu_GetProductType", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public ProductType.Add GetProductType(GetResponse Modal)
        {

            ProductType.Add result = new ProductType.Add();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@PDTypeID", dbType: DbType.Int64, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetProductType", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<ProductType.Add>().FirstOrDefault();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetProductType", "spu_GetProductType", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }


        public PostResponse fnSetProductType(ProductType.Add modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetProductType", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@PDTypeID", SqlDbType.Int).Value = modal.PDTypeID;
                        command.Parameters.Add("@TypeName", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.TypeName);
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


        public List<Product.List> GetProductList(GetResponse Modal)
        {

            List<Product.List> result = new List<Product.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@ProductID", dbType: DbType.Int64, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetProductList", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Product.List>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetProductList", "spu_GetProduct", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public Product.Add GetProduct(GetResponse Modal)
        {

            Product.Add result = new Product.Add();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@ProductID", dbType: DbType.Int64, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetProduct", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Product.Add>().FirstOrDefault();
                        if (result == null)
                        {
                            result = new Product.Add();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.ProductTypeList = reader.Read<DropDownlist>().ToList();
                        }
                    }

                    DBContext.Close();
                }

            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetProduct", "spu_GetProduct", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }
        public PostResponse fnSetProduct(Product.Add modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetProduct", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ProductID", SqlDbType.Int).Value = modal.ProductID;
                        command.Parameters.Add("@PDTypeID", SqlDbType.Int).Value = modal.PDTypeID;
                        command.Parameters.Add("@ProductName", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.ProductName);
                        command.Parameters.Add("@ProductCode", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.ProductCode);
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



        public List<ProductTran.List> GetProduct_TranList(GetResponse Modal)
        {

            List<ProductTran.List> result = new List<ProductTran.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@PDTranID", dbType: DbType.Int64, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@ProductID", dbType: DbType.Int64, value: Modal.AdditionalID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetProduct_Tran", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<ProductTran.List>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetProduct_TranList", "spu_GetProduct_Tran", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }


        public ProductTran.Add GetProduct_Tran(GetResponse Modal)
        {

            ProductTran.Add result = new ProductTran.Add();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@PDTranID", dbType: DbType.Int64, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@ProductID", dbType: DbType.Int64, value: Modal.AdditionalID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetProduct_Tran", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<ProductTran.Add>().FirstOrDefault();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetProduct_TranList", "spu_GetProduct_Tran", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public PostResponse fnSetProductTran(ProductTran.Add modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetProductTran", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@PDTranID", SqlDbType.Int).Value = modal.PDTranID;
                        command.Parameters.Add("@ProductID", SqlDbType.Int).Value = modal.ProductID;
                        command.Parameters.Add("@TranCode", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.TranCode);
                        command.Parameters.Add("@TranName", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.TranName);
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






        public List<Items.List> GetItemList(GetResponse Modal)
        {

            List<Items.List> result = new List<Items.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@ItemID", dbType: DbType.Int64, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetItemList", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Items.List>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetItemList", "spu_GetItemList", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }


        public Items.Add GetItem(GetResponse Modal)
        {

            Items.Add result = new Items.Add();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@ItemID", dbType: DbType.Int64, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetItem", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Items.Add>().FirstOrDefault();
                        if (result == null)
                        {
                            result = new Items.Add();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.ProductList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.IncentiveRulesList = reader.Read<Items.IncentiveRules>().ToList();
                        }

                    }
                    result.ProductTranList = new List<DropDownlist>();

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetItem", "spu_GetItem", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public PostResponse fnSetItems(Items.Add modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetItem", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ItemID", SqlDbType.Int).Value = modal.ItemID ?? 0;
                        command.Parameters.Add("@PDTranID", SqlDbType.Int).Value = modal.PDTranID ?? 0;
                        command.Parameters.Add("@ProductID", SqlDbType.Int).Value = modal.ProductID ?? 0;
                        command.Parameters.Add("@ItemCode", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.ItemCode);
                        command.Parameters.Add("@ItemName", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.ItemName);
                        command.Parameters.Add("@IsActive", SqlDbType.Int).Value = modal.IsActive;
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = modal.Priority ?? 0;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = modal.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = modal.IPAddress;
                        command.Parameters.Add("@IncentiveRules_Type", SqlDbType.Structured).Value = ClsCommon.ToDataTable(modal.IncentiveRulesList);
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


        public List<Items_Import.List> GetItemsImportList(GetResponse Modal)
        {
            List<Items_Import.List> result = new List<Items_Import.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetItemsImportList", commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Items_Import.List>().ToList();
                    }

                    DBContext.Close();
                }

            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetItemsImportList", "GetItemsImportList", "DataModal", Modal.LoginID, Modal.IPAddress);

            }
            return result;
        }


        public PostResponse UploadItemsImportDataExcelFile(HttpPostedFileBase file, string SheetName, GetResponse getResponse)
        {
            PostResponse Result = new PostResponse();
            DataSet TempDataset = default(DataSet);
            string ExcelFileName = null;
            string DirectotyPath = null;
            string FileExtension = null;
            ArrayList SqlArrayList = new ArrayList();

            try
            {
                ExcelFileName = "ItemsImportData";
                DirectotyPath = ConfigurationManager.AppSettings["ApplicationPath_Physical"] + "\\Attachments\\ImportExcels";

                if (!System.IO.Directory.Exists(DirectotyPath + "\\ItemsImportData"))
                {
                    System.IO.Directory.CreateDirectory(DirectotyPath + "\\ItemsImportData");
                }
                FileExtension = System.IO.Path.GetExtension(file.FileName);
                if (FileExtension == ".xlsx" || FileExtension == ".xls")
                {

                    file.SaveAs(DirectotyPath + "\\ItemsImportData\\" + ExcelFileName + "" + FileExtension);
                    TempDataset = clsDataBaseHelper.GetExcelDataAsDataSet(DirectotyPath + "\\ItemsImportData\\" + ExcelFileName + "" + FileExtension, SheetName.Replace("'", "''").Replace("$", "") + "$");

                    if ((TempDataset != null))
                    {

                        if (TempDataset.Tables[0].Columns.Count > 0)
                        {
                            Result = SaveItemsImportTempDetails(TempDataset, getResponse);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Result;

        }

        public PostResponse SaveItemsImportTempDetails(DataSet TempDataset, GetResponse getResponse)
        {
            PostResponse Result = new PostResponse();
            Result.SuccessMessage = "Error occurred while saving into Items import";
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetItemsImport", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        TempDataset.Tables[0].Rows.RemoveAt(0);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@LoginID", SqlDbType.Int).Value = getResponse.LoginID;
                        command.Parameters.Add("@ItemImport", SqlDbType.Structured).Value = TempDataset.Tables[0];
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
        public PostResponse ClearItemsImportTemp(GetResponse getResponse)
        {
            PostResponse Result = new PostResponse();
            try
            {
                string SQL = "Truncate table Items_Import";
                clsDataBaseHelper.ExecuteNonQuery(SQL);
                Result.Status = true;
                Result.SuccessMessage = "data cleared";
            }
            catch (Exception ex)
            {

            }
            return Result;
        }
        public PostResponse SetItemsFromItemsImport(GetResponse getResponse)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetItemsFromItemsImport", con))
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
    }
}
