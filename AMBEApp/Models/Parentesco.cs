using System.Text.Json.Serialization;

namespace AMBEApp.Models
{
    public class Parentesco
    {
        [JsonPropertyName("idParentesco")]
        public int IdParentesco { get; set; }

        [JsonPropertyName("idPersonaAlumno")]
        public int IdPersonaAlumno { get; set; }

        [JsonPropertyName("idPersonaResponsable")]
        public int IdPersonaResponsable { get; set; }

        [JsonPropertyName("idInstituto")]
        public string IdInstituto { get; set; }

        [JsonPropertyName("parentesco")]
        public string TipoParentesco { get; set; }

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
