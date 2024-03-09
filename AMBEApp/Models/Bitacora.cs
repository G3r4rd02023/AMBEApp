using AMBEApp.Services;
using System.Text.Json.Serialization;

namespace AMBEApp.Models
{
    public class Bitacora
    {
        [JsonPropertyName("idUsuario")]
        public int IdUsuario { get; set; }

        [JsonPropertyName("idInstituto")]
        public int IdInstituto { get; set; }

        [JsonPropertyName("tipoAccion")]
        public string TipoAccion { get; set; }

        [JsonPropertyName("tabla")]
        public string Tabla { get; set; }

        [JsonPropertyName("fecha")]
        public DateTime Fecha { get; set; }

        public Task<string> NombreInstituto
        {
            get
            {
                ServicioInstituto servicioInstituto = new();
                return servicioInstituto.ObtenerNombreInstituto(IdInstituto);
            }
        }

        public Task<string> NombreUsuario
        {
            get
            {
                ServicioUsuario servicioUsuario = new();
                return servicioUsuario.ObtenerNombreUsuario(IdUsuario);
            }
        }
    }
}
