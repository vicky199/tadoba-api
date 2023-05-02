using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tadoba_api.Entity
{
    [Table("appconfig")]
    public class AppConfig
    {
        [Key]
        public long Id { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }
}
