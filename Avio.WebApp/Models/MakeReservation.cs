using AvioProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Avio.WebApp.Models
{
    public class MakeReservation
    {
        public Reservation Reservation{ get; set; }
        public int FlightId { get; set; }
        public int UserId { get; set; }

    }
}
