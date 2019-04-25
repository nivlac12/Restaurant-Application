using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace Restaurants_Database
{
    class SuppliersRepo
    {
        const string connectionString = @"Server=(localdb)\MSSQLLocalDb;Database=nivlac12;Integrated Security=SSPI;";
        public void initSupps()
        {
            string[] supps = { "", "", "" };
            foreach (string supp in supps)
            {
                CreateSuppliers(supp);
            }
        }

        public Suppliers CreateSuppliers(string SuppliersName)
        {
            using (var transaction = new TransactionScope())
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    //Set the string parameter to call the appropriate sql function
                    using (var command = new SqlCommand("Suppliers.Insert_Suppliers", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        //Hardcode this attribute because it is not an output parameter, rather
                        //we have to pass it into the function ourselves
                        command.Parameters.AddWithValue("SuppliersName", SuppliersName);

                        //The next two parameters are output parameters, so instead of hardcoding
                        //these we initialize them and we'll get the values from the function
                        var idParam = command.Parameters.Add("SuppliersID", SqlDbType.Int);                   
                        idParam.Direction = ParameterDirection.Output;
                        

                        //Executes the SQL itself, giving the output parameters we declared a value
                        connection.Open();
                        command.ExecuteNonQuery();

                        transaction.Complete();

                        //This line will return a unique object of the appropriate type, keeping in mind the parameters we stored
                        return new Suppliers((int)idParam.Value, SuppliersName);
                    }
                }
            }
        }

        public Suppliers GetSuppliersn(string suppName)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("Restaurants.GetSuppliers", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    //Name of Primary Key is what we pass in, everything else
                    //we get from the SQL
                    command.Parameters.AddWithValue("SuppliersName", suppName);

                    connection.Open();

                    var reader = command.ExecuteReader();

                    if (!reader.Read())
                        return null;

                    return new Suppliers(reader.GetInt32(Convert.ToInt32(reader.GetOrdinal("SupplierID"))),
                       reader.GetString(reader.GetOrdinal("SuppliersName")));
                }
            }
        }

        public IReadOnlyList<Suppliers> RetrieveSuppliers()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("Restaurants.RetrieveSuppliers", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    var reader = command.ExecuteReader();

                    var supps = new List<Suppliers>();

                    while (reader.Read())
                    {
                        supps.Add(new Suppliers(
                           reader.GetInt32(reader.GetOrdinal("SuppliersID")),
                           reader.GetString(reader.GetOrdinal("SuppliersName"))));
                    }

                    return supps;
                }
            }
        }
    }
}
