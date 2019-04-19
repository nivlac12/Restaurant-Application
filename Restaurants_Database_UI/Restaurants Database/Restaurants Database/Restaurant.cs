using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants_Database
{
    class Restaurant
    {
        public int RestaurantID { get; }
        public int OrganizationID { get; }
        public string RestaurantName { get; }
        public string DateFounded { get; }
        public bool IsOperational { get; }

        public Restaurant(int restID, int orgID, string restName, string date, bool isop)
        {
            RestaurantID = RestaurantID;
            OrganizationID = orgID;
            RestaurantName = restName;
            DateFounded = date;
            IsOperational = isop;
        }
    }
}
