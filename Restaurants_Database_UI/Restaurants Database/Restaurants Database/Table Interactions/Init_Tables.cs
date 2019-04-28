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
            //                     1              2            3           4                   5               6                7               8
            string[] orgs = { "McDonald's", "Dairy Queen", "Chili's", "Applebee's", "Buffalo Wild Wings", "Taco Bell", "Tasty China House", "Domino's" };
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
                new Tuple<int, string, bool> (3, "Manhattan Chili's", true),
                new Tuple<int, string, bool> (4, "Manhattan Applebee's", true),
                new Tuple<int, string, bool> (5, "Manhattan Buffalo Wild Wings", true),
                new Tuple<int, string, bool> (5, "Legends Outlet Buffalo Wild Wings", true),                           
                new Tuple<int, string, bool> (6, "Manhattan Taco Bell", true),
                new Tuple<int, string, bool> (6, "Kansas City Taco Bell", true),
                new Tuple<int, string, bool> (7, "Manhattan Tasty China House", true),
                new Tuple<int, string, bool> (8, "Manhattan Dominos", true),
                new Tuple<int, string, bool> (8, "Lawrence Dominos", true),

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
                new Tuple<string, double>("Delivery Driver", 11.00)
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
            //                       1                 2                      3                       4                        5                         6               7         
            string[] supps = { "Kansas Beef", "General Chicanery", "Vegetables Incorporated", "Pacific Seafood", "All Things Fried International", "Ardent Mills", "DG Foods Inc."};

            foreach (string supp in supps)
            {
                sr.CreateSupplier(supp);
            }
        }

        public void initFoods(FoodRepo fr)
        {
            Tuple<int, string, double, double>[] foods =
            {
                new Tuple<int, string, double, double>(1, "Quarter Pound Angus Burger Patties", (double)(.5), (double)(.75)), //1
                new Tuple<int, string, double, double>(1, "Dark Roast Coffee - 20 lb", (double)(10), (double)(30)),           //2
                new Tuple<int, string, double, double>(1, "24oz. Polish Dill Pickles", (double)(2), (double)(4.30)),          //3
                new Tuple<int, string, double, double>(1, "Sixth Pound Angus Burger Patties", (.4), (.7)),                    //4
                new Tuple<int, string, double, double>(1, "8oz. Ribeye Steak", (4), (6)),                                     //5
                new Tuple<int, string, double, double>(1, "12oz. New York Strip", (5), (7)),                                  //6
                new Tuple<int, string, double, double>(1, "6oz. Filet Mignon", (7), (10)),                                    //7          
                new Tuple<int, string, double, double>(2, "Seasoned Chicken Strips", (.1), (.14)),                            //8
                new Tuple<int, string, double, double>(2, "Grilled Chicken Breast", (.7), (.9)),                              //9
                new Tuple<int, string, double, double>(2, "Crispy Chicken Patty", (.4), (.7)),                                //10
                new Tuple<int, string, double, double>(3, "1oz. Romaine Lettuce", (.05), (.06)),                              //11
                new Tuple<int, string, double, double>(3, "Tomato", (.25), (.35)),                                            //12
                new Tuple<int, string, double, double>(3, "Yellow Onion", (.3), (.4)),                                        //13
                new Tuple<int, string, double, double>(4, "8oz. Lobster", (12), (16)),                                        //14
                new Tuple<int, string, double, double>(4, "Fresh Caught Salmon Fillet", (7), (9)),                            //15
                new Tuple<int, string, double, double>(4, "Alaskan King crab", (10), (13)),                                   //16
                new Tuple<int, string, double, double>(5, "6 Onion Rings", (.5), (.7)),                                       //17
                new Tuple<int, string, double, double>(5, "Beer Battered Manchester Cod", (1), (1.25)),                       //18
                new Tuple<int, string, double, double>(5, "6 Mozzarrella Sticks", (1), (1.25)),                               //19
                new Tuple<int, string, double, double>(5, "Serving of French Fries", (.3), (.4)),                             //20
                new Tuple<int, string, double, double>(6, "Pizza Dough", (20), (60)),                                         //21
                new Tuple<int, string, double, double>(6, "Marinara Sauce", (3), (7)),                                        //22
                new Tuple<int, string, double, double>(6, "Cheese", (2.50), (4.50)),                                          //23
                new Tuple<int, string, double, double>(6, "Taco Shells", (50), (120)),                                        //24
                new Tuple<int, string, double, double>(7, "Lo Mein Noodles", (20), (42)),                                     //25
                new Tuple<int, string, double, double>(7, "Rice", (15), (28)),                                                //26
                new Tuple<int, string, double, double>(7, "Soy Sauce", (5), (8)),                                             //27
                new Tuple<int, string, double, double>(3, "Broccoli", (17), (27)),                                            //28

            };

            foreach(Tuple<int, string, double, double> food in foods)
            {
                fr.CreateFood(food.Item1, food.Item2, food.Item3, food.Item4);
            }
        }

        public void initStockItems(StockItemsRepo sir)
        {
            Tuple<int, int, int>[] stockitems =
            {
                new Tuple<int, int, int> (1, 1, 800),
                new Tuple<int, int, int> (2, 1, 30),
                new Tuple<int, int, int> (3, 1, 16),
                new Tuple<int, int, int> (4, 1, 1200),
                new Tuple<int, int, int> (5, 1, 400),
                new Tuple<int, int, int> (6, 1, 200),
                new Tuple<int, int, int> (7, 1, 650),
                new Tuple<int, int, int> (8, 2, 7),
                new Tuple<int, int, int> (9, 2, 8),
                new Tuple<int, int, int>(10, 2, 300),
                new Tuple<int, int, int>(11, 3, 250),
                new Tuple<int, int, int>(12, 3, 10),
                new Tuple<int, int, int>(13, 3, 6),
                new Tuple<int, int, int>(14, 4, 13),
                new Tuple<int, int, int>(15, 4, 6),
                new Tuple<int, int, int>(16, 4, 10),
                new Tuple<int, int, int>(17, 5, 100),
                new Tuple<int, int, int>(18, 5, 17),
                new Tuple<int, int, int>(19, 5, 80),
                new Tuple<int, int, int>(20, 5, 120),
                new Tuple<int, int, int> (5, 6, 200),
                new Tuple<int, int, int> (7, 6, 200),
                new Tuple<int, int, int> (9, 6, 300),
                new Tuple<int, int, int>(11, 6, 200),
                new Tuple<int, int, int>(13, 6, 8),
                new Tuple<int, int, int>(23, 6, 150),
                new Tuple<int, int, int>(24, 6, 30),
                new Tuple<int, int, int>(5, 7, 70),
                new Tuple<int, int, int>(7, 7, 50),
                new Tuple<int, int, int>(9, 7, 400),
                new Tuple<int, int, int>(13, 7, 12),
                new Tuple<int, int, int>(25, 7, 20),
                new Tuple<int, int, int>(26, 7, 12),
                new Tuple<int, int, int>(27, 7, 30),
                new Tuple<int, int, int>(28, 7, 8),
                new Tuple<int, int, int>(21, 8, 120),
                new Tuple<int, int, int>(22, 8, 120),
                new Tuple<int, int, int>(23, 8, 120),
            };
            foreach (Tuple<int,int, int> stockitem in stockitems)
            {
                sir.CreateStockItems(stockitem.Item1, stockitem.Item2, stockitem.Item3);
            }
        }
    }
}
