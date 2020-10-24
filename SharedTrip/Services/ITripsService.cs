using System;
using System.Collections.Generic;
using System.Text;
using SharedTrip.Models;
using SharedTrip.ViewModels.Trips;

namespace SharedTrip.Services
{
    interface ITripsService
    {
        public IList<Trip> GetAll();

        public string Create(AddTripInputModel input);

        TripDetailsViewModel GetDetails(string tripId);
        void AddUserToTrip(string userId, string tripId);
    }
}
