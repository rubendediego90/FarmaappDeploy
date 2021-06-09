using Datos.Models.Venta;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Mapper.Venta
{
    public static class VentaCreateMapper
    {
        public static DetalleVentaEntity ToEntity(this CrearVentaModel model)
        {
            return new DetalleVentaEntity()
            {
                ArticuloId = model.ArticuloId,
                Cantidad = model.Cantidad,
                Precio = model.Precio

            };
        }

        public static IEnumerable<DetalleVentaEntity> ToEntityList(this IEnumerable<CrearVentaModel> modelList)
        {
            return modelList.Select(m => ToEntity(m)).ToList();
        }

    }
}
