using BLL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroupProject_Joole.Controllers
{
    public class HomeController : Controller
    {
        BLLClass bLLClass = new BLLClass();
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult SignUp()
        {
            return View();
        }

        public ActionResult Search()
        {
            var categoryList = bLLClass.getCategoryList();
            ViewBag.categoryList = new SelectList(categoryList, "CategoryID", "CategoryName");
            //tempdata

            return View();
        }
        [HttpGet]
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
            return RedirectToAction("Search");
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