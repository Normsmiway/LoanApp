namespace DlmCredit.Infrastructure.Services
{
    public class UserInformation
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public UserInformation(string userId, string userName, string email)
        {
            UserId = userId;
            UserName = userName;
            Email = email;
        }
    }
}