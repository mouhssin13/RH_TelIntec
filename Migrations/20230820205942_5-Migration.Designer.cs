﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Telintec_RH.Data;

#nullable disable

namespace Telintec_RH.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230820205942_5-Migration")]
    partial class _5Migration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Telintec_RH.Models.Absence", b =>
                {
                    b.Property<int?>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("ID"));

                    b.Property<DateTime>("Date_Dep")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Date_Fin")
                        .HasColumnType("datetime2");

                    b.Property<int>("EmployeeID")
                        .HasColumnType("int");

                    b.Property<string>("reason")
                        .HasColumnType("varchar(250)");

                    b.HasKey("ID");

                    b.HasIndex("EmployeeID");

                    b.ToTable("Absence", "TIRH");
                });

            modelBuilder.Entity("Telintec_RH.Models.Account", b =>
                {
                    b.Property<int?>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("ID"));

                    b.Property<string>("Tel")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("firstName")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("lastName")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<string>("role")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("userName")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.HasKey("ID");

                    b.ToTable("Account", "TIRH");
                });

            modelBuilder.Entity("Telintec_RH.Models.Departement", b =>
                {
                    b.Property<int?>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("ID"));

                    b.Property<string>("chef")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<string>("nom")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("ID");

                    b.ToTable("Departement", "TIRH");
                });

            modelBuilder.Entity("Telintec_RH.Models.Employee", b =>
                {
                    b.Property<int?>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("ID"));

                    b.Property<int>("DepartementID")
                        .HasColumnType("int");

                    b.Property<string>("Tel")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("cin")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<string>("fulleName")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("image")
                        .HasColumnType("varchar(250)");

                    b.Property<string>("poste")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<double>("salary")
                        .HasColumnType("float");

                    b.Property<string>("statu")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.HasKey("ID");

                    b.HasIndex("DepartementID");

                    b.ToTable("Employee", "TIRH");
                });

            modelBuilder.Entity("Telintec_RH.Models.Holiday", b =>
                {
                    b.Property<int?>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("ID"));

                    b.Property<DateTime>("Date_Dep")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Date_Fin")
                        .HasColumnType("datetime2");

                    b.Property<int>("EmployeeID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("EmployeeID");

                    b.ToTable("Holiday", "TIRH");
                });

            modelBuilder.Entity("Telintec_RH.Models.Absence", b =>
                {
                    b.HasOne("Telintec_RH.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Telintec_RH.Models.Employee", b =>
                {
                    b.HasOne("Telintec_RH.Models.Departement", "Departement")
                        .WithMany()
                        .HasForeignKey("DepartementID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Departement");
                });

            modelBuilder.Entity("Telintec_RH.Models.Holiday", b =>
                {
                    b.HasOne("Telintec_RH.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });
#pragma warning restore 612, 618
        }
    }
}