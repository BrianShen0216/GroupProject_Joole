using BLL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroupProject_Joole.Controllers
{
    public class HomeController : Controller
    {
        private BLLClass _bll;
        public HomeController() : base()
        {
            _bll= new BLLClass();
        }

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