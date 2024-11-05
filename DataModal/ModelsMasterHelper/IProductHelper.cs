using DataModal.Models;
using System.Collections.Generic;
using System.Web;

namespace DataModal.ModelsMasterHelper
{
    public interface IProductHelper
    {
        List<ProductType.List> GetProductTypeList(GetResponse Modal);
        ProductType.Add GetProductType(GetResponse Modal);
        PostResponse fnSetProductType(ProductType.Add modal);

        List<Product.List> GetProductList(GetResponse Modal);
        Product.Add GetProduct(GetResponse Modal);
        PostResponse fnSetProduct(Product.Add modal);


        List<ProductTran.List> GetProduct_TranList(GetResponse Modal);

        ProductTran.Add GetProduct_Tran(GetResponse Modal);

        PostResponse fnSetProductTran(ProductTran.Add modal);


        List<Items.List> GetItemList(GetResponse Modal);
        Items.Add GetItem(GetResponse Modal);
        PostResponse fnSetItems(Items.Add modal);

        List<Items_Import.List> GetItemsImportList(GetResponse Modal);
        PostResponse UploadItemsImportDataExcelFile(HttpPostedFileBase file, string SheetName, GetResponse getResponse);
        PostResponse ClearItemsImportTemp(GetResponse getResponse);
        PostResponse SetItemsFromItemsImport(GetResponse getResponse);

    }
}
