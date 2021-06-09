using Datos.Models.Venta;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Servicios.Venta
{
    public interface IVentaServicio
    {
        Task<double> CrearVenta();
        Task<IEnumerable<AuxModel>> LeerRegistro();
        Task CrearRegistro(AuxModel model);

        Task ActualizaRegistro(AuxModel model);

        Task DeleteAsync(int id);

        Task<float> getSumma();


    }
}