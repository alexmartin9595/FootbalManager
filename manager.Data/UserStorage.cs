using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using manager.Entities;

namespace manager.Data
{
    public class UserStorage
    {
        private EFDbContext currentContext;

        public UserStorage()
        {
            currentContext = new EFDbContext();
            currentContext.Configuration.ProxyCreationEnabled = false;
        }
        public IEnumerable<ManagerUser> GetAllUsers()
        {
            return currentContext.Users;
        }

        public ManagerUser GetUserByName(string name)
        {
            return currentContext.Users.FirstOrDefault(x => x.UserName == name);
        }

        public ManagerUser GetUserById(int id)
        {
            return currentContext.Users.FirstOrDefault(x => x.Id == id);
        }
        public void AddUser(ManagerUser user)
        {
            currentContext.Users.Add(user);
            currentContext.SaveChanges();
        }
    }
}
