using Microsoft.AspNetCore.Mvc;
using Xapp.Domain.Entities;
using Xapp.Domain.DTOs;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;

namespace Xapp.Web.Services
{
    public class XipeCoinsService
    {
        private readonly string _baseUrl = "https://localhost:44331/api/XipeCoins";

        public async Task<ApiResponse<AccountOutput>> GetProfile(int id)
        {
            var url = $"{_baseUrl}/GetProfile?id={id}";
            var client = new RestClient(url);
            var request = new RestRequest() { Method = Method.Get };
            request.RequestFormat = RestSharp.DataFormat.Json;
            request.AddHeader("Content-Type", "application/json");
            var response = await client.ExecuteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var output = JsonConvert.DeserializeObject<ApiResponse<AccountOutput>>(response.Content);
                return output;
            }
            else
            {
                var output = JsonConvert.DeserializeObject < ApiResponse <AccountOutput>>(response.Content);
                return output;
            }
        }

        public async Task<ApiResponse<Transfer>> PostTransfer(TransferInput dto)
        {
            var url = $"{_baseUrl}/TransferXipeCoins";
            var client = new RestClient(url);
            var request = new RestRequest() { Method = Method.Patch};
            request.RequestFormat = RestSharp.DataFormat.Json;
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(dto);
            var response = await client.ExecuteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var output = JsonConvert.DeserializeObject<ApiResponse<Transfer>>(response.Content);
                return output;
            }
            else
            {
                var output = JsonConvert.DeserializeObject<ApiResponse<Transfer>>(response.Content);
                return output;
            }
        }


        public async Task<ApiResponse<AccountOutput>> GetTransfers(AccountOutput dto)
        {
            var url = $"{_baseUrl}/GetTransfers";
            var client = new RestClient(url);
            var request = new RestRequest() { Method = Method.Get };
            request.RequestFormat = RestSharp.DataFormat.Json;
            request.AddHeader("Content-Type", "application/json");
            var response = await client.ExecuteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var output = JsonConvert.DeserializeObject<ApiResponse<AccountOutput>>(response.Content);
                return output;
            }
            else
            {
                var output = JsonConvert.DeserializeObject<ApiResponse<AccountOutput>>(response.Content);
                return output;
            }
        }


    }
}
