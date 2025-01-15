using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaDotNet.Database;
using PruebaDotNet.Domain;
using PruebaDotNet.Models;

namespace PruebaDotNet.Controllers
{
    [ApiController]
    [Route("api/pedidos")]
    public class PedidoController : Controller
    {
        private readonly PedidoDomain _pedidoDomain;
        
        public PedidoController(PedidoDomain pedidoDomain)
        {
            _pedidoDomain = pedidoDomain;
        }

        [HttpPost("/")]
        [Authorize(Roles = "Admin,Cliente")]
        public async Task<IActionResult> CreatePedido([FromBody] Pedido pedido) { 
            return Json(await _pedidoDomain.CreatePedido(pedido));

        }

        [HttpGet("/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetPedido(int id)
        {

            return Json(await _pedidoDomain.GetPedido(id));
        }
    }
}
