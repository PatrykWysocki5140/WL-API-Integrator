using Antheap.Model.APIwl.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Antheap.Model.APIwl
{
    internal class WLwebAPI
    {
        private readonly HttpClient httpClient;
        private JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        public WLwebAPI(HttpClient httpClient, string url = "https://wl-api.mf.gov.pl")
        {
            httpClient.BaseAddress = new Uri(url);
            this.httpClient = httpClient;
        }

        //public async Task<EntityResponse> GetDataFromNipAsync(string nip, DateTime dateTime)
        public async Task<EntityResponse> GetDataFromNipAsync(string nip, DateTime dateTime)
        {
            string GetString = string.Empty;
            try
            {
                var Getresult = await httpClient.GetAsync($"/api/search/nip/{nip}?date={dateTime.ToString("yyyy-MM-dd")}");

                GetString = await Getresult.Content.ReadAsStringAsync();

                if (Getresult.IsSuccessStatusCode)
                {
                    return JsonSerializer.Deserialize<EntityResponse>(GetString, JsonSerializerOptions);
                }
                return new EntityResponse { Exception = JsonSerializer.Deserialize<Data.Exception>(GetString, JsonSerializerOptions) };
            }
            catch (System.Exception)
            {
                throw;
            }
        }

       
        
        
        
    }
}
