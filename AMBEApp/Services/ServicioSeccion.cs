using AMBEApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AMBEApp.Services
{
    public class ServicioSeccion
    {
        private readonly string urlApi = "https://ambetest.somee.com/api/Secciones";

        public async Task<List<Seccion>> ObtenerLista()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(urlApi);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
                var seccionData = JsonSerializer.Deserialize<List<Seccion>>(responseBody);
                return seccionData;
            }
            else
            {
                Console.WriteLine($"Error en la solicitud: {response.StatusCode}");
                return [];
            }
        }

        public async Task<int> ObtenerIdSeccionPorNombre(string nombreSeccion)
        {
            try
            {
                var secciones = await ObtenerLista();
                var seccion = secciones.FirstOrDefault(r => r.NombreSeccion == nombreSeccion);
                return seccion != null ? seccion.IdSeccion : -1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener las secciones: {ex.Message}");
                return -1;
            }
        }


        public async Task<bool> SeccionExiste(string nombreSeccion)
        {
            try
            {
                var secciones = await ObtenerLista();
                var seccionEncontrada = secciones.FirstOrDefault(u => u.NombreSeccion == nombreSeccion);

                if (seccionEncontrada != null)
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
                Console.WriteLine($"Error al obtener las secciones: {ex.Message}");
                return false;
            }
        }

        public async Task<string> ObtenerNombreSeccion(int idSeccion)
        {
            try
            {
                var secciones = await ObtenerLista();
                var seccionEncontrada = secciones.FirstOrDefault(r => r.IdSeccion == idSeccion);
                return seccionEncontrada != null ? seccionEncontrada.NombreSeccion : "Seccion no encontrado";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener la seccion: {ex.Message}");
                return "Error";
            }
        }
    }
}