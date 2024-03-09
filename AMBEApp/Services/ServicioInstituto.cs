using AMBEApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AMBEApp.Services
{
    public class ServicioInstituto
    {
        private readonly string urlApi = "https://ambetest.somee.com/api/Institutos";

        public async Task<List<Institutos>> ObtenerLista()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(urlApi);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
                var institutoData = JsonSerializer.Deserialize<List<Institutos>>(responseBody);
                return institutoData;
            }
            else
            {                
                Console.WriteLine($"Error en la solicitud: {response.StatusCode}");
                return [];
            }
        }

        public async Task<int> ObtenerIdInstitutoPorNombre(string nombreInstituto)
        {
            try
            {
                var institutos = await ObtenerLista();
                var instituto = institutos.FirstOrDefault(r => r.NombreInstituto == nombreInstituto);
                return instituto != null ? instituto.IdInstituto : -1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los institutos: {ex.Message}");
                return -1;
            }
        }

        public async Task<string> ObtenerNombreInstituto(int idInstituto)
        {
            try
            {
                var institutos = await ObtenerLista();
                var institutoEncontrado = institutos.FirstOrDefault(r => r.IdInstituto == idInstituto);
                return institutoEncontrado != null ? institutoEncontrado.NombreInstituto : "Rol no encontrado";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el nombre del instituto: {ex.Message}");
                return "Error";
            }
        }

        public async Task<bool> InstitutoExiste(string nombreInstituto)
        {
            try
            {
                var institutos = await ObtenerLista();
                var institutoEncontrado = institutos.FirstOrDefault(u => u.NombreInstituto == nombreInstituto);

                if (institutoEncontrado != null)
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
