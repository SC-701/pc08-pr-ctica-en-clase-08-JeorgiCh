using Abstracciones.Interfaces.Reglas;
using Abstracciones.Interfaces.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reglas
{
    public class ProductoReglas : IProductoReglas
    {
        private readonly ITipoCambioServicio _tipoCambioServicio;

        public ProductoReglas(ITipoCambioServicio tipoCambioServicio)
        {
            _tipoCambioServicio = tipoCambioServicio;
        }

        public async Task<decimal> ConvertirPrecioADolares(decimal precioColones)
        {
            try
            {
                decimal tipoCambio = await _tipoCambioServicio.ObtenerTipoCambioVenta();

                if (tipoCambio <= 0)
                    throw new Exception("El tipo de cambio obtenido no es válido.");

                decimal precioDolares = precioColones / tipoCambio;
                return Math.Round(precioDolares, 2);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al calcular la conversión de moneda.", ex);
            }
        }
    }
}
