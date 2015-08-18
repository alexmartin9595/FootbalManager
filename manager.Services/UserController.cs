using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Security;
using manager.Business;
using manager.Entities;

namespace manager.Services
{
    public class UsersController : ApiController
    {
        [Authorize]
        [ActionName("GetAllUsers")]
        public IEnumerable<ManagerUser> GetAllUsers()
        {
            UserLogic userLogic = new UserLogic();
            return userLogic.GetAllUsers();
        }

        [Authorize]
        [ActionName("GetUserName")]
        public string GetUserName(int id)
        {
            UserLogic userLogic = new UserLogic();
            return userLogic.GetUserName(id);
        }

        [Authorize]
        [ActionName("GetCurrentUserId")]
        public int GetCurrentUserId()
        {
            UserLogic userLogic = new UserLogic();
            return userLogic.GetIdByNameUser(User.Identity.Name);
        }
    }
}
