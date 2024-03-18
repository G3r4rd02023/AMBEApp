using System.Text.Json.Serialization;

namespace AMBEApp.Models
{
    public class Secciones
    {
        [JsonPropertyName("idSeccion")]
        public int IdSeccion { get; set; }

        [JsonPropertyName("idInstituto")]
        public int IdInstituto { get; set; }

        [JsonPropertyName("idGrado")]
        public int IdGrado { get; set; }

        [JsonPropertyName("seccion ")]
        public string Seccion { get; set; }

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
