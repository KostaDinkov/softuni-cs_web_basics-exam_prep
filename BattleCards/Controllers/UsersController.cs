using System;
using System.Collections.Generic;
using System.Text;
using SUS.HTTP;
using SUS.MvcFramework;

namespace BattleCards.Controllers
{
    class UsersController:Controller
    {

        public HttpResponse Login()
        {
            if (IsUserSignedIn())
            {
                return Redirect("/Cards/All");
            }
            return this.View();
        }

        public HttpResponse Register()
        {
            if (IsUserSignedIn())
            {
                return Redirect("/Cards/All");
            }
            return this.View();
        }
    }
}
