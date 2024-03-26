using AMBEApp.Models;
using System.Text;
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

        //Actualizar Marca
        public async Task<bool> ActualizarMarca(string marcaJson, Marca marcaEditada)
        {
            try
            {
                using var httpClient = new HttpClient();
                var content = new StringContent(marcaJson, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PutAsync($"{urlApi}/{marcaEditada.IdMarca}", content);

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

        //eliminar marca
        public async Task<bool> EliminarMarca(int idMarca)
        {
            try
            {
                using var httpClient = new HttpClient();
                HttpResponseMessage response = await httpClient.DeleteAsync($"{urlApi}/{idMarca}");

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error al eliminar la marca: {errorMessage}");
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