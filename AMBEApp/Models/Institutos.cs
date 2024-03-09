using System.Text.Json.Serialization;

namespace AMBEApp.Models
{
    public class Institutos
    {
        [JsonPropertyName("idInstituto")]
        public int IdInstituto { get; set; }

        [JsonPropertyName("nombreInstituto")]
        public string NombreInstituto { get; set; }
    }
}
