using BLL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Collections;



namespace GroupProject_Joole.Controllers
{
    public class HomeController : Controller
    {
        BLLClass bLLClass = new BLLClass();

        [HttpGet]
        public ActionResult Login()
        {
            ModelUser loginUser = new ModelUser();
            return View("Login", loginUser);
        }

        [HttpPost]
        public ActionResult Login(ModelUser loginUser)
        {
            var userList = bLLClass.getUserList();
            if(!ModelState.IsValid)
            {
                if(userList.Where(m => m.UserName == loginUser.UserName &&
                m.UserPassword == loginUser.UserPassword).FirstOrDefault() == null)
                {
                    ModelState.AddModelError("Error", "Username or password is not matching");
                    return View();
                }
            }
            return View("MainSearch");
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
                var user = bLLClass.AddUser(ojbUser);
                return RedirectToAction("Login");
                /*return RedirectToAction("GetInfor", user);*/
            }
            return View();
        }

        public ActionResult MainSearch()
        {
            return View("MainSearch");
        }
        public ActionResult Search()
        {
            var categoryList = bLLClass.getCategoryList();
            ViewBag.categoryList = new SelectList(categoryList, "CategoryID", "CategoryName");
            //tempdata

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
            List<SubCategory> subList = bLLClass.GetSubCategoryList().Where(x => x.CategoryID == CategoryID).ToList();
            return Json(subList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Result(CategorySub categorySub)
        {
            if (ModelState.IsValid)
            {
                var str = categorySub.userInput.Trim();
                var userInput = String.Concat(str.Where(s => !Char.IsWhiteSpace(s)));

                List<Products> productList = null;
                if (categorySub.SubCategoryID == null)
                {
                    productList = bLLClass.getProductsList().Where(x => x.ProductName.Contains(userInput)).Include("Manufacturers").Include("PropertyValue").Include("PropertyValue.Property").ToList();
                }
                else
                {
                    productList = bLLClass.getProductsList().Where(x => x.SubCategoryID == categorySub.SubCategoryID && x.ProductName.Contains(userInput)).Include("Manufacturers").Include("PropertyValue").Include("PropertyValue.Property").ToList();
                }
                
                if(productList.Count != 0)
                {
                    int tempId = productList[0].SubCategoryID;
                    Filters filters = bLLClass.GetFilters(tempId);
                    TempData["Products"] = productList;
                    TempData["Filters"] = filters;

                    return View("MainPage");
                }
            }
            if(categorySub.userInput == null)
            {
                ViewBag.errorMessage = "Please Check your input!!!!";
                return View("MainSearch");
            }
            ViewBag.nothing = true;
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

        public ActionResult ReturnMainPage()
        {
            return View("MainPage");
        }

        public ActionResult Details(int DetailID)
        {
            List<Products> products = (List<Products>)TempData.Peek("Products");
            Products res = products.Where(m=>m.ProductID == DetailID).FirstOrDefault();
            return PartialView("Details",res);
        }
        public ActionResult Compare(FormCollection form)
        {
            string[] str = form["CompareList"].Split(',');
            List<int> CprID = bLLClass.GetCprList(str);
            List<Products> products = (List<Products>)TempData.Peek("Products");
            List<Products> res = products.Where(m => CprID.Contains(m.ProductID)).ToList();
            return PartialView("Compare",res);
        }
    }

}