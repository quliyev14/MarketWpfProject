namespace MarketWpfProject.Moduls
{
    public class Product
    {
        public string? Name { get; set; } = string.Empty;
        public decimal? Price { get; set; } = 0;
        public int Count { get; set; } = 0;
        public string? ImagePath { get; set; } = "C:\\Users\\Quliy_cy96\\Source\\Repos\\MarketWpfProject\\MarketDestkop\\Images\\image_default.png";
        public int? Quantity { get; set; } = 0;

        public Product Clone() => new() { Name = Name, Price = Price, Count = Count, ImagePath = ImagePath, Quantity = Quantity };
    }
}
