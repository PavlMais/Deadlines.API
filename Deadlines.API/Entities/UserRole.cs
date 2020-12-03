using Microsoft.AspNetCore.Identity;

namespace Deadlines.API.Entities
{
    public class UserRole : IdentityUserRole<long>
    {
        public virtual DbUser DbUser { get; set; }
        public virtual Role Role { get; set; }
    }
}