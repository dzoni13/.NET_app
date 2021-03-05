using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using IdeaTrading.Models;
using IdeaTrading.Services;
using System.Web.Script.Serialization;

namespace IdeaTrading.Controllers
{
    public class UserController : Controller
    {

        // GET: User
        public ActionResult Index()
        {
            IUserService userService = new UserService();
            IEnumerable<User> users = userService.GetUsers();
            return View("Index", users);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            IUserService userService = new UserService();
            User user = userService.GetUser((int)id);
            return View("Edit", user);
        }

        // POST: User/Update

        public ActionResult UpdateRecord(FormCollection frm, string action)
        {
            if (action == "Submit")
            {
                User user = new User();
                user.FirstName = frm["FirstName"];
                user.LastName = frm["LastName"];
                user.Address = frm["Address"];
                user.City = frm["City"];
                user.DateOfEmployment = Convert.ToDateTime(frm["DateOfEmployment"]);
                user.Position = frm["Position"];
                user.Phone = frm["Phone"];
                user.Id = Convert.ToInt32(frm["hdnID"]);
                IUserService userService = new UserService();
                userService.EditUser(user);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Insert()
        {
            return View("Create");
        }

        public ActionResult InsertRecord(FormCollection frm, string action)
        {
            if (action == "Submit")
            {
                User user = new User();
                user.FirstName = frm["FirstName"];
                user.LastName = frm["LastName"];
                user.Address = frm["Address"];
                user.City = frm["City"];
                user.DateOfEmployment = Convert.ToDateTime(frm["DateOfEmployment"]);
                user.Position = frm["Position"];
                user.Phone = frm["Phone"];

                IUserService userService = new UserService();
                userService.CreateUser(user);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            IUserService userService = new UserService();
            userService.DeleteUser(id);
            return RedirectToAction("Index");
        }
      
        public string Cities()
        {
            IUserService userService = new UserService();
            IEnumerable<object> users = userService.GetCities();
            string json = new JavaScriptSerializer().Serialize(users);
            return json;
         
        }
    }
}
