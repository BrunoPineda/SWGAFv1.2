using BackendSWGAF.Models;
using BackendSWGAF.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSWGAF.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
               .HasOne<UsuarioStatus>(e => e.usuariostatus)
               .WithMany(te => te.usuarios)
               .HasForeignKey(e => e.idStatus);

            modelBuilder.Entity<Solicitud>()
               .HasOne<Usuario>(e => e.usuario)
               .WithMany(te => te.solicitudes)
               .HasForeignKey(e => e.idUsuario);

            modelBuilder.Entity<Producto>()
               .HasOne<Category>(p => p.categoria)
               .WithMany(c => c.productos)
               .HasForeignKey(p => p.idCategory);

            modelBuilder.Entity<Producto>()
               .HasOne<ProductoStatus>(p => p.productoStatus)
               .WithMany(c => c.productos)
               .HasForeignKey(p => p.idStatus);

            modelBuilder.Entity<Producto>()
              .HasOne<Rack>(p => p.rack)
              .WithMany(c => c.productos)
              .HasForeignKey(p => p.idRack);

            modelBuilder.Entity<Kardex>()
              .HasOne<Factura>(p => p.factura)
              .WithMany(c => c.kardexs)
              .HasForeignKey(p => p.id);

            modelBuilder.Entity<productoHasFactura>()
              .HasOne<Factura>(p => p.factura)
              .WithMany(c => c.productoHasFacturas)
              .HasForeignKey(p => p.IdFactura);

            modelBuilder.Entity<productoHasFactura>()
              .HasOne<Producto>(p => p.producto)
              .WithMany(c => c.productoHasFacturas)
              .HasForeignKey(p => p.IdProducto);

            modelBuilder.Entity<solicitudhasProducto>()
            .HasOne<Solicitud>(p => p.solicitud)
            .WithMany(c => c.solicitudhasProductos)
            .HasForeignKey(p => p.IdSolicitud);

            modelBuilder.Entity<solicitudhasProducto>()
           .HasOne<Producto>(p => p.producto)
           .WithMany(c => c.solicitudhasProductos)
           .HasForeignKey(p => p.IdProducto);

        }
        public AppDbContext() { }
        public DbSet<Category> category { get; set; }
        public DbSet<Usuario> usuario { get; set; }
        public DbSet<Solicitud> solicitud { get; set; }
        public DbSet<Producto> producto { get; set; }
        public DbSet<Rack> rack { get; set; }
        public DbSet<Kardex> kardex { get; set; }
        public DbSet<Factura> factura { get; set; }
        public DbSet<solicitudhasProducto> solicitudhasProducto { get; set; }
        public DbSet<productoHasFactura> productoHasFactura { get; set; }

        public DbSet<ProductoStatus> productostatus { get; set; }
        public DbSet<UsuarioStatus> usuariostatus { get; set; }
    }
}
