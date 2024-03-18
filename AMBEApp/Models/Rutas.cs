using System.Text.Json.Serialization;

namespace AMBEApp.Models
{
    public class Rutas
    {
        [JsonPropertyName("idruta")]
        public int IdRuta { get; set; }

        [JsonPropertyName("idinstituto")]
        public int IdInstituto { get; set; }

        [JsonPropertyName("nombreRuta")]
        public string NombreRuta { get; set; }

        [JsonPropertyName("origen")]
        public string Origen { get; set; }

        [JsonPropertyName("destino")]
        public string Destino { get; set; }

        [JsonPropertyName("distancia")]
        public decimal Distancia { get; set; }

        [JsonPropertyName("colonias")]
        public List<string> Colonias { get; set; }

        [JsonPropertyName("departamento")]
        public string Departamento { get; set; }

        [JsonPropertyName("municipio")]
        public string Municipio { get; set; }

        [JsonPropertyName("descripcion")]
        public string Descripcion { get; set; }

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
