using AMBEApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AMBEApp.Services
{
    public class ServicioGrados
    {
        private readonly string urlApi = "https://ambetest.somee.com/api/Grados";

        public async Task<List<Grado>> ObtenerLista()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(urlApi);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
                var objetoData = JsonSerializer.Deserialize<List<Grado>>(responseBody);
                return objetoData;
            }
            else
            {
                Console.WriteLine($"Error en la solicitud: {response.StatusCode}");
                return [];
            }
        }

        public async Task<bool> GradoExiste(string nombreGrado)
        {
            try
            {
                var grados = await ObtenerLista();
                var gradoEncontrado = grados.FirstOrDefault(u => u.NombreGrado == nombreGrado);

                if (gradoEncontrado != null)
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
                Console.WriteLine($"Error al obtener los grados: {ex.Message}");
                return false;
            }
        }

        //obtener id por nombre
        public async Task<int> ObtenerIdGradoPorNombre(string nombreGrado)
        {
            try
            {
                var grados = await ObtenerLista();
                var grado = grados.FirstOrDefault(r => r.NombreGrado == nombreGrado);
                return grado != null ? grado.IdGrado : -1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los roles: {ex.Message}");
                return -1;
            }
        }



        public async Task<bool> ActualizarGrado(string gradoJson, Grado gradoEditado)
        {
            try
            {
                using var httpClient = new HttpClient();
                var content = new StringContent(gradoJson, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PutAsync($"{urlApi}/{gradoEditado.IdGrado}", content);

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

        public async Task<bool> EliminarObjeto(int idGrado)
        {
            try
            {
                using var httpClient = new HttpClient();
                HttpResponseMessage response = await httpClient.DeleteAsync($"{urlApi}/{idGrado}");

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error al eliminar el grado: {errorMessage}");
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
