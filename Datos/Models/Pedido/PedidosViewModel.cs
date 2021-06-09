using System;
using System.Collections.Generic;
using System.Text;

namespace Datos.Models.Pedido
{
    public class PedidosViewModel
    {
        //Pedido
        public int LineaId { get; set; }

        public int CantidadPedido { get; set; }

        public string Estado { get; set; }

        public int ArticuloId { get; set; }

        public int ProveedorId { get; set; }

        public float PrecioCompra { get; set; }

        public DateTime FechaPedido { get; set; }

        public String FechaPedidoString { get; set; }
        //Articulo
        public string NombreArticulo { get; set; }

        //Proveedor
        public string NombreProveedor { get; set; }

    }
}
