using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entidades
{
    public class StockEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StockId { get; set; }
        public int ArticuloId { get; set; }
        public string Estado { get; set; }
        public int Cantidad { get; set; }

        public ArticuloEntity Articulo { get; set; }

    }
}
