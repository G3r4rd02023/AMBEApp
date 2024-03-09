using System.Text.Json.Serialization;

namespace AMBEApp.Models
{
    public class TipoPersonas
    {
        [JsonPropertyName("idTipoPersona")]
        public int IdTipoPersona { get; set; }

        [JsonPropertyName("tipoPersona")]
        public string TipoPersona { get; set; }
    }
}
