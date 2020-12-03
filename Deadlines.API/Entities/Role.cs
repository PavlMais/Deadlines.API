using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Deadlines.API.Entities
{
    public class Role : IdentityRole<long>
    {
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}