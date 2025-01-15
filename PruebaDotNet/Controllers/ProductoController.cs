using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaDotNet.Domain;
using PruebaDotNet.Models;

namespace PruebaDotNet.Controllers
{
    [ApiController]
    [Route("api/")]
    public class ProductoController : Controller
    {
        private readonly ProductoDomain _productoDomain;

        public ProductoController(ProductoDomain productoDomain)
        {
            _productoDomain = productoDomain;
        }

        [HttpGet("/")]
        [Authorize(Roles = "Admin,Cliente")]
        public async Task<IActionResult> GetProductos([FromQuery] decimal? minPrecio, [FromQuery] decimal? maxPrecio, [FromQuery] int? minStock)
        {
            return Json(await _productoDomain.GetProductos(minPrecio, maxPrecio, minStock));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProducto(int id, [FromBody] Producto producto)
        {
            return Json(await _productoDomain.UpdateProducto(id, producto));
        }
    }
}
