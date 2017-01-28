using CoreLayer;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class UserService
    {
        UserCRUD userTableActions;

        public UserService()
        {
            userTableActions = new UserCRUD();
        }

        public int SaveUser (User userData)
        {
           return userTableActions.SaveUser(userData);
        }

        public User GetUserById(int UserID)
        {
            return userTableActions.GetUserByUserID(UserID);
        }
        public User GetUserByUsername(string Username)
        {
            return userTableActions.GetUserByUsername(Username);
        }

        public User GetUserByRole(string Role)
        {
            return userTableActions.GetUserByRole(Role);
        }
        public void UpdateUser(User dataToBeUpdated)
        {
            userTableActions.UpdateUser(dataToBeUpdated);
        }
        public void RemoveUser(User dataToBeDeleted)
        {
            userTableActions.RemoveUser(dataToBeDeleted);
        }

        public void DeleteUser(int UserID)
        {
            User userToBeDeleted = userTableActions.GetUserByUserID(UserID);
            userTableActions.RemoveUser(userToBeDeleted);
        }

        public IList<User> GetAllUsers(string sortOrder, string searchString)
        {
            return userTableActions.GetAllUsers(sortOrder, searchString);
        }
    }
}
