namespace tadoba_api.Models
{
    public class UserModel
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public bool IsInternal { get; set; }
        public bool IsActive { get; set; }
        public string MobileNo { get; set; }
        public bool IsMfaEnabled { get; set; }
        public string AccessToken { get; set; }
    }
}
