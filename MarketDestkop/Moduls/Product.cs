namespace MarketWpfProject.Moduls
{
    public class Product
    {
        public Product(string? name, decimal? price, int count)
        {
            Name = name;
            Price = price;
            Count = count;
        }

        public string? Name { get; set; } = default!;
        public decimal? Price { get; set; } = default!;
        public int Count { get; set; } = default!;

        public override string ToString() => $"{Name} {Price} {Count}\n";
    }
}
