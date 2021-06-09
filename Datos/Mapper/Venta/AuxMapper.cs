using Datos.Models.Venta;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Mapper.Venta
{
    public static class AuxMapper
    {
        public static AuxVentaEntity ToEntity(this AuxModel model)
        {
            return new AuxVentaEntity()
            {
                ArticuloId = model.ArticuloId,
                Cantidad = model.Cantidad,
                Precio = model.Precio,
                NombreArticulo = model.NombreArticulo,
                Id = model.id
            };
        }

        public static IEnumerable<AuxVentaEntity> ToEntityList(this IEnumerable<AuxModel> modelList)
        {
            return modelList.Select(m => ToEntity(m)).ToList();
        }

        public static AuxModel ToModel(AuxVentaEntity entity)
        {
            return new AuxModel()
            {
                ArticuloId = entity.ArticuloId,
                Cantidad = entity.Cantidad,
                Precio = entity.Precio,
                NombreArticulo = entity.NombreArticulo,
                id = entity.Id

            };
        }

        public static IEnumerable<AuxModel> ToModelList(this IEnumerable<AuxVentaEntity> entityList)
        {
            return entityList.Select(e => ToModel(e)).ToList();
        }
    }
}
