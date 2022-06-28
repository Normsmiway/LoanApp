using System.Threading.Tasks;

namespace DlmCredit.Infrastructure.Services
{

    public interface IUserService
    {
        public UserInformation GetUserDetails(string userId);
    }
}
