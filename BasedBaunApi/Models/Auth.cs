using Microsoft.Build.Framework;

namespace BasedBaunApi.Models;

public class Auth
{
    public class Login
    {
        [Required] public string Email { get; set; }

        [Required] public string Password { get; set; }
    }

    public class LoginResponse
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }

    public class Register
    {
        [Required] public string Email { get; set; }

        [Required] public string Username { get; set; }

        [Required] public string Password { get; set; }
    }
}