using Datos.Models.Proveedor;
using farmappProyecto.Models.Proveedor;
using Microsoft.AspNetCore.Mvc;
using Servicios.Proveedores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace farmappProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedoresController : ControllerBase
    {
        private readonly IProveedoresServicios _proveedoresServicios;

        public ProveedoresController(IProveedoresServicios proveedorServicio)
        {
            _proveedoresServicios = proveedorServicio != null ? proveedorServicio : throw new NullReferenceException(nameof(proveedorServicio));
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<ProveedorViewModel>> GetProveedor()
        {
            // return articulo;
            var art = await _proveedoresServicios.ListarProveedor();

            return art;
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<ProveedorSelectModel>> SelectProveedor()
        {
            // return articulo;
            var art = await _proveedoresServicios.SelectProveedor();

            return art;
        }
        [HttpPost]
        public async Task<ActionResult<ProveedorViewModel>> Post([FromBody] ProveedorViewModel model)
        {
            try
            {
                await _proveedoresServicios.CrearProveedor(model);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
            
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ProveedorViewModel>> Put(int id,[FromBody] ProveedorViewModel model)
        {
            try
            {
                await _proveedoresServicios.ActualizaFila(id,model);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
