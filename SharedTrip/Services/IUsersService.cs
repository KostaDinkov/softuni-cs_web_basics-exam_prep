
namespace SharedTrip.Services
{
    public interface IUsersService
    {
        public string GetUserId(string username, string password);

        public string Create(string username, string password, string email);
    }
}
