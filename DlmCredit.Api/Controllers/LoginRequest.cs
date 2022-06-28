namespace DlmCredit.Api.Controllers
{
    public partial class AuthController
    {
        public class LoginRequest
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }
    }
}
