namespace HotelPtyxiaki.Models
{
    public class RestaurantReservation
    {
        public int RestaurantReservPeopleNumber { get; set; } = 0;
        public string RestaurantReservComment { get; set; } = string.Empty;
        public string RestaurantReservDateTime { get; set; } = string.Empty;
        public int RequestState { get; set; } = 1;
        public string AdminMessage { get; set; } = string.Empty;
    }
}
