using SharedTrip.Services;
using SharedTrip.Validation;
using SharedTrip.ViewModels.Users;
using SUS.HTTP;
using SUS.MvcFramework;



namespace SharedTrip.Controllers
{
    class UsersController : Controller
    {

        private readonly UsersService usersService;
        private readonly ApplicationDbContext db;

        public UsersController(UsersService usersService, ApplicationDbContext db)
        {
            this.usersService = usersService;
            this.db = db;
        }

        public HttpResponse Login()
        {
            if (IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(LoginInputModel input)
        {
            if (IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var userId = this.usersService.GetUserId(input.Username, input.Password);

            if (userId == null)
            {
                return this.Error("Invalid username or password");
            }

            this.SignIn(userId);
            return this.Redirect("/Trips/All");
        }

        public HttpResponse Register()
        {
            return View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterInputModel input)
        {

            try
            {
                var userId = usersService.Create(input);
                this.SignIn(userId);
                return Redirect("/Trips/All");
            }
            catch (InputValidationException e)
            {
                return this.Error(e.ToHtmlString());
            }
        }
      

        public HttpResponse Logout()
        {
            if (!IsUserSignedIn())
            {
                return Redirect("/Users/Login");
            }
            SignOut();
            return Redirect("/");
        }
    }
}
