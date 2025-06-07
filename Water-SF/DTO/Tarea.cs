using Newtonsoft.Json;

namespace Water_SF.DTO
{
    public class Tarea
    {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("startdate")]
            public DateOnly StartDate { get; set; }

            [JsonProperty("enddate")]
            public DateOnly EndDate { get; set; }

            [JsonProperty("perincharge")]
            public string PerInCharge { get; set; }

            [JsonProperty("description")]
            public string Description { get; set; }

            [JsonProperty("Priority")]
            public string Priority { get; set; }
        }
}
