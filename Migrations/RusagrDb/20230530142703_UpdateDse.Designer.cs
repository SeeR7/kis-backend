﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ServerApp.Data;

#nullable disable

namespace ServerApp.Migrations.RusagrDb
{
    [DbContext(typeof(RusagrDbContext))]
    [Migration("20230530142703_UpdateDse")]
    partial class UpdateDse
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ServerApp.Models.Rusagr.DSE", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("DepCons")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("DepProd")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("DseCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Material")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ZagType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("DSE");
                });

            modelBuilder.Entity("ServerApp.Models.Rusagr.DepRoute", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("DepCons")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("DepProd")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("DseId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("EntryDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("OutDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("DseId");

                    b.ToTable("DepRoute");
                });

            modelBuilder.Entity("ServerApp.Models.Rusagr.DseSostav", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("ChildId")
                        .HasColumnType("bigint");

                    b.Property<long?>("ParentId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ChildId");

                    b.HasIndex("ParentId");

                    b.ToTable("DseSostav");
                });

            modelBuilder.Entity("ServerApp.Models.Rusagr.DepRoute", b =>
                {
                    b.HasOne("ServerApp.Models.Rusagr.DSE", "Dse")
                        .WithMany()
                        .HasForeignKey("DseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dse");
                });

            modelBuilder.Entity("ServerApp.Models.Rusagr.DseSostav", b =>
                {
                    b.HasOne("ServerApp.Models.Rusagr.DSE", "Child")
                        .WithMany()
                        .HasForeignKey("ChildId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServerApp.Models.Rusagr.DSE", "Parent")
                        .WithMany()
                        .HasForeignKey("ParentId");

                    b.Navigation("Child");

                    b.Navigation("Parent");
                });
#pragma warning restore 612, 618
        }
    }
}
