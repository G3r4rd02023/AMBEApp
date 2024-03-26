using System.Text.Json.Serialization;

namespace AMBEApp.Models
{
    public class Unidad
    {
        [JsonPropertyName("idUnidad")]
        public int IdUnidad { get; set; }

        [JsonPropertyName("numeroUnidad")]
        public string NumeroUnidad { get; set; }

        [JsonPropertyName("placa")]
        public string Placa { get; set; }

        [JsonPropertyName("color")]
        public string Color { get; set; }

        [JsonPropertyName("capacidad")]
        public int Capacidad { get; set; }

        [JsonPropertyName("chasis")]
        public string Chasis { get; set; }

        [JsonPropertyName("idPersonaConductor")]
        public int IdPersonaConductor { get; set; }

        [JsonPropertyName("idModelo")]
        public int IdModelo { get; set; }

        [JsonPropertyName("idMarca")]
        public int IdMarca { get; set; }

        [JsonPropertyName("idInstituto")]
        public int IdInstituto { get; set; }

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
