using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants_Database
{
    class Suppliers
    {
        public int SuppliersID { get; }
        public string SuppliersName { get; }

        public Suppliers(int suppID,string suppName)
        {
            SuppliersID =suppID;
            SuppliersName = suppName;
        }
    }
}
