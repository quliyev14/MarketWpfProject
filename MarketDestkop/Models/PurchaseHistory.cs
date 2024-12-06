using MarketWpfProject.Moduls;

namespace MarketWpfProject.Models
{
    public class PurchaseHistory
    {
        public DateTime? PurchaseDate { get; set; }
        public List<Product>? Products { get; set; }
    }
}
