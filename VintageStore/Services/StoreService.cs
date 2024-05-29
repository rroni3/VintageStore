
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using VintageStore.Models;
using Microsoft.Extensions.Logging;
using static System.Net.WebRequestMethods;
using Microsoft.Maui.Controls;
using System.Net.Http.Headers;

namespace VintageStore.Services
{
    public class StoreService
    {
        User logedUser;
        readonly HttpClient _httpClient;
        readonly JsonSerializerOptions _serializerOptions;
        static string URL = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5286/Api/StoreApi/" : "http://localhost:5286/Api/StoreApi/";
        static string IMAGE_URL = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5286/Images/" : "http://localhost:5286/Images/";

        public StoreService()
        {
            _httpClient = new HttpClient();


            _serializerOptions = new JsonSerializerOptions()
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true,
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve
            };

        }


        public User GetCurrentUser()
        {
            return logedUser;
        }
        public void SetCurrentUser(User u)
        {
            logedUser=u;
        }

        public async Task<UserDTO> LoginAsync(string UserName, string Password)
        {
            try
            {
                var user = new LoginDTO() { Username = UserName, Password = Password };
                var jsonContent = JsonSerializer.Serialize(user, _serializerOptions);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{URL}Login", content);

                switch (response.StatusCode)
                {
                    case (HttpStatusCode.OK):
                        {
                            jsonContent = await response.Content.ReadAsStringAsync();
                            User u = JsonSerializer.Deserialize<User>(jsonContent, _serializerOptions);
                            await Task.Delay(2000);
                            logedUser = u;
                            return new UserDTO() { Success = true, Message = string.Empty, User = u };

                        }
                    case (HttpStatusCode.Unauthorized):
                        {
                            return new UserDTO() { Success = false, User = null, Message = Models.ErrorMessages.INVALID_LOGIN };


                        }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return new UserDTO() { Success = false, User = null, Message = Models.ErrorMessages.INVALID_LOGIN };


        }

        public async Task<bool> RegisterAsync(User u)
        {
            try
            {
                var user = u;
                var jsonContent = JsonSerializer.Serialize(user, _serializerOptions);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{URL}Register", content);

                switch (response.StatusCode)
                {
                    case (HttpStatusCode.OK):
                        {
                            jsonContent = await response.Content.ReadAsStringAsync();
                            User us = JsonSerializer.Deserialize<User>(jsonContent, _serializerOptions);
                            logedUser = us;
                            await Task.Delay(2000);
                            
                            return true;

                        }
                    case (HttpStatusCode.Unauthorized):
                        {
                            return false;


                        }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;


        }

        public async Task<int> AddJewleryAsync(Jewelry j)
        {
            try
            {
                var Jewlery = j;
                var jsonContent = JsonSerializer.Serialize(j, _serializerOptions);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{URL}AddJewlery", content);

                switch (response.StatusCode)
                {
                    case (HttpStatusCode.OK):
                        {
                            jsonContent = await response.Content.ReadAsStringAsync();
                            Jewelry je = JsonSerializer.Deserialize<Jewelry>(jsonContent, _serializerOptions);
                            await Task.Delay(2000);
                            return je.Id;

                        }
                    case (HttpStatusCode.Unauthorized):
                        {
                            return -1;


                        }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return -1;


        }

        internal async Task<bool> UploadImage(FileResult photo,int id)
        {
            byte[] streamBytes;
            //take the photo and make it a byte array
            using (var stream = await photo.OpenReadAsync())
            using (var memoryStream = new MemoryStream())
            {
                await stream.CopyToAsync(memoryStream);
                streamBytes = memoryStream.ToArray();
            }

            using (var content = new MultipartFormDataContent())
            {
                var fileContent = new ByteArrayContent(streamBytes);
                fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
                //"file" --זהה לשם הפרמטר של הפעולה בשרת שמייצגת את הקובץ
                content.Add(fileContent, "file", photo.FileName);
                var response = await _httpClient.PostAsync(@$"{URL}UploadProfile/{id}", content);
                if (response.IsSuccessStatusCode)
                {
                    //string jsonContent = await response.Content.ReadAsStringAsync();
                    //ImageResponse res = JsonSerializer.Deserialize<ImageResponse>(jsonContent, _serializerOptions);
                    return true;
                }
            }
            return false;
        }
        
        public async Task<List<Jewelry>> GetJewlsAsync()
        {
            try
            {


                var response = await _httpClient.GetAsync($"{URL}GetJewleries");

                switch (response.StatusCode)
                {
                    case (HttpStatusCode.OK):
                        {
                            var jsonContent = await response.Content.ReadAsStringAsync();
                            List<Jewelry> j = JsonSerializer.Deserialize<List<Jewelry>>(jsonContent, _serializerOptions);
                            if (j != null && j.Count > 0)
                                foreach (var item in j)
                                {
                                   item.Photo=IMAGE_URL+item.Photo;
                                }
                            await Task.Delay(2000);
                            return j;

                        }
                    case (HttpStatusCode.Unauthorized):
                        {
                            return null;


                        }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }


        public async Task<List<Order>> GetOrdersAsync(int userId)
        {
            try
            {


                var response = await _httpClient.GetAsync($"{URL}GetOrders?Id={userId}");

                switch (response.StatusCode)
                {
                    case (HttpStatusCode.OK):
                        {
                            

                                var jsonContent = await response.Content.ReadAsStringAsync();
                                List<Order> orders = JsonSerializer.Deserialize<List<Order>>(jsonContent, _serializerOptions);
                            if (orders != null && orders.Count > 0)
                                foreach (var item in orders)
                                {
                                    if (item.OrderItems.Count > 0)
                                    {
                                        foreach (var item2 in item.OrderItems)
                                        {
                                            item2.Photo = IMAGE_URL + item2.Photo;
                                        }
                                    }

                                }

                                await Task.Delay(2000);
                                return orders;

                            }
                            
                       


                    case (HttpStatusCode.Unauthorized):
                        {
                            return null;


                        }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public async Task<List<Jewelry>> GetOrderJewleriesAsync(int orderId)
        {
            try
            {


                var response = await _httpClient.GetAsync($"{URL}GetOrderJewleries?Id={orderId}");

                switch (response.StatusCode)
                {
                    case (HttpStatusCode.OK):
                        {


                            var jsonContent = await response.Content.ReadAsStringAsync();
                            List<Jewelry> jewelries = JsonSerializer.Deserialize<List<Jewelry>>(jsonContent, _serializerOptions);
                            if (jewelries != null && jewelries.Count > 0)
                                foreach (var item in  jewelries)
                                {
                                    if (item != null)
                                    {
                                        item.Photo = IMAGE_URL + item.Photo;
                                    }
                                       
                                        

                                }

                            await Task.Delay(2000);
                            return jewelries;

                        }




                    case (HttpStatusCode.Unauthorized):
                        {
                            return null;


                        }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }


        public async Task<bool> AddOrder(Order o)
        {
            try
            {
                
                var jsonContent = JsonSerializer.Serialize(o, _serializerOptions);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{URL}AddOrder", content);

                switch (response.StatusCode)
                {
                    case (HttpStatusCode.OK):
                        {
                            
                            return true;

                        }
                    default:
                        {
                            
                            return false;


                        }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;


        }
    }

    } 


