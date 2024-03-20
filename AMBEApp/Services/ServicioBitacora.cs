using AMBEApp.Models;
using System.Text;
using System.Text.Json;

namespace AMBEApp.Services
{
    public class ServicioBitacora
    {
        public static async Task<bool> AgregarRegistro(int idUsuario, int idInstituto, string tipoAccion, string tabla)
        {

            var registro = new Bitacora
            {
                IdUsuario = idUsuario,
                IdInstituto = idInstituto,
                TipoAccion = tipoAccion,
                Tabla = tabla,
                Fecha = DateTime.Now
            };

            try
            {
                var jsonBitacora = JsonSerializer.Serialize(registro);
                HttpClient httpClient = new();
                var content = new StringContent(jsonBitacora, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync("https://ambetest.somee.com/api/Bitacora", content);

                if (response.IsSuccessStatusCode)
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
                Console.WriteLine($"Error al registrar bitacora: {ex.Message}");
                return false;
            }
        }
    }
}
