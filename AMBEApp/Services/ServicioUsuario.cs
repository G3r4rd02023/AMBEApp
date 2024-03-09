using AMBEApp.Models;
using System.Text;
using System.Text.Json;

namespace AMBEApp.Services
{
    public class ServicioUsuario
    {
        private readonly string urlApi = "https://ambetest.somee.com/api/Usuarios";
        public static string UsuarioAutenticado { get; private set; }

        public static void SetUsuarioAutenticado(string username)
        {
            UsuarioAutenticado = username;
        }

        public async Task<List<Usuarios>> ObtenerLista()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(urlApi);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
                var usuariosData = JsonSerializer.Deserialize<List<Usuarios>>(responseBody);
                return usuariosData;
            }
            else
            {
                Console.WriteLine($"Error en la solicitud: {response.StatusCode}");
                return [];
            }
        }

        public async Task<bool> ValidarPrimerLogin(string usuario)
        {
            var listaUsuarios = await ObtenerLista();

            var usuarioEncontrado = listaUsuarios.FirstOrDefault(u => u.NombreUsuario == usuario || u.Usuario == usuario);

            if (usuarioEncontrado != null && usuarioEncontrado.Estado == "Nuevo")
            {
                //accede directamente a menu
                return false;
            }
            else
            {
                //envia a registro
                return true;
            }
        }

        public async Task<bool> UsuarioExiste(string usuario)
        {
            try
            {               
                var usuarios = await ObtenerLista();
                var user = usuarios.FirstOrDefault(u => u.Usuario == usuario);

                if (user != null)
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
                Console.WriteLine($"Error al obtener los usuarios: {ex.Message}");
                return false;
            }
        }

        public static async Task<bool> RegistrarUsuario(string personaJson)
        {
            try
            {
                var httpClient = new HttpClient();
                var content = new StringContent(personaJson, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync("https://ambetest.somee.com/api/Usuarios", content);

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
                Console.WriteLine($"Error al registrar usuario: {ex.Message}");
                return false;
            }
        }

        public async Task<int> ObtenerIdUsuario(string usuario)
        {
            try
            {
                var usuarios = await ObtenerLista();
                var user = usuarios.FirstOrDefault(u => u.Usuario == usuario);
                return user != null ? user.IdUsuario : -1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los usuarios: {ex.Message}");
                return -1;
            }
        }

        public async Task<string> ObtenerNombreUsuario(int idUsuario)
        {
            try
            {
                var usuarios = await ObtenerLista();
                var usuarioEncontrado = usuarios.FirstOrDefault(r => r.IdUsuario == idUsuario);
                return usuarioEncontrado != null ? usuarioEncontrado.NombreUsuario : "Usuario no encontrado";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el nombre del usuario: {ex.Message}");
                return "Error";
            }
        }
    }
}
