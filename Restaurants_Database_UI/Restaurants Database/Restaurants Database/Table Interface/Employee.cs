using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants_Database
{
    class Employee
    {
        public int PersonID { get; }
        public int ResturantID { get; }
        public int JobTitleID { get; }
        public string EmployeeName { get; }
        public string Seniority { get; }

        public Employee(int personID, int restID, int jTID, string empName, string senior)
        {
            PersonID = personID;
            ResturantID = restID;
            JobTitleID = jTID;
            EmployeeName = empName;
            Seniority = senior;
        }
    }
}
