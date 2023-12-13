using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult input()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult AddUserToDatabase(FormCollection fc)
        {
            String firstName = fc["firstname"];
            String lastName = fc["lastname"];
            String email = fc["email"];
            String diko = fc["password"];

            user use = new user();
            use.firstName = firstName;
            use.lastName = lastName;
            use.email = email;
            use.password = diko;
            use.roleId = 1;

            mydatabaseEntities4 fe = new mydatabaseEntities4();
            fe.users.Add(use);
            fe.SaveChanges();

            //insert the code that will save these information to the DB

            return RedirectToAction("input");
        }
        public ActionResult ShowUser()
        {
            mydatabaseEntities4 fe = new mydatabaseEntities4();
            var userList = (from a in fe.users
                            select a).ToList();

            ViewData["UserList"] = userList;
            return View();
        }

        [HttpPost]
        public ActionResult EditUser(int id)
        {
            int x = id;


            mydatabaseEntities4 SelectedUser = new mydatabaseEntities4();

            var selectedUser = (from a in SelectedUser.users where a.userId == x select a).ToList();


            ViewData["User"] = selectedUser;

            return View();
            //  return RedirectToAction("UserUpdate");  // Redirect to the appropriate action or view
        }
        public ActionResult Update(FormCollection fc, int id)
        {
            mydatabaseEntities4 rdbe = new mydatabaseEntities4();
            user u = (from a in rdbe.users
                      where a.userId == id
                      select a).FirstOrDefault();

            String new_first_name = fc["new_firstname"];
            String new_last_name = fc["new_lastname"];
            String new_email = fc["new_email"];
            String new_password = fc["new_password"];

            u.firstName = new_first_name;
            u.lastName = new_last_name;
            u.email = new_email;
            u.password = new_password;

            rdbe.SaveChanges();

            return RedirectToAction("ShowUser");
        }

        public ActionResult DeleteUser(int id)
        {
            mydatabaseEntities4 fe = new mydatabaseEntities4();
            var user = fe.users.Find(id);

            if (user != null)
            {
                fe.users.Remove(user);
                fe.SaveChanges();
            }

            return RedirectToAction("ShowUser");
        }


    }
}
