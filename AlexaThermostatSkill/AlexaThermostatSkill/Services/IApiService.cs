using BDRCustomSkill.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlexaThermostatSkill
{
    interface IApiService
    {
        Task<TemperatureModel> GetTemperatureAsync(string accessToken);
        Task SetTemperatureAsync(string accessToken, double temperature);
    }
}
