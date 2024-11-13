namespace MarketWpfProject.Models
{
    public class GmailService
    {
        public enum GP
        {
            Email,
            Password
        }

        public string? Email { get; set; } = string.Empty;
        public string? Password { get; set; } = string.Empty;

        public GmailService Clone() => new() { Email = Email, Password = Password };
    }
}