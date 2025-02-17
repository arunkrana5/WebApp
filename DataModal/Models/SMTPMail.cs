using System.Collections.Generic;

namespace DataModal.Models
{
    public class SMTPMail
    {
        public string SMTP { get; set; }
        public string SMTP_USER { get; set; }
        public string SMTP_PASSWORD { get; set; }
        public string SMTP_EMAIL { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
        public string ToEmail { get; set; }
        public string CC { get; set; }
        public string BCC { get; set; }
        public string AttachmenPath { get; set; }
        public string Subject { get; set; }
        public string MailBody { get; set; }
        public Template TemplateData { get; set; }
        public List<AutoReportUsers> AutoReportUsersList { get; set; }

    }


    public class Template
    {
        public string TemplateName { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }

        public string CCMail { get; set; }
        public string BCCMail { get; set; }
        public string Repository { get; set; }
        public string SMSBody { get; set; }
        public List<ConfigSetting> ConfigSettingList { get; set; }
    }

    public class AutoReportUsers
    {
        public string UserName { get; set; }
        public string UserType { get; set; }
        public long LoginID { get; set; }
        public string UserID { get; set; }
        public string EmailID { get; set; }
        public bool ShowPriceOn_AutoReport { get; set; }
    }

    public class UserDetails
    {
        public long LoginID { get; set; }
        public string UserName { get; set; }
        public string UserType { get; set; }
        public string UserID { get; set; }
        public string EmailID { get; set; }
    }
    public class MailDetails
    {
        public string TemplateName { get; set; }
        public string MailBody { get; set; }
        public string Subject { get; set; }
        public string CCMail { get; set; }
        public string BCCMail { get; set; }
        public string SMSBody { get; set; }
        public string ToEmail { get; set; }
        public string CC { get; set; }
        public string BCC { get; set; }
    }
}
