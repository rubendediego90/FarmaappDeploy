using Datos.Models.Pedido;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Servicios.Pedido
{
    public interface IPedidosServicios
    {
        Task<CrearPedidoModel> CrearPedido(CrearPedidoModel model);
        Task<IEnumerable<PedidosViewModel>> ListarPedidos();
        Task<PedidosViewModel> RecepcionarPedido(PedidosViewModel model);
        Task DeleteAsync(int id);
    }
}