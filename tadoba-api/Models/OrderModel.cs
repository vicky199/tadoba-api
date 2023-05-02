namespace tadoba_api.Models
{
    public class OrderModel
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string OrderNo { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public DateTime? ExpectedDeliveryDate { get; set; }
        public double Total { get; set; }
        public double Discount { get; set; }
        public long AddressId { get; set; }
        public string? TransactionId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public int Pincode { get; set; }
        public List<OrderDetailsModel> OrderDetails { set; get; }
    }
}
