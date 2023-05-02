using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tadoba_api.Entity
{
    [Table("products")]
    public class Product
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public double Price { set; get; }
        public double Discount { set; get; }
        public int AvailableQuantity { set; get; }
        public int Weight { set; get; }
        public string ImagePath { get; set; }
        public bool IsActive { get; set; }
    }
}
