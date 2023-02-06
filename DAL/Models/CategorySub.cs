using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class CategorySub
    {

        public Nullable<int> CategoryID { get; set; }

        public Nullable<int> SubCategoryID { get; set; }

        [Required(ErrorMessage = "Please Check Your Input")]
        public string userInput { get; set; }
    }
}
