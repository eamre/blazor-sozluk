﻿using BlazorSozluk.Common.Events.User;
using BlazorSozluk.Common.Infrastructure.Exceptions;
using BlazorSozluk.Common.Infrastructure.Results;
using BlazorSozluk.Common.Models.Queries;
using BlazorSozluk.Common.Models.RequestModels;
using BlazorSozluk.WebApp.Infrastructure.Services.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorSozluk.WebApp.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient client;

        public UserService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<UserDetailViewModel> GetUserDetail(Guid? id)
        {
            var result = await client.GetFromJsonAsync<UserDetailViewModel>($"/api/user/{id}");

            return result;
        }

        public async Task<UserDetailViewModel> GetUserDetail(string userName)
        {
            var result = await client.GetFromJsonAsync<UserDetailViewModel>($"/api/user/username/{userName}");

            return result;
        }

        public async Task<bool> UpdateUser(UserDetailViewModel user)
        {
            var result = await client.PostAsJsonAsync($"/api/user/update", user);

            return result.IsSuccessStatusCode;
        }

        public async Task<bool> ChangeUserPassword(string oldPassword, string newPassword)
        {
            var command = new ChangeUserPasswordCommand(null, oldPassword, newPassword);
            var httpResponse = await client.PostAsJsonAsync($"/api/User/ChangePassword", command);

            if (httpResponse != null && !httpResponse.IsSuccessStatusCode)
            {
                if (httpResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var responseStr = await httpResponse.Content.ReadAsStringAsync();
                    var validation = JsonSerializer.Deserialize<ValidationResponseModel>(responseStr, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    responseStr = validation.FlattenErrors;
                    throw new DatabaseValidationException(responseStr);
                }

                return false;
            }

            return httpResponse.IsSuccessStatusCode;

        }
    }
}
