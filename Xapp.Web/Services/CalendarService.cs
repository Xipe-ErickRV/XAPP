using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RestSharp;
using System.IO;
using System.Threading.Tasks;
using Xapp.Domain.DTOs;
using Xapp.Domain.Entities;

namespace Xapp.Web.Services
{
    public class CalendarService
    {
        private readonly string _baseUrl = "https://localhost:44331/api/Calendar";

        public async Task<ApiResponse<EventList>> GetEvents()
        {
            var url = $"{_baseUrl}/GetEvents";
            var client = new RestClient(url);
            var request = new RestRequest() { Method = Method.Get };
            request.RequestFormat = RestSharp.DataFormat.Json;
            request.AddHeader("Content-Type", "application/json");
            var response = await client.ExecuteAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var output = JsonConvert.DeserializeObject<ApiResponse<EventList>>(response.Content);
                return output;
            }
            else
            {
                var output = JsonConvert.DeserializeObject<ApiResponse<EventList>>(response.Content);
                return output;
            }
        }

        public async Task<ApiResponse<Event>> AddEvent(int userId, EventInput dto)
        {
            var url = $"{_baseUrl}/AddEvent?id={userId}";
            var client = new RestClient(url);
            var request = new RestRequest() { Method= Method.Post };
            request.RequestFormat = RestSharp.DataFormat.Json;
            request.AddHeader("Content-Type", "application/json");
            request.AddBody(dto);
            var response = await client.ExecuteAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var output = JsonConvert.DeserializeObject<ApiResponse<Event>>(response.Content);
                return output;
            }
            else
            {
                var output = JsonConvert.DeserializeObject<ApiResponse<Event>>(response.Content);
                return output;
            }
        }

        public async Task<ApiResponse<EditEvent>> UpdateEvent(EventInput dto)
        {
            var url = "${_baseUrl}/EditEvent";
            var client = new RestClient(url);
            var request = new RestRequest() { Method = Method.Patch };
            request.RequestFormat = RestSharp.DataFormat.Json;
            request.AddHeader("Content-Type", "application/json");
            request.AddBody(dto);
            var response = await client.ExecuteAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var output = JsonConvert.DeserializeObject<ApiResponse<EditEvent>>(response.Content);
                return output;
            }
            else
            {
                var output = JsonConvert.DeserializeObject<ApiResponse<EditEvent>>(response.Content);
                return output;
            }
        }
    }
}
