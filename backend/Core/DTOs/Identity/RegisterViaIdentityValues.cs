namespace Core.DTOs.Identity
{
    public class RegisterViaIdentityValues
    {
        public string? Email { get; set; }
        public string? UserNickname { get; set; }
        public string? UserName { get; set; }
        public string? UserSurname { get; set; }
        public string? Password { get; set; }
    }
}