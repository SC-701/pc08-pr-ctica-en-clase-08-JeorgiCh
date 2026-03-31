using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    using System.Text.Json.Serialization;

    namespace TuProyecto.Models
    {
        public class TipoCambio
        {
            [JsonPropertyName("estado")]
            public bool Estado { get; set; }

            [JsonPropertyName("mensaje")]
            public string Mensaje { get; set; }

            [JsonPropertyName("datos")]
            public List<DatoBanco> Datos { get; set; }
        }

        public class DatoBanco
        {
            [JsonPropertyName("titulo")]
            public string Titulo { get; set; }

            [JsonPropertyName("periodicidad")]
            public string Periodicidad { get; set; }

            [JsonPropertyName("indicadores")]
            public List<Indicador> Indicadores { get; set; }
        }

        public class Indicador
        {
            [JsonPropertyName("codigoIndicador")]
            public string CodigoIndicador { get; set; }

            [JsonPropertyName("nombreIndicador")]
            public string NombreIndicador { get; set; }

            [JsonPropertyName("series")]
            public List<SerieHistorica> Series { get; set; }
        }

        public class SerieHistorica
        {
            [JsonPropertyName("fecha")]
            public DateTime Fecha { get; set; }

            [JsonPropertyName("valorDatoPorPeriodo")]
            public decimal ValorDatoPorPeriodo { get; set; }
        }
    }
}
