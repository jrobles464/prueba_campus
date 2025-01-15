using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaDotNet.Domain;
using PruebaDotNet.Models;

namespace PruebaDotNet.Controllers
{
    [ApiController]
    [Route("api/clientes")]
    public class ClienteController : Controller
    {
        private ClienteDomain _clienteDomain { get; set; }
        public ClienteController(ClienteDomain clienteDomain) 
        { 
            this._clienteDomain = clienteDomain;
        }

        [HttpPost("/")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCliente([FromBody] Cliente cliente)
        {
            return Json(await this._clienteDomain.CreateCliente(cliente));
        }
    }
}
