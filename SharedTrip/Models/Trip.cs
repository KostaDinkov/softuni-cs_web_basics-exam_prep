using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SharedTrip.Models
{
    public class Trip
    {
        public Trip()
        {
            UserTrips = new HashSet<UserTrip>();
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }

        [Required] 
        public string StartPoint { get; set; }

        [Required]
        public string EndPoing { get; set; }

        [Required]
        public DateTime DepartureTime { get; set; }

        
        public byte Seats { get; set; }

        [Required]
        [MaxLength (80)]
        public string Description { get; set; }

        public string ImagePath { get; set; }

        public ICollection<UserTrip> UserTrips { get; set; }
    }
}
