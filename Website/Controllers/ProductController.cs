using DataModal.CommanClass;
using DataModal.Models;
using DataModal.ModelsMaster;
using DataModal.ModelsMasterHelper;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Website.CommonClass;

namespace Website.Controllers
{
    [CheckLoginFilter]
    public class ProductController : Controller
    {
        long LoginID = 0;
        string IPAddress = "";
        GetResponse getResponse;
        IProductHelper Product;

        public ProductController()
        {
            getResponse = new GetResponse();
            long.TryParse(ClsApplicationSetting.GetSessionValue("LoginID"), out LoginID);
            IPAddress = ClsApplicationSetting.GetIPAddress();
            getResponse.IPAddress = IPAddress;
            getResponse.LoginID = LoginID;
            Product = new ProductModal();

        }


        public ActionResult ProductTypeList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            List<ProductType.List> result = new List<ProductType.List>();
            result = Product.GetProductTypeList(getResponse);


            return View(result);
        }
        public ActionResult _ProductTypeAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.PDTypeID = GetQueryString[2];
            long PDTypeID = 0;
            long.TryParse(ViewBag.PDTypeID, out PDTypeID);
            getResponse.ID = PDTypeID;
            ProductType.Add result = new ProductType.Add();
            if (PDTypeID > 0)
            {
                result = Product.GetProductType(getResponse);


            }
            return PartialView(result);
        }

        [HttpPost]
        public ActionResult _ProductTypeAdd(string src, ProductType.Add Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.PDTypeID = GetQueryString[2];
            long PDTypeID = 0;
            long.TryParse(ViewBag.PDTypeID, out PDTypeID);
            Result.SuccessMessage = "Product Type Can't Update";
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.PDTypeID = PDTypeID;
                Result = Product.fnSetProductType(Modal);

            }
            if (Result.Status)
            {
                Result.RedirectURL = "/Product/ProductTypeList?src=" + ClsCommon.Encrypt(ViewBag.MenuID.ToString() + "*/Product/ProductTypeList");
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }



        public ActionResult ProductList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            List<Product.List> result = new List<Product.List>();
            result = Product.GetProductList(getResponse);
            return View(result);
        }
        public ActionResult _ProductAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ProductID = GetQueryString[2];
            long ProductID = 0;
            long.TryParse(ViewBag.ProductID, out ProductID);
            getResponse.ID = ProductID;
            Product.Add result = new Product.Add();
            result = Product.GetProduct(getResponse);

            return PartialView(result);
        }

        [HttpPost]
        public ActionResult _ProductAdd(string src, Product.Add Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ProductID = GetQueryString[2];
            long ProductID = 0;
            long.TryParse(ViewBag.ProductID, out ProductID);
            Result.SuccessMessage = "Product Can't Update";
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.ProductID = ProductID;
                Result = Product.fnSetProduct(Modal);
            }
            if (Result.Status)
            {
                Result.RedirectURL = "/Product/ProductList?src=" + ClsCommon.Encrypt(ViewBag.MenuID.ToString() + "*/Product/ProductList");
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }




        public ActionResult ProductTranList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ProductID = GetQueryString[2];

            long ProductID = 0, PDTranID = 0;
            long.TryParse(ViewBag.ProductID, out ProductID);
            getResponse.AdditionalID = ProductID;

            List<ProductTran.List> result = new List<ProductTran.List>();
            result = Product.GetProduct_TranList(getResponse);
            return View(result);
        }
        public ActionResult _ProductTranAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ProductID = GetQueryString[2];
            ViewBag.PDTranID = GetQueryString[3];
            long ProductID = 0, PDTranID = 0;
            long.TryParse(ViewBag.ProductID, out ProductID);
            long.TryParse(ViewBag.PDTranID, out PDTranID);
            getResponse.ID = PDTranID;
            getResponse.AdditionalID = ProductID;
            ProductTran.Add result = new ProductTran.Add();
            if (PDTranID > 0)
            {
                result = Product.GetProduct_Tran(getResponse);

            }
            return PartialView(result);
        }

        [HttpPost]
        public ActionResult _ProductTranAdd(string src, ProductTran.Add Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ProductID = GetQueryString[2];
            ViewBag.PDTranID = GetQueryString[3];
            long ProductID = 0, PDTranID = 0;
            long.TryParse(ViewBag.ProductID, out ProductID);
            long.TryParse(ViewBag.PDTranID, out PDTranID);
            Result.SuccessMessage = "Product Tran Can't Update";
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.ProductID = ProductID;
                Modal.PDTranID = PDTranID;
                Result = Product.fnSetProductTran(Modal);
            }
            if (Result.Status)
            {
                Result.RedirectURL = "/Product/ProductTranList?src=" + ClsCommon.Encrypt(ViewBag.MenuID.ToString() + "*/Product/ProductTranList*" + ProductID);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }




        public ActionResult ItemList(string src)
        {
            ViewBag.Import = "True";
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<Items.List> result = new List<Items.List>();
            result = Product.GetItemList(getResponse);


            return View(result);
        }
        public ActionResult _ItemAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ItemID = GetQueryString[2];

            long ItemID = 0;
            long.TryParse(ViewBag.ItemID, out ItemID);
            getResponse.ID = ItemID;
            Items.Add result = new Items.Add();

            result = Product.GetItem(getResponse);



            return PartialView(result);
        }

        [HttpPost]
        public ActionResult _ItemAdd(string src, Items.Add Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ItemID = GetQueryString[2];

            long ItemID = 0;
            long.TryParse(ViewBag.ItemID, out ItemID);
            getResponse.ID = ItemID;
            Result.SuccessMessage = "Item Can't Update";
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.ItemID = ItemID;
                Result = Product.fnSetItems(Modal);
            }
            if (Result.Status)
            {
                Result.RedirectURL = "/Product/ItemList?src=" + ClsCommon.Encrypt(ViewBag.MenuID.ToString() + "*/Product/ItemList*");
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }

        public ActionResult ItemsImport(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<Items_Import.List> Result = new List<Items_Import.List>();
            Result = Product.GetItemsImportList(getResponse);
            ViewBag.ListCount = Result.Count;
            return View(Result);
        }

        [HttpPost]
        public ActionResult ItemsImport(FormCollection Form, string Command, string src)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            if (Command == "ImportData")
            {
                string SheetName = Form.Get("txtSheet");
                if (Request.Files == null)
                {
                    Result.SuccessMessage = "please select file";
                    return Json(Result, JsonRequestBehavior.AllowGet);
                }
                foreach (string upload in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[upload];
                    Result = Product.UploadItemsImportDataExcelFile(file, SheetName, getResponse);
                }
            }
            else if (Command == "ClearData")
            {
                Result = Product.ClearItemsImportTemp(getResponse);
            }
            else if (Command == "UploadData")
            {
                Result = Product.SetItemsFromItemsImport(getResponse);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }


    }
}