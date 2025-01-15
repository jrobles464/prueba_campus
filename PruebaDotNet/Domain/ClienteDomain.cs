using Microsoft.EntityFrameworkCore;
using PruebaDotNet.Database;
using PruebaDotNet.Models;

namespace PruebaDotNet.Domain
{
    public class ClienteDomain : BaseDomain
    {
        public ClienteDomain(AppDbContext context) : base(context) { }

        public async Task<object> CreateCliente(Cliente cliente)
        {
            if (await _context.Clientes.AnyAsync(c => c.Email == cliente.Email)) 
            { 
                return BadRequest(message: "El email ya está registrado."); 
            }

            _context.Clientes.Add(cliente); 
            await _context.SaveChangesAsync();

            return SuccessOperation();
        }
    }
}
