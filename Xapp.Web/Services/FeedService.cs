using Microsoft.AspNetCore.Mvc;
using Xapp.Domain.Entities;
using Xapp.Domain.DTOs;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;

namespace Xapp.Web.Services
{
    public class FeedService
    {
        private readonly string _baseUrl = "https://localhost:44331/api/Post";

        public async Task<ApiResponse<PostList>> GetAllPosts()
        {
            var url = $"{_baseUrl}/GetAll";
            var client = new RestClient(url);
            var request = new RestRequest() { Method = Method.Get };
            request.RequestFormat = RestSharp.DataFormat.Json;
            request.AddHeader("Content-Type", "application/json");
            var response = await client.ExecuteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var output = JsonConvert.DeserializeObject<ApiResponse<PostList>>(response.Content);
                return output;
            }
            else
            {
                var output = JsonConvert.DeserializeObject<ApiResponse<PostList>>(response.Content);
                return output;
            }
        }

        public async Task<ApiResponse<PostOutput>> GetPost(int id)
        {
            var url = $"{_baseUrl}/Get?id={id}";
            var client = new RestClient(url);
            var request = new RestRequest() { Method = Method.Get };
            request.RequestFormat = RestSharp.DataFormat.Json;
            request.AddHeader("Content-Type", "application/json");
            var response = await client.ExecuteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var output = JsonConvert.DeserializeObject<ApiResponse<PostOutput>>(response.Content);
                return output;
            }
            else
            {
                var output = JsonConvert.DeserializeObject<ApiResponse<PostOutput>>(response.Content);
                return output;
            }
        }

        public async Task<ApiResponse<Post>> CreatePost(int id, PostInput dto)
        {
            var url = $"{_baseUrl}/Get?id={id}";
            var client = new RestClient(url);
            var request = new RestRequest() { Method = Method.Post };
            request.RequestFormat = RestSharp.DataFormat.Json;
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(dto);
            var response = await client.ExecuteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var output = JsonConvert.DeserializeObject<ApiResponse<Post>>(response.Content);
                return output;
            }
            else
            {
                var output = JsonConvert.DeserializeObject<ApiResponse<Post>>(response.Content);
                return output;
            }
        }

    }
}
