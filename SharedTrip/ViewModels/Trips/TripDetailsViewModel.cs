using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using SharedTrip.Models;

namespace SharedTrip.ViewModels.Trips
{
    public class TripDetailsViewModel
    {
       public string Id { get; set; }
        public string StartPoint { get; set; }
        public string EndPoint { get; set; }
        public DateTime DepartureTime { get; set; }
        public byte Seats { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        
    }
}
