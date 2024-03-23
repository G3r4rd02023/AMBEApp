using AMBEApp.Models;
using System.Text.Json;

namespace AMBEApp.Services
{
    public class ServicioModelo
    {
        private readonly string urlApi = "https://ambetest.somee.com/api/Modelos";

        public async Task<List<Modelo>> ObtenerLista()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(urlApi);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
                var modelosData = JsonSerializer.Deserialize<List<Modelo>>(responseBody);
                return modelosData;
            }
            else
            {
                Console.WriteLine($"Error en la solicitud: {response.StatusCode}");
                return [];
            }
        }

        public async Task<bool> ModeloExiste(string nombreModelo)
        {
            try
            {
                var modelos = await ObtenerLista();
                var modeloEncontrado = modelos.FirstOrDefault(u => u.NombreModelo == nombreModelo);

                if (modeloEncontrado != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los roles: {ex.Message}");
                return false;
            }
        }
    }
}
