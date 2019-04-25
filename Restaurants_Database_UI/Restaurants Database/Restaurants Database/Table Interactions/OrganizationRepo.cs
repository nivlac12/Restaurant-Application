using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace Restaurants_Database
{
    class OrganizationRepo
    {
        const string connectionString = @"Server=mssql.cs.ksu.edu;Database=nivlac12;Integrated Security=SSPI;";
        public void initOrgs()
        {
            string[] orgs = { "Berkshire Hathaway", "McDonald's Corp", "Restaurant Brands International" };
            foreach(string org in orgs)
            {
                CreateOrganization(org);
            }
        }

        public Organization CreateOrganization(string OrganizationName)
        {
            using (var transaction = new TransactionScope())
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    //Set the string parameter to call the appropriate sql function
                    using (var command = new SqlCommand("Restaurants.CreateOrganization", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        //Hardcode this attribute because it is not an output parameter, rather
                        //we have to pass it into the function ourselves
                        command.Parameters.AddWithValue("OrganizationName", OrganizationName);

                        //The next two parameters are output parameters, so instead of hardcoding
                        //these we initialize them and we'll get the values from the function
                        var idParam = command.Parameters.Add("OrganizationID", SqlDbType.Int);
                        var dateParam = command.Parameters.Add("DateFounded", SqlDbType.DateTimeOffset);
                        idParam.Direction = ParameterDirection.Output;
                        dateParam.Direction = ParameterDirection.Output;

                        //Executes the SQL itself, giving the output parameters we declared a value
                        connection.Open();
                        command.ExecuteNonQuery();

                        transaction.Complete();
                        string dateValue = dateParam.Value.ToString();

                        //This line will return a unique object of the appropriate type, keeping in mind the parameters we stored
                        return new Organization((int)idParam.Value, OrganizationName, dateValue);
                    }
                }
            }
        }

        public Organization GetOrganization(string orgName)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("Restaurants.GetOrganization", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    //Name of Primary Key is what we pass in, everything else
                    //we get from the SQL
                    command.Parameters.AddWithValue("OrganizationName", orgName);

                    connection.Open();

                    var reader = command.ExecuteReader();

                    if (!reader.Read())
                        return null;

                    return new Organization(reader.GetInt32(Convert.ToInt32(reader.GetOrdinal("OrganizationID"))),
                                            orgName,
                                            reader.GetDateTimeOffset(reader.GetOrdinal("DateFounded")).ToString());
                }
            }
        }

        public Organization GetOrganizationByID(int orgID)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("Restaurants.GetOrganizationByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    //Name of Primary Key is what we pass in, everything else
                    //we get from the SQL
                    command.Parameters.AddWithValue("OrganizationID", orgID);

                    connection.Open();

                    var reader = command.ExecuteReader();

                    if (!reader.Read())
                        return null;

                    return new Organization(orgID,
                                            reader.GetString(reader.GetOrdinal("OrganizationName")),
                                            reader.GetDateTimeOffset(reader.GetOrdinal("DateFounded")).ToString());
                }
            }
        }

        public IReadOnlyList<Organization> RetrieveOrganizations()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("Restaurants.RetrieveOrganizations", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    var reader = command.ExecuteReader();

                    var orgs = new List<Organization>();

                    while (reader.Read())
                    {
                        string dateValue = reader.GetDateTimeOffset(reader.GetOrdinal("DateFounded")).ToString();
                        orgs.Add(new Organization(
                           reader.GetInt32(reader.GetOrdinal("OrganizationID")),
                           reader.GetString(reader.GetOrdinal("OrganizationName")),
                           dateValue));
                    }

                    return orgs;
                }
            }
        }
    }
}
