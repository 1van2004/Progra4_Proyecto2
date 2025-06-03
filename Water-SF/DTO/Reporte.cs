using Newtonsoft.Json;

namespace Water_SF.DTO
{
    public class Reporte
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("nombre")]
        public string Nombre { get; set; }

        [JsonProperty("direccion")]
        public string Direccion { get; set; }

        [JsonProperty("tiporeporte")]
        public string Tiporeporte { get; set; }

        [JsonProperty("descripcionFuga")]
        public string DescripcionFuga { get; set; }

        [JsonProperty("ubicaciondeReferencia")]
        public string UbicaciondeReferencia { get; set; }

        [JsonProperty("fechaHora")]
        public DateTime FechaHora { get; set; }
    }
}
