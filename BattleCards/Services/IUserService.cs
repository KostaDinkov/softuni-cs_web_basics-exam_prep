using BattleCards.ViewModels.User;

namespace BattleCards.Services
{
    public interface IUserService
    {
        public string Create(RegisterInputModel input);

        public string GetUserId(string username, string password);  
    }
}
