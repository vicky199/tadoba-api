using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tadoba_api.Entity
{
    [Table("useraddress")]
    public class UserAddress
    {
        [Key]
        public long Id { get; set; }
        public long UserId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public int Pincode { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
