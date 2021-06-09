using Datos.Models.Pedido;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Mapper.Pedido
{
    public static class PedidoViewMapper
    {
        public static PedidosViewModel ToModel(this LineaPedidoEntity entity)
        {
            return new PedidosViewModel()
            {
                LineaId = entity.LineaId,
                CantidadPedido = entity.Cantidad,
                Estado = entity.Estado,
                ArticuloId = entity.ArticuloId,
                FechaPedidoString = entity.FechaPedido?.ToString("dd/MM/yyyy"),
                FechaPedido = (DateTime)entity.FechaPedido,
                ProveedorId = entity.ProveedorId,
                PrecioCompra = entity.PrecioCompra,
                NombreArticulo = entity.Articulo.Nombre,
                NombreProveedor = entity.Articulo.Proveedor.Nombre

            };
        }
        public static IEnumerable<PedidosViewModel> ToModelList(this IEnumerable<LineaPedidoEntity> entityList)
        {
            return entityList.Select(e => ToModel(e)).ToList();
        }
        public static LineaPedidoEntity ToEntity(this PedidosViewModel model)
        {
            return new LineaPedidoEntity()
            {
                ArticuloId = model.ArticuloId,
                LineaId = model.LineaId,
                Cantidad = model.CantidadPedido,
                Estado = "ok",
                ProveedorId = model.ProveedorId,
                PrecioCompra = model.PrecioCompra,
                FechaPedido = model.FechaPedido,
                FechaRecepcion = DateTime.UtcNow
            };
        }

    }
}
