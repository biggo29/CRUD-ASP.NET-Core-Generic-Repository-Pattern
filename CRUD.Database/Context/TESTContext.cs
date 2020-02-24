using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using CRUD.Database.Models;
using Microsoft.Extensions.Options;

namespace CRUD.Database.Context
{
    public partial class TESTContext : DbContext
    {
        private SqlOption _connectionString;
        //public TESTContext()
        //{
        //}

        public TESTContext(IOptions<SqlOption> connectionString)
        {
            this._connectionString = connectionString.Value;
        }

        public TESTContext(DbContextOptions<TESTContext> options, IOptions<SqlOption> connectionString)
            : base(options)
        {
            this._connectionString = connectionString.Value;
        }

        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                //optionsBuilder.UseSqlServer("Server=BIGGO;Database=TEST;User Id=sa;Password=SqlServer;Trusted_Connection=True;");
                optionsBuilder.UseSqlServer(this._connectionString.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.DeptId);

                entity.Property(e => e.DeptName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmpId);

                entity.Property(e => e.EmpEmail).HasMaxLength(50);

                entity.Property(e => e.EmpName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
