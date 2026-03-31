using Abstracciones.Modelos;
using Abstracciones.Reglas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Web.Pages.Productos;

public class EditarModel : PageModel
{
    private readonly IProductoReglas _productoReglas;

    public EditarModel(IProductoReglas productoReglas)
    {
        _productoReglas = productoReglas;
    }

    [BindProperty]
    public ProductoResponse producto { get; set; }

    [BindProperty]
    public Guid IdSubCategoria { get; set; }

    public async Task<ActionResult> OnGet(Guid? id)
    {
        if (id == null || id == Guid.Empty)
            return NotFound();

        string endpoint = _productoReglas.ObtenerMetodo("ApiEndPoints", "ObtenerProductoPorId");
        var httpCliente = new HttpClient();
        var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, id));

        var respuesta = await httpCliente.SendAsync(solicitud);

        if (respuesta.IsSuccessStatusCode)
        {
            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            producto = JsonSerializer.Deserialize<ProductoResponse>(resultado, opciones);
        }

        return Page();
    }

    public async Task<ActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        string endpoint = _productoReglas.ObtenerMetodo("ApiEndPoints", "ActualizarProducto");
        var httpproducto = new HttpClient();

        var request = new ProductoRequest
        {
            Nombre = producto.Nombre,
            Descripcion = producto.Descripcion,
            Precio = producto.Precio,
            Stock = producto.Stock,
            CodigoBarras = producto.CodigoBarras,
            IdSubCategoria = IdSubCategoria 
        };

        var respuesta = await httpproducto.PutAsJsonAsync(
            string.Format(endpoint, producto.Id),
            request);

        if (respuesta.IsSuccessStatusCode)
        {
            return RedirectToPage("./Index");
        }

        ModelState.AddModelError(string.Empty, "Error al actualizar en el API.");
        return Page();
    }
}
