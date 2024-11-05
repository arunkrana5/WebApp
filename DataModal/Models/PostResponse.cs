using System.Collections.Generic;
using System.Web;

namespace DataModal.Models
{
    public class PostResponse
    {
        public string ViewAsString { get; set; }
        public bool Status { get; set; }
        public int StatusCode { get; set; }
        public string SuccessMessage { get; set; }
        public string RedirectURL { get; set; }
        public long ID { get; set; }
        public string AdditionalMessage { get; set; }
    }


    public class UploadAttachment
    {
        public string Token { get; set; }
        public bool IsValid { get; set; }
        public string Message { get; set; }
        public string Doctype { get; set; }
        public HttpPostedFileBase File { get; set; }
        public long? AttachID { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public string Description { get; set; }
        public long? tableid { get; set; }
        public string TableName { get; set; }
        public long LoginID { get; set; }
        public string IPAddress { get; set; }
        public string FixFileName { get; set; }

    }
    public class FileResponse
    {
        public bool IsValid { get; set; }
        public bool IsImage { get; set; }
        public string Message { get; set; }
        public string FileName { get; set; }
        public int FileLength { get; set; }
        public string ReadAbleFileSize { get; set; }
        public string FileExt { get; set; }
        public string FileType { get; set; }
        public System.IO.Stream InputStream { get; set; }
        public string Token { get; set; }
        public long? ID { get; set; }
        public string Description { get; set; }
        public long? tableid { get; set; }
        public string TableName { get; set; }
        public long LoginID { get; set; }
        public string IPAddress { get; set; }
        public string ImageBase64String { get; set; }
        public string Doctype { get; set; }
        public string ExpenseType { get; set; }
    }

    public class GetResponse
    {
        public int Approved { get; set; }
        public long ID { get; set; }
        public long AdditionalID { get; set; }
        public string Doctype { get; set; }
        public string Date { get; set; }
        public string Param1 { get; set; }
        public string Param2 { get; set; }
        public long LoginID { get; set; }
        public string IPAddress { get; set; }

    }
    public class GetDropDownResponse
    {
        public string Values { get; set; }
        public string Doctype { get; set; }
        public long LoginID { get; set; }
        public string IPAddress { get; set; }

    }
    public class GetMenuResponse
    {
        public long RoleID { get; set; }
        public long WebMenuID { get; set; }
        public string MenuType { get; set; }
        public long ParentMenuID { get; set; }
        public string Doctype { get; set; }
        public long LoginID { get; set; }
        public string IPAddress { get; set; }

    }

    public class GetUpdateColumnResponse
    {
        public long ID { get; set; }
        public string Value { get; set; }
        public string Doctype { get; set; }
        public long LoginID { get; set; }
        public string IPAddress { get; set; }
        public string Reason { get; set; }

    }

    public class GetRecordExitsResponse
    {
        public long ID { get; set; }
        public string Value { get; set; }
        public string Doctype { get; set; }
        public long LoginID { get; set; }
        public string IPAddress { get; set; }

    }

    public class Tab
    {
        public class Approval
        {
            public string Month { get; set; }
            public int Approved { get; set; }
            public int RoleID { get; set; }
            public string Doctype { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }
            public long? ID { get; set; }
            public long? EMPID { get; set; }
            public string SeperatedIDs { get; set; }
            public List<DropDownlist> List { get; set; }
            public string Flag1 { get; set; }

            public string StartDate { get; set; }
            public string EndDate { get; set; }

            public long?[] CommmaSeperatedIDs { get; set; }


        }
        public class Date
        {
            public string Month { get; set; }
            public string Doctype { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }
            public long ID { get; set; }
        }



    }


    public class MyTab
    {
        public class Approval
        {
            public string Month { get; set; }
            public int Approved { get; set; }
            public string Usertype { get; set; }
            public string Doctype { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }
            public List<DropDownlist> List { get; set; }

        }
        public class Billing
        {
            public string Date { get; set; }
            public string ClientCode { get; set; }
            public string SC_Code { get; set; }
            public long? FYID { get; set; }
            public List<DropDownlist> FinyearList { get; set; }
            public List<DropDownlist> ClientList { get; set; }
            public List<DropDownlist> SubClientList { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }
        }
        public class Onboarding
        {
            public string Date { get; set; }
            public string ClientCode { get; set; }
            public string SC_Code { get; set; }
            public long? FYID { get; set; }
            public List<DropDownlist> FinyearList { get; set; }
            public List<DropDownlist> ClientList { get; set; }
            public List<DropDownlist> SubClientList { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }
        }



    }
}

