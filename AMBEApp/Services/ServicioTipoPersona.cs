using AMBEApp.Models;
using System.Text.Json;

namespace AMBEApp.Services
{
    public class ServicioTipoPersona
    {
        private readonly string urlApi = "https://ambetest.somee.com/api/TipoPersonas";

        public async Task<List<TipoPersonas>> ObtenerLista()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(urlApi);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
                var tipoPersonasData = JsonSerializer.Deserialize<List<TipoPersonas>>(responseBody);
                return tipoPersonasData;
            }
            else
            {
                Console.WriteLine($"Error en la solicitud: {response.StatusCode}");
                return [];
            }
        }

        public async Task<int> ObtenerIdTipoPersonaPorNombre(string tipoPersona)
        {
            try
            {
                var tipoPersonas = await ObtenerLista();
                var tipo = tipoPersonas.FirstOrDefault(r => r.TipoPersona == tipoPersona);
                return tipo != null ? tipo.IdTipoPersona : -1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los tipos de persona: {ex.Message}");
                return -1;
            }
        }
    }
}
