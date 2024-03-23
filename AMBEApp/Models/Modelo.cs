using System.Text.Json.Serialization;

namespace AMBEApp.Models
{
    public class Modelo
    {
        [JsonPropertyName("idModelo")]
        public int IdModelo { get; set; }

        [JsonPropertyName("idMarca")]
        public int IdMarca { get; set; }

        [JsonPropertyName("nombreModelo")]
        public string NombreModelo { get; set; }

        [JsonPropertyName("estado")]
        public string Estado { get; set; }

        [JsonPropertyName("creadoPor")]
        public string CreadoPor { get; set; }

        [JsonPropertyName("fechaDeCreacion")]
        public DateTime FechaDeCreacion { get; set; }

        [JsonPropertyName("modificadoPor")]
        public string ModificadoPor { get; set; }

        [JsonPropertyName("fechaModificacion")]
        public DateTime FechaDeModificacion { get; set; }
    }
}
