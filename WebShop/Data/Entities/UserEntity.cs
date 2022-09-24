using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace WebShop.Data.Entities
{
    public class UserEntity: IdentityUser<int>
    {
        public virtual ICollection<UserRoleEntity> UserRoles { get; set; }
    }
}
