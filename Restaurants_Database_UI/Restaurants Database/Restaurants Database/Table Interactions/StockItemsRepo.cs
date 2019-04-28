using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace Restaurants_Database
{
    class StockItemsRepo
    {
        const string connectionString = @"Server=mssql.cs.ksu.edu;Database=nivlac12;Integrated Security=SSPI;";

        public StockItem CreateStockItems(int FoodID, int RestaurantID, int Quantity)
        {
            using (var transaction = new TransactionScope())
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    //Set the string parameter to call the appropriate sql function
                    using (var command = new SqlCommand("Inventory.CreateStockItem", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        //Hardcode this attribute because it is not an output parameter, rather
                        //we have to pass it into the function ourselves
                        command.Parameters.AddWithValue("FoodID", FoodID);
                        command.Parameters.AddWithValue("RestaurantID", RestaurantID);
                        command.Parameters.AddWithValue("Quantity", Quantity);
                        //The next two parameters are output parameters, so instead of hardcoding
                        //these we initialize them and we'll get the values from the function
                        var idParam = command.Parameters.Add("InventoryID", SqlDbType.Int);
                        idParam.Direction = ParameterDirection.Output;

                        //Executes the SQL itself, giving the output parameters we declared a value
                        connection.Open();
                        command.ExecuteNonQuery();

                        transaction.Complete();

                        //This line will return a unique object of the appropriate type, keeping in mind the parameters we stored
                        return new StockItem((int)idParam.Value, FoodID, RestaurantID, Quantity);
                    }
                }
            }
        }

        public StockItem GetStockItem(string foodName, string restName)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("Inventory.GetStockItem", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    //Name of Primary Key is what we pass in, everything else
                    //we get from the SQL
                    command.Parameters.AddWithValue("FoodName", foodName);
                    command.Parameters.AddWithValue("RestaurantName", restName);
                    connection.Open();

                    var reader = command.ExecuteReader();

                    if (!reader.Read())
                        return null;

                    return new StockItem(reader.GetInt32(Convert.ToInt32(reader.GetOrdinal("InventoryID"))),
                        reader.GetInt32(reader.GetOrdinal("FoodID")),
                        reader.GetInt32(reader.GetOrdinal("RestaurantID")),
                        reader.GetInt32(reader.GetOrdinal("Quantity")));
                }
            }
        }

        public StockItem GetStockItemByID(int itemID)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("Inventory.GetStockItemByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    //Name of Primary Key is what we pass in, everything else
                    //we get from the SQL
                    command.Parameters.AddWithValue("itemID", itemID);
                    connection.Open();

                    var reader = command.ExecuteReader();

                    if (!reader.Read())
                        return null;

                    return new StockItem(itemID,
                        reader.GetInt32(reader.GetOrdinal("FoodID")),
                        reader.GetInt32(reader.GetOrdinal("RestaurantID")),
                        reader.GetInt32(reader.GetOrdinal("Quantity")));
                }
            }
        }

        public void UpdateStockItem(int id, int foodID, int restID, int quantity)
        {
            using (var transaction = new TransactionScope())
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var command = new SqlCommand("Inventory.UpdateStockItem", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("InventoryID", id);
                        command.Parameters.AddWithValue("FoodID", foodID);
                        command.Parameters.AddWithValue("RestaurantID", restID);
                        command.Parameters.AddWithValue("Quantity", quantity);

                        connection.Open();
                        command.ExecuteNonQuery();
                        transaction.Complete();
                    }
                }
            }
        }

        public IReadOnlyList<StockItem> RetrieveStockItems()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("Inventory.RetrieveStockItems", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    var reader = command.ExecuteReader();

                    var items = new List<StockItem>();

                    while (reader.Read())
                    {
                        items.Add(new StockItem(
                           reader.GetInt32(reader.GetOrdinal("InventoryID")),
                           reader.GetInt32(reader.GetOrdinal("FoodID")),
                           reader.GetInt32(reader.GetOrdinal("RestaurantID")),
                           reader.GetInt32(reader.GetOrdinal("Quantity"))));
                    }

                    return items;
                }
            }
        }
    }
}
