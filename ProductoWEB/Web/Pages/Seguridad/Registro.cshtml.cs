// Registro.cshtml.cs
using Abstracciones.Modelos.Seguridad;
using Abstracciones.Reglas;
using Autorizacion.Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Reglas;

namespace Web.Pages.Cuenta
{
    public class RegistroModel : PageModel
    {
        [BindProperty]
        public Usuario usuario { get; set; } = default!;
        private IProductoReglas _configuracion;

        public RegistroModel(IProductoReglas configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid) return Page();

            var hash = Autenticacion.GenerarHash(usuario.Password);
            usuario.PasswordHash = Autenticacion.ObtenerHash(hash);

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPointsSeguridad", "Registro");
            var cliente = new HttpClient();
            var respuesta = await cliente.PostAsJsonAsync<UsuarioBase>(endpoint, usuario);
            if (!respuesta.IsSuccessStatusCode)
            {
                // Esto te leerá el error real que viene desde Azure
                var contenidoError = await respuesta.Content.ReadAsStringAsync();
                throw new Exception($"Error en la API: {contenidoError}");
            }
            return RedirectToPage("../Index");
        }
    }
}