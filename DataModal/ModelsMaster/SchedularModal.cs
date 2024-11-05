using DataModal.ModelsMasterHelper;
using System.Configuration;

namespace DataModal.ModelsMaster
{
    public class SchedularModal : ISchedularHelper
    {
        string ConnectionStrings = ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString.ToString();




    }
}
