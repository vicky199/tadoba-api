using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tadoba_api.Entity
{
    [Table("dropdownmaster")]
    public class DropDownMaster
    {
        [Key]
        public long Id { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public long? ParentId { get; set; }
        public bool IsActive { get; set; }

        [ForeignKey("ParentId")]
        public virtual DropDownMaster DropDownMasters { get; set; }
    }
}
