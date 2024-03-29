﻿// <auto-generated />
using System;
using CRUD.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CRUD.Database.Migrations
{
    [DbContext(typeof(TESTContext))]
    partial class TESTContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CRUD.Database.Models.Department", b =>
                {
                    b.Property<int>("DeptId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DeptName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("DeptId");

                    b.ToTable("Department");
                });

            modelBuilder.Entity("CRUD.Database.Models.Employee", b =>
                {
                    b.Property<int>("EmpId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DeptId")
                        .HasColumnType("int");

                    b.Property<int>("EmpAge")
                        .HasColumnType("int");

                    b.Property<string>("EmpEmail")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("EmpGender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmpName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<byte[]>("EmpPhoto")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("EmpId");

                    b.HasIndex("DeptId");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("CRUD.Database.Models.Employee", b =>
                {
                    b.HasOne("CRUD.Database.Models.Department", "Dept")
                        .WithMany("Employee")
                        .HasForeignKey("DeptId")
                        .HasConstraintName("FK_Employee_Department")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
