using Datos;
using Datos.Mapper;
using Datos.Models.Proveedor;
using Entidades;
using farmappProyecto.Models.Proveedor;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servicios.Proveedores
{
    public class ProveedoresServicios : IProveedoresServicios

    {
        private Context _context;
        public ProveedoresServicios(Context context)
        {
            _context = context != null ? context : throw new NullReferenceException(nameof(context));
        }
        public async Task<IEnumerable<ProveedorViewModel>> ListarProveedor()
        {
            IQueryable<ProveedorEntity> query = _context.Set<ProveedorEntity>(); ;

            return (await query.ToListAsync()).ToModelList();
        }

        public async Task<ProveedorViewModel> CrearProveedor(ProveedorViewModel model)
        {

            ProveedorEntity prov = new ProveedorEntity
            {
                Nombre = model.Nombre,
                Telefono = model.Telefono,
                Estado = true,
                Direccion = model.Direccion,
                Email = model.Email

            };
            await _context.Proveedores.AddAsync(prov);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<ProveedorViewModel> ActualizaFila(int id, ProveedorViewModel model)
        {
            _context.Entry(model.ToEntity()).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<IEnumerable<ProveedorSelectModel>> SelectProveedor()
        {
            var proveedor = await _context.Proveedores.Where(p => p.Estado == true).ToListAsync();

            return proveedor.Select(p => new ProveedorSelectModel
            {
                ProveedorId = p.ProveedorId,
                Nombre = p.Nombre,
            });
        }
    }
}
