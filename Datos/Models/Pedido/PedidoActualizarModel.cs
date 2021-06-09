using System;
using System.Collections.Generic;
using System.Text;

namespace Datos.Models.Pedido
{
    public class PedidoActualizarModel
    {
        public int LineaId { get; set; }

        public int CantidadPedido { get; set; }

        public string Estado { get; set; }

        public int ArticuloId { get; set; }

        public DateTime FechaPedido { get; set; }

        public DateTime FechaRecepcion { get; set; }


    }
}
