using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entidades
{
    public class LineaPedidoEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LineaId { get; set; }

        public int ArticuloId { get; set; }

        public int Cantidad { get; set; }

        public string Estado { get; set; }

        public int ProveedorId { get; set; }

        public float PrecioCompra { get; set; }

        public DateTime? FechaPedido { get; set; } = new DateTime();

        public DateTime? FechaRecepcion { get; set; } = new DateTime();

        public ArticuloEntity Articulo { get; set; }


    }

}
