using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using manager.Data;
using manager.Entities;

namespace manager.Business
{
    public class UserLogic
    {
        private UserStorage userStorage;
        public UserLogic()
        {
            userStorage = new UserStorage();
        }

        public IEnumerable<ManagerUser> GetAllUsers()
        {
            return userStorage.GetAllUsers();
        }

        public ManagerUser GetUserByName(string name)
        {
            return userStorage.GetUserByName(name);
        }

        public int GetIdByNameUser(string name)
        {
            return userStorage.GetUserByName(name).Id;
        }

        public string GetUserName(int id)
        {
            return userStorage.GetUserById(id).UserName;
        }

        public void AddUser(ManagerUser user)
        {
            userStorage.AddUser(user);
        }
    }
}
