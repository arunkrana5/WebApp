using DataModal.Models;
using System.Collections.Generic;
using System.Data;

namespace DataModal.ModelsMasterHelper
{
    public interface IToolHelper
    {
        List<AdminMenu> GetAdminMenuList(GetResponse modal);
        List<ErrorLog> ErrorLogList(GetResponse modal);
        List<AdminUser.Role.List> GetRolesList(GetResponse modal);
        AdminUser.Role.Add GetRoles(GetResponse modal);
        List<AdminModule> GetModuleListWithMenu(GetResponse modal);
        PostResponse fnSetUserRole(AdminUser.Role.Add modal);
        List<EmailTemplate.List> GetEmailTemplateList(GetResponse modal);
        EmailTemplate.Add GetEmailTemplate(GetResponse modal);
        PostResponse fnSetEmailTemplate(EmailTemplate.Add modal);
        List<Users.List> GetLoginUserList(JqueryDatatableParam Modal);
        List<Users.List> GetUsersList(GetResponse modal);
        Users.Add GetUsers(GetResponse modal);
        PostResponse fnSetUsers(Users.Add modal);
        List<AdminMenu> GetAppMenuList(GetMenuResponse modal);
        List<ContactUs_Query.List> GetContactUs_QueryList(GetResponse modal);
        PostResponse fnSetApproveContactUs_Query(ContactUs_Query.Approve modal);
        DataSet GetUserCurrent_LocationList(GetResponse Modal);

        List<Announcement.List> GetAnnouncementList(GetResponse modal);
        Announcement.Add GetAnnouncement(GetResponse modal);
        PostResponse fnSetAnnouncement(Announcement.Add modal);

        List<AutoReport.Log> GetAutoReport_LogList(Tab.Approval modal);
        PostResponse fnSet_Sales_Manage(ManageActivities.Sale modal);
        PostResponse fnSet_Attendance_Manage(ManageActivities.Attendance modal);
        PostResponse fnSet_EMPDOL(ManageActivities.EMPDOL model);
    }
}
