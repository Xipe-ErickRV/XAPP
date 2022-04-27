using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xapp.Domain.DTOs;
using Xapp.Domain.DTOs.Perfil;
using Xapp.Domain.Entities;

namespace Xapp.Web.Services
{
    public class PerfilService
    {
        private readonly string _baseUrl = "https://localhost:44331/api/Perfil";
        
        public async Task<ApiResponse<User>> LogInAsync(LoginInput dto)
        {
            var url = $"{_baseUrl}/login";
            var client = new RestClient(url);
            var request = new RestRequest() { Method = Method.Post };
            request.RequestFormat = RestSharp.DataFormat.Json;
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(dto);
            var response = await client.ExecuteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var output = JsonConvert.DeserializeObject<ApiResponse<User>>(response.Content);
                return output;
            }
            else
            {
                var output = JsonConvert.DeserializeObject<ApiResponse<User>>(response.Content);
                return output;
            }
        }
    }
}
