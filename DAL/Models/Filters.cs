using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Filters
    {
        public List<TypeFilterWithValue> TypeFilters { get; set; }
        public List<TechSpecFilterWithValue> TechSpecFilters { get; set; }
    }
}
