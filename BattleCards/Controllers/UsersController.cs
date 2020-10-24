using BattleCards.Services;
using BattleCards.Validation;
using BattleCards.ViewModels.User;
using SUS.HTTP;
using SUS.MvcFramework;

namespace BattleCards.Controllers
{
    class UsersController:Controller
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }
        public HttpResponse Login()
        {
            if (IsUserSignedIn())
            {
                return Redirect("/Cards/All");
            }
            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(string username, string password)
        {
            if (IsUserSignedIn())
            {
                return Redirect("/Cards/All");
            }

            var userId = userService.GetUserId(username, password);
            if (string.IsNullOrEmpty(userId))
            {
                return Error("Incorrect username or password");
            }

            SignIn(userId);
            return Redirect("/Cards/All");
        }

        public HttpResponse Register()
        {
            if (IsUserSignedIn())
            {
                return Redirect("/Cards/All");
            }
            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterInputModel input)
        {
            if (IsUserSignedIn())
            {
                return Redirect("Cards/All");
            }

            try
            {
                var userId = userService.Create(input);
                this.SignIn(userId);
                return Redirect("/Cards/All");
            }
            catch (InputValidationException e)
            {
                return Error(e.ToHtmlString());
            }

        }
    }
}
