using System;
using System.Collections.Generic;
using System.Text;

namespace HotelPtyxiaki.Models
{
    public class CleaningModel
    {
        public bool Activated { get; set; } = true;
        public bool SelectedHour { get; set; } = false;
        public int Hour { get; set; }
    }
}
