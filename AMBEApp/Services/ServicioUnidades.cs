using AMBEApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AMBEApp.Services
{
    public class ServicioUnidades
    {
        private readonly string urlApi = "https://ambetest.somee.com/api/Unidades";

        public async Task<List<Unidad>> ObtenerLista()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(urlApi);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
                var unidadData = JsonSerializer.Deserialize<List<Unidad>>(responseBody);
                return unidadData;
            }
            else
            {
                Console.WriteLine($"Error en la solicitud: {response.StatusCode}");
                return [];
            }
        }

     

        public async Task<bool> UnidadExiste(string placaUnidad)
        {
            try
            {
                var unidades = await ObtenerLista();
                var unidadEncontrada = unidades.FirstOrDefault(u => u.Placa == placaUnidad);

                if (unidadEncontrada != null)
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
                Console.WriteLine($"Error al obtener las unidades: {ex.Message}");
                return false;
            }
        }

       
    }
}
