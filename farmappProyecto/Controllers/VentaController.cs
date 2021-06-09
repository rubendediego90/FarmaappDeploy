using Datos.Models.Venta;
using Microsoft.AspNetCore.Mvc;
using Servicios.Venta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace farmappProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        private readonly IVentaServicio _ventaServicio;

        public VentaController(IVentaServicio pedidosServicios)
        {
            _ventaServicio = pedidosServicios != null ? pedidosServicios : throw new NullReferenceException(nameof(pedidosServicios));
        }
        [HttpPost]
        public async Task<float> Post()
        {

            var res = await _ventaServicio.CrearVenta();
            return (float)res;
            
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CrearRegistro([FromBody] AuxModel model)
        {
            try
            {
                await _ventaServicio.CrearRegistro(model);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
           
        }

        [HttpPut]
        public async Task Put([FromBody] AuxModel model)
        {
            try
            {
                await _ventaServicio.ActualizaRegistro(model);
                Ok();
            }
            catch
            {
                BadRequest();
            }
        }

        [HttpGet]
        public async Task<IEnumerable<AuxModel>> LeerRegistro()
        {
            var registro = await _ventaServicio.LeerRegistro();
            return registro;
        }


        [HttpGet("[action]")]
        public async Task<float> getSumma()
        {
            var registro = await _ventaServicio.getSumma();
            return registro;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            await _ventaServicio.DeleteAsync(id);

            return NoContent();
        }

    }
}
