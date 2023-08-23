using System;
using System.Collections.Generic;
using System.Text;

namespace HotelPtyxiaki.Models
{
    public class CleaningService
    {
        public bool CleaningServiceActivate { get; set; } = true;
        public string CleaningServiceReservDateTime { get; set; } = string.Empty;
    }
}
