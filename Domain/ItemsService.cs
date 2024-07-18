using Api.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;

namespace Api.Domain
{
    public class ItemsService : IItemsService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public ItemsService(HttpClient httpClient, IConfiguration config)
        {
            _config = config;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(_config.GetValue<string>("ApiSettings:Url"));
            _httpClient.DefaultRequestHeaders.Add("X-Functions-Key", _config.GetValue<string>("ApiSettings:Key"));
        }

        public async Task<IEnumerable<ItemDto>> GetItems()
        {
            var res = await _httpClient.GetAsync("api/fetch");

            /** 
             * Appropriate place for error handling that throws a custom exception "ApiException" for example,
             * containing status code and message. This is then caught in ItemsController which return the status code and the message.
             */
            if (!res.IsSuccessStatusCode)
            {

            }

            var json = await res.Content.ReadAsStringAsync();
            var items = JsonSerializer.Deserialize<IEnumerable<ItemDto>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return items;
        }

    }
}

