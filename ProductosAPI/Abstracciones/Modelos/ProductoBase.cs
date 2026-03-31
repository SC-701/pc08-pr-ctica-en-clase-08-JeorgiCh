using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class ProductoBase
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La descripción es necesaria")]
        [MaxLength(500, ErrorMessage = "La descripción no puede superar los 500 caracteres")]
        public string Descripcion { get; set; }

        [Required]
        [Range(0.01, 999999.99, ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal Precio { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser un número negativo")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "El código de barras es indispensable")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "El código de barras solo debe contener números")]
        public string CodigoBarras { get; set; }
    }

    public class ProductoRequest : ProductoBase
    {
        [Required(ErrorMessage = "Debe especificar una subcategoría")]
        [RegularExpression(@"^({?([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}}?)$",
        ErrorMessage = "El formato del ID de subcategoría no es un GUID válido")]
        public Guid IdSubCategoria { get; set; }
    }

    public class ProductoResponse : ProductoBase
    {
        [Required]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "La respuesta debe incluir el nombre de la subcategoría")]
        [MinLength(2, ErrorMessage = "El nombre de la subcategoría es demasiado corto")]
        public string SubCategoria { get; set; }

        [Required(ErrorMessage = "La respuesta debe incluir el nombre de la categoría padre")]
        [StringLength(50, ErrorMessage = "El nombre de la categoría no debe exceder los 50 caracteres")]
        public string Categoria { get; set; }
    }

    public class ProductoPrecioDolares : ProductoResponse
    {
        [Required]
        [Range(0.01, 999999.99, ErrorMessage = "El precio en dólares debe ser mayor a 0")]
        public decimal PrecioDolares { get; set; }
    }
}
