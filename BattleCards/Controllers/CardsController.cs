using System;
using System.Collections.Generic;
using System.Text;
using BattleCards.Services;
using SUS.HTTP;
using SUS.MvcFramework;

namespace BattleCards.Controllers
{
    class CardsController:Controller
    {
        private readonly ICardService cardService;

        public CardsController(ICardService cardService)
        {
            this.cardService = cardService;
        }

        public HttpResponse All()
        {
            if (IsUserSignedIn())
            {
                return View();
            }

            return Redirect("/Users/Login");
        }

        public HttpResponse Collection()
        {
            if (IsUserSignedIn())
            {
                return View();
            }
            return Redirect("/Users/Login");
        }

        public HttpResponse Add()
        {
            if (IsUserSignedIn())
            {
                return View();
            }
            return Redirect("/Users/Login");
        }

    }
}
