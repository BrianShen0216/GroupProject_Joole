using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class TechSpecFilterWithValue
    {
        public int PropertyID { get; set; }
        public string PropertyName { get; set; }
        public int SubCategoryID { get; set; }
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
        public int Value { get; set; }
    }
}
