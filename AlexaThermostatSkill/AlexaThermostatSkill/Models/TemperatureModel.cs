using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BDRCustomSkill.Models
{
    class TemperatureModel
    {
        [JsonProperty("temperature")]
        public double temperature { get; set; }
    }
}
