using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tadoba_api.Entity
{
    [Table("orderdetails")]
    public class OrderDetails
    {
        [Key]
        public long Id { get; set; }
        public long ProductId { get; set; }
        public long OrderId { get; set; }
        public long Price { get; set; }
        public long Discount { get; set; }
        public long Quantity { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }

    }
}
