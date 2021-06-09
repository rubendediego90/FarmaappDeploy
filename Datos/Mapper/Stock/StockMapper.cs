using Datos.Models.Stock;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Mapper.Stock
{
    public static class StockMapper
    {
        public static StockEntity ToEntity(this StockModel model)
        {
            return new StockEntity()
            {
                StockId = model.StockId,
                ArticuloId = model.ArticuloId,
                Cantidad = model.Cantidad,
                Estado = model.Estado,
            };
        }

        public static IEnumerable<StockEntity> ToEntityList(this IEnumerable<StockModel> modelList)
        {
            return modelList.Select(m => ToEntity(m)).ToList();
        }

        public static StockModel ToModel(this StockEntity entity)
        {
            return new StockModel()
            {
                StockId = entity.StockId,
                ArticuloId = entity.ArticuloId,
                Nombre = entity.Articulo.Nombre,
                Cantidad = entity.Cantidad,
                Estado = entity.Estado,
                Descripcion = entity.Articulo.Descripcion
            };
        }
        public static IEnumerable<StockModel> ToModelList(this IEnumerable<StockEntity> entityList)
        {
            return entityList.Select(e => ToModel(e)).ToList();
        }
    }
}
