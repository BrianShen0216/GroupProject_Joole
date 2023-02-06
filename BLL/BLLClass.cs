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
    public class BLLClass
    {
        //JooleDatabaseEntities jooleDatabaseEntities = new JooleDatabaseEntities();
        DALClass dalClass = new DALClass();
        
        public List<Users> getUserList() 
        {
            //DbContext jooleDatabaseEntities = dalClass.DAL_Entity();
            
            /*var userList = dalClass.getUsers();
            return userList.ToList();*/
            //return jooleDatabaseEntities.Users.ToList();
            return dalClass.Users.ToList();
        }

        public Users AddUser(ModelUser ojbUser)
        {
            Users user = new Users();
            user.UserName = ojbUser.UserName;
            user.UserPassword = ojbUser.UserPassword;
            user.UserEmail = ojbUser.UserEmail;
            dalClass.Users.Add(user);
            //dalClass.SaveChanges();
            return user;
        }

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

        public Filters GetFilters(int subCategoryID)
        {
            Filters filters = new Filters();
            var subList = jooleDatabaseEntities.SubCategory.Where(sc => sc.SubCategoryID == subCategoryID)
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
