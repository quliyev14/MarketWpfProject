using MarketWpfProject.Exceptions;
using MarketWpfProject.Hashed;
using MarketWpfProject.Helper.GmailHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MarketWpfProject.Models
{
    //Email = GmailAndPasswordCheck.GPCheck(GP.Email, email);
    //string passwordcheckresult = GmailAndPasswordCheck.GPCheck(GP.Password, password);
    //Password = passwordcheckresult is not null ? DatasIsHashed.PasswordHash(passwordcheckresult) : null;
    //if (Password is null || Password is null)
    //    throw new ArgumentMailNullException("Gmail or Password is null");
    public class GmailService
    {
        public enum GP
        {
            Email,
            Password
        }

        public GmailService(string? email, string? password)
        {
            Email = email;
            Password = password;
        }


        public string? Email { get; set; }
        public string? Password { get; set; }

        public override string ToString() => $"{Email} {Password}\n";
    }
}