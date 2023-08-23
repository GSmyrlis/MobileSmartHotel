using System;
using System.Collections.Generic;
using System.Text;

namespace HotelPtyxiaki.Models
{
    public class RestaurantReservation
    {
        public int RestaurantReservPeopleNumber { get; set; } = 0;
        public string RestaurantReservComment { get; set; } = string.Empty;
        public string RestaurantReservDateTime { get; set; } = string.Empty;
    }
}
