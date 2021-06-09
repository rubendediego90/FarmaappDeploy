using System;
using System.Collections.Generic;
using System.Text;

namespace Datos.Models.Venta
{
    public class AuxModel
    {
        public int id { get; set; }
        public int ArticuloId { get; set; }

        public int Cantidad { get; set; }

        public float Precio { get; set; }

        public string NombreArticulo { get; set; }
    }
}
