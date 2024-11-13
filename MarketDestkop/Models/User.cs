namespace MarketWpfProject.Models
{
    public class User
    {
        public string? Name { get; init; } = string.Empty;
        public string? Surname { get; init; } = string.Empty;
        public string? Gender { get; init; } = string.Empty;
        public DateTime? DateTime { get; init; } = new();
        public string? CountryMobileCode { get; init; } = string.Empty;
        public string? Mobile { get; init; } = string.Empty;
        public GmailService GmailService { get; set; } = new();
        public User Clone() => new()
        {
            Name = Name,
            Surname = Surname,
            Gender = Gender,
            DateTime = DateTime,
            CountryMobileCode = CountryMobileCode,
            Mobile = Mobile,
            GmailService = GmailService
        };
    }
}