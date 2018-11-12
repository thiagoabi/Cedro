﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Restaurante.Infrastructure.Context;

namespace Restaurante.Infrastructure.Context.Migrations
{
    [DbContext(typeof(RestauranteContext))]
    [Migration("20181112063527_RestauranteMigration_Init")]
    partial class RestauranteMigration_Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Restaurante.Domain.Entity.Estabelecimento", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CodigoInterno");

                    b.Property<DateTime>("DataInclusao");

                    b.Property<DateTime?>("DataUltimaAlteracao");

                    b.Property<string>("Nome")
                        .HasMaxLength(100);

                    b.Property<string>("UsuarioInclusao");

                    b.Property<string>("UsuarioUltimaAlteracao");

                    b.HasKey("Id");

                    b.ToTable("Restaurantes");
                });

            modelBuilder.Entity("Restaurante.Domain.Entity.Prato", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CodigoInterno");

                    b.Property<DateTime>("DataInclusao");

                    b.Property<DateTime?>("DataUltimaAlteracao");

                    b.Property<long>("EstabelecimentoId");

                    b.Property<string>("Nome")
                        .HasMaxLength(100);

                    b.Property<string>("UsuarioInclusao");

                    b.Property<string>("UsuarioUltimaAlteracao");

                    b.Property<double>("Valor");

                    b.HasKey("Id");

                    b.HasIndex("EstabelecimentoId");

                    b.ToTable("Pratos");
                });

            modelBuilder.Entity("Restaurante.Domain.Entity.Prato", b =>
                {
                    b.HasOne("Restaurante.Domain.Entity.Estabelecimento", "Estabelecimento")
                        .WithMany("Pratos")
                        .HasForeignKey("EstabelecimentoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}