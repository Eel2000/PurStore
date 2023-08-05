namespace PureStore.Test.Mocks.Identity
{
    public static class AuthenticationMock
    {
        public static Auth Credentials()
            => new("johnDoe6619", "john.doe.189");

        public static AuthResponse AuthenticationResult()
        {
            return new()
            {
                Username = "johnDoe6619",
                Email = "john.doe@gmail.com",
                Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c"
            };
        }


    }
}
