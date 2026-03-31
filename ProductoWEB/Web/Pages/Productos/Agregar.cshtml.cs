using Abstracciones.Modelos;
using Abstracciones.Reglas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace Web.Pages.Productos
{
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
            var httpProducto = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Post, endpoint);

            var respuesta = await httpProducto.PostAsJsonAsync(endpoint, producto);
            respuesta.EnsureSuccessStatusCode();

            return RedirectToPage("./Index");
        }
    }
}

