using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class clsUserEntity
    {
        [Flags]
        public enum enPermissions
        {
            None = 0,
            AdminsManagment = 1,
            UsersManagment = 2,
            ClientsManagment = 4,
            PersonalAccountManagment = 8,
            All = -1
        }
        public int UserID { set; get; }
        public string UserName { set; get; }
        public string PasswordHash { set; get; }
        public enPermissions Permissions { set; get; }
        public bool IsActive { set; get; }
    }
}
