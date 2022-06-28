namespace DlmCredit.Infrastructure.Services
{
    internal class UserService : IUserService
    {
        public UserInformation GetUserDetails(string userId)
        {
            //In an ideal situation, I'll need to get user details from a DB or third party provider
            if (!string.IsNullOrWhiteSpace(userId) && userId == "Adewale")
            {
                return new UserInformation(userId, userId, $"{userId}@email.com");
            }

            return null;
        }
    }
}
