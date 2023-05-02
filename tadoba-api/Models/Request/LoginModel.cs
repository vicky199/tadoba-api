namespace tadoba_api.Models
{
    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string VerificationCode { get; set; }
        public bool IsMfa { get; set; }
    }
}
