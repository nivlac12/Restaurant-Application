using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace Restaurants_Database
{
    class FoodRepo
    {
        const string connectionString = @"Server=mssql.cs.ksu.edu;Database=nivlac12;Integrated Security=SSPI;";
        public void initFoods()
        {
            Food[] food = { };
            foreach (Food fo in food)
            {
                CreateFood(fo.SupplierID, fo.FoodName, fo.SupplierPrice, fo.RetailPrice);
            }
        }

        public Food CreateFood(int SupplierID, string FoodName, decimal SupplierPrice, decimal RetailPrice)
        {
            using (var transaction = new TransactionScope())
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    //Set the string parameter to call the appropriate sql function
                    using (var command = new SqlCommand("Food.CreateFood", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        //Hardcode this attribute because it is not an output parameter, rather
                        //we have to pass it into the function ourselves
                        command.Parameters.AddWithValue("SupplierID", SupplierID);
                        command.Parameters.AddWithValue("FoodName", FoodName);
                        command.Parameters.AddWithValue("SupplierPrice", SupplierPrice);
                        command.Parameters.AddWithValue("RetailPrice", RetailPrice);

                        //The next two parameters are output parameters, so instead of hardcoding
                        //these we initialize them and we'll get the values from the function
                        var idParam = command.Parameters.Add("FoodID", SqlDbType.Int);
                        idParam.Direction = ParameterDirection.Output;

                        //Executes the SQL itself, giving the output parameters we declared a value
                        connection.Open();
                        command.ExecuteNonQuery();

                        transaction.Complete();

                        //This line will return a unique object of the appropriate type, keeping in mind the parameters we stored
                        return new Food((int)idParam.Value, SupplierID, FoodName, (decimal)SupplierPrice, (decimal)RetailPrice);
                    }
                }
            }
        }

        public Food GetFood(string foodName)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("Food.GetFood", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    //Name of Primary Key is what we pass in, everything else
                    //we get from the SQL
                    command.Parameters.AddWithValue("FoodName", foodName);

                    connection.Open();

                    var reader = command.ExecuteReader();

                    if (!reader.Read())
                        return null;

                    return new Food(reader.GetInt32(Convert.ToInt32(reader.GetOrdinal("FoodID"))),
                       reader.GetInt32(reader.GetOrdinal("SupplierID")),
                       foodName,
                       reader.GetDecimal(reader.GetOrdinal("SupplierPrice")),
                       reader.GetDecimal(reader.GetOrdinal("RetailPrice")));
                }
            }
        }

        public Food GetFoodByID(int foodID)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("Food.GetFoodByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    //Name of Primary Key is what we pass in, everything else
                    //we get from the SQL
                    command.Parameters.AddWithValue("FoodID", foodID);

                    connection.Open();

                    var reader = command.ExecuteReader();

                    if (!reader.Read())
                        return null;

                    return new Food(foodID,
                       reader.GetInt32(reader.GetOrdinal("SupplierID")),
                       reader.GetString(reader.GetOrdinal("FoodName")),
                       reader.GetDecimal(reader.GetOrdinal("SupplierPrice")),
                       reader.GetDecimal(reader.GetOrdinal("RetailPrice")));
                }
            }
        }

        public IReadOnlyList<Food> RetrieveFood()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("Food.RetrieveFood", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    var reader = command.ExecuteReader();

                    var food = new List<Food>();

                    while (reader.Read())
                    {
                        food.Add(new Food(
                       reader.GetInt32(reader.GetOrdinal("FoodID")),
                       reader.GetInt32(reader.GetOrdinal("SupplierID")),
                       reader.GetString(reader.GetOrdinal("FoodName")),
                       reader.GetDecimal(reader.GetOrdinal("SupplierPrice")),
                       reader.GetDecimal(reader.GetOrdinal("RetailPrice"))));
                    }

                    return food;
                }
            }
        }
    }
}
