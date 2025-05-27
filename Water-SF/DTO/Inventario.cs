using Newtonsoft.Json;

namespace Water_SF.DTO
{
    public class Inventario
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("nombre")]
        public string Nombre { get; set; }

        [JsonProperty("descripcion")]
        public string Descripcion { get; set; }

        [JsonProperty("cantidad")]
        public int Cantidad { get; set; }

        [JsonProperty("unidad")]
        public string Unidad { get; set; }

        [JsonProperty("fechaIngreso")]
        public DateTime FechaIngreso { get; set; }

        [JsonProperty("precio")]
        public decimal Precio { get; set; }

        [JsonProperty("categoria")]
        public string Categoria { get; set; }
    }
}
