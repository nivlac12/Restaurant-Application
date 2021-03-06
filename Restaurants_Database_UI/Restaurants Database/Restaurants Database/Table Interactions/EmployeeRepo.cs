﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using System.IO;

namespace Restaurants_Database
{
    class EmployeeRepo
    {
        const string connectionString = @"Server=mssql.cs.ksu.edu;Database=nivlac12;Integrated Security=SSPI;";


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
                        command.Parameters.AddWithValue("RestaurantID", RestaurantID);
                        command.Parameters.AddWithValue("JobTitleID", JobTitleID);
                        command.Parameters.AddWithValue("Name", EmployeeName);
                        command.Parameters.AddWithValue("Seniority", Seniority);

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
                    command.Parameters.AddWithValue("PersonName", empName);

                    connection.Open();

                    var reader = command.ExecuteReader();

                    if (!reader.Read())
                        return null;

                    return new Employee(reader.GetInt32(Convert.ToInt32(reader.GetOrdinal("PersonID"))),
                       reader.GetInt32(reader.GetOrdinal("RestaurantID")),
                       reader.GetInt32(reader.GetOrdinal("JobTitleID")),
                       empName,
                       reader.GetInt32(reader.GetOrdinal("Seniority")));
                }
            }
        }

         public Employee GetEmployeeByID(int empID)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("Employees.GetEmployeeByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    //Name of Primary Key is what we pass in, everything else
                    //we get from the SQL
                    command.Parameters.AddWithValue("PersonID", empID);

                    connection.Open();

                    var reader = command.ExecuteReader();

                    if (!reader.Read())
                        return null;

                    return new Employee(empID,
                       reader.GetInt32(reader.GetOrdinal("RestaurantID")),
                       reader.GetInt32(reader.GetOrdinal("JobTitleID")),
                       reader.GetString(reader.GetOrdinal("Name")),
                       reader.GetInt32(reader.GetOrdinal("Seniority")));
                }
            }
        }

        public void UpdateEmployee(int empID, int restID, int jobID, string empName, int seniority)
        {
            using (var transaction = new TransactionScope())
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var command = new SqlCommand("Employees.UpdateEmployee", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("PersonID", empID);
                        command.Parameters.AddWithValue("RestaurantID", restID);
                        command.Parameters.AddWithValue("JobTitleID", jobID);
                        command.Parameters.AddWithValue("PersonName", empName);
                        command.Parameters.AddWithValue("Seniority", seniority);

                        connection.Open();
                        command.ExecuteNonQuery();

                        transaction.Complete();
                    }
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
                        reader.GetString(reader.GetOrdinal("Name")),
                        reader.GetInt32(reader.GetOrdinal("Seniority"))));
                    }

                    return emp;
                }
            }
        }
    }
}
