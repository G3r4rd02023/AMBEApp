using AMBEApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AMBEApp.Services
{
    public class ServicioRutas
    {
        private readonly string urlApi = "https://ambetest.somee.com/api/Rutas";

        public async Task<List<Ruta>> ObtenerLista()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(urlApi);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
                var rutaData = JsonSerializer.Deserialize<List<Ruta>>(responseBody);
                return rutaData;
            }
            else
            {
                Console.WriteLine($"Error en la solicitud: {response.StatusCode}");
                return [];
            }
        }

        public async Task<bool> RutaExiste(string nombreRuta)
        {
            try
            {
                var rutas = await ObtenerLista();
                var rutaEncontrada = rutas.FirstOrDefault(u => u.NombreRuta == nombreRuta);

                if (rutaEncontrada != null)
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
                Console.WriteLine($"Error al obtener las rutas: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> ActualizarRuta(string rutaJson, Ruta rutaEditada)
        {
            try
            {
                using var httpClient = new HttpClient();
                var content = new StringContent(rutaJson, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PutAsync($"{urlApi}/{rutaEditada.IdRuta}", content);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error en la solicitud HTTP: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado: {ex.Message}");
                return false;
            }
        }
    }
}
