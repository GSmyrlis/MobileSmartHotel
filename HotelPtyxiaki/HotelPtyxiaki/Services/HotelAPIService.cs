using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace HotelPtyxiaki.Services
{
    public class HotelAPIService
    {
        private const string BaseUrl = "http://192.168.200.42:5000";
        private readonly HttpClient _httpClient;
        private string BearerToken = string.Empty;
        public HotelAPIService()
        {
            _httpClient = new HttpClient();
            if (Preferences.ContainsKey("Token"))
            {
                BearerToken = Preferences.Get("Token", string.Empty);
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", BearerToken);
            }
        }

        public async Task<Models.Hotel> GetHotelDataAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(BaseUrl + "/api/hotel");
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
                Models.Hotel nodata = new Models.Hotel();
                return nodata;
            }
        }

        public async Task<Models.CleaningService> GetCleaningServiceDataAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(BaseUrl + "/api/hotel/cleaning");
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
                Models.CleaningService nodata = new Models.CleaningService();
                return nodata;
            }
        }

        public async Task<(bool, string)> Login(Models.LoginCredentials credentials)
        {
            try
            {
                string jsonData = JsonConvert.SerializeObject(credentials);
                HttpContent content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(BaseUrl + "/login", content);
                // Read the response content as a string
                string responseContent = await response.Content.ReadAsStringAsync();

                // Deserialize the response content to a LoginResponse object
                Models.LoginResponse jsonResponse = JsonConvert.DeserializeObject<Models.LoginResponse>(responseContent);

                if (response.IsSuccessStatusCode)
                {
                    // Extract the bearer token from the response
                    BearerToken = jsonResponse.Token.Substring(7);
                    Preferences.Set("Token", BearerToken);
                    return (true, "Connected");
                }
                else
                {
                    return (false, jsonResponse.Message);
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }



        public async Task<Models.RestaurantReservation> GetRestaurantReservationAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(BaseUrl + "/api/hotel/restaurant");
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
                Models.RestaurantReservation nodata = new Models.RestaurantReservation();
                return nodata;
            }
        }

        public async Task<Models.Rating> GetRatingAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(BaseUrl + "/api/hotel/rating");
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
                Models.Rating norate = new Models.Rating();
                return norate;
            }
        }

        public async Task<bool> PostRatingAsync(Models.Rating rates)
        {
            try
            {
                string jsonData = JsonConvert.SerializeObject(rates);
                HttpContent content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(BaseUrl + "/api/hotel/rating", content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> PostCleaningAsync(Models.CleaningService cleanings)
        {
            try
            {
                string jsonData = JsonConvert.SerializeObject(cleanings);
                HttpContent content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(BaseUrl + "/api/hotel/cleaning", content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> PostRestaurantReservationAsync(Models.RestaurantReservation restres)
        {
            try
            {
                string jsonData = JsonConvert.SerializeObject(restres);
                HttpContent content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(BaseUrl + "/api/hotel/restaurant", content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
