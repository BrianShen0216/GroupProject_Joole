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
        JooleDatabaseEntities jooleDatabaseEntities = new JooleDatabaseEntities();

        public List<Category> GetCategories()
        {
            return jooleDatabaseEntities.Category.ToList();
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
        public List<Users> GetUsers()
        {
            return jooleDatabaseEntities.Users.ToList();
        }

        public void AddUsers(Users users)
        {
            jooleDatabaseEntities.Users.Add(users);
            jooleDatabaseEntities.SaveChanges();
        }
    }
}
