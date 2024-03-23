using AMBEApp.Models;
using System.Text.Json;

namespace AMBEApp.Services
{
    public class ServicioMarcas
    {
        private readonly string urlApi = "https://ambetest.somee.com/api/Marcas";

        public async Task<List<Marca>> ObtenerLista()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(urlApi);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
                var marcaData = JsonSerializer.Deserialize<List<Marca>>(responseBody);
                return marcaData;
            }
            else
            {
                Console.WriteLine($"Error en la solicitud: {response.StatusCode}");
                return [];
            }
        }

        public async Task<int> ObtenerIdMarcaPorNombre(string nombreMarca)
        {
            try
            {
                var marcas = await ObtenerLista();
                var marca = marcas.FirstOrDefault(r => r.NombreMarca == nombreMarca);
                return marca != null ? marca.IdMarca : -1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los roles: {ex.Message}");
                return -1;
            }
        }


        public async Task<bool> MarcaExiste(string nombreMarca)
        {
            try
            {
                var marcas = await ObtenerLista();
                var marcaEncontrada = marcas.FirstOrDefault(u => u.NombreMarca == nombreMarca);

                if (marcaEncontrada != null)
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

        public async Task<string> ObtenerNombreMarca(int idMarca)
        {
            try
            {
                var marcas = await ObtenerLista();
                var marcaEncontrado = marcas.FirstOrDefault(r => r.IdMarca == idMarca);
                return marcaEncontrado != null ? marcaEncontrado.NombreMarca : "Marca no encontrado";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el nombre de la marca: {ex.Message}");
                return "Error";
            }
        }
    }
}
