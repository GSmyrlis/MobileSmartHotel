using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace HotelPtyxiaki.Services
{
    public class HotelAPIService
    {
        private const string BaseUrl = "http://192.168.102.42:5000";
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

        public async Task<List<Models.CleaningService>> GetCleaningServiceDataAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(BaseUrl + "/api/hotel/cleaning");
                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Models.CleaningService>>(responseData);
                }
                else
                {
                    throw new Exception("Failed to get cleaning service requests: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                return (new List<Models.CleaningService>());
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



        public async Task<List<Models.RestaurantReservation>> GetRestaurantReservationAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(BaseUrl + "/api/hotel/restaurant");
                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Models.RestaurantReservation>>(responseData);
                }
                else
                {
                    throw new Exception("Failed to get restaurant reservation data: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                return new List<Models.RestaurantReservation>();
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

        public async Task<Models.Room> GetRoomAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(BaseUrl + "/api/hotel/room");
                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Models.Room>(responseData);
                }
                else
                {
                    throw new Exception("Failed to get room data: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                Models.Room noroom = new Models.Room();
                return noroom;
            }
        }

        public async Task<bool> PostRoomAsync(Models.Room room)
        {
            try
            {
                string jsonData = JsonConvert.SerializeObject(room);
                HttpContent content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(BaseUrl + "/api/hotel/room", content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool?> GetCleaningServiceActivateAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(BaseUrl + "/api/hotel/room/cleaning-activate");
                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    Models.CleaningServiceActivateResponse Activated = new Models.CleaningServiceActivateResponse();
                    Activated = JsonConvert.DeserializeObject<Models.CleaningServiceActivateResponse>(responseData);
                    return Activated.CleaningServiceActivate;
                }
                else
                {
                    throw new Exception("Failed to get CleaningServiceActivate: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<bool> UpdateCleaningServiceActivateAsync(bool cleaningServiceActivate)
        {
            try
            {
                string jsonData = JsonConvert.SerializeObject(new { CleaningServiceActivate = cleaningServiceActivate });
                HttpContent content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(BaseUrl + "/api/hotel/room/cleaning-activate", content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                return false;
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

        public async Task<bool> DeleteCleaningServiceRequestAsync(Models.CleaningServiceDeleteRequest cleanservdt)
        {
            try
            {
                // Serialize the cleanservdt object to JSON string
                string jsonData = JsonConvert.SerializeObject(cleanservdt);

                // Create a new HttpClient instance
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(BaseUrl);

                    // Set the authorization header
                    httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", BearerToken);

                    // Create a StringContent with the JSON data
                    HttpContent content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");

                    HttpRequestMessage request = new HttpRequestMessage
                    {
                        Content = content,
                        Method = HttpMethod.Delete,
                        RequestUri = new Uri(BaseUrl + "/api/hotel/cleaning")
                    };

                    // Send a DELETE request with the JSON content
                    HttpResponseMessage response = await httpClient.SendAsync(request);

                    return response.IsSuccessStatusCode;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteRestaurantRequestAsync(Models.RestaurantReservationDeleteRequest cleanservdt)
        {
            try
            {
                // Serialize the cleanservdt object to JSON string
                string jsonData = JsonConvert.SerializeObject(cleanservdt);

                // Create a new HttpClient instance
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(BaseUrl);

                    // Set the authorization header
                    httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", BearerToken);

                    // Create a StringContent with the JSON data
                    HttpContent content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");

                    HttpRequestMessage request = new HttpRequestMessage
                    {
                        Content = content,
                        Method = HttpMethod.Delete,
                        RequestUri = new Uri(BaseUrl + "/api/hotel/restaurant")
                    };

                    // Send a DELETE request with the JSON content
                    HttpResponseMessage response = await httpClient.SendAsync(request);

                    return response.IsSuccessStatusCode;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }


    }
}
