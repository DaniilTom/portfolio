using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.Domain
{
    public class User : IdentityUser
    {
        public const string AdminRoleName = "Admin";
        public const string DefaultAdminPassword = "Qwer";

        public const string UserRoleName = "User";
    }
}
