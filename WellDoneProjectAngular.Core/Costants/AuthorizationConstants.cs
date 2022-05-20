namespace WellDoneProjectAngular.Core.Costants
{
    public static class AuthorizationConstants
    {
        public static class Roles
        {
            public const string Admin = "Administrator";
            public const string User = "User";

            public const string All = "Administrator,User";
            public const string AllOperatives = "Administrator";
        }

        public const string DefaultPassword = "Pass@word1";
        public const string TokeProvider = "Email";
        public const string JwtSecretKey = "SecretKeyOfDoomThatMustBeAMinimumNumberOfBytes";
    }
}
