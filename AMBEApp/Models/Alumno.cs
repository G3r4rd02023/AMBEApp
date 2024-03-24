using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBEApp.Models
{
    public class Alumno
    {
        public int IdPersona { get; set; }

        public int IdTipoPersona { get; set; }

        public int IdInstituto { get; set; }

        public string PrimerNombre { get; set; }

        public string SegundoNombre { get; set; }

        public string PrimerApellido { get; set; }

        public string SegundoApellido { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public string Genero { get; set; }

        public string CreadoPor { get; set; }

        public DateTime FechaCreacion { get; set; }

        public string ModificadoPor { get; set; }

        public DateTime FechaModificacion { get; set; }

        public string Estado { get; set; }
    }
}
