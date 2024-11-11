using MarketWpfProject.Exceptions;
using MarketWpfProject.Hashed;
using MarketWpfProject.Helper.GmailHelper;

namespace MarketWpfProject.Models
{


    public class GmailService
    {
        public enum GP
        {
            Email,
            Password
        }

        public GmailService(string? email, string? password)
        {
            Email = GmailAndPasswordCheck.GPCheck(GP.Email, email);
            Password = GmailAndPasswordCheck.GPCheck(GP.Password, password);
            if (Email is null || Password is null)
                throw new ArgumentMailNullException("Gmail or Password is null");
        }

        public string? Email { get; set; }
        public string? Password { get; set; }

        public override string ToString() => $"{Email} {Password}\n";
    }
}