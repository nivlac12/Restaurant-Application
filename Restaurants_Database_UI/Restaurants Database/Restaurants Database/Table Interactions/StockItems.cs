using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants_Database
{
    class StockItem
    {
        public int InventoryID { get; }
        public int FoodID { get; }
        public int RestaurantID { get; }
        public int Quantity { get; }

        public StockItem(int invenID, int foodID, int restID, int quantity)
        {
            InventoryID = invenID;
            FoodID = foodID;
            RestaurantID = restID;
            Quantity = quantity;
        }
    }
}
