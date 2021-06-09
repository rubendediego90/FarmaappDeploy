using Datos;
using Datos.Mapper;
using Datos.Mapper.Articulo;
using Datos.Mapper.Stock;
using Datos.Models.Articulo;
using Entidades;
using farmappProyecto.Models.Articulo;
using Microsoft.EntityFrameworkCore;
using Servicios.Articulo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Articulos
{
    public class ArticulosServicios : IArticulosServicios
    {
        private Context _context;
        public ArticulosServicios(Context context)
        {
            _context = context != null ? context : throw new NullReferenceException(nameof(context));
        }
        public async Task<IEnumerable<ArticuloViewModel>> ListarArticulo()
        {
            IQueryable<ArticuloEntity> query = _context.Set<ArticuloEntity>(); ;
            return (await query.Include(p => p.Proveedor).ToListAsync()).ToModelList();
        }

        public async Task<ArticuloCrearModel> CrearArticulo(ArticuloCrearModel model)
        {
            var articuloEntity = ArticuloCrearMapper.ToEntityCrear(model);
            await _context.Articulos.AddAsync(articuloEntity);
            await _context.SaveChangesAsync();

            var stockAdd1 = new StockEntity()
            {
                ArticuloId = articuloEntity.ArticuloId,
                Cantidad = 0,
                Estado = "ok",
            };

            await _context.Stock.AddAsync(stockAdd1);

            var stockAdd2 = new StockEntity()
            {
                ArticuloId = articuloEntity.ArticuloId,
                Cantidad = 0,
                Estado = "vendido",
            };

            await _context.Stock.AddAsync(stockAdd2);

            await _context.SaveChangesAsync();

            return model;
        }

        public async Task<ArticuloViewModel> ActualizaFila(int id, ArticuloViewModel model)
        {
            _context.Entry(model.ToEntity()).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return model;
        }

        public async Task<IEnumerable<ArticuloPedidoModel>> ListarPedidos()
        {
            var query = await _context.Articulos
                .Include(d => d.Proveedor)
                .Include(d => d.Stock)
                .Where(a => a.Estado == true)
                .ToListAsync();

            return query.ToModelListProv();
        }

    }
}