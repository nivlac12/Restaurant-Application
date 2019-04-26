using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace Restaurants_Database
{
    class EmployeeRepo
    {
        const string connectionString = @"Server=mssql.cs.ksu.edu;Database=nivlac12;Integrated Security=SSPI;";
        public void initEmp()
        {
            Employee[] emp = { };
            foreach (Employee e in emp)
            {
                CreateEmployee(e.RestaurantID, e.JobTitleID, e.EmployeeName, e.Seniority);
            }
        }

        public Employee CreateEmployee(int RestaurantID, int JobTitleID, string EmployeeName, int Seniority)
        {
            using (var transaction = new TransactionScope())
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    //Set the string parameter to call the appropriate sql function
                    using (var command = new SqlCommand("Employees.CreateEmployee", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        //Hardcode this attribute because it is not an output parameter, rather
                        //we have to pass it into the function ourselves
                        command.Parameters.AddWithValue("OrganizationID", RestaurantID);
                        command.Parameters.AddWithValue("RestaurantNamee", JobTitleID);
                        command.Parameters.AddWithValue("IsOperational", EmployeeName);
                        command.Parameters.AddWithValue("IsOperational", Seniority);

                        //The next two parameters are output parameters, so instead of hardcoding
                        //these we initialize them and we'll get the values from the function
                        var idParam = command.Parameters.Add("PersonID", SqlDbType.Int);
                        idParam.Direction = ParameterDirection.Output;

                        //Executes the SQL itself, giving the output parameters we declared a value
                        connection.Open();
                        command.ExecuteNonQuery();

                        transaction.Complete();

                        //This line will return a unique object of the appropriate type, keeping in mind the parameters we stored
                        return new Employee((int)idParam.Value, RestaurantID, JobTitleID, EmployeeName, Seniority);
                    }
                }
            }
        }

        public Employee GetEmployee(string empName)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("Employees.GetEmployee", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    //Name of Primary Key is what we pass in, everything else
                    //we get from the SQL
                    command.Parameters.AddWithValue("EmployeeName", empName);

                    connection.Open();

                    var reader = command.ExecuteReader();

                    if (!reader.Read())
                        return null;

                    return new Employee(reader.GetInt32(Convert.ToInt32(reader.GetOrdinal("PersonID"))),
                       reader.GetInt32(reader.GetOrdinal("RestaurantID")),
                       reader.GetInt32(reader.GetOrdinal("JobTitleID")),
                       reader.GetString(reader.GetOrdinal("EmployeeName")),
                       reader.GetInt32(reader.GetOrdinal("Seniority")));
                }
            }
        }

        public IReadOnlyList<Employee> RetrieveEmployee()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("Employees.RetrieveEmployees", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    var reader = command.ExecuteReader();

                    var emp = new List<Employee>();

                    while (reader.Read())
                    {
                        emp.Add(new Employee(
                        reader.GetInt32(reader.GetOrdinal("PersonID")),
                        reader.GetInt32(reader.GetOrdinal("RestaurantID")),
                        reader.GetInt32(reader.GetOrdinal("JobTitleID")),
                        reader.GetString(reader.GetOrdinal("EmployeeName")),
                        reader.GetInt32(reader.GetOrdinal("Seniority"))));
                    }

                    return emp;
                }
            }
        }
    }
}
