using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Models;

namespace BLL
{
    public partial class BLLClass
    {
        public List<int> GetCprList(string[] str)
        {
            List<int> CprID = new List<int>();
            foreach (string item in str)
            {
                CprID.Add(int.Parse(item));
            }
            return CprID;
        }
    }
}
