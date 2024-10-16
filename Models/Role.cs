using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public ICollection<User> Users { get; set; } = new HashSet<User>();

        public Role(string roleName) {
            switch (roleName)
            {
                case "User":
                    this.RoleName = roleName;
                    this.RoleId = 0;
                    break;
                case "Admin":
                    this.RoleName = roleName;
                    this.RoleId = 1;
                    break;
                default:
                    this.RoleName = "User";
                    this.RoleId = 0;
                    break;
            }
        }

        static public Role User() {
            return new Role("User");
        }

        static public Role Admin() {
            return new Role("Admin");
        }
    }
}