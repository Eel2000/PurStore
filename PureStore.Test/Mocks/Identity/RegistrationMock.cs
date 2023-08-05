using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureStore.Test.Mocks.Identity
{
    public static class RegistrationMock
    {
        public static RegisterDTO Register()
            => new("test", "test", "test@gmail.com");

        public static AuthResponse ResgistrationResponse()
        {
            return new()
            {
                Username = "test",
                Email = "test",
                Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c"
            };
        }
    }
}
