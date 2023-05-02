namespace tadoba_api.Models
{
    public class PlaceOrderModel
    {
        public long UserId { get; set; }
        public string TransactionId { get; set; }
        public List<long> CartIds { set; get; }
        public long AddressId { get; set; }
        public double Discount { get; set; }
        public double Total { get; set; }
    }
}
