using System;
using System.Collections.Generic;
using System.Text;

namespace HotelPtyxiaki.Models
{
    public class Hotel
    {
        public string HotelName = string.Empty;
        public string HotelAddress = string.Empty;
        public string HotelInfo = string.Empty;
        public string HotelEmail = string.Empty;
        public string HotelWebsite = string.Empty;
        public string RestaurantMenuLink = string.Empty;
        public uint ReceptionTelephone = 0;
        public int RestaurantReservPeopleNumber = 0;
        public string RestaurantReservComment = string.Empty;
        public string RestaurantReservDateTime = string.Empty;
        public bool CleaningServiceActivate = false;
        public string CleaningServiceReservDateTime = string.Empty;
    }
}
