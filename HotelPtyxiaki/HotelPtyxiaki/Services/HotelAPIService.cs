using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HotelPtyxiaki.Services
{
    public class HotelAPIService
    {
        private const string BaseUrl = "http://192.168.1.22:5000/api/hotel";
        private readonly HttpClient _httpClient;

        public HotelAPIService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<Models.Hotel> GetHotelDataAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(BaseUrl);
                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Models.Hotel>(responseData);
                }
                else
                {
                    throw new Exception("Failed to get hotel data: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                // Handle exception or log the error
                throw;
            }
        }

        public async Task<bool> UpdateHotelDataAsync(Models.Hotel hotel)
        {
            try
            {
                string jsonData = JsonConvert.SerializeObject(hotel);
                HttpContent content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(BaseUrl, content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                // Handle exception or log the error
                throw;
            }
        }

        public async Task<Models.CleaningService> GetCleaningServiceDataAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(BaseUrl + "/cleaning");
                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Models.CleaningService>(responseData);
                }
                else
                {
                    throw new Exception("Failed to get cleaning service data: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                // Handle exception or log the error
                throw;
            }
        }

        public async Task<Models.RestaurantReservation> GetRestaurantReservationAsync()
        {
                try
                {
                    HttpResponseMessage response = await _httpClient.GetAsync(BaseUrl + "/restaurant");
                    if (response.IsSuccessStatusCode)
                    {
                        string responseData = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<Models.RestaurantReservation>(responseData);
                    }
                    else
                    {
                        throw new Exception("Failed to get restaurant reservation data: " + response.StatusCode);
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception or log the error
                    throw;
                }
        }

        public async Task<Models.Rating> GetRatingAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(BaseUrl + "/rating");
                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Models.Rating>(responseData);
                }
                else
                {
                    throw new Exception("Failed to get restaurant reservation data: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                // Handle exception or log the error
                throw;
            }
        }

        public async Task<bool> PostRatingAsync(Models.Rating rates)
        {
            try
            {
                string jsonData = JsonConvert.SerializeObject(rates);
                HttpContent content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(BaseUrl + "/rating", content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                // Handle exception or log the error
                throw;
            }
        }
    }
}
