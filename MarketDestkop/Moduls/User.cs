namespace MarketWpfProject.Models
{
    public class User
    {
        public User(string? name, string? surname, GmailService gmailService,
                    string? gender, DateOnly? dateOfBirth, string? mobile, string? countrymobilecode)
        {
            Name = name;
            Surname = surname;
            GmailService = gmailService;
            Gender = gender;
            DateOfBirth = dateOfBirth;
            Mobile = mobile;
            CountryMobileCode = countrymobilecode;
        }

        public string? Name { get; init; } = default!;
        public string? Surname { get; init; } = default!;
        public string? Gender { get; init; } = default!;
        public DateOnly? DateOfBirth { get; init; } = default!;
        public string? CountryMobileCode { get; init; } = default!;
        public string? Mobile { get; init; } = default!;
        public GmailService GmailService { get; set; } = default!;

        public override string ToString() => $"{Name} {Surname} {GmailService.Email} {GmailService.Password} {Gender} {DateOfBirth} {CountryMobileCode} {Mobile} \n";
    }
}
