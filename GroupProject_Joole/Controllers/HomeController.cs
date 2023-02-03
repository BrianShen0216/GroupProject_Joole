using BLL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using DAL.Models;

namespace GroupProject_Joole.Controllers
{
    public class HomeController : Controller
    {
        //JooleDatabaseEntities jooleDatabaseEntities = new JooleDatabaseEntities();
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

            //var categoryList = jooleDatabaseEntities.Category.ToList();
            var categoryList = bLLClass.getCategoryList();
            ViewBag.categoryList = new SelectList(categoryList, "CategoryID", "CategoryName");

            return View();
            List<Products> products = _bll.GetSubCategoryProducts(1);
            Filters filters = _bll.GetFilters(1);
            TempData["Products"] = products;
            TempData["Filters"] = filters;
            return View("MainPage");
        }
        [HttpGet]
        public ActionResult ApplyFilter(Filters filters)
        {
            List<Products> products = (List<Products>)TempData.Peek("Products");
            List<Products> filteredProducts = _bll.FilterProducts(products, filters);
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
            return View("Result",productList);
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