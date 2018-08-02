using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class HotelBooking
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public int NumberOfAvailableRooms { get; set; }
        public int LocationCode { get; set; }

    }
}