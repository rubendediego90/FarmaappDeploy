using Entidades;
using farmappProyecto.Models.Proveedor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Mapper
{
    public static class ProveedorMapper
    {
        public static ProveedorEntity ToEntity(this ProveedorViewModel model)
        {
            return new ProveedorEntity()
            {
                ProveedorId = model.ProveedorId,
                Nombre = model.Nombre,
                Direccion = model.Direccion,
                Telefono = model.Telefono,
                Email = model.Email,
                Estado = model.Estado,
            };
        }

        public static IEnumerable<ProveedorEntity> ToEntityList(this IEnumerable<ProveedorViewModel> modelList)
        {
            return modelList.Select(m => ToEntity(m)).ToList();
        }

        public static ProveedorViewModel ToModel(ProveedorEntity entity)
        {
            return new ProveedorViewModel()
            {
                ProveedorId = entity.ProveedorId,
                Nombre = entity.Nombre,
                Direccion = entity.Direccion,
                Telefono = entity.Telefono,
                Email = entity.Email,
                Estado = entity.Estado,
            };
        }

        public static IEnumerable<ProveedorViewModel> ToModelList(this IEnumerable<ProveedorEntity> entityList)
        {
            return entityList.Select(e => ToModel(e)).ToList();
        }
    }
}

