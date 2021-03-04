using Newtonsoft.Json;

namespace Points.Dtos
{
    public class SpendPointsDto
    {
        [JsonProperty(Required = Required.Always)]
        public int? Points { get; set; }
    }
}
