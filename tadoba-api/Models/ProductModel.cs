namespace tadoba_api.Models
{
    public class ProductModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public double Price { set; get; }
        public double Discount { set; get; }
        public int AvailableQuantity { set; get; }
        public int Weight { set; get; }
        public string ImagePath { get; set; }
    }
}
