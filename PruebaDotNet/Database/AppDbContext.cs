using Microsoft.EntityFrameworkCore;
using PruebaDotNet.Models;

namespace PruebaDotNet.Database
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoProducto> PedidoProductos { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration) : base(options) 
        { 
            this._configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.FechaRegistro).IsRequired();
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Precio).IsRequired().HasColumnType("decimal(10, 2)");
                entity.Property(e => e.Stock).IsRequired();
                entity.Property(e => e.FechaCreacion).IsRequired();
            });

            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FechaPedido).IsRequired();
                entity.Property(e => e.Total).IsRequired().HasColumnType("decimal(10, 2)");
                entity.HasOne(e => e.Cliente)
                      .WithMany(c => c.Pedidos)
                      .HasForeignKey(e => e.ClienteId);
            });

            modelBuilder.Entity<PedidoProducto>(entity =>
            {
                entity.HasKey(e => new { e.PedidoId, e.ProductoId });
                entity.Property(e => e.Cantidad).IsRequired();
                entity.HasOne(e => e.Pedido)
                      .WithMany(p => p.PedidoProductos)
                      .HasForeignKey(e => e.PedidoId);
                entity.HasOne(e => e.Producto)
                      .WithMany(p => p.PedidoProductos)
                      .HasForeignKey(e => e.ProductoId);
            });
        }
    }

}
