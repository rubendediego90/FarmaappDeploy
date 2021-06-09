﻿// <auto-generated />
using Datos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;

namespace Datos.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Entidades.ArticuloEntity", b =>
                {
                    b.Property<int>("ArticuloId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<float>("Precio")
                        .HasColumnType("real");

                    b.Property<int>("ProveedorId")
                        .HasColumnType("int");

                    b.HasKey("ArticuloId");

                    b.HasIndex("ProveedorId");

                    b.ToTable("Articulos");
                });

            modelBuilder.Entity("Entidades.AuxVentaEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ArticuloId")
                        .HasColumnType("int");

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<string>("NombreArticulo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Precio")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("ArticuloId");

                    b.ToTable("Aux");
                });

            modelBuilder.Entity("Entidades.DetalleVentaEntity", b =>
                {
                    b.Property<int>("DetalleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ArticuloId")
                        .HasColumnType("int");

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<float>("Precio")
                        .HasColumnType("real");

                    b.Property<int>("VentaId")
                        .HasColumnType("int");

                    b.HasKey("DetalleId");

                    b.HasIndex("ArticuloId");

                    b.HasIndex("VentaId");

                    b.ToTable("Detalle");
                });

            modelBuilder.Entity("Entidades.LineaPedidoEntity", b =>
                {
                    b.Property<int>("LineaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ArticuloId")
                        .HasColumnType("int");

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<string>("Estado")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("FechaPedido")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaRecepcion")
                        .HasColumnType("datetime2");

                    b.Property<float>("PrecioCompra")
                        .HasColumnType("real");

                    b.Property<int>("ProveedorId")
                        .HasColumnType("int");

                    b.HasKey("LineaId");

                    b.HasIndex("ArticuloId");

                    b.ToTable("LineaPedidos");
                });

            modelBuilder.Entity("Entidades.ProveedorEntity", b =>
                {
                    b.Property<int>("ProveedorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Direccion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefono")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.HasKey("ProveedorId");

                    b.ToTable("Proveedores");
                });

            modelBuilder.Entity("Entidades.StockEntity", b =>
                {
                    b.Property<int>("StockId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ArticuloId")
                        .HasColumnType("int");

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<string>("Estado")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StockId");

                    b.HasIndex("ArticuloId");

                    b.ToTable("Stock");
                });

            modelBuilder.Entity("Entidades.VentaEntity", b =>
                {
                    b.Property<int>("VentaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("FechaVenta")
                        .HasColumnType("datetime2");

                    b.Property<float>("TotalVenta")
                        .HasColumnType("real");

                    b.HasKey("VentaId");

                    b.ToTable("Ventas");
                });

            modelBuilder.Entity("Entidades.ArticuloEntity", b =>
                {
                    b.HasOne("Entidades.ProveedorEntity", "Proveedor")
                        .WithMany("Articulo")
                        .HasForeignKey("ProveedorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Entidades.AuxVentaEntity", b =>
                {
                    b.HasOne("Entidades.ArticuloEntity", "Articulo")
                        .WithMany("Aux")
                        .HasForeignKey("ArticuloId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Entidades.DetalleVentaEntity", b =>
                {
                    b.HasOne("Entidades.ArticuloEntity", "Articulo")
                        .WithMany("Detalle")
                        .HasForeignKey("ArticuloId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Entidades.VentaEntity", "Venta")
                        .WithMany("Detalle")
                        .HasForeignKey("VentaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Entidades.LineaPedidoEntity", b =>
                {
                    b.HasOne("Entidades.ArticuloEntity", "Articulo")
                        .WithMany("Venta")
                        .HasForeignKey("ArticuloId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Entidades.StockEntity", b =>
                {
                    b.HasOne("Entidades.ArticuloEntity", "Articulo")
                        .WithMany("Stock")
                        .HasForeignKey("ArticuloId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
