using System;
using System.Collections.Generic;
using System.Text;

namespace HotelPtyxiaki.Models
{
    public class CleaningModel
    {
        public bool Activated { get; set; } = true;
        public bool SelectedDateTime { get; set; } = false;
        public List<string> SpecificDateTimes { get; set; }
    }
}
