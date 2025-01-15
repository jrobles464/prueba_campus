using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaDotNet.Database;
using PruebaDotNet.Models;
using PruebaDotNet.Models.Request;

namespace PruebaDotNet.Domain
{
    public class PedidoDomain : BaseDomain
    {
        public PedidoDomain(AppDbContext context) : base(context) { }

        public async Task<object> CreatePedido(Pedido pedido)
        {
            var productos = await this._context.Productos.Where(p => pedido.PedidoProductos.Select(pp => pp.ProductoId).Contains(p.Id)).ToListAsync();
            foreach (var pp in pedido.PedidoProductos)
            {
                var producto = productos.FirstOrDefault(p => p.Id == pp.ProductoId);
                if (producto == null || producto.Stock < pp.Cantidad)
                {
                    return new Response<object> { Message = "Stock insuficiente para el producto con el ID: " + pp.ProductoId, Success = false };
                }
            }
            pedido.Total = pedido.PedidoProductos.Sum(pp => productos.First(p => p.Id == pp.ProductoId).Precio * pp.Cantidad);
            _context.Pedidos.Add(pedido);
            foreach (var pp in pedido.PedidoProductos)
            {
                var producto = productos.First(p => p.Id == pp.ProductoId);
                producto.Stock -= pp.Cantidad; _context.Productos.Update(producto);
            }
            await _context.SaveChangesAsync();
            return pedido;
        }

        public async Task<object> GetPedido(int id)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.Cliente)
                .Include(p => p.PedidoProductos)
                .ThenInclude(pp => pp.Producto)
                .FirstOrDefaultAsync(p => p.Id == id); 
            if (pedido == null) 
            { 
                return new Response<object> { Message = "No existe el pedido con el ID: " + id, Success = false }; 
            }
            return new Response<Pedido> { Message = "Success", Success = true, Data = pedido};
        }
    }
}
