using Newtonsoft.Json;

namespace Water_SF.DTO
{
    public class Users
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("nis")]
        public string Nis { get; set; }

        [JsonProperty("numeroMedidor")]
        public string NumeroMedidor { get; set; }

        [JsonProperty("nombre")]
        public string Nombre { get; set; }

        [JsonProperty("apellido")]
        public string Apellido { get; set; }

        [JsonProperty("cedula")]
        public string Cedula { get; set; }

        [JsonProperty("telefono")]
        public string Telefono { get; set; }

        [JsonProperty("direccion")]
        public string Direccion { get; set; }

        [JsonProperty("correo")]
        public string Correo { get; set; }

        [JsonProperty("zona")]
        public string Zona { get; set; }
    }
}
