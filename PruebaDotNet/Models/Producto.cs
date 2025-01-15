﻿namespace PruebaDotNet.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public DateTime FechaCreacion { get; set; }
        public ICollection<PedidoProducto> PedidoProductos { get; set; }
    }
}
