using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLClass
    {
        private DALClass _dal;
        public BLLClass()
        {
            _dal = new DALClass();
        }

        public List<ProductDetail> GetAllProdcutDetails()
        {
            return _dal.GetAllProdcutDetails();
        }

        public List<Products> GetAllProducts()
        {
            return _dal.GetAllProducts();
        }

        public List<Products> GetSubCategoryProducts(int subCategoryID)
        {
            return _dal.GetSubCategoryProducts(subCategoryID);
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

        public Filters GetFilters(int subCategoryID)
        {
            Filters filters = new Filters();
            SubCategory sub = _dal.GetSubCategory(subCategoryID);
            if (sub == null)
            {
                filters.TypeFilters = new List<TypeFilterWithValue>();
                filters.TechSpecFilters = new List<TechSpecFilterWithValue>();
            }else
            {
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
