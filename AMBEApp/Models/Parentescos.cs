using System.Text.Json.Serialization;

namespace AMBEApp.Models
{
    public class Parentescos
    {
        [JsonPropertyName("idParentesco")]
        public int IdParentesco { get; set; }

        [JsonPropertyName("idPersonaAlumno")]
        public int IdPersonaAlumno { get; set; }

        [JsonPropertyName("idPersonaResponsable")]
        public string IdPersonaResponsable { get; set; }

        [JsonPropertyName("idInstituto")]
        public string IdInstituto { get; set; }

        [JsonPropertyName("parentesco")]
        public string Parentesco { get; set; }

        [JsonPropertyName("creadoPor")]
        public string CreadoPor { get; set; }

        [JsonPropertyName("fechaDeCreacion")]
        public List<string> FechaDeCreacion { get; set; }

        [JsonPropertyName("modificadoPor")]
        public string ModificadoPor { get; set; }

        [JsonPropertyName("fechaDeModificacion")]
        public DateTime FechaDeModificacion { get; set; }
    }
}
