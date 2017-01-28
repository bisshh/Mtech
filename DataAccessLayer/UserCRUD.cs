using CoreLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class UserCRUD
    {
        //Create User
        public int SaveUser(User user)
        {
            using (var dataContext = new DataContext())
            {
                dataContext.user.Add(user);
                return dataContext.SaveChanges();
            }
        }

        //Read User
        public User GetUserByUserID(int UserID)
        {
            using (var dataContext = new DataContext())
            {
                User user = (from User in dataContext.user
                             where User.UserID == UserID
                             select User).First();
                return user;
            }
        }
        public User GetUserByUsername(string Username)
        {
            using (var dataContext = new DataContext())
            {
                var result = (from User in dataContext.user
                            where User.Username == Username
                            select User).FirstOrDefault();
                return result;
            }
        }

        public User GetUserByRole(string Role)
        {
            using (var dataContext = new DataContext())
            {
                User user = (from User in dataContext.user
                             where User.Role == Role
                             select User).First();
                return user;
            }
        }

        //Update User
        public void UpdateUser(User newUser)
        {
            using (var dataContext = new DataContext())
            {
                dataContext.Entry(newUser).State = EntityState.Modified;
                dataContext.SaveChanges();
            }
        }

        //Delete User
        public void RemoveUser(User userToBeDeleted)
        {
            using (var dataContext = new DataContext())
            {
                dataContext.Entry(userToBeDeleted).State = EntityState.Deleted;
                dataContext.SaveChanges();
            }
        }

        public List<User> GetAllUsers(string sortOrder, string searchString)
        {
            using (var dataContext = new DataContext())
            {
                IEnumerable<User> users = from User in dataContext.user
                                          select User;

                if (!string.IsNullOrEmpty(searchString))
                {
                    users = users.Where(x => (x.Username.ToLower().Contains(searchString.ToLower())));
                }
                if (!string.IsNullOrEmpty(sortOrder))
                {
                    switch (sortOrder)
                    {
                        case "username_asc":
                            users = users.OrderBy(x => x.Username).ToList();
                            break;
                        case "username_desc":
                            users = users.OrderByDescending(x => x.Username).ToList();
                            break;
                        case "password_asc":
                            users = users.OrderBy(x => x.Password).ToList();
                            break;
                        case "password_desc":
                            users = users.OrderByDescending(x => x.Password).ToList();
                            break;
                        case "estd_asc":
                            users = users.OrderBy(x => x.InsertedDate).ToList();
                            break;
                        case "estd_desc":
                            users = users.OrderByDescending(x => x.InsertedDate).ToList();
                            break;
                    }
                }
                else
                {
                    users = users.ToList();
                }
                return users.ToList();
            }
        }
    }
}
