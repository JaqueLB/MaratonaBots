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
        public DateTime DayTime { get; set; }

        [JsonProperty("Phone")]
        public string Phone { get; set; }

        [JsonProperty("Place")]
        public string Place { get; set; }

        [JsonProperty("PaymentType")]
        public PaymentTypes PaymentType { get; set; }

        [JsonProperty("Service")]
        public ServiceTypes ServiceType { get; set; }
    }

    public enum ServiceTypes
    {
        Manicure = 1,
        Pedicure,
        Completo,
    }

    public enum ServicePrices
    {
        Manicure = 25,
        Pedicure = 25,
        Completo = 40
    }

    public enum PaymentTypes
    {
        Money = 1,
        DebitCard,
    }
}