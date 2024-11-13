namespace MarketWpfProject.Moduls
{
    public class Product
    {
        public string? Name { get; set; } = default!;
        public decimal? Price { get; set; } = default!;
        public int Count { get; set; } = default!;
        public string? ImagePath { get; set; } = default!;

        public Product Clone() => new() { Name = Name, Price = Price, Count = Count, ImagePath = ImagePath };
    }
}
