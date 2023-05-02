namespace tadoba_api.Models
{
    public class DropDownModel
    {
        public long Id { get; set; }
        public string Value { get; set; }
        public long? ParentId { get; set; }
    }
}
