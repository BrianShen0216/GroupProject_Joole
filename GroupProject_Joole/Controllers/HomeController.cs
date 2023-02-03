using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.Models;
using System.Web.UI.WebControls;
using GroupProject_Joole.Models;


namespace GroupProject_Joole.Controllers
{
    public class HomeController : Controller
    {
        protected JooleDatabaseEntities userEntity = new JooleDatabaseEntities();
        [HttpGet]
        public ActionResult Login()
        {
            //LoginUser loginUser = new LoginUser();
            ModelUser loginUser = new ModelUser();
            return View("Login", loginUser);
        }

        [HttpPost]
        public ActionResult Login(ModelUser loginUser)
        {
            if(!ModelState.IsValid)
            {
                if(userEntity.Users.Where(m => m.UserName == loginUser.UserName &&
                m.UserPassword == loginUser.UserPassword).FirstOrDefault() == null)
                {
                    ModelState.AddModelError("Error", "Username or password is not matching");
                    return View();
                    //return RedirectToAction("SignUp");
                }
            }
            return View("Search");
        }

        public ActionResult SignUp()
        {
            ModelUser objUser = new ModelUser();
            return View(objUser);
        }

        [HttpPost]
        public ActionResult SignUp(ModelUser ojbUser)
        {
            if (ModelState.IsValid)
            {
                Users user = new Users();
                user.UserName = ojbUser.UserName;
                user.UserEmail = ojbUser.UserEmail;
                user.UserPassword = ojbUser.UserPassword;
                userEntity.Users.Add(user);
                userEntity.SaveChanges();
                return RedirectToAction("GetInfor", user);
            }
            return View();
        }

        public ActionResult GetInfor(Users user)
        {
            //userEntity.addUser(ojbUser.UserName, ojbUser.UserEmail, ojbUser.UserPassword);
            var list = userEntity.Users.ToList();
            
            return View(list);                
        }

        /*public ActionResult SignUp()
        {

            return View();
        }*/

        public ActionResult Search()
        {
            return View();
        }
        public ActionResult Summary()
        {
            return View();
        }
        public ActionResult Details()
        {
            return View();
        }
        public ActionResult Compare()
        {
            return View();
        }
    }

}