using Newtonsoft.Json;

namespace LoginPageMVC.Models
{
    public class LoginResponse
    {
        public string? message { get; set; }

        public Metadata? metadata { get; set; }
    }

    public class Metadata
    {
    }
}