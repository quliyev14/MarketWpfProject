using System.ComponentModel;

namespace MarketWpfProject.Moduls
{
    public class Product : INotifyPropertyChanged
    {
        public Product(string? name, decimal? price, int count, string? imagePath)
        {
            Name = name;
            Price = price;
            Count = count;
            ImagePath = imagePath;
        }

        public string? Name { get; set; } = default!;
        public decimal? Price { get; set; } = default!;
        public int Count { get; set; } = default!;
        public int Quantity { get; set; } = 1;
        public string? ImagePath { get; set; } = default!;
        public override string ToString() => $"{Name} {Price} {Count} {ImagePath}\n";

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}
