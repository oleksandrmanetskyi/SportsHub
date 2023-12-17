namespace SportsHub.Services.DTO
{
    public class JwtTokenInfo
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int Time { get; set; }
    }
}
