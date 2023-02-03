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
