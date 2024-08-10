using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Roles
    {
        private int rolesID;
        private string rolesName;

        public Roles(int rolesID, string rolesName)
        {
            this.rolesID = rolesID;
            this.rolesName = rolesName;
        }

        public int RolesID { get => rolesID; set => rolesID = value; }
        public string RolesName { get => rolesName; set => rolesName = value; }
    }
}
