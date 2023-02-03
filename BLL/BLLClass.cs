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
    }
}
