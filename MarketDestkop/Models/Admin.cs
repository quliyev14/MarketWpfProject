using MarketWpfProject.Models;

namespace MarketWpfProject.Moduls
{
    public record Admin(string? name, string? surname, GmailService gmailService)
    {
        public override string ToString() => $"{name} {surname} {gmailService.Email} {gmailService.Password}\n";
    }

    // name and surname equlas init;
}
