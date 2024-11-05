using System.Text;
using static MarketWpfProject.Models.GmailService;

namespace MarketWpfProject.Helper.GmailHelper
{
    public static class GmailAndPasswordCheck
    {
        public static string GPCheck(GP? gp, string? text)
        {
            var result = gp switch
            {
                GP.Email => GBResult(text),
                GP.Password => GBResult(text),
                _ => throw new ArgumentException(nameof(gp))
            };

            if (result is null)
                return null!;

            var sb = new StringBuilder();
            return sb.Append(result).ToString();
        }

        private static string GBResult(string? text)
        {
            int digitCount = 0;
            int letterCount = 0;

            for (int i = 0; i < text!.Length; i++)
            {
                if (char.IsDigit(text[i]))
                    ++digitCount;
                else if (char.IsLetter(text[i]))
                    ++letterCount;
            }
            //UPDATE
            if (letterCount >= 2 && digitCount >= 2)
                return text;
            return null!;
        }
    }
}