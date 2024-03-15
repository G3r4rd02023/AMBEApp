using System.Text.Json.Serialization;

namespace AMBEApp.Models
{
    public class Persona
    {
        [JsonPropertyName("idPersona")]
        public int IdPersona { get; set; }

        [JsonPropertyName("idTipoPersona")]
        public int IdTipoPersona { get; set; }

        [JsonPropertyName("idInstituto")]
        public int IdInstituto { get; set; }

        [JsonPropertyName("primerNombre")]
        public string PrimerNombre { get; set; }

        [JsonPropertyName("segundoNombre")]
        public string SegundoNombre { get; set; }

        [JsonPropertyName("primerApellido")]
        public string PrimerApellido { get; set; }

        [JsonPropertyName("segundoApellido")]
        public string SegundoApellido { get; set; }

        [JsonPropertyName("fechaNacimiento")]
        public DateTime FechaNacimiento { get; set; }

        [JsonPropertyName("genero")]
        public string Genero { get; set; }

        [JsonPropertyName("usuario")]
        public string Usuario { get; set; }

        [JsonPropertyName("nombreUsuario")]
        public string NombreUsuario { get; set; }

        [JsonPropertyName("correoElectronico")]
        public string CorreoElectronico { get; set; }

        [JsonPropertyName("contraseña")]
        public string Contraseña { get; set; }

        [JsonPropertyName("estado")]
        public string Estado { get; set; }

        [JsonPropertyName("idRol")]
        public int IdRol { get; set; }

        [JsonPropertyName("fechaUltimaConexion")]
        public DateTime FechaUltimaConexion { get; set; }

        [JsonPropertyName("creadoPor")]
        public string CreadoPor { get; set; }

        [JsonPropertyName("modificadoPor")]
        public string ModificadoPor { get; set; }

        [JsonPropertyName("nombreCompleto")]
        public string NombreCompleto
        {
            get { return $"{PrimerNombre} {PrimerApellido}"; }
        }

    }
}
