namespace PruebaDotNet.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public DateTime FechaRegistro { get; set; }
        public ICollection<Pedido> Pedidos { get; set; }
    }
}
