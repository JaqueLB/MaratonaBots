using Newtonsoft.Json;

namespace ManicureBot.Models
{
    public class Service
    {

        // fix for serialize and deserialize json
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Price")]
        public string Price { get; set; }
    }

    public enum ServicePrices
    {
        Manicure = 25,
        Pedicure = 25,
        Completo = 40
    } 
}