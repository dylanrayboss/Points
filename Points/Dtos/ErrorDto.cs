using Newtonsoft.Json;

namespace Points.Dtos
{
    public class ErrorDto
    {
        [JsonProperty("error")]
        public string Error { get; set; }
    }
}
