using Entidades;
using farmappProyecto.Models.Articulo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Mapper
{
    public static class ArticuloMapper
    {
        public static ArticuloEntity ToEntity(this ArticuloViewModel model)
        {
            return new ArticuloEntity()
            {
                ArticuloId = model.ArticuloId,
                Nombre = model.NombreArticulo,
                Descripcion = model.Descripcion,
                Precio = model.Precio,
                ProveedorId = model.ProveedorId,
                Estado = model.Estado,
            };
        }

        public static IEnumerable<ArticuloEntity> ToEntityList(this IEnumerable<ArticuloViewModel> modelList)
        {
            return modelList.Select(m => ToEntity(m)).ToList();
        }

        public static ArticuloViewModel ToModel(this ArticuloEntity entity)
        {
            return new ArticuloViewModel()
            {
                ArticuloId = entity.ArticuloId,
                NombreProveedor = entity.Proveedor.Nombre,
                Descripcion = entity.Descripcion,
                Precio = entity.Precio,
                ProveedorId = entity.ProveedorId,
                Estado = entity.Estado,
                NombreArticulo = entity.Nombre
            };
        }
        public static IEnumerable<ArticuloViewModel> ToModelList(this IEnumerable<ArticuloEntity> entityList)
        {
            return entityList.Select(e => ToModel(e)).ToList();
        }
    }
}