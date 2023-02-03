using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLClass
    {
        JooleDatabaseEntities jooleDatabaseEntities = new JooleDatabaseEntities();


        public List<Category> getCategoryList()
        {
            return jooleDatabaseEntities.Category.ToList();
        }

        public DbSet<SubCategory> GetSubCategoryList()
        {
            jooleDatabaseEntities.Configuration.ProxyCreationEnabled = false;
            return jooleDatabaseEntities.SubCategory;
        }

        public DbSet<Products> getProductsList()
        {
            return jooleDatabaseEntities.Products;
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

        public List<TypeFilterWithValue> GetTypeFilters(int subCategoryID)
        {
            SubCategory sub = _dal.GetSubCategory(subCategoryID);
            if (sub==null)
            {
                return new List<TypeFilterWithValue>();
            }
            return sub.TypeFilter.Select(tf => new TypeFilterWithValue
            {
                PropertyID = tf.PropertyID,
                SubCategoryID = tf.SubCategoryID,
                TypeName = tf.TypeName,
                PropertyName = tf.Property.PropertyName
            }).ToList();
        }

        public List<TechSpecFilterWithValue> GetTechSpecFilters(int subCategoryID)
        {
            SubCategory sub = _dal.GetSubCategory(subCategoryID);
            if (sub == null)
            {
                return new List<TechSpecFilterWithValue>();
            }
            return sub.TechSpecFilter.Select(tsf => new TechSpecFilterWithValue
            {
                PropertyID = tsf.PropertyID,
                SubCategoryID = tsf.SubCategoryID,
                MaxValue = tsf.MaxValue,
                MinValue= tsf.MinValue,
                PropertyName = tsf.Property.PropertyName
            }).ToList();
        }
    }
}
