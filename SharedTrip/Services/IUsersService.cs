
using SharedTrip.ViewModels.Users;

namespace SharedTrip.Services
{
    public interface IUsersService
    {
        public string GetUserId(string username, string password);

        public string Create(RegisterInputModel input);
    }
}
