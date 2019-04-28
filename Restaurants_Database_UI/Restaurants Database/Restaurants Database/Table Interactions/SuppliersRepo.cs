using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace Restaurants_Database
{
    class SuppliersRepo
    {
        const string connectionString = @"Server=mssql.cs.ksu.edu;Database=nivlac12;Integrated Security=SSPI;";

        public Supplier CreateSupplier(string SuppliersName)
        {
            using (var transaction = new TransactionScope())
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    //Set the string parameter to call the appropriate sql function
                    using (var command = new SqlCommand("Supplier.CreateSupplier", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        //Hardcode this attribute because it is not an output parameter, rather
                        //we have to pass it into the function ourselves
                        command.Parameters.AddWithValue("Name", SuppliersName);

                        //The next two parameters are output parameters, so instead of hardcoding
                        //these we initialize them and we'll get the values from the function
                        var idParam = command.Parameters.Add("SupplierID", SqlDbType.Int);                   
                        idParam.Direction = ParameterDirection.Output;
                        

                        //Executes the SQL itself, giving the output parameters we declared a value
                        connection.Open();
                        command.ExecuteNonQuery();

                        transaction.Complete();

                        //This line will return a unique object of the appropriate type, keeping in mind the parameters we stored
                        return new Supplier((int)idParam.Value, SuppliersName);
                    }
                }
            }
        }

        public Supplier GetSupplier(string suppName)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("Supplier.GetSupplier", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    //Name of Primary Key is what we pass in, everything else
                    //we get from the SQL
                    command.Parameters.AddWithValue("SupplierName", suppName);

                    connection.Open();

                    var reader = command.ExecuteReader();

                    if (!reader.Read())
                        return null;

                    return new Supplier(reader.GetInt32(Convert.ToInt32(reader.GetOrdinal("SupplierID"))),
                       suppName);
                }
            }
        }

        public Supplier GetSupplierByID(int suppID)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("Supplier.GetSupplierByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    //Name of Primary Key is what we pass in, everything else
                    //we get from the SQL
                    command.Parameters.AddWithValue("SupplierID", suppID);

                    connection.Open();

                    var reader = command.ExecuteReader();

                    if (!reader.Read())
                        return null;

                    return new Supplier(suppID,
                       reader.GetString(reader.GetOrdinal("Name")));
                }
            }
        }

        public void UpdateSupplier(int suppID, string name)
        {
            using (var transaction = new TransactionScope())
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var command = new SqlCommand("Supplier.UpdateSupplier", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("SupplierID", suppID);
                        command.Parameters.AddWithValue("Name", name);

                        connection.Open();
                        command.ExecuteNonQuery();
                        transaction.Complete();
                    }
                }
            }
        }

        public IReadOnlyList<Supplier> RetrieveSuppliers()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("Supplier.RetrieveSuppliers", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    var reader = command.ExecuteReader();

                    var supps = new List<Supplier>();

                    while (reader.Read())
                    {
                        supps.Add(new Supplier(
                           reader.GetInt32(reader.GetOrdinal("SupplierID")),
                           reader.GetString(reader.GetOrdinal("Name"))));
                    }

                    return supps;
                }
            }
        }
    }
}
