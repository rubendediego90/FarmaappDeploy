using Datos.Models.Stock;
using Microsoft.AspNetCore.Mvc;
using Servicios.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace farmappProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockServicio _stockServicios;

        public StockController(IStockServicio stockServicio)
        {
            _stockServicios = stockServicio != null ? stockServicio : throw new NullReferenceException(nameof(stockServicio));
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<StockModel>> GetStock()
        {
            // return articulo;
            var art = await _stockServicios.ListarStock();

            return art;
        }

        [HttpGet("{id}")]
        public async Task<StockModel> GetStockId(int id)
        {
            // return articulo;
            var art = await _stockServicios.ListarStockId(id);

            return art;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<StockModel>> Put(int id, [FromBody] StockModel model)
        {
            try
            {
                await _stockServicios.ActualizaFila(id, model);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
