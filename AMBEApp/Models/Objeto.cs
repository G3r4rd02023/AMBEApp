using System.Text.Json.Serialization;

namespace AMBEApp.Models
{
    public class Objeto
    {
        [JsonPropertyName("idObjeto")]
        public int IdObjeto { get; set; }

        [JsonPropertyName("idInstituto")]
        public int IdInstituto { get; set; }

        [JsonPropertyName("objeto")]
        public string NombreObjeto { get; set; }

        [JsonPropertyName("descripcion")]
        public string Descripcion { get; set; }

        [JsonPropertyName("tipoObjeto")]
        public string TipoObjeto { get; set; }

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
