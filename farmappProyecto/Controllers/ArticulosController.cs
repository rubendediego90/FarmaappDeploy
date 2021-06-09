using Datos.Models.Articulo;
using Entidades;
using farmappProyecto.Models.Articulo;
using Microsoft.AspNetCore.Mvc;
using Servicios.Articulo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace farmappProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticulosController : ControllerBase
    {
        private readonly IArticulosServicios _articulosServicios;

        public ArticulosController(IArticulosServicios articulosServicio)
        {
            _articulosServicios = articulosServicio != null ? articulosServicio : throw new NullReferenceException(nameof(articulosServicio));
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<ArticuloViewModel>> Listar()
        {
            var art = await _articulosServicios.ListarArticulo();

            return art;
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<ArticuloPedidoModel>> ListarPedido()
        {
            var art = await _articulosServicios.ListarPedidos();

            return art;
        }


        [HttpPost]
        public async Task<ActionResult<ArticuloViewModel>> Post([FromBody] ArticuloCrearModel model)
        {
            try
             {
                 await _articulosServicios.CrearArticulo(model);
                 return Ok();
             }
             catch
             {
                 return BadRequest();
             }

        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ArticuloViewModel>> Put(int id, [FromBody] ArticuloViewModel model)
        {
            try
            {
                await _articulosServicios.ActualizaFila(id, model);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
