
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

namespace VintageStore.Services
{
    public class StoreService
    {
        readonly HttpClient _httpClient;
        readonly JsonSerializerOptions _serializerOptions;
        const string URL = @"https://ldcxfb5h-7294.uks1.devtunnels.ms/api/storeapi/";
        const string IMAGE_URL = @"https://ldcxfb5h-7294.uks1.devtunnels.ms/";

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

            public async Task<UserDTO> LoginAsync(string UserName, string Password)
            {
                try
                {
                    var user = new LoginDTO() {  Username = UserName, Password = Password };
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
                                return new UserDTO() { Success = true, Message = string.Empty,   User=u };

                            }
                        case (HttpStatusCode.Unauthorized):
                            {
                            return new UserDTO() { Success = false, User = null, Message= Models.ErrorMessages.INVALID_LOGIN };


                            }

                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return new UserDTO() { Success = false, User = null, Message = Models.ErrorMessages.INVALID_LOGIN };


            }

        public async Task<UserDTO> RegisterAsync(string UserName, string Password)
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


    } 
}

