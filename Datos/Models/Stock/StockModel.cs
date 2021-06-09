using System;
using System.Collections.Generic;
using System.Text;

namespace Datos.Models.Stock
{
    public class StockModel
    {
        public int StockId { get; set; }
        public int ArticuloId { get; set; }

        public string Descripcion { get; set; }

        public string Nombre { get; set; }
        public string Estado { get; set; }
        public int Cantidad { get; set; }

    }
}
