using Microsoft.AspNetCore.Identity;

namespace GeoService.API.Auth.Identity
{
    public class UserIdentity : IdentityUser<int>, IEntity { }
}
