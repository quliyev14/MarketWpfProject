using System.ComponentModel;

namespace MarketWpfProject.Moduls
{
    public class Product : INotifyPropertyChanged
    {
        public Product(string? name, decimal? price, int count, string? imagePath)
        {
            Quantity = 1;
            Name = name;
            Price = price;
            Count = count;
            ImagePath = imagePath;
        }

        public string? Name { get; set; } = default!;
        public decimal? Price { get; set; } = default!;
        public int Count { get; set; } = default!;

        private int _quantity; 
        public int Quantity
        {
            get => _quantity;
            set
            {
                if (_quantity != value)
                {
                    _quantity = value;
                    OnPropertyChanged(nameof(Quantity));
                }
            }
        }

        public string? ImagePath { get; set; } = default!;
        public override string ToString() => $"{Name} {Price} {Count} {ImagePath}\n";



        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}
