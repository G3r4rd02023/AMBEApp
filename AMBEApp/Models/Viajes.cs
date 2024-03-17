using System.Text.Json.Serialization;

namespace AMBEApp.Models
{
    public class Viajes
    {
        [JsonPropertyName("idViaje")]
        public int IdViaje { get; set; }

        [JsonPropertyName("fecha")]

        public DateTime Fecha { get; set; }

        [JsonPropertyName("horaInicio")]
        public string HoraInicio { get; set; }

        [JsonPropertyName("horaFinal")]
        public string HoraFinal { get; set; }

        [JsonPropertyName("idPersonaConductor")]
        public int IdPersonaConductor { get; set; }

        [JsonPropertyName("idPersonaNinera")]
        public int IdPersonaNinera { get; set; }

        [JsonPropertyName("idUnidad")]
        public int IdUnidad { get; set; }

        [JsonPropertyName("idInstituto")]
        public int IdInstituto { get; set; }

        [JsonPropertyName("comentarios")]
        public string Comentarios { get; set; }

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
