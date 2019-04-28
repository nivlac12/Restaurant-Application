using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants_Database
{
    class Jobs
    {
        public int JobTitleID { get; }
        public string JobName { get; }
        public double Salary { get; }

        public Jobs(int jTID, string jobName, double sal)
        {
            JobTitleID = jTID;
            JobName = jobName;
            Salary = sal;
        }
    }
}
