using AMBEApp.Models;
using System.Text.Json;

namespace AMBEApp.Services
{

    public class ServicioRoles
    {
        private readonly string urlApi = "https://ambetest.somee.com/api/Roles";

        public async Task<List<Roles>> ObtenerLista()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(urlApi);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
                var rolesData = JsonSerializer.Deserialize<List<Roles>>(responseBody);
                return rolesData;
            }
            else
            {                               
                Console.WriteLine($"Error en la solicitud: {response.StatusCode}");
                return [];
            }
        }

        public async Task<int> ObtenerIdRolPorNombre(string nombreRol)
        {
            try
            {                
                var roles = await ObtenerLista();                
                var rol = roles.FirstOrDefault(r => r.Descripcion == nombreRol);               
                return rol != null ? rol.IdRol : -1;
            }
            catch (Exception ex)
            {                
                Console.WriteLine($"Error al obtener los roles: {ex.Message}");
                return -1;
            }
        }

        public async Task<bool> RolExiste(string nombreRol)
        {
            try
            {
                var roles = await ObtenerLista();
                var rolEncontrado = roles.FirstOrDefault(u => u.Descripcion == nombreRol);

                if (rolEncontrado != null)
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

        public async Task<string> ObtenerNombreRol(int idRol)
        {
            try
            {
                var roles = await ObtenerLista();
                var rolEncontrado = roles.FirstOrDefault(r => r.IdRol == idRol);
                return rolEncontrado != null ? rolEncontrado.Descripcion : "Rol no encontrado";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el nombre del rol: {ex.Message}");
                return "Error";
            }
        }

    }
}
