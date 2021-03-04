using Newtonsoft.Json;
using System;

namespace Points.Dtos
{
    public class TransactionDto
    {
        [JsonProperty(Required = Required.Always)]
        public string Payer { get; set; }
        [JsonProperty(Required = Required.Always)]
        public int? Points { get; set; }
        [JsonProperty(Required = Required.Always)]
        public DateTime? Timestamp { get; set; }
    }
}
