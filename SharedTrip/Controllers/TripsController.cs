using System;
using System.Collections.Generic;
using System.Net;
using SharedTrip.Services;
using SharedTrip.Validation;
using SharedTrip.ViewModels.Trips;
using SUS.HTTP;
using SUS.MvcFramework;

namespace SharedTrip.Controllers
{
    class TripsController : Controller
    {
        private readonly ITripsService tripsService;

        public TripsController(ITripsService service)
        {
            tripsService = service;
        }

        public HttpResponse All()
        {
            if (!IsUserSignedIn())
            {
                return Redirect("/Users/Login");
            }

            var trips = tripsService.GetAll();
            return this.View(trips);
        }

        public HttpResponse Add()
        {
            if (!IsUserSignedIn())
            {
                return Redirect("/Users/Login");
            }

            return View();
        }

        public HttpResponse Details()
        {
            if (!IsUserSignedIn())
            {
                return Redirect("/Users/Login");
            }
            return View()
        }

        [HttpPost]
        public HttpResponse Add(AddTripInputModel input)
        {
            if (!IsUserSignedIn())
            {
                return Redirect("/Users/Login");
            }

            try
            {
                tripsService.Create(input);
                return Redirect("/Trips/All");
            }
            catch (InputValidationException e)
            {
                return Error(e.ToHtmlString());
            }
        }
    }
}
