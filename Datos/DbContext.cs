using Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos
{
    public class Context : DbContext
    {
        public DbSet<ProveedorEntity> Proveedores { get; set; }
        public DbSet<ArticuloEntity> Articulos { get; set; }
        public DbSet<VentaEntity> Ventas { get; set; }

        public DbSet<DetalleVentaEntity> Detalle { get; set; }

        public DbSet<LineaPedidoEntity> LineaPedidos { get; set; }

        public DbSet<AuxVentaEntity> Aux { get; set; }
        public DbSet<StockEntity> Stock { get; set; }
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=Farmapp.db");

            }

        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Remove Delete cascade
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }


        }



    }
}
