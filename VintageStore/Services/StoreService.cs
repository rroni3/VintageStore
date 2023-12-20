using Java.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using VintageStore.Models;

namespace VintageStore.Services
{
    public class StoreService
    {
        internal Task<string> GetImage()
        {
            throw new NotImplementedException();
        }

        internal Task<string> GetUserEmail(string x)
        {
            throw new NotImplementedException();
        }

        internal Task LogInAsync(string userName, string password)
        {
            throw new NotImplementedException();
        }

        internal Task<bool> UploadFile(FileResult file)
        {
            throw new NotImplementedException();
        }

        public async Task<LoginDTO> LoginAsync(string UserName, string Password)
        {
            try
            {
                var user = new LoginDTO() { User = UserName, Password = Password };
                var jsonContent = JsonSerializer.Serialize(user, _serializerOptions);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync($"{URL}LoginPage", content);

                switch (response.StatusCode)
                {
                    case (HttpStatusCode.OK):
                        {
                            jsonContent = await response.Content.ReadAsStringAsync();
                            User u = JsonSerializer.Deserialize<User>(jsonContent, _serializerOptions);
                            await Task.Delay(2000);
                            return new LoginDTO() { Success = true, Message = string.Empty, Password = u.UserPswd, User = u.UserName };

                        }
                    case (HttpStatusCode.Unauthorized):
                        {
                            return new LoginDTO() { Success = false, User = null, Password = null, Message = Models.Messages.INVALID_LOGIN };


                        }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return new LoginDTO() { Success = false, User = null, Password = null, Message = Models.Messages.INVALID_LOGIN };


        }


    }
}
