using Datos.Models.Proveedor;
using farmappProyecto.Models.Proveedor;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Servicios.Proveedores
{
    public interface IProveedoresServicios
    {
        Task<IEnumerable<ProveedorViewModel>> ListarProveedor();
        Task<ProveedorViewModel> CrearProveedor(ProveedorViewModel model);

        Task<ProveedorViewModel> ActualizaFila(int id, ProveedorViewModel model);

        Task<IEnumerable<ProveedorSelectModel>> SelectProveedor();

    }

}