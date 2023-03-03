using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json.Linq;

namespace JwtAuthenticationManager
{
    public class ClaimsTransformer : IClaimsTransformation
    {
        private readonly string _clientName;

        public ClaimsTransformer(string clientName)
        {
            _clientName = clientName;
        }

        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var claimsIdentity = (ClaimsIdentity)principal.Identity!;

            if (claimsIdentity.IsAuthenticated && claimsIdentity.HasClaim((claim) => claim.Type == "resource_access"))
            {
                var userRole = claimsIdentity.FindFirst((claim) => claim.Type == "resource_access");

                var content = JObject.Parse(userRole.Value);

                foreach (var role in content[_clientName]["roles"])
                {
                    claimsIdentity.AddClaim(new Claim("Role", role.ToString()));
                }
            }
            return Task.FromResult(principal);
        }
    }
}