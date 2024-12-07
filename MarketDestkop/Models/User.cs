namespace MarketWpfProject.Models
{
    public class User
    {
        public string? Name { get; set; } = default!;
        public string? Surname { get; set; } = default!;
        public  GmailService GmailService { get; set; } = new GmailService();
        public DateTime? DateTime { get; set; } = new();
        public string? CountryMobileCode { get; set; } = default!;
        public string? Mobile { get; set; } = default!;
        public decimal? Balance { get; set; } = default!;
        public string? ImagePath { get; set; } = "https://www.shutterstock.com/image-vector/default-avatar-profile-icon-vector-260nw-1706867365.jpg";

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