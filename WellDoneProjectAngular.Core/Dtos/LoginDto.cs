namespace WellDoneProjectAngular.Core.Dtos
{
    public class LoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; } = false;
        public string? TwoFactorCode { get; set; }
        public bool RememberMachine { get; set; } = false;
    }
}
