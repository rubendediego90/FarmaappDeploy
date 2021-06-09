using Datos.Models.Articulo;
using Entidades;
using farmappProyecto.Models.Articulo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Servicios.Articulo
{
    public interface IArticulosServicios
    {
        Task<IEnumerable<ArticuloViewModel>> ListarArticulo();

        Task<ArticuloCrearModel> CrearArticulo(ArticuloCrearModel model);

        Task<ArticuloViewModel> ActualizaFila(int id, ArticuloViewModel model);

        Task<IEnumerable<ArticuloPedidoModel>> ListarPedidos();
    }
}