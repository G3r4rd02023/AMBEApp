using AMBEApp.Models;
using System.Text.Json;
namespace AMBEApp.Services
{
    public class ServicioParentesco
    {
        private readonly string urlApi = "https://ambetest.somee.com/api/Parentescos";

        public async Task<List<Parentesco>> ObtenerLista()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(urlApi);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
                var objetoData = JsonSerializer.Deserialize<List<Parentesco>>(responseBody);
                return objetoData;
            }
            else
            {
                Console.WriteLine($"Error en la solicitud: {response.StatusCode}");
                return [];
            }
        }        
    }
}
