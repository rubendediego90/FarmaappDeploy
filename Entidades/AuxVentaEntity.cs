using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entidades
{
    public class AuxVentaEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ArticuloId { get; set; }

        public int Cantidad { get; set; }

        public float Precio { get; set; }

        public string NombreArticulo { get; set; }

        public ArticuloEntity Articulo { get; set; }

    }
}
