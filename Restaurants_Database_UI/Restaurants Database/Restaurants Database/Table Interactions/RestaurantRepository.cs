﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace Restaurants_Database
{
    class RestaurantRepository
    {
        const string connectionString = @"Server=(localdb)\MSSQLLocalDb;Database=nivlac12;Integrated Security=SSPI;";
        public void initRests()
        {
            Restaurant[] rest = { };
            foreach (Restaurant re in rest)
            {
                CreateRestaurant(re.OrganizationID,re.RestaurantName,re.DateFounded,re.IsOperational);
            }
        }

        public Restaurant CreateRestaurant(int OrganizationID, string RestaurantName, string DateFounded, bool IsOperational)
        {
            using (var transaction = new TransactionScope())
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    //Set the string parameter to call the appropriate sql function
                    using (var command = new SqlCommand("Restaurants.Insert_Restaurant", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        //Hardcode this attribute because it is not an output parameter, rather
                        //we have to pass it into the function ourselves
                        command.Parameters.AddWithValue("OrganizationID", OrganizationID);
                        command.Parameters.AddWithValue("RestaurantNamee", RestaurantName);
                        command.Parameters.AddWithValue("IsOperational", DateFounded);
                        command.Parameters.AddWithValue("IsOperational", IsOperational);

                        //The next two parameters are output parameters, so instead of hardcoding
                        //these we initialize them and we'll get the values from the function
                        var idParam = command.Parameters.Add("RestaurantID", SqlDbType.Int);
                        idParam.Direction = ParameterDirection.Output;

                        //Executes the SQL itself, giving the output parameters we declared a value
                        connection.Open();
                        command.ExecuteNonQuery();

                        transaction.Complete();

                        //This line will return a unique object of the appropriate type, keeping in mind the parameters we stored
                        return new Restaurant((int)idParam.Value, OrganizationID, (string)RestaurantName, DateFounded, (bool)IsOperational);
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
                       reader.GetInt32(reader.GetOrdinal("OrganizationID")),
                       reader.GetString(reader.GetOrdinal("RestaurantName")),
                       reader.GetString(reader.GetOrdinal("DateFounded")),
                       reader.GetBoolean(reader.GetOrdinal("IsOperational")));
                }
            }
        }

        public IReadOnlyList<Restaurant> RetrieveRestaurant()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("Restaurants.RetrieveRestaurant", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    var reader = command.ExecuteReader();

                    var rest = new List<Restaurant>();

                    while (reader.Read())
                    {
                       rest.Add(new Restaurant(
                       reader.GetInt32(reader.GetOrdinal("InventoryID")),
                       reader.GetInt32(reader.GetOrdinal("OrganizationID")),
                       reader.GetString(reader.GetOrdinal("RestaurantName")),
                       reader.GetString(reader.GetOrdinal("DateFounded")),
                       reader.GetBoolean(reader.GetOrdinal("IsOperational"))));
                    }

                    return rest;
                }
            }
        }
    }
}
