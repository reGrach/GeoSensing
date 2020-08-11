using Microsoft.AspNetCore.Identity;

namespace GeoService.API.Auth.Identity
{
    public class Role : IdentityRole<int>, IEntity
    {
        protected Role() { }

        public Role(string roleName) : base(roleName)
        {
            NormalizedName = roleName.ToUpperInvariant();
        }
    }
}
