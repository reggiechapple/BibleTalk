using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace BibleTalk.Data.Identity
{
    public class ApplicationRole : IdentityRole
    {
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}