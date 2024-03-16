using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AMBEApp.Models
{
    public class Incidentes
    {
        [JsonPropertyName("idIncidente")]
        public int IdIncidente { get; set; }

        [JsonPropertyName("idViaje")]
        public int? IdViaje { get; set; }

        [JsonPropertyName("descripcion")]
        public string Descripcion { get; set; }

        [JsonPropertyName("idInstituto ")]
        public int? IdInstituto { get; set; }

        [JsonPropertyName("creadoPor")]
        public string CreadoPor { get; set; }

        [JsonPropertyName("fechadecreacion")]
        public DateTime? FechaDeCreacion { get; set; }

        [JsonPropertyName("modificadoPor")]
        public string ModificadoPor { get; set; }

        [JsonPropertyName("fechadeModificacion")]
        public DateTime? FechaDeModificacion { get; set; }
    }
}
