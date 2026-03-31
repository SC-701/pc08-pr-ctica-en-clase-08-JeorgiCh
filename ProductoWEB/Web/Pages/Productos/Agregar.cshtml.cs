using Abstracciones.Modelos;
using Abstracciones.Reglas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace Web.Pages.Productos
{
    [Authorize(Roles = "2")]
    public class AgregarModel : PageModel
    {
        private readonly IProductoReglas _productoReglas;

        public AgregarModel(IProductoReglas productoReglas)
        {
            _productoReglas = productoReglas;
        }

        [BindProperty]
        public ProductoRequest producto { get; set; } = default!;

        public async Task<ActionResult> OnGet()
        {
            return Page();
        }

        public async Task<ActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string endpoint = _productoReglas.ObtenerMetodo("ApiEndPoints", "AgregarProducto");
            var httpCliente = ObtenerClienteConToken();
            var solicitud = new HttpRequestMessage(HttpMethod.Post, endpoint);

            var respuesta = await httpCliente.PostAsJsonAsync(endpoint, producto);
            respuesta.EnsureSuccessStatusCode();

            return RedirectToPage("./Index");
        }

        private HttpClient ObtenerClienteConToken()
        {
            var tokenClaim = HttpContext.User.Claims
                .FirstOrDefault(c => c.Type == "AccessToken");
            var cliente = new HttpClient();
            if (tokenClaim != null)
                cliente.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue(
                        "Bearer", tokenClaim.Value);
            return cliente;
        }
    }
}

