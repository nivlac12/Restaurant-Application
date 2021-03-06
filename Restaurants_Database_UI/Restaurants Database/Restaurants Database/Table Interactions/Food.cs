﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants_Database
{
    class Food
    {
        public int FoodID { get; }
        public int SupplierID { get; }
        public string FoodName { get; }
        public double SupplierPrice { get; }
        public double RetailPrice { get; }

        public Food(int foodID, int suppID, string foodName, double suppPrice, double retailPrice)
        {
            FoodID= foodID;
            SupplierID = suppID;
            FoodName = foodName;
            SupplierPrice = suppPrice;
            RetailPrice = retailPrice;
        }
    }
}
