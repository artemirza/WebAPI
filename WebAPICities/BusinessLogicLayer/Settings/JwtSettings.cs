namespace BusinessLogicLayer.Settings
{
    public class JwtSettings
    {
        public string Issuer { get; set; }  
        public decimal ExpiryMinutes { get; set;}
        public string Key { get; set; }
    }
}
