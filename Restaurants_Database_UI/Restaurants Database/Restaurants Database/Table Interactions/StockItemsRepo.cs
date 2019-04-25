using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace Restaurants_Database
{
    class StockItemsRepo
    {
        const string connectionString = @"Server=(localdb)\MSSQLLocalDb;Database=nivlac12;Integrated Security=SSPI;";
        public void initStockItems()
        {
            StockItems[] items = { };
            foreach (StockItems item in items)
            {
                CreateStockItems(item.FoodID,item.RestaurantID,item.Quantity);
            }
        }

        public StockItems CreateStockItems(int FoodID, int RestaurantID, int Quantity)
        {
            using (var transaction = new TransactionScope())
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    //Set the string parameter to call the appropriate sql function
                    using (var command = new SqlCommand("Restaurants.Insert_StockItems", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        //Hardcode this attribute because it is not an output parameter, rather
                        //we have to pass it into the function ourselves
                        command.Parameters.AddWithValue("FoodID", FoodID);
                        command.Parameters.AddWithValue("FoodID", RestaurantID);
                        command.Parameters.AddWithValue("FoodID", Quantity);
                        //The next two parameters are output parameters, so instead of hardcoding
                        //these we initialize them and we'll get the values from the function
                        var idParam = command.Parameters.Add("InventoryID", SqlDbType.Int);
                        idParam.Direction = ParameterDirection.Output;

                        //Executes the SQL itself, giving the output parameters we declared a value
                        connection.Open();
                        command.ExecuteNonQuery();

                        transaction.Complete();

                        //This line will return a unique object of the appropriate type, keeping in mind the parameters we stored
                        return new StockItems((int)idParam.Value, FoodID, RestaurantID, Quantity);
                    }
                }
            }
        }

        public StockItems GetStockItems(int foodID, int restID)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("Restaurants.GetStockItems", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    //Name of Primary Key is what we pass in, everything else
                    //we get from the SQL
                    command.Parameters.AddWithValue("FoodID", foodID);
                    command.Parameters.AddWithValue("Restaurant", restID);
                    connection.Open();

                    var reader = command.ExecuteReader();

                    if (!reader.Read())
                        return null;

                    return new StockItems(reader.GetInt32(Convert.ToInt32(reader.GetOrdinal("InventoryID"))),
                        reader.GetInt32(reader.GetOrdinal("FoodID")),
                        reader.GetInt32(reader.GetOrdinal("ResturantID")),
                        reader.GetInt32(reader.GetOrdinal("QuantityID")));
                }
            }
        }

        public IReadOnlyList<StockItems> RetrieveStockItems()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("Restaurants.RetrieveStockItems", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    var reader = command.ExecuteReader();

                    var items = new List<StockItems>();

                    while (reader.Read())
                    {
                        items.Add(new StockItems(
                           reader.GetInt32(reader.GetOrdinal("inventoryID")),
                           reader.GetInt32(reader.GetOrdinal("FoodID")),
                           reader.GetInt32(reader.GetOrdinal("ResturantID")),
                           reader.GetInt32(reader.GetOrdinal("QuantityID"))));
                    }

                    return items;
                }
            }
        }
    }
}
