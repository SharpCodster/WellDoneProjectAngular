namespace WellDoneProjectAngular.Core.Dtos
{
    public class AuthenticationDto
    {
        public bool UserAuthenticated { get; set; }
        public bool Required2FA { get; set; }
        public bool LockedOut { get; set; }
        public bool NotAllowed { get; set; }
    }
}
