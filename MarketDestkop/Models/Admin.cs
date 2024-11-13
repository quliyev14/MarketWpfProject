using MarketWpfProject.Models;

namespace MarketWpfProject.Moduls
{
    public class Admin
    {
        public string? Name { get; set; } = string.Empty;
        public string? Surname { get; set; } = string.Empty;
        public GmailService? GmailService { get; set; } = new();
        public Admin Clone() => new() { Name = Name, Surname = Surname, GmailService = GmailService };
    }
}
