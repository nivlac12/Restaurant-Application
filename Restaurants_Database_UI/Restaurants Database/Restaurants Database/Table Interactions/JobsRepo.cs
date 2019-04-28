using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace Restaurants_Database
{
    class JobsRepo
    {
        const string connectionString = @"Server=mssql.cs.ksu.edu;Database=nivlac12;Integrated Security=SSPI;";

        public Jobs CreateJobs(string JobName, double Salary)
        {
            using (var transaction = new TransactionScope())
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    //Set the string parameter to call the appropriate sql function
                    using (var command = new SqlCommand("Employees.CreateJob", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        //Hardcode this attribute because it is not an output parameter, rather
                        //we have to pass it into the function ourselves
                        command.Parameters.AddWithValue("JobName", JobName);
                        command.Parameters.AddWithValue("Salary", Salary);
                        //The next two parameters are output parameters, so instead of hardcoding
                        //these we initialize them and we'll get the values from the function
                        var idParam = command.Parameters.Add("JobTitleID", SqlDbType.Int);
                        idParam.Direction = ParameterDirection.Output;

                        //Executes the SQL itself, giving the output parameters we declared a value
                        connection.Open();
                        command.ExecuteNonQuery();

                        transaction.Complete();

                        //This line will return a unique object of the appropriate type, keeping in mind the parameters we stored
                        return new Jobs((int)idParam.Value, JobName,  Salary);
                    }
                }
            }
        }

        public Jobs GetJobs(string jobName)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("Employees.GetJob", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    //Name of Primary Key is what we pass in, everything else
                    //we get from the SQL
                    command.Parameters.AddWithValue("JobName", jobName);

                    connection.Open();

                    var reader = command.ExecuteReader();

                    if (!reader.Read())
                        return null;

                    return new Jobs(reader.GetInt32(Convert.ToInt32(reader.GetOrdinal("JobTitleID"))),
                       jobName,
                       reader.GetDouble(reader.GetOrdinal("Salary")));
                }
            }
        }

        public Jobs GetJobByID(int jobID)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("Employees.GetJobByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    //Name of Primary Key is what we pass in, everything else
                    //we get from the SQL
                    command.Parameters.AddWithValue("JobID", jobID);

                    connection.Open();

                    var reader = command.ExecuteReader();

                    if (!reader.Read())
                        return null;

                    return new Jobs(jobID,
                       reader.GetString(reader.GetOrdinal("JobName")),
                       reader.GetDouble(reader.GetOrdinal("Salary")));
                }
            }
        }

        public void UpdateJob(int jobID, string JobName, double salary)
        {
            using (var transaction = new TransactionScope())
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var command = new SqlCommand("Employees.UpdateJob", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("JobTitleID", jobID);
                        command.Parameters.AddWithValue("JobName", JobName);
                        command.Parameters.AddWithValue("Salary", salary);

                        connection.Open();
                        command.ExecuteNonQuery();

                        transaction.Complete();
                    }
                }
            }
        }

        public IReadOnlyList<Jobs> RetrieveJobs()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("Employees.RetrieveJobs", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    var reader = command.ExecuteReader();

                    var jobs = new List<Jobs>();

                    while (reader.Read())
                    {
                        jobs.Add(new Jobs(
                           reader.GetInt32(reader.GetOrdinal("JobTitleID")),
                           reader.GetString(reader.GetOrdinal("JobName")),
                           reader.GetDouble(reader.GetOrdinal("Salary"))));
                    }

                    return jobs;
                }
            }
        }
    }
}
