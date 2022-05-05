using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
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

        public async Task<ApiResponse<Perfil>> GetPerfil(string email)
        {
            var url = $"{_baseUrl}/getPerfil?email={email}";
            var client = new RestClient(url);
            var request = new RestRequest() { Method = Method.Get };
            request.RequestFormat = RestSharp.DataFormat.Json;
            request.AddHeader("Content-Type", "application/json");
            //request.AddJsonBody(dto);
            var response = await client.ExecuteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var output = JsonConvert.DeserializeObject<ApiResponse<Perfil>>(response.Content);
                return output;
            }
            else
            {
                var output = JsonConvert.DeserializeObject<ApiResponse<Perfil>>(response.Content);
                return output;
            }
        }

        public async Task<ApiResponse<Perfil>> PatchPerfil(string email, ProfileOutput dto)
        {
            //Primero mando el file, lo subo y me regresa el url y lo adjunto al dto
            if(dto.File != null)
            {
                var urlresume = await UploadResume(dto.File);
                dto.UrlCv = urlresume;
            }
            
            dto.File = null;

            if (dto.Image != null)
            {
                var urlimage = await UploadResume(dto.Image);
                dto.UrlCv = urlimage;
            }

            var url = $"{_baseUrl}/patchPerfil?email={email}";
            var client = new RestClient(url);
            var request = new RestRequest() { Method = Method.Patch };
            request.RequestFormat = RestSharp.DataFormat.Json;
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(dto);
            var response = await client.ExecuteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var output = JsonConvert.DeserializeObject<ApiResponse<Perfil>>(response.Content);
                return output;
            }
            else
            {
                var output = JsonConvert.DeserializeObject<ApiResponse<Perfil>>(response.Content);
                return output;
            }
        }

        public async Task<string> UploadResume(IFormFile file)
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest($"UploadResume", Method.Post) { AlwaysMultipartFormData = true };

            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                var fileBytes = ms.ToArray();
                request.AddFile("resume", fileBytes, file.FileName);
            }

            var response = await client.ExecuteAsync(request);

            var output = JsonConvert.DeserializeObject<ApiResponse<string>>(response.Content);
            return output.Result;
        }
    }
}
