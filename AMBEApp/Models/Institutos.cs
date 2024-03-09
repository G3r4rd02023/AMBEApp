using System.Text.Json.Serialization;

namespace AMBEApp.Models
{
    public class Institutos
    {
        [JsonPropertyName("idInstituto")]
        public int IdInstituto { get; set; }

        [JsonPropertyName("nombreInstituto")]
        public string NombreInstituto { get; set; }

        [JsonPropertyName("rtn")]
        public string Rtn { get; set; }

        [JsonPropertyName("telefono")]
        public string Telefono { get; set; }

        [JsonPropertyName("direccion")]
        public string Direccion { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("descripcion")]
        public string Descripcion { get; set; }

        [JsonPropertyName("creadoPor")]
        public string CreadoPor { get; set; }

        [JsonPropertyName("fechaCreacion")]
        public DateTime FechaCreacion { get; set; }

        [JsonPropertyName("modificadoPor")]
        public string ModificadoPor { get; set; }

        [JsonPropertyName("fechaModificacion")]
        public DateTime FechaModificacion { get; set; }
    }
}
