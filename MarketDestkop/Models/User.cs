namespace MarketWpfProject.Models
{
    public class User
    {
        public string? Name { get; init; } = default!;
        public string? Surname { get; init; } = default!;
        public DateTime? DateTime { get; init; } = default!;
        public string? CountryMobileCode { get; init; } = default!;
        public string? Mobile { get; init; } = default!;
        public string? ImagePath { get; set; } = "https://www.shutterstock.com/image-vector/default-avatar-profile-icon-vector-260nw-1706867365.jpg";
        public GmailService GmailService { get; set; } = new GmailService();

        public User Clone() => new()
        {
            Name = Name,
            Surname = Surname,
            DateTime = DateTime,
            CountryMobileCode = CountryMobileCode,
            Mobile = Mobile,
            ImagePath = ImagePath,
            GmailService = GmailService
        };
    }
}