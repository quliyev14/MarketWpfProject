
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            this.countrymobilecode = countrymobilecode;
        }

        public string? Name { get; init; } = default!;
        public string? Surname { get; init; } = default!;
        public string? Gender { get; init; } = default!;
        public DateOnly? DateOfBirth { get; init; } = default!;
        public string? countrymobilecode { get; init; } = default!;
        public string? Mobile { get; init; } = default!;
        public GmailService GmailService { get; init; } = default!;

        public override string ToString() => $"{Name} {Surname} {GmailService} {Gender} {DateOfBirth} {countrymobilecode} {Mobile} \n";
    }
}
