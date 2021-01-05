using System.Collections.Generic;
using System.Threading.Tasks;
using WorkQueue.MVC.Models.ViewModels;

namespace WorkQueue.MVC.Helpers
{
    public interface IUserPermissionLogic
    {
        UserPermissions GetUserPermissions(string activeDirectoryUserName);
        string GetDisplayName(string userlogin);
    }
}
