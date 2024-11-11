using System.Security.Cryptography;
using System.Text;

namespace MarketWpfProject.Hashed
{
    public static class DatasIsHashed
    {
        public static string WithSHA256PasswordHash(string? password)
        {
            var sb = new StringBuilder();
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password!));
                foreach (byte @byte in bytes)
                    sb.Append(@byte.ToString("x2"));
                return sb.ToString();
            }
        }
    }
}