using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using SharedTrip.Models;
using SharedTrip.Validation;
using SharedTrip.ViewModels.Trips;
using SUS.MvcFramework;

namespace SharedTrip.Services
{
    class TripsService : ITripsService
    {
        private readonly ApplicationDbContext db;

        public TripsService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IList<Trip> GetAll()
        {
            return db.Trips.ToList();
        }

        [HttpPost]
        public string Create(AddTripInputModel input)
        {

            var validationResult = ValidateAddTripInput(input);

            if (validationResult.IsValid)
            {
                var newTrip = new Trip()
                {
                    StartPoint = input.StartPoint,
                    EndPoint = input.EndPoint,
                    Description = input.Description,
                    ImagePath = input.ImagePath,
                    Seats = byte.Parse(input.Seats),
                    DepartureTime = DateTime.ParseExact(input.DepartureTime, "dd.MM.yyyy HH:mm",CultureInfo.InvariantCulture,DateTimeStyles.None)
                };
                db.Trips.Add(newTrip);
                db.SaveChanges();
                return newTrip.Id;
            }

            throw new InputValidationException(validationResult.Errors);

        }

        private IValidationResult ValidateAddTripInput(AddTripInputModel model)
        {
            var result = new ValidationResult() { IsValid = true };

            if (string.IsNullOrWhiteSpace(model.StartPoint))
                SetError("Start point cannot be empty.");

            if (string.IsNullOrWhiteSpace(model.EndPoint))
                SetError("End point cannot be empty");

            var isSeatsValid = byte.TryParse(model.Seats, out var seats);
            if (!isSeatsValid && (seats < 2 || seats > 6))
                SetError("Seats must be between 2 and 6, inclusive.");

            if (string.IsNullOrWhiteSpace(model.Description) || model.Description.Length > 80)
                SetError("Description must be between 1 and 80 characters long.");

            if (!DateTime.TryParseExact(model.DepartureTime, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                SetError("Invalid departure time. Please use dd.MM.yyyy HH:mm format.");
            

            return result;

            void SetError(string error)
            {
                result.Errors.Add(error);
                result.IsValid = false;
            }
        }
    }
}
