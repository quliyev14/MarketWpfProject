using System.ComponentModel;

namespace MarketWpfProject.Moduls
{
    public class Product : INotifyPropertyChanged
    {
        public string? Name { get; set; } = string.Empty;
        public decimal? Price { get; set; } = 0;
        public int Count { get; set; } = 0;
        public string? ImagePath { get; set; } = "C:\\Users\\elgun668icloud.com\\MarketWpfProject\\MarketDestkop\\Images\\image_default.png";

        private int? _quantity = 1;
        public int? Quantity { get => _quantity; set { { if (value < 1) _quantity = 1; else if (value > 30) _quantity = 30; else _quantity = value; OnPropertyChanged(nameof(Quantity)); } } }

        public Product Clone() => new() { Name = Name, Price = Price, Count = Count, ImagePath = ImagePath, Quantity = Quantity };

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
