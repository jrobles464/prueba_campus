using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaDotNet.Database;
using PruebaDotNet.Models;

namespace PruebaDotNet.Domain
{
    public class ProductoDomain : BaseDomain
    {
        public ProductoDomain(AppDbContext context) : base(context) { }
        public async Task<object> GetProductos(decimal? minPrecio, decimal? maxPrecio, int? minStock)
        {
            var productos = _context.Productos.AsQueryable();

            if (minPrecio.HasValue)
            {
                productos = productos.Where(p => p.Precio >= minPrecio.Value);
            }

            if (maxPrecio.HasValue) { productos = productos.Where(p => p.Precio <= maxPrecio.Value); }
            if (minStock.HasValue) { productos = productos.Where(p => p.Stock >= minStock.Value); }
            var listResponse = await productos.ToListAsync();

            return new Response<object> { Message = "Success", Success = true, Data = listResponse };
        }

        public async Task<object> UpdateProducto(int id, Producto producto)
        {
            var existingProducto = await _context.Productos.FindAsync(id);
            if (existingProducto == null)
            {
                return new Response<object> { Message="No existe el producto", Success = false };
            }
            existingProducto.Stock = producto.Stock;
            existingProducto.Precio = producto.Precio;
            _context.Productos.Update(existingProducto);
            await _context.SaveChangesAsync();
            return new Response<object> { Message = "Operacion exitosa", Success = true };
        }
    }
}
