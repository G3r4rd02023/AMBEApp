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

        public async Task<List<Objeto>> ObtenerLista()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(urlApi);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
                var objetoData = JsonSerializer.Deserialize<List<Objeto>>(responseBody);
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
                var objetoEncontrado = objetos.FirstOrDefault(u => u.NombreObjeto == nombreObjeto);

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

        public async Task<bool> ActualizarObjeto(string objetoJson, Objeto objetoEditado)
        {
            try
            {
                using var httpClient = new HttpClient();
                var content = new StringContent(objetoJson, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PutAsync($"{urlApi}/{objetoEditado.IdObjeto}", content);

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

        public async Task<bool> EliminarObjeto(int idObjeto)
        {
            try
            {
                using var httpClient = new HttpClient();
                HttpResponseMessage response = await httpClient.DeleteAsync($"{urlApi}/{idObjeto}");

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {                   
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error al eliminar el objeto: {errorMessage}");
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
