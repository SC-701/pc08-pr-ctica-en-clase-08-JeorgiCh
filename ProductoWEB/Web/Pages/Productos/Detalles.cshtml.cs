using Abstracciones.Modelos;
using Abstracciones.Reglas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Text.Json;

namespace Web.Pages.Productos
{
    public class DetallesModel : PageModel
    {
        private IProductoReglas _productoReglas;
        public ProductoResponse producto { get; set; } = default!;
        public DetallesModel(IProductoReglas productoReglas)
        {
            _productoReglas = productoReglas;
        }

        public async Task OnGet(Guid? id)
        {
            string endpoint = _productoReglas.ObtenerMetodo("ApiEndPoints", "ObtenerProductoPorId");
            var cliente = new HttpClient();

            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, id));
            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                producto = JsonSerializer.Deserialize<ProductoResponse>(resultado, opciones);
            }
        }
    }
}
