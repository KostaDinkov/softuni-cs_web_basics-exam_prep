namespace BattleCards.Controllers
{
    using SUS.HTTP;
    using SUS.MvcFramework;

    public class HomeController : Controller
    { 
        [HttpGet("/")]
        public HttpResponse Index()
        {
            if (IsUserSignedIn())
            {
                return Redirect("/Cards/All");
            }
            return this.View();
        }
        [HttpGet("/Logout")]
        public HttpResponse Logout()
        {
            if (IsUserSignedIn())
            {
                SignOut();
            }
            return Redirect("/");
        }



    }
}