using Datos.Models.Stock;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Servicios.Stock
{
    public interface IStockServicio
    {
        Task<IEnumerable<StockModel>> ListarStock();
        Task<StockModel> ListarStockId(int id);

        Task<StockModel> ActualizaFila(int id, StockModel model);
        Task CrearStock(int id);

    }
}