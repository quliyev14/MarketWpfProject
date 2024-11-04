using MarketWpfProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketWpfProject.Moduls
{
    public record Admin(string? name, string? surname, GmailService gmailService)
    {
        public override string ToString() => $"{name} {surname} {gmailService.Email} {gmailService.Password}\n";
    }

    // name and surname equlas init;
}
