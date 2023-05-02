namespace tadoba_api.Models
{
    public class OrderDetailsModel
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public long OrderId { get; set; }
        public long Price { get; set; }
        public long Discount { get; set; }
        public long Quantity { get; set; }
        public string ProductName { get; set; }
        public string ShortDescription { get; set; }
        public int Weight { set; get; }
        public string ImagePath { get; set; }
    }
}
