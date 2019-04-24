using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants_Database
{
    class Controller
    {
        OrganizationRepo _org;
        RestaurantRepository _rest;
        FoodRepo _food;
        JobsRepo _jobs;
        StockItemsRepo _stock;
        EmployeeRepo _emp;
        SuppliersRepo _suppliers;

        public Controller()
        {
            _org = new OrganizationRepo();
            _rest = new RestaurantRepository();
            _food = new FoodRepo(); 
        }
    
    }
}
