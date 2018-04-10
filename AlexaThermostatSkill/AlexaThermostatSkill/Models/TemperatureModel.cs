using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BDRCustomSkill.Models
{
    class TemperatureModel
    {
        [JsonProperty("value")]
        public double Temperature { get; set; }

        [JsonProperty("scale")]
        public string Scale => "Celcius";
    }
}
