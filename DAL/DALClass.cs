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
    public partial class DALClass
    {
        JooleDatabaseEntities jooleDatabaseEntities = new JooleDatabaseEntities();

        public DbSet<Category> GetCategories()
        {
            return jooleDatabaseEntities.Category;
        }

        public DbSet<SubCategory> GetSubCategories()
        {
            jooleDatabaseEntities.Configuration.ProxyCreationEnabled = false;
            return jooleDatabaseEntities.SubCategory;
        }

        public DbSet<Products> GetProducts()
        {
            return jooleDatabaseEntities.Products;
        }
    }
}
