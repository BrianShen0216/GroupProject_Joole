using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALClass
    {
        public List<ProductDetail> GetAllProdcutDetails()
        {
            using (var dbcontext = new JooleDatabaseEntities())
            {
                var products = dbcontext.Products;
                var propertyvalue = dbcontext.PropertyValue;
                var property = dbcontext.Property;
                var productporpertyvalue = from p in products
                                           join pv in propertyvalue on p.ProductID equals pv.ProductID into ppv
                                           from x in ppv.DefaultIfEmpty()
                                           select new
                                           {
                                               p,
                                               x.PropertyID,
                                               x.PropertyValue1
                                           };
                var result = from ppv in productporpertyvalue
                             join pp in property on ppv.PropertyID equals pp.PropertyID into ppvp
                             from x in ppvp.DefaultIfEmpty()
                             select new
                             {
                                 ppv,
                                 x.PropertyName,
                                 x.IsType,
                                 x.IsTechSpec
                             };
                List<ProductDetail> productdetails = new List<ProductDetail>();
                foreach (var item in result)
                {
                    ProductDetail pd = new ProductDetail()
                    {
                        ProductID = item.ppv.p.ProductID,
                        ProductName = item.ppv.p.ProductName,
                        Manufacturer = item.ppv.p.Manufacturers,
                        SubCategoryID = item.ppv.p.SubCategoryID,
                        ProductImage = item.ppv.p.ProductImage,
                        Series = item.ppv.p.Series,
                        Model = item.ppv.p.Model,
                        ModelYear = item.ppv.p.ModelYear,
                        SeriesInfo = item.ppv.p.SeriesInfo,
                        PropertyValue1 = item.ppv.PropertyValue1,
                        PropertyID = item.ppv.PropertyID,
                        PropertyName = item.PropertyName,
                        IsType = item.IsType,
                        IsTechSpec = item.IsTechSpec
                    };
                    productdetails.Add(pd);
                };
                return productdetails;
            }
        }

        public List<Products> GetAllProducts()
        {
            using (var dbcontext = new JooleDatabaseEntities())
            {
                var products = dbcontext.Products.Include("Manufacturers")
                    .Include("PropertyValue").Include("PropertyValue.Property").ToList();
                return products;
            }
        }

        public List<Products> GetSubCategoryProducts(int subCategoryID)
        {
            using (var dbcontext = new JooleDatabaseEntities())
            {
                return dbcontext.Products
                    .Where(p=>p.SubCategoryID==subCategoryID)
                    .Include("Manufacturers")
                    .Include("PropertyValue")
                    .Include("PropertyValue.Property")
                    .ToList();
            }
        }

        public SubCategory GetSubCategory(int subCategoryID)
        {
            using (var dbcontext = new JooleDatabaseEntities())
            {
                var subList = dbcontext.SubCategory.Where(sc => sc.SubCategoryID == subCategoryID)
                    .Include("TypeFilter")
                    .Include("TypeFilter.Property")
                    .Include("TechSpecFilter")
                    .Include("TechSpecFilter.Property")
                    .ToList();
                if (subList.Count>0)
                {
                    return subList[0];
                }
                return null;
            }
        }
    }
}
