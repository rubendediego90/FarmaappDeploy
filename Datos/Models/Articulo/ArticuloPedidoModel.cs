using System;
using System.Collections.Generic;
using System.Text;

namespace Datos.Models.Articulo
{
    public class ArticuloPedidoModel
    {
        public int ArticuloId { get; set; }
        public string NombreArticulo { get; set; }
        public float Precio { get; set; }
        //Proveedor
        public string NombreProveedor { get; set; }

        public int ProveedorId { get; set; }
        //Stock
        public int CantidadStock { get; set; }
    }
}
