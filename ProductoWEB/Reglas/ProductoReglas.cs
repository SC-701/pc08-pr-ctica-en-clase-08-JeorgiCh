using Abstracciones.Modelos;
using Abstracciones.Reglas;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Reglas
{
    public class ProductoReglas : IProductoReglas
    {
        private IConfiguration _configuration;

        public ProductoReglas(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string ObtenerMetodo(string seccion, string nombre)
        {
            string UrlBase = ObtenerUrlBase(seccion);
            var Metodo = _configuration.GetSection(seccion).Get<ApiEndPoint>().Metodos.Where(m => m.Nombre == nombre).FirstOrDefault().Url;
            return $"{UrlBase}/{Metodo}";
        }

        private string ObtenerUrlBase(string seccion)
        {
            return _configuration.GetSection(seccion).Get<ApiEndPoint>().UrlBase;
        }
    }
}
