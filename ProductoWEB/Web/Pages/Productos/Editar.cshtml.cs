using Abstracciones.Modelos;
using Abstracciones.Reglas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Web.Pages.Productos;

[Authorize(Roles = "2")]
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
        var httpCliente = ObtenerClienteConToken();
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
        var httpCliente = ObtenerClienteConToken();

        var request = new ProductoRequest
        {
            Nombre = producto.Nombre,
            Descripcion = producto.Descripcion,
            Precio = producto.Precio,
            Stock = producto.Stock,
            CodigoBarras = producto.CodigoBarras,
            IdSubCategoria = IdSubCategoria 
        };

        var respuesta = await httpCliente.PutAsJsonAsync(
            string.Format(endpoint, producto.Id),
            request);

        if (respuesta.IsSuccessStatusCode)
        {
            return RedirectToPage("./Index");
        }

        ModelState.AddModelError(string.Empty, "Error al actualizar en el API.");
        return Page();
    }

    private HttpClient ObtenerClienteConToken()
    {
        var tokenClaim = HttpContext.User.Claims
            .FirstOrDefault(c => c.Type == "Token");
        var cliente = new HttpClient();
        if (tokenClaim != null)
            cliente.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue(
                    "Bearer", tokenClaim.Value);
        return cliente;
    }
}
