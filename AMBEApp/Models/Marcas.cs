using System.Text.Json.Serialization;

namespace AMBEApp.Models
{
    public class Marcas
    {
        [JsonPropertyName("idMarca")]
        public int IdMarca { get; set; }

        [JsonPropertyName("nombreMarca")]
        public string NombreMarca { get; set; }

        [JsonPropertyName("estado")]
        public string Estado { get; set; }

        [JsonPropertyName("idInstituto")]
        public int IdInstituto { get; set; }

        [JsonPropertyName("creadoPor")]
        public string CreadoPor { get; set; }

        [JsonPropertyName("fechaDeCreacion")]
        public DateTime FechaDeCreacion { get; set; }

        [JsonPropertyName("modificadoPor")]
        public string ModificadoPor { get; set; }

        [JsonPropertyName("fechaDeModificacion")]
        public DateTime FechaDeModificacion { get; set; }
    }
}
