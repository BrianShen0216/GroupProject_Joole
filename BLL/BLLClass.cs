using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public partial class BLLClass
    {
        //JooleDatabaseEntities jooleDatabaseEntities = new JooleDatabaseEntities();
        DALClass dALClass = new DALClass();


        public List<Category> getCategoryList()
        {
            return dALClass.GetCategories().ToList();
            //return jooleDatabaseEntities.Category.ToList();
        }

        public DbSet<SubCategory> GetSubCategoryList()
        {
            
            return dALClass.GetSubCategories();
        }

        public DbSet<Products> getProductsList()
        {
            return dALClass.GetProducts();
        }
        public List<Products> FilterProducts(List<Products> products, Filters filters)
        {
            List<Products> result = products;
            foreach(var tf in filters.TypeFilters)
            {
                result = result.Where(p =>
                {
                    foreach(var pv in p.PropertyValue)
                    {
                        if (pv.PropertyID==tf.PropertyID && pv.Property.IsType == true && pv.PropertyValue1==tf.Value)
                        {
                            return true;
                        }
                    }
                    return false;
                }).ToList();
            }
            foreach(var tsf in filters.TechSpecFilters)
            {
                result = result.Where(p =>
                {
                    foreach (var pv in p.PropertyValue)
                    {
                        if (pv.Property.IsTechSpec == true && pv.PropertyID == tsf.PropertyID)
                        {
                            try
                            {
                                int val = Int32.Parse(pv.PropertyValue1);
                                if (val>=tsf.MinValue && val<=tsf.Value)
                                {
                                    return true;
                                }
                            } catch (FormatException ex)
                            {}
                        }
                    }
                    return false;
                }).ToList();
            }
            return result;
        }

        public Filters GetFilters(int subCategoryID)
        {
            Filters filters = new Filters();
            var subList = dALClass.GetSubCategories().Where(sc => sc.SubCategoryID == subCategoryID)
                    .Include("TypeFilter")
                    .Include("TypeFilter.Property")
                    .Include("TechSpecFilter")
                    .Include("TechSpecFilter.Property")
                    .ToList();
            if (subList.Count > 0)
            {
                SubCategory sub = subList[0];
                filters.TypeFilters = sub.TypeFilter.Select(tf => new TypeFilterWithValue
                {
                    PropertyID = tf.PropertyID,
                    SubCategoryID = tf.SubCategoryID,
                    TypeName = tf.TypeName,
                    PropertyName = tf.Property.PropertyName
                }).ToList();
                filters.TechSpecFilters = sub.TechSpecFilter.Select(tsf => new TechSpecFilterWithValue
                {
                    PropertyID = tsf.PropertyID,
                    SubCategoryID = tsf.SubCategoryID,
                    MaxValue = tsf.MaxValue,
                    MinValue = tsf.MinValue,
                    PropertyName = tsf.Property.PropertyName
                }).ToList();
            }
            return filters;
        }
    }
}
