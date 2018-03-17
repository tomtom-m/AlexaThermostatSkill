using BDRCustomSkill.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AlexaThermostatSkill
{
    class ThermostatApiService : IApiService
    {
        private const string ApiBaseUrl = "";

        public async Task<TemperatureModel> GetTemperatureAsync(string accessToken)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("Bearer " + accessToken);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var url = ApiBaseUrl + "/alexa/action/gettemperature";

            var result = await client.GetAsync(url).Result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TemperatureModel>(result);
        }

        public async Task SetTemperatureAsync(string accessToken, double temperature)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("Bearer " + accessToken);
            var content = JsonConvert.SerializeObject(new TemperatureModel() { temperature = temperature });
            var url = ApiBaseUrl + "/alexa/action/settemperature";

            var result = await client.PostAsync(url, new StringContent(content, Encoding.UTF8, "application/json"));
        }
    }
}
