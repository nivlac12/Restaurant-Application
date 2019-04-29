using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace Restaurants_Database
{
    class RestaurantRepository
    {
        const string connectionString = @"Server=mssql.cs.ksu.edu;Database=nivlac12;Integrated Security=SSPI;";

        public Restaurant CreateRestaurant(int OrganizationID, string RestaurantName, bool IsOperational)
        {
            using (var transaction = new TransactionScope())
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    //Set the string parameter to call the appropriate sql function
                    using (var command = new SqlCommand("Restaurants.CreateRestaurant", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        //Hardcode this attribute because it is not an output parameter, rather
                        //we have to pass it into the function ourselves
                        command.Parameters.AddWithValue("OrganizationID", OrganizationID);
                        command.Parameters.AddWithValue("RestaurantName", RestaurantName);
                        command.Parameters.AddWithValue("IsOperational", IsOperational);

                        //The next two parameters are output parameters, so instead of hardcoding
                        //these we initialize them and we'll get the values from the function
                        var idParam = command.Parameters.Add("RestaurantID", SqlDbType.Int);
                        var dateParam = command.Parameters.Add("DateFounded", SqlDbType.DateTimeOffset);
                        idParam.Direction = ParameterDirection.Output;
                        dateParam.Direction = ParameterDirection.Output;

                        //Executes the SQL itself, giving the output parameters we declared a value
                        connection.Open();
                        command.ExecuteNonQuery();

                        transaction.Complete();
                        int val = (int)idParam.Value;
                        string dateValue = dateParam.Value.ToString();
                        //This line will return a unique object of the appropriate type, keeping in mind the parameters we stored
                        return new Restaurant(val, OrganizationID, RestaurantName, dateValue, IsOperational);
                    }
                }
            }
        }

        public Restaurant GetRestaurant(string restName)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("Restaurants.GetRestaurant", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    //Name of Primary Key is what we pass in, everything else
                    //we get from the SQL
                    command.Parameters.AddWithValue("RestaurantName", restName);

                    connection.Open();

                    var reader = command.ExecuteReader();

                    if (!reader.Read())
                        return null;

                    return new Restaurant(reader.GetInt32(Convert.ToInt32(reader.GetOrdinal("RestaurantID"))),
                       reader.GetInt32(Convert.ToInt32(reader.GetOrdinal("OrganizationID"))),
                       restName,
                       reader.GetDateTimeOffset(reader.GetOrdinal("DateFounded")).ToString(),
                       reader.GetBoolean(reader.GetOrdinal("IsOperational")));
                }
            }
        }

        public Restaurant GetRestaurantByID(int restID)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("Restaurants.GetRestaurantByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    //Name of Primary Key is what we pass in, everything else
                    //we get from the SQL
                    command.Parameters.AddWithValue("RestaurantID", restID);

                    connection.Open();

                    var reader = command.ExecuteReader();

                    if (!reader.Read())
                        return null;

                    return new Restaurant(restID,
                       reader.GetInt32(Convert.ToInt32(reader.GetOrdinal("OrganizationID"))),
                       reader.GetString(reader.GetOrdinal("RestaurantName")),
                       reader.GetDateTimeOffset(reader.GetOrdinal("DateFounded")).ToString(),
                       reader.GetBoolean(reader.GetOrdinal("IsOperational")));
                }
            }
        }

        public void UpdateRestaurant(int restID, int orgID, string restName, bool isOp)
        {
            using (var transaction = new TransactionScope())
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var command = new SqlCommand("Restaurants.UpdateRestaurant", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("RestaurantID", restID);
                        command.Parameters.AddWithValue("OrganizationID", orgID);
                        command.Parameters.AddWithValue("RestaurantName", restName);
                        command.Parameters.AddWithValue("IsOperational", isOp);

                        connection.Open();
                        command.ExecuteNonQuery();

                        transaction.Complete();
                    }
                }
            }
        }

        public double CalcRestExp(int restID)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("Restaurants.CalcRestExp", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("RestaurantID", restID);

                    connection.Open();

                    var reader = command.ExecuteReader();

                    if (!reader.Read())
                        return -1;
                    try
                    {
                        return reader.GetDouble(reader.GetOrdinal("RestExpense"));
                    }
                    catch(Exception e)
                    {
                        return 0;
                    }
                }
            }
        }

        public IReadOnlyList<Tuple<string, string, double, int>> GetEmployeeInfo(int restID)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("Restaurants.GetEmployeeInfo", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("RestaurantID", restID);

                    connection.Open();

                    var reader = command.ExecuteReader();

                    var rests = new List<Tuple<string, string, double, int>>();

                    while (reader.Read())
                    {
                        rests.Add(
                            new Tuple<string, string, double, int> (
                                reader.GetString(reader.GetOrdinal("Name")),
                                reader.GetString(reader.GetOrdinal("JobName")),
                                reader.GetDouble(reader.GetOrdinal("Salary")),
                                reader.GetInt32(reader.GetOrdinal("Seniority"))
                                ));
                    }

                    return rests;
                }
            }
        }

        public IReadOnlyList<Restaurant> RetrieveRestaurants()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("Restaurants.RetrieveRestaurants", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    var reader = command.ExecuteReader();

                    var rest = new List<Restaurant>();

                    while (reader.Read())
                    {
                       string dateValue = reader.GetDateTimeOffset(reader.GetOrdinal("DateFounded")).ToString();
                       rest.Add(new Restaurant(
                       reader.GetInt32(reader.GetOrdinal("RestaurantID")),
                       reader.GetInt32(reader.GetOrdinal("OrganizationID")),
                       reader.GetString(reader.GetOrdinal("RestaurantName")),
                       dateValue,
                       reader.GetBoolean(reader.GetOrdinal("IsOperational"))));
                    }

                    return rest;
                }
            }
        }
    }
}
