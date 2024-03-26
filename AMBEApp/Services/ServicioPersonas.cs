using AMBEApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AMBEApp.Services
{
    public class ServicioPersonas
    {
        private readonly string urlApi = "https://ambetest.somee.com/api/Personas";

        public async Task<List<Persona>> ObtenerLista()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(urlApi);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
                var personasData = JsonSerializer.Deserialize<List<Persona>>(responseBody);
                return personasData;
            }
            else
            {
                Console.WriteLine($"Error en la solicitud: {response.StatusCode}");
                return [];
            }
        }

        public async Task<int> ObtenerIdPersonaPorNombre(string nombrePersona)
        {
            try
            {
                var personas = await ObtenerLista();
                var persona = personas.FirstOrDefault(r => r.NombreCompleto == nombrePersona);
                return persona != null ? persona.IdPersona : -1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los tipos de persona: {ex.Message}");
                return -1;
            }
        }



    }
}
