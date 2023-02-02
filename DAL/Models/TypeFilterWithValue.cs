using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class TypeFilterWithValue
    {
        public int PropertyID { get; set; }
        public string PropertyName { get; set; }
        public int SubCategoryID { get; set; }
        public string TypeName { get; set; }
        public string Value { get; set; }
    }
}
