using System;
using System.Collections.Generic;
using System.Text;

namespace HotelPtyxiaki.Models
{
    public class Room
    {
        public bool CleaningServiceActivate { get; set; } = true;
        public string CleaningServiceReservDateTime { get; set; } = string.Empty;
        public int RestaurantReservPeopleNumber { get; set; } = 0;
        public string RestaurantReservComment { get; set; } = string.Empty;
        public string RestaurantReservDateTime { get; set; } = string.Empty;
        public int RateComfort { get; set; } = 0;
        public int RateFacilities { get; set; } = 0;
        public int RateHospitality { get; set; } = 0;
        public int RateLocation { get; set; } = 0;
        public int RateOverall { get; set; } = 0;
    }
}
