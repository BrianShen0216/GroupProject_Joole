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
        public ActionResult Login()
        {
            /*JooleDatabaseEntities entities = new JooleDatabaseEntities();
            var ls = entities.Users.ToList();*/
            return View("Login");
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(Users users)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("GetInfor",users);
            }
            return View();
        }

        public ActionResult GetInfor(Users users)
        //public ActionResult GetInfor(ModelUser ojbUser)
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