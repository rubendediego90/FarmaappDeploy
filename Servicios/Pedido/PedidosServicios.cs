using Datos;
using Datos.Mapper;
using Datos.Mapper.Pedido;
using Datos.Models.Pedido;
using Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Pedido
{
    public class PedidosServicios : IPedidosServicios
    {
        private Context _context;
        public PedidosServicios(Context context)
        {
            _context = context != null ? context : throw new NullReferenceException(nameof(context));
        }

        public async Task<IEnumerable<PedidosViewModel>> ListarPedidos()
        {
            IQueryable<LineaPedidoEntity> query = _context.Set<LineaPedidoEntity>();
            var res = await query.Where(p => p.Estado == "pendiente").Include(i => i.Articulo).ThenInclude(p => p.Proveedor).ToListAsync();
            return res.ToModelList();
        }

        public async Task<PedidosViewModel> RecepcionarPedido(PedidosViewModel model)
        {
            //actualizar listado de pedidos
            _context.Entry(model.ToEntity()).State = EntityState.Modified;


            //obtener el nuevo Stock
            var query = await _context.Stock.Where(x => x.ArticuloId == model.ArticuloId && x.Estado == "ok").FirstOrDefaultAsync();

            //actualizar el stock
            query.Cantidad = query.Cantidad + model.CantidadPedido;

            await _context.SaveChangesAsync();

            return model;
        }

        public async Task<CrearPedidoModel> CrearPedido(CrearPedidoModel model)
        {
            model.Estado = "Pendiente";
            await _context.LineaPedidos.AddAsync(model.ToEntityCrear());
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task DeleteAsync(int id)
        {
            var pedidoEntity = await _context.LineaPedidos.FindAsync(id);

            _context.Remove(pedidoEntity);
            await _context.SaveChangesAsync();

        }

    }
}
