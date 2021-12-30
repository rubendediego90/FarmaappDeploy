using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entidades
{
    public class DetalleVentaEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DetalleId { get; set; }

        public int VentaId { get; set; }

        public int ArticuloId { get; set; }

        public int Cantidad { get; set; }



        public float Precio { get; set; }

        public VentaEntity Venta { get; set; }

        public ArticuloEntity Articulo { get; set; }

        // public IEnumerable<VentaEntity> Venta { get; set; } = new List<VentaEntity>();

        // public IEnumerable<ArticuloEntity> Articulo { get; set; } = new List<ArticuloEntity>();

    }
}
