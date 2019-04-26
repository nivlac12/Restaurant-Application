using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants_Database
{
    class Supplier
    {
        public int SuppliersID { get; }
        public string SuppliersName { get; }

        public Supplier(int suppID,string suppName)
        {
            SuppliersID =suppID;
            SuppliersName = suppName;
        }
    }
}
