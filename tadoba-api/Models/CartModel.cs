namespace tadoba_api.Models
{
    public class CartModel
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public long Price { get; set; }
        public long Discount { get; set; }
        public long Quantity { get; set; }
        public string ProductName { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public int Weight { set; get; }
        public string ImagePath { get; set; }
    }
}
