using System.Security.Claims;

namespace MyApp.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetUserName(this ClaimsIdentity claimsIdentity)
        {
            var claim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name);

            return (claim != null) ? claim.Value : string.Empty;
        }
    }
}
