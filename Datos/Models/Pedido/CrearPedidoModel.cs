using System;
using System.Collections.Generic;
using System.Text;

namespace Datos.Models.Pedido
{
    public class CrearPedidoModel
    {
        public int CantidadPedido { get; set; }

        public int ArticuloId { get; set; }

        public int ProveedorId { get; set; }

        public String Estado { get; set; }

        public float PrecioCompra { get; set; }
    }
}
