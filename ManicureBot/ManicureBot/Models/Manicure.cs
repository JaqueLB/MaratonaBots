using Newtonsoft.Json;
using System;

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
        public DateTime DayTime { get; set; }

        [JsonProperty("Phone")]
        public string Phone { get; set; }

        [JsonProperty("Place")]
        public string Place { get; set; }

        [JsonProperty("PaymentType")]
        public int PaymentType { get; set; }

        [JsonProperty("Service")]
        public int ServiceType { get; set; }
    }
}