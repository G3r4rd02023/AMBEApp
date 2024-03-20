using AMBEApp.Models;
using System.Text;
using System.Text.Json;

namespace AMBEApp.Services
{
    public class ServicioUsuario
    {
        private readonly string urlApi = "https://ambetest.somee.com/api/Usuarios";
        public static string UsuarioAutenticado { get; private set; }

        public static ImageSource ImagenUsuario { get; private set; }
        public static void SetUsuarioAutenticado(string username)
        {
            UsuarioAutenticado = username;
        }
      
        public static void SetImagenUsuario(ImageSource imagenUsuario)
        {
            ImagenUsuario = imagenUsuario;
        }

        public async Task<List<Usuarios>> ObtenerLista()
        {
            using var client = new HttpClient();
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
          
            if (usuarioEncontrado == null)
            {
                return true;
            }          
            else
            {                
                //acceso directo
                return false;
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

        public async Task<bool> RegistrarUsuario(string personaJson)
        {
            try
            {
                using var httpClient = new HttpClient();
                var content = new StringContent(personaJson, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync(urlApi, content);

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

        public async Task<bool> ActualizarUsuario(string userJson, Usuarios user)
        {
            try
            {
                using var httpClient = new HttpClient();               
                var content = new StringContent(userJson, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PutAsync($"{urlApi}/{user.IdUsuario}", content);

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

        public async Task<bool> VerificarRol(int idRol)
        {
            try
            {
                var usuarios = await ObtenerLista();
                var usuarioEncontrado = usuarios.FirstOrDefault(r => r.Usuario == UsuarioAutenticado);

                if (usuarioEncontrado != null && usuarioEncontrado.IdRol == idRol)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el rol del usuario: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> ValidarUsuarioActivo(string nombreUsuario)
        {
            try
            {
                var usuarios = await ObtenerLista();
                var user = usuarios.FirstOrDefault(u => u.Usuario == nombreUsuario);

                if (user == null)
                {
                    return false;
                }

                if (user.Estado == "Activo")
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
                Console.WriteLine($"Error al validar el usuario activo: {ex.Message}");
                return false;
            }
        }
    }
}
