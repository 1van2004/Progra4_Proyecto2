using Newtonsoft.Json;

namespace Water_SF.DTO
{
    public class Proveedor
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("nombreEmpresa")]
        public string NombreEmpresa { get; set; }

        [JsonProperty("nombreRepresentante")]
        public string NombreRepresentante { get; set; }

        [JsonProperty("cedulaRepresentante")]
        public string CedulaRepresentante { get; set; }

        [JsonProperty("correoEmpresa")]
        public string CorreoEmpresa { get; set; }

        [JsonProperty("telefonoEmpresa")]
        public string TelefonoEmpresa { get; set; }

        [JsonProperty("descripcionProductos")]
        public string DescripcionProductos { get; set; }

        [JsonProperty("numeroCuenta")]
        public string NumeroCuenta { get; set; }
    }
}
