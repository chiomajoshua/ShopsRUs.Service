namespace ShopsRUs.Service.Core.Config
{
    public class JwtToken
    {
        public int Expiry { get; set; }
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}