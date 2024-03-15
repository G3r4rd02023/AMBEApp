using System.Text.Json.Serialization;

namespace AMBEApp.Models
{
    public class TipoViaje
    {
        [JsonPropertyName("idTipoViaje")]
        public int IdTipoViaje { get; set; }

        [JsonPropertyName("evento")]
        public string Evento { get; set; }

        [JsonPropertyName("descripcion")]
        public string Descripcion { get; set; }

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
