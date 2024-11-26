namespace MarketWpfProject.Models
{
    public class User
    {
        public string? Name { get; init; } = default!;
        public string? Surname { get; init; } = default!;
        public DateTime? DateTime { get; init; } = default!;
        public string? CountryMobileCode { get; init; } = default!;
        public string? Mobile { get; init; } = default!;
        public GmailService GmailService { get; set; } = new GmailService(); 

        public User Clone() => new()
        {
            Name = Name,
            Surname = Surname,
            DateTime = DateTime,
            CountryMobileCode = CountryMobileCode,
            Mobile = Mobile,
            GmailService = GmailService
        };
    }
}