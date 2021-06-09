using System;
using System.Collections.Generic;
using System.Text;

namespace Datos.Models.Articulo
{
    public class ArticuloCrearModel
    {
        public string NombreArticulo { get; set; }
        public string Descripcion { get; set; }
        public float Precio { get; set; }
        public int ProveedorId { get; set; }
        public Boolean Estado { get; set; }
    }
}
