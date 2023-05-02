using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tadoba_api.Entity
{
    [Table("orders")]
    public class Order
    {
        [Key]
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

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        [ForeignKey("AddressId")]
        public virtual UserAddress UserAddresses { get; set; }
    }
}
