using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Deadlines.API.Entities
{
    public class DbUser : IdentityUser<long>
    {
        public virtual ICollection<Deadline> Deadlines { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}