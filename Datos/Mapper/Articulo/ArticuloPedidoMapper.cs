using Datos.Models.Articulo;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Mapper.Articulo
{
    public static class ArticuloPedidoMapper
    {
        public static ArticuloPedidoModel ToModel(this ArticuloEntity entity)
        {
            return new ArticuloPedidoModel()
            {
                ArticuloId = entity.ArticuloId,
                ProveedorId = entity.ProveedorId,
                NombreArticulo = entity.Nombre,
                NombreProveedor = entity.Proveedor.Nombre,
                Precio = entity.Precio,
                CantidadStock = (int)entity.Stock?.Where(d => d.Estado == "ok").FirstOrDefault()?.Cantidad
            };
        }
        public static IEnumerable<ArticuloPedidoModel> ToModelListProv(this IEnumerable<ArticuloEntity> entityList)
        {
            return entityList.Select(e => ToModel(e)).ToList();
        }
    }
}
