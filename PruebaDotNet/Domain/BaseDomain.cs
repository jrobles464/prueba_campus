using PruebaDotNet.Database;
using PruebaDotNet.Models;

namespace PruebaDotNet.Domain
{
    public class BaseDomain
    {
        protected readonly AppDbContext _context;

        public BaseDomain(AppDbContext context)
        {
            _context = context;
        }

        protected Response<object> BadRequest(string message = "Entidad no encontrada")
        {
            return new Response<object> { Data = null, Message = message, Success=false };
        }

        protected Response<object> SuccessOperation(string message = "Operacion exitosa") {
            return new Response<object> { Success = true, Data = null, Message = message};
        }
    }
}
