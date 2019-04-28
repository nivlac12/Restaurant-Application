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
            Tuple<string, double>[] jobs = 
            {
                new Tuple<string, double>("Manager", 15.00),
                new Tuple<string, double>("Waiter", 12.00),
                new Tuple<string, double>("DishWasher", 9.50),
                new Tuple<string, double>("Cook", 13.00),
                new Tuple<string, double>("Host", 10.00),
            };
            foreach (Tuple<string, double> j in jobs)
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

            string [] names = new string[100];
            Random rand = new Random();
            for (int i = 0; i<100; i++)
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

        public void initSupps(SuppliersRepo sr)
        {
            string[] supps = { "Kansas Beef", "General Chicanery", "Vegetables Incorporated", "Pacific Seafood", "All Things Fried International"};
            foreach (string supp in supps)
            {
                sr.CreateSupplier(supp);
            }
        }

        public void initFoods(FoodRepo fr)
        {
            Tuple<int, string, double, double>[] foods =
            {
                new Tuple<int, string, double, double>(1, "Quarter Pound Angus Burger Patties", (double)(.5), (double)(.75)),
                new Tuple<int, string, double, double>(1, "Sixth Pound Angus Burger Patties", (.4), (.7)),
                new Tuple<int, string, double, double>(1, "8oz. Ribeye Steak", (4), (6)),
                new Tuple<int, string, double, double>(1, "12oz. New York Strip", (5), (7)),
                new Tuple<int, string, double, double>(1, "6oz. Filet Mignon", (7), (10)),
                new Tuple<int, string, double, double>(2, "Seasoned Chicken Strips", (.1), (.14)),
                new Tuple<int, string, double, double>(2, "Grilled Chicken Breast", (.7), (.9)),
                new Tuple<int, string, double, double>(2, "Crispy Chicken Patty", (.4), (.7)),
                new Tuple<int, string, double, double>(3, "1oz. Romaine Lettuce", (.05), (.06)),
                new Tuple<int, string, double, double>(3, "Tomato", (.25), (.35)),
                new Tuple<int, string, double, double>(3, "Yellow Onion", (.3), (.4)),
                new Tuple<int, string, double, double>(4, "8oz. Lobster", (12), (16)),
                new Tuple<int, string, double, double>(4, "Fresh Caught Salmon Fillet", (7), (9)),
                new Tuple<int, string, double, double>(4, "Alaskan King crab", (10), (13)),
                new Tuple<int, string, double, double>(5, "6 Onion Rings", (.5), (.7)),
                new Tuple<int, string, double, double>(5, "Beer Battered Manchester Cod", (1), (1.25)),
                new Tuple<int, string, double, double>(5, "6 Mozzarrella Sticks", (1), (1.25)),
                new Tuple<int, string, double, double>(5, "Serving of French Fries", (.3), (.4))
            };

            foreach(Tuple<int, string, double, double> food in foods)
            {
                fr.CreateFood(food.Item1, food.Item2, food.Item3, food.Item4);
            }
        }
    }
}
