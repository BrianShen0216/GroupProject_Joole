using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class ProductDetail
    {
        public int ProductID { get; set; }
        public Manufacturers Manufacturer { get; set; }
        public int SubCategoryID { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public string Series { get; set; }
        public string Model { get; set; }
        public Nullable<int> ModelYear { get; set; }
        public string SeriesInfo { get; set; }
        public string PropertyValue1 { get; set; }
        public int PropertyID { get; set; }
        public string PropertyName { get; set; }
        public Nullable<bool> IsType { get; set; }
        public Nullable<bool> IsTechSpec { get; set; }
    }
}
