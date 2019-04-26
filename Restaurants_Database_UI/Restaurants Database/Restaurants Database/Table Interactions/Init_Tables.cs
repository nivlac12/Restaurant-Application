using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Restaurants_Database
{
    class Init_Tables
    {
        public void initOrgs(OrganizationRepo or)
        {
            string[] orgs = { "Mcdonalds", "Dairy Queen", "Chili's", "Applebee's", "Buffalo Wild Wings" };
            foreach (string org in orgs)
            {
                or.CreateOrganization(org);
            }
        }

        public void initRests(RestaurantRepository rr)
        {
            Tuple<int, string, bool>[] rests =
                {
                new Tuple<int, string, bool> (1, "West Manhattan Mcdonald's", true),
                new Tuple<int, string, bool> (1, "East Manhattan Mcdonald's", true),
                new Tuple<int, string, bool> (2, "Lansing Dairy Queen", true),
                new Tuple<int, string, bool> (2, "Manhattan Dairy Queen", true),
                new Tuple<int, string, bool> (5, "Manhattan Buffalo Wild Wings", true),
                new Tuple<int, string, bool> (5, "Legends Outlet Buffalo Wild Wings", true),
                new Tuple<int, string, bool> (3, "Manhattan Chili's", true),
                new Tuple<int, string, bool> (4, "Manhattan Applebee's", true)
            };
            foreach (Tuple<int, string, bool> rest in rests)
            {
                rr.CreateRestaurant(rest.Item1, rest.Item2, rest.Item3);
            }
        }

        public void initJobs(JobsRepo jr)
        {
            Tuple<string, decimal>[] jobs = {
                new Tuple<string, decimal>("Manager", Convert.ToDecimal(15.00)),
                new Tuple<string, decimal>("Waiter", Convert.ToDecimal(12.00)),
                new Tuple<string, decimal>("DishWasher", Convert.ToDecimal(9.50)),
                new Tuple<string, decimal>("Cook", Convert.ToDecimal(13)),
                new Tuple<string, decimal>("Host", Convert.ToDecimal(10)),
            };
            foreach (Tuple<string, decimal> j in jobs)
            {
                jr.CreateJobs(j.Item1, j.Item2);
            }
        }

        public void initEmp(EmployeeRepo er, IReadOnlyList<Restaurant> rests, IReadOnlyList<Jobs> jobs)
        {
            // First names are taken from https://github.com/smashew/NameDatabases/blob/master/NamesDatabases/
            // Last names are taken from https://github.com/arineng/arincli/blob/master/lib/last-names.txt
            int firstLength = 0, lastLength = 0;

            using (StreamReader srFirst = new StreamReader("..\\..\\Names\\firstNames.txt"))
            {
                using (StreamReader srLast = new StreamReader("..\\..\\Names\\lastNames.txt"))
                {
                    while (!srFirst.EndOfStream)
                    {
                        string s = srFirst.ReadLine();
                        firstLength++; 
                    }
                    while (!srLast.EndOfStream)
                    {
                        string s = srLast.ReadLine();
                        lastLength++;
                    }
                }
            }
            string [] firstNames = new string[firstLength];
            string[] lastNames = new string[lastLength];
            string temp;

            using (StreamReader srFirst = new StreamReader("..\\..\\Names\\firstNames.txt"))
            {
                using (StreamReader srLast = new StreamReader("..\\..\\Names\\lastNames.txt"))
                {
                    for(int i = 0; i<firstLength; i++)
                    {
                        firstNames[i] = srFirst.ReadLine();
                    }
                    for (int i = 0; i<lastLength; i++)
                    {
                        temp = srLast.ReadLine();
                        lastNames[i] = temp[0] + temp.Substring(1).ToLower();
                    }
                }
            }

            string [] names = new string[20];
            Random rand = new Random();
            for (int i = 0; i<20; i++)
            {
                names[i] = firstNames[rand.Next(firstLength)] + " " + lastNames[rand.Next(lastLength)];
            }

            Restaurant r;
            Jobs j;
            foreach (string name in names)
            {
                r = rests[rand.Next(rests.Count)];
                j = jobs[rand.Next(jobs.Count)];
                er.CreateEmployee(r.RestaurantID, j.JobTitleID, name, rand.Next(20));
            }
        }
    }
}
