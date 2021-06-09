using Datos;
using Datos.Mapper.Venta;
using Datos.Models.Venta;
using Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Venta
{
    public class VentaServicio : IVentaServicio

    {
        private Context _context;
        public VentaServicio(Context context)
        {
            _context = context != null ? context : throw new NullReferenceException(nameof(context));
        }

        public async Task CrearRegistro(AuxModel model)
        {
            //comprobar si está en el carrito
            var aux = await _context.Aux.ToListAsync();
            foreach (var art in aux)
            {
                if (art.ArticuloId == model.ArticuloId)
                {
                    throw new ArgumentException("Ya esta en el carrito");
                }

            }
            await _context.Aux.AddAsync(model.ToEntity());
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<AuxModel>> LeerRegistro()
        {
            IQueryable<AuxVentaEntity> query = _context.Set<AuxVentaEntity>(); ;

            return (await query.ToListAsync()).ToModelList();
        }

        public async Task ActualizaRegistro(AuxModel model)
        {
            _context.Entry(model.ToEntity()).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(int id)
        {
            var registroEntity = await _context.Aux.FindAsync(id);
            _context.Remove(registroEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<float> getSumma()
        {

            var aux = await _context.Aux.ToListAsync();
            double PrecioTotal = 0.0f;

            foreach (var det in aux)
            {
                PrecioTotal = PrecioTotal + (det.Precio * det.Cantidad);

            }

            PrecioTotal = Math.Round(PrecioTotal, 2);

            return (float)PrecioTotal;
        }


        public async Task<double> CrearVenta()
        {
            var dbContextTransaction = _context.Database.BeginTransaction();
            try
            {
                //Inicio Transacción
                dbContextTransaction.Commit();

                //Crear registro en venta
                var ventaAdd = new VentaEntity()
                {
                    FechaVenta = DateTime.UtcNow,
                    TotalVenta = 0.0f,
                };

                await _context.Ventas.AddAsync(ventaAdd);
                await _context.SaveChangesAsync();

                //Obtener el Id creado
                var ventaId = ventaAdd.VentaId;

                double PrecioTotal = 0.00;

                //Leer el carrito e insartarlo en la tabla final
                var aux = await _context.Aux.ToListAsync();

                foreach (var det in aux)
                {
                    DetalleVentaEntity detalle = new DetalleVentaEntity
                    {
                        //Crear registro en detalle
                        VentaId = ventaId,
                        ArticuloId = det.ArticuloId,
                        Cantidad = det.Cantidad,
                        Precio = det.Precio,
                    };

                    //Actualizar Stock
                    var stockOk = await _context.Stock.Where(d => d.ArticuloId == det.ArticuloId && d.Estado == "ok").FirstOrDefaultAsync();
                    stockOk.Cantidad = stockOk.Cantidad - det.Cantidad;

                    var stockVendido = await _context.Stock.Where(d => d.ArticuloId == det.ArticuloId && d.Estado == "vendido").FirstOrDefaultAsync();
                    stockVendido.Cantidad = stockVendido.Cantidad + det.Cantidad;

                    await _context.Detalle.AddAsync(detalle);
                    PrecioTotal = PrecioTotal + (det.Precio * det.Cantidad);
                }

                //Actualizar el precio
                var PrecioVenta = await _context.Ventas.Where(d => d.VentaId == ventaId).FirstOrDefaultAsync();
                PrecioTotal = Math.Round(PrecioTotal, 2); //redondear precio a 2 decimales
                PrecioVenta.TotalVenta = (float)PrecioTotal;


                //Borrar el carrito
                var all = from c in _context.Aux select c;
                _context.Aux.RemoveRange(all);
                _context.SaveChanges();

                //Guardado
                await _context.SaveChangesAsync();

                return PrecioTotal;
            }

            catch
            {
                //Rollback si hay fallo
                dbContextTransaction.Rollback();

                return 0.0f;
            }

        }

    }
}