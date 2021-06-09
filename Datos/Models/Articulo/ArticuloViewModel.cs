using farmappProyecto.Models.Proveedor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace farmappProyecto.Models.Articulo
{
    public class ArticuloViewModel
    {
        public int ArticuloId { get; set; }
        public string NombreArticulo { get; set; }
        public string Descripcion { get; set; }
        public float Precio { get; set; }
        public int ProveedorId { get; set; }

        public string NombreProveedor { get; set; }

        public Boolean Estado { get; set; }
    }
}
