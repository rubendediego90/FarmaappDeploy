using Datos.Models.Pedido;
using Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Datos.Mapper.Pedido
{
    public static class PedidoCrearMapper
    {
        public static LineaPedidoEntity ToEntityCrear(this CrearPedidoModel model)
        {
            return new LineaPedidoEntity()
            {
                Cantidad = model.CantidadPedido,
                FechaPedido = DateTime.UtcNow,
                ArticuloId = model.ArticuloId,
                ProveedorId = model.ProveedorId,
                PrecioCompra = model.PrecioCompra,
                Estado = model.Estado
            };
        }
    }
}
