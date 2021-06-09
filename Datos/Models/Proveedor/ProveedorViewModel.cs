using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace farmappProyecto.Models.Proveedor
{
    public class ProveedorViewModel
    {
        public int ProveedorId { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public Boolean Estado { get; set; }
    }
}
