﻿
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

        //public async Task<bool> AddJewleryAsync(Jewelry j)
        //{
        //    try
        //    {
        //        var Jewlery = j;
        //        var jsonContent = JsonSerializer.Serialize(Jewelry, _serializerOptions);
        //        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        //        var response = await _httpClient.PostAsync($"{URL}AddJewlery", content);

        //        switch (response.StatusCode)
        //        {
        //            case (HttpStatusCode.OK):
        //                {
        //                    jsonContent = await response.Content.ReadAsStringAsync();
        //                    Jewelry je = JsonSerializer.Deserialize<Jewelry>(jsonContent, _serializerOptions);
        //                    await Task.Delay(2000);
        //                    return true;

        //                }
        //            case (HttpStatusCode.Unauthorized):
        //                {
        //                    return false;


        //                }

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //    return false;


        //}

        public async Task<bool> UploadPhoto(FileResult file)
        {
            try
            {
                byte[] bytes;
                using (MemoryStream ms = new MemoryStream())
                {
                    var stream = await file.OpenReadAsync();
                    stream.CopyTo(ms);
                    bytes = ms.ToArray();
                }
                var multipartFormDataContent = new MultipartFormDataContent();
                var content = new ByteArrayContent(bytes);
                multipartFormDataContent.Add(content, "file", "robot.jpg");
                var response = await _httpClient.PostAsync($@"{URL}UploadImage?Id=1", multipartFormDataContent);
                if (response.IsSuccessStatusCode) { return true; }
            }
            catch(System.Exception ex)
            {
                Console.WriteLine(ex.Message   );
            }
            return false;
        }
         
        public async Task<string> GetImage() { return $"{IMAGE_URL}images/"; }
        //add Get returns List of Jewelry from server
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


                var response = await _httpClient.GetAsync($"{URL}GetOrders");

                switch (response.StatusCode)
                {
                    case (HttpStatusCode.OK):
                        {
                            var jsonContent = await response.Content.ReadAsStringAsync();
                            List<Order> o = JsonSerializer.Deserialize<List<Order>>(jsonContent, _serializerOptions);
                            //if (o != null && o.Count > 0)
                            //    foreach (var item in o)
                            //    {
                            //        item.Photo = IMAGE_URL + item.Photo;
                            //    }
                            await Task.Delay(2000);
                            return o;

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

        public async Task<User> GetUserAsync(int userId)
        {
            try
            {


                var response = await _httpClient.GetAsync($"{URL}GetUser");

                switch (response.StatusCode)
                {
                    case (HttpStatusCode.OK):
                        {
                            var jsonContent = await response.Content.ReadAsStringAsync();
                            User u = JsonSerializer.Deserialize<User>(jsonContent, _serializerOptions);
                            
                            await Task.Delay(2000);
                            return u;

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
                            //
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


