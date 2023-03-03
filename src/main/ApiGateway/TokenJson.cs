namespace ApiGateway
{
    using Newtonsoft.Json;

    public class RealmAccess
    {
        public List<string>? Roles { get; set; }
    }


    public class TokenJson
    {
      
        public string? SessionState { get; set; }
        public string? Name { get; set; }
        public string? GivenName { get; set; }
        public string? FamilyName { get; set; }
        public string? PreferredUsername { get; set; }
        public string? Email { get; set; }
        public bool? EmailVerified { get; set; }
    
        [JsonProperty("allowed-origins")] public List<string>? AllowedOrigins { get; set; }
        public RealmAccess? RealmAccess { get; set; }
        public string? Scope { get; set; }
        public string? Sid { get; set; }
        public int ProjectId { get; set; }
        public int OrganizationId { get; set; }
        public string? ClientId { get; set; }
        public string? Username { get; set; }
        public bool Active { get; set; }
    }
}