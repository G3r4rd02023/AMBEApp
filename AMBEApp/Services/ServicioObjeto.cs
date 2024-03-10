using AMBEApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AMBEApp.Services
{
    public class ServicioObjeto
    {
        private readonly string urlApi = "https://ambetest.somee.com/api/Objetos";

        public async Task<List<Objetos>> ObtenerLista()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(urlApi);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
                var objetoData = JsonSerializer.Deserialize<List<Objetos>>(responseBody);
                return objetoData;
            }
            else
            {
                Console.WriteLine($"Error en la solicitud: {response.StatusCode}");
                return [];
            }
        }

        public async Task<bool> ObjetoExiste(string nombreObjeto)
        {
            try
            {
                var objetos = await ObtenerLista();
                var objetoEncontrado = objetos.FirstOrDefault(u => u.Objeto == nombreObjeto);

                if (objetoEncontrado != null)
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
