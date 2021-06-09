using Datos.Models.Pedido;
using Microsoft.AspNetCore.Mvc;
using Servicios.Pedido;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace farmappProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidosServicios _pedidosServicios;

        public PedidosController(IPedidosServicios pedidosServicios)
        {
            _pedidosServicios = pedidosServicios != null ? pedidosServicios : throw new NullReferenceException(nameof(pedidosServicios));
        }

        [HttpGet]
        public async Task<IEnumerable<PedidosViewModel>> Listar()
        {
            var art = await _pedidosServicios.ListarPedidos();
            return art;
        }

        [HttpPost]
        public async Task<ActionResult<CrearPedidoModel>> Post([FromBody] CrearPedidoModel model)
        {
            try
            {
                await _pedidosServicios.CrearPedido(model);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<ActionResult<PedidosViewModel>> Put([FromBody] PedidosViewModel model)
        {
            try
            {
                await _pedidosServicios.RecepcionarPedido(model);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
  
            await _pedidosServicios.DeleteAsync(id);

            return NoContent();
        }

    }
}
