using System;
using System.Collections.Generic;
using System.Text;

namespace Datos.Models.Venta
{
    public class CrearVentaModel
    {
        public int Cantidad { get; set; }

        public int ArticuloId { get; set; }

        public float Precio { get; set; }

        public String Nombre { get; set; }

    }
}
