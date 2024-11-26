namespace MarketWpfProject.Models
{
    public class GmailService
    {
        public enum GP
        {
            Email,
            Password
        }

        public string? Email { get; set; } = default!;
        public string? Password { get; set; } = default!;

        public GmailService Clone() => new() { Email = Email, Password = Password };
    }
}