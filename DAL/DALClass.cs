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
        //DbSet<Users> Users = jooleDatabaseEntities.Users;
        /*public DbContext DAL_Entity()
        {
            return jooleDatabaseEntities;
        }*/
        public DbSet<Users> Users { get; set; }
    }
}
