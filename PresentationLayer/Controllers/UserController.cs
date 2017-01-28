using CoreLayer;
using BusinessLayer;
using PresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Security.Cryptography;
using System.Web.Security;
using System.Text;

namespace PresentationLayer.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/
        UserService userService = new UserService();
        EmployeeService employeeService = new EmployeeService();
        private static RNGCryptoServiceProvider m_cryptoServiceProvider = null;
        private const int SALT_SIZE = 24;

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.UserNameSortParm = sortOrder == "username_asc" ? "username_desc" : "username_asc";
            ViewBag.EmailSortParm = sortOrder == "email_asc" ? "email_desc" : "email_asc";
            ViewBag.PasswordSortParm = sortOrder == "password_asc" ? "password_desc" : "password_asc";
            ViewBag.EstdSortParm = sortOrder == "estd_asc" ? "estd_desc" : "estd_asc";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            var users = userService.GetAllUsers(sortOrder, searchString);

            return View(users.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Register()
        {
            return View();
        }

        // Save User
        [HttpPost]
        public ActionResult SaveUser(UserViewModel userDetails)
        {
            int UserID = 0;
            User user;
            if (ModelState.IsValid)
            {
                User userDataInTable = userService.GetUserByUsername(userDetails.Username);
                Employee employee = employeeService.GetEmployeeByEmail(userDetails.Email);

                if (userDataInTable != null)
                {
                    ModelState.AddModelError("Username", "Username Already Exist. Please try another one.");
                    return View("Register", userDetails);
                }
                else
                {
                    user = new User()
                    {
                        Username = userDetails.Username,
                        Password = getEncryptedPassword(userDetails.Password),
                        InsertedDate = DateTime.Now
                    };
                    UserID = userService.SaveUser(user);
                }

                if (employee != null)
                {
                    if (employee.UserID != null)
                    {
                        ModelState.AddModelError("Email", "This email is already in use. Please Try another one.");
                        return View("Register", userDetails);
                    }
                    else {
                        employee.UserID = UserID;
                        employeeService.UpdateEmployee(employee);
                    }
                }
            }
            return View(userDetails);
        }

        //Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserLoginViewModel userLoginData)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Login data is incorrect!");
                return RedirectToAction("Index", "Home", null);
            }

            User userDataSavedInDatabase = userService.GetUserByUsername(userLoginData.Username);
            if (userDataSavedInDatabase != null)
            {
                if (string.Equals(userDataSavedInDatabase.Password, getEncryptedPassword(userLoginData.Password))) //to get password in encrypted form
                {
                    FormsAuthentication.SetAuthCookie(userLoginData.Username, userLoginData.RememberMe);
                    return RedirectToAction("Dashboard", "Home", null);
                }
            }
            return RedirectToAction("Index", "Home", null);
        }

        public ActionResult LogOut()
        {
            // FormsAuthentication.SignOut();
            if (Request.Cookies["user"] != null)
            {
                HttpCookie user = new HttpCookie("user");
                user.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(user);
                Session.Clear();
            }
            else {
                Session.Clear();
            }
            return RedirectToAction("Index", "Home", null);
        }

        private string getEncryptedPassword(string Password)
        {
            // string salt = getSaltString();
            string hashedString = getEncryptedString(Password);//getEncryptedString(Password);
            // string encryptedPassword = string.Concat(salt, hashedString);
            // return encryptedPassword;
            return hashedString;
        }

        //private string getSaltString()
        //{
        //    RNGCryptoServiceProvider saltGenerator = new RNGCryptoServiceProvider();
        //    byte[] buff = new byte[32];
        //    saltGenerator.GetBytes(buff);
        //    return Convert.ToBase64String(buff);
        //}

        private string getEncryptedString(string text)
        {
            UnicodeEncoding uEncode = new UnicodeEncoding();
            byte[] bytD2e = uEncode.GetBytes(text);
            SHA256Managed sha = new SHA256Managed();
            byte[] hash = sha.ComputeHash(bytD2e);
            return Convert.ToBase64String(hash);
        }

        //User Update
        [HttpPost]
        public ActionResult UpdateUser(UserViewModel newUserData)
        {
            User user = new User()
            {
                Username = newUserData.Username,
                Password = newUserData.Password,
                InsertedDate = DateTime.Now,
            };
            userService.UpdateUser(user);
            return RedirectToAction("Index");
        }

        //User Edit
        public ActionResult Edit(int UserID)
        {
            User user = userService.GetUserById(UserID);
            UserViewModel userViewModel = new UserViewModel()
            {
                Username = user.Username,
                Password = user.Password
            };
            return View(userViewModel);
        }

        //User Delete
        public ActionResult DeleteUser(int UserID)
        {
            try
            {
                userService.DeleteUser(UserID);
            }
            catch
            {
                return View();
            }
            return RedirectToAction("Index");
        }
    }
}
