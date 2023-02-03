using BLL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        //JooleDatabaseEntities jooleDatabaseEntities = new JooleDatabaseEntities();
        BLLClass bLLClass = new BLLClass();

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
            return RedirectToAction("Search");
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

            //var categoryList = jooleDatabaseEntities.Category.ToList();
            var categoryList = bLLClass.getCategoryList();
            ViewBag.categoryList = new SelectList(categoryList, "CategoryID", "CategoryName");

            return PartialView("Search");
        }
        public ActionResult ApplyFilter(Filters filters)
        {
            List<Products> products = (List<Products>)TempData.Peek("Products");
            List<Products> filteredProducts = bLLClass.FilterProducts(products, filters);
            return PartialView("ProductDisplay", filteredProducts);
        }

        public JsonResult GetSubList(int CategoryID)
        {
            //jooleDatabaseEntities.Configuration.ProxyCreationEnabled = false;
            //List<SubCategory> subList = jooleDatabaseEntities.SubCategory.Where(x => x.CategoryID == CategoryID).ToList();
            List<SubCategory> subList = bLLClass.GetSubCategoryList().Where(x => x.CategoryID == CategoryID).ToList();
            return Json(subList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Result(CategorySub categorySub)
        {
            var str = categorySub.userInput.Trim();
            var userInput = String.Concat(str.Where(s => !Char.IsWhiteSpace(s)));
            
            List<Products> productList = null;
            if(categorySub.SubCategoryID == null)
            {
                //productList = jooleDatabaseEntities.Products.Where(x => x.ProductName.Contains(userInput)).ToList();
                productList = bLLClass.getProductsList().Where(x => x.ProductName.Contains(userInput)).Include("Manufacturers").Include("PropertyValue").Include("PropertyValue.Property").ToList();
            }
            else
            {
                //productList = jooleDatabaseEntities.Products.Where(x => x.SubCategoryID == categorySub.SubCategoryID && x.ProductName.Contains(userInput)).ToList();
                productList = bLLClass.getProductsList().Where(x => x.SubCategoryID == categorySub.SubCategoryID && x.ProductName.Contains(userInput)).Include("Manufacturers").Include("PropertyValue").Include("PropertyValue.Property").ToList();
            }
            //var productList = jooleDatabaseEntities.Products.Where(x => x.SubCategoryID == id && x.ProductName.Contains(userInput)).Include("Manufacturers").Include("PropertyValue").Include("PropertyValue.Property").ToList();
            //Console.WriteLine(productList);
            Filters filters = bLLClass.GetFilters(categorySub.SubCategoryID);
            TempData["Products"] = productList;
            TempData["Filters"] = filters;

            return View("MainPage");
        }

        [HttpGet]
        public PartialViewResult ReturnDisplayPage()
        {
            return PartialView("DisplayPage");
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