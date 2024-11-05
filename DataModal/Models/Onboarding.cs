using System;

namespace DataModal.Models
{
    public class Onboarding
    {
        public class List
        {
            public int TotalCount { get; set; }
            public int OnBoardID { get; set; }
            public string DocNo { get; set; }
            public string DocDate { get; set; }
            public string Name { get; set; }
            public string FatherName { get; set; }
            public string Mobile { get; set; }
            public string EmailID { get; set; }
            public string DOB { get; set; }
            public string Gender { get; set; }
            public string BloodGroup { get; set; }
            public string DOJ { get; set; }
            public string Experience { get; set; }
            public string MaritalStatus { get; set; }
            public string Category { get; set; }
            public string PAN { get; set; }
            public string AadharNo { get; set; }
            public string PassportNo { get; set; }

            public string PassportValid_From { get; set; }
            public string PassportValid_To { get; set; }
            public string Approved { get; set; }
            public string ApprovedRemarks { get; set; }
            public string ApprovedDate { get; set; }
            public string ApprovedBy { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedDate { get; set; }
            public string ModifiedDate { get; set; }
            public string ModifiedBy { get; set; }
            public string IPAddress { get; set; }
        }
        public class Onboarding_Attachments
        {
            public string FileName { get; set; }
            public string contenttype { get; set; }
            public string Description { get; set; }
            public string AttachmentPath { get; set; }
            public long? ID { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }
        }
        public class ApprovalAction
        {
            public string IDs { get; set; }
            public int Approved { get; set; }
            public string ApprovedRemarks { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }
            public DateTime dt { get; set; }
            public string Doctype { get; set; }


        }
    }
}
