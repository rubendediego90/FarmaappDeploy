using Datos.Models.Articulo;
using Entidades;
using farmappProyecto.Models.Articulo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Datos.Mapper.Articulo
{
    public static class ArticuloCrearMapper
    {
        public static ArticuloEntity ToEntityCrear(this ArticuloCrearModel model)
        {
            return new ArticuloEntity()
            {
                Nombre = model.NombreArticulo,
                Descripcion = model.Descripcion,
                Precio = model.Precio,
                ProveedorId = model.ProveedorId,
                Estado = true
            };
        }

    }
}