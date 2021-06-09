using Datos;
using Datos.Mapper.Stock;
using Datos.Models.Stock;
using Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Stock
{
    public class StockServicio : IStockServicio

    {
        private Context _context;
        public StockServicio(Context context)
        {
            _context = context != null ? context : throw new NullReferenceException(nameof(context));
        }
        public async Task CrearStock(int id)
        {
            var stockAdd1 = new StockEntity()
            {
                ArticuloId = id,
                Cantidad = 0,
                Estado = "ok",
            };

            await _context.Stock.AddAsync(stockAdd1);

            var stockAdd2 = new StockEntity()
            {
                ArticuloId = id,
                Cantidad = 0,
                Estado = "vendido",
            };
            await _context.Stock.AddAsync(stockAdd2);
            await _context.SaveChangesAsync();
        }


        public async Task<StockModel> ListarStockId(int id)
        {
            IQueryable<StockEntity> query = _context.Set<StockEntity>().Where(s => s.Estado == "ok" && s.ArticuloId == id);
            return (await query.Include(s => s.Articulo).Where(s => s.Articulo.Estado == true).FirstOrDefaultAsync()).ToModel();
        }

        public async Task<IEnumerable<StockModel>> ListarStock()
        {
            IQueryable<StockEntity> query = _context.Set<StockEntity>().Where(s => s.Estado == "ok");
            return (await query.Include(s => s.Articulo).Where(s => s.Articulo.Estado == true).ToListAsync()).ToModelList();
        }
        public async Task<StockModel> ActualizaFila(int id, StockModel model)
        {
            _context.Entry(model.ToEntity()).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return model;
        }
    }
}
