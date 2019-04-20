using System;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace Restaurants_Database
{
    class OrganizationRepo
    {
        const string connectionString = @"Server=(localdb)\MSSQLLocalDb;Database=nivlac12;Integrated Security=SSPI;";
        public void initOrgs()
        {

        }

        public Organization CreateOrganization(string OrganizationName)
        {
            using (var transaction = new TransactionScope())
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var command = new SqlCommand("Restaurants.Insert_Organization", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("OrganizationName", OrganizationName);

                        var idParam = command.Parameters.Add("OrganizationID", SqlDbType.Int);
                        var dateParam = command.Parameters.Add("DateFounded", SqlDbType.DateTimeOffset);
                        idParam.Direction = ParameterDirection.Output;
                        dateParam.Direction = ParameterDirection.Output;

                        connection.Open();

                        command.ExecuteNonQuery();

                        transaction.Complete();

                        return new Organization((int)idParam.Value, OrganizationName, (string)dateParam.Value);
                    }
                }
            }
        }

    }
}
