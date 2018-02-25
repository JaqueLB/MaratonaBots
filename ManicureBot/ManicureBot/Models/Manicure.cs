using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManicureBot.Models
{
    public class Manicure
    {
        // fix for serialize and deserialize json
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("DayTime")]
        public string DayTime { get; set; }

        [JsonProperty("Place")]
        public string Place { get; set; }

        [JsonProperty("PaymentType")]
        public int PaymentType { get; set; }

        [JsonProperty("Service")]
        public int ServiceValue { get; set; }

        //Todo service type from api!!!
    }
}