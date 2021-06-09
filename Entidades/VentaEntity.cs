using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entidades
{
    public class VentaEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VentaId { get; set; }

        public float TotalVenta { get; set; }

        public DateTime FechaVenta { get; set; } = new DateTime();

        public IEnumerable<DetalleVentaEntity> Detalle { get; set; } = new List<DetalleVentaEntity>();
    }
}
