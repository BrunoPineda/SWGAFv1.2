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


        }
        public AppDbContext() { }
        public DbSet<Category> category { get; set; }
        public DbSet<Usuario> usuario { get; set; }
        public DbSet<Solicitud> solicitud { get; set; }
    }
}
