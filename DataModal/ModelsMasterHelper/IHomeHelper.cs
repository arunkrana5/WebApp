using DataModal.Models;
using System.Collections.Generic;

namespace DataModal.ModelsMasterHelper
{
    public interface IHomeHelper
    {
        Attendance_Log.PunchStatus GetPunchTime_DateWise(GetResponse Modal);
        List<TrgVsAch> GetTargetAchieved_MonthWise(GetResponse Modal);
        List<Announcement.My> GetMyAnnouncement(GetResponse modal);
        List<BirthdayList> GetBirthdayList(GetResponse modal);
    }
}
