namespace ServerApp.Models.DBO
{
    public class RefreshToken
    {
        public string? Token { get; set; }
        public DateTime? Expires { get; set; }

    }
}
