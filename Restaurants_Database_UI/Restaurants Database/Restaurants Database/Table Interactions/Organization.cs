using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants_Database
{
    class Organization
    {
        public int OrganizationID { get; }
        public string OrganizationName { get; }
        public string DateFounded { get; }

        public Organization(int orgID, string orgName, string date)
        {
            OrganizationID = orgID;
            OrganizationName = orgName;
            DateFounded = date;
        }
    }
}
