using AMBEApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AMBEApp.Services
{
    public class ServicioTipoViaje
    {
        private readonly string urlApi = "https://ambetest.somee.com/api/TipoViajes";

        public async Task<List<TipoViaje>> ObtenerLista()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(urlApi);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
                var objetoData = JsonSerializer.Deserialize<List<TipoViaje>>(responseBody);
                return objetoData;
            }
            else
            {
                Console.WriteLine($"Error en la solicitud: {response.StatusCode}");
                return [];
            }
        }

        public async Task<bool> TipoViajeExiste(string nombreTipoViaje)
        {
            try
            {
                var tiposViaje = await ObtenerLista();
                var tipoViajeEncontrado = tiposViaje.FirstOrDefault(u => u.Evento == nombreTipoViaje);

                if (tipoViajeEncontrado != null)
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
                Console.WriteLine($"Error al obtener los objetos: {ex.Message}");
                return false;
            }
        }
    }
}
