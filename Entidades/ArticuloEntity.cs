using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entidades
{
    public class ArticuloEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ArticuloId { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre no debe de tener más de 50 caracteres, ni menos de 3 caracteres.")]
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public float Precio { get; set; }

        public int ProveedorId { get; set; }

        public Boolean Estado { get; set; } = true;

        public ProveedorEntity Proveedor { get; set; }

        public IEnumerable<LineaPedidoEntity> Venta { get; set; } = new List<LineaPedidoEntity>();
        public IEnumerable<StockEntity> Stock { get; set; } = new List<StockEntity>();

        public IEnumerable<DetalleVentaEntity> Detalle { get; set; } = new List<DetalleVentaEntity>();

        public IEnumerable<AuxVentaEntity> Aux { get; set; } = new List<AuxVentaEntity>();



    }
}
