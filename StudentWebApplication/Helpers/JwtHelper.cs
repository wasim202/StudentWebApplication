using System.IdentityModel.Tokens.Jwt;

namespace StudentWebApplication.Helpers
{
    public class JwtHelper
    {
        public static bool IsAdmin(string token)
        {
            if (string.IsNullOrEmpty(token)) return false;

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var isAdminClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "IsAdmin");
            return isAdminClaim != null && bool.TryParse(isAdminClaim.Value, out bool isAdmin) && isAdmin;
        }
    }

}

