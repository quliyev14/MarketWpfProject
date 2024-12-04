using System.Text;

namespace MarketWpfProject.Helper
{
    public static class RandomCardGenerator
    {
        public static string RandomForCvcAndCardNumber(int lenght)
        {
            var sb = new StringBuilder();
            var random = new Random();

            for (int i = 0; i < lenght; i++)
                sb.Append(random.Next(0, 9));

            if (lenght == 16)
                return string.Join("-", Enumerable.Range(0, 4).Select(i => sb.ToString().Substring(i * 4, 4)));

            return sb.ToString();
        }
    }
}
