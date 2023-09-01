namespace HotelPtyxiaki.Models
{
    public class RestaurantReservationDeleteRequest
    {
        public int RestaurantReservPeopleNumber { get; set; } = 0;
        public string RestaurantReservComment { get; set; } = string.Empty;
        public string RestaurantReservDateTime { get; set; } = string.Empty;

    }
}
