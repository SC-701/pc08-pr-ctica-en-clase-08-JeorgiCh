using Abstracciones.Interfaces.Reglas;
using Abstracciones.Interfaces.Servicios;
using Abstracciones.Modelos;
using Abstracciones.Modelos.TuProyecto.Models;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Servicios
{
    public class TipoCambioServicio : ITipoCambioServicio
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public TipoCambioServicio(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<decimal> ObtenerTipoCambioVenta()
{
    try
    {
        var urlBase = _configuration["BancoCentralCR:UrlBase"];
        var token = _configuration["BancoCentralCR:BearerToken"];

        string fecha = DateTime.Now.ToString("yyyy/MM/dd");

        string urlFinal = $"{urlBase}?fechaInicio={fecha}&fechaFin={fecha}&idioma=ES";

        _httpClient.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.GetAsync(urlFinal);
        
        if (response.IsSuccessStatusCode)
        {
            var contenido = await response.Content.ReadFromJsonAsync<TipoCambio>();

                    if (contenido != null && contenido.Estado && contenido.Datos.Any())
                    {
                        return contenido.Datos
                            .First().Indicadores
                            .First().Series
                            .First().ValorDatoPorPeriodo;
                    }
                    return contenido.Datos[0].Indicadores[0].Series[0].ValorDatoPorPeriodo;
        }

        throw new Exception($"Error BCCR: {response.StatusCode}");
    }
    catch (Exception ex)
    {
        throw new Exception("Error en la respuesta del BCCR: " + ex.Message);
    }
}
    }
}
