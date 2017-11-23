using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AgendaTelefonica.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
       
        public ApplicationDbContext()
            : base("DefaultConnection"/*, throwIfV1Schema: false*/)
        {
        }
        public virtual DbSet<Correo> Correos { get; set; }
        public virtual DbSet<Direccion> Direcciones { get; set; }
        public virtual DbSet<Persona> Personas { get; set; }
        public virtual DbSet<Telefono> Telefonos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Configure domain classes using modelBuilder here
            //Configuaracion Persona
            modelBuilder.Entity<Persona>()
                .ToTable("Personas");
            modelBuilder.Entity<Persona>()
                .Property(p => p.Cedula)
                .HasMaxLength(11)
                .IsRequired();

            modelBuilder.Entity<Persona>()
                .Property(p => p.Nombre)
                .HasMaxLength(30)
                .IsRequired();
            modelBuilder.Entity<Persona>()
                .Property(p => p.Apellidos)
                .HasMaxLength(60)
                .IsRequired();
            modelBuilder.Entity<Persona>()
                .Property(p => p.Genero)
                .HasMaxLength(1)
                .IsRequired();
            modelBuilder.Entity<Persona>()
                .Property(p => p.FechaNacimiento)
                .IsRequired();

            modelBuilder.Entity<Persona>()// key
                .HasKey(p => p.Id);
            modelBuilder.Entity<Persona>()
                .Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Correo>()
                .HasOptional<Persona>(c => c.Persona)
                .WithOptionalPrincipal(ad => ad.Correo);

            modelBuilder.Entity<Direccion>()
             .HasOptional<Persona>(c => c.Persona)
             .WithOptionalPrincipal(ad => ad.Direccion);

           
           
           //Configuracion tabla telefono
           modelBuilder.Entity<Telefono>()
                .ToTable("Telefonos");
            modelBuilder.Entity<Telefono>()
                .Property(t => t.TipoTelefono)
                .HasMaxLength(10)
                .IsRequired();
            modelBuilder.Entity<Telefono>()
                .Property(t => t.Numero)
                .HasMaxLength(20)
                .IsRequired();
            modelBuilder.Entity<Telefono>()//key
                .HasKey(t => t.Id);
            modelBuilder.Entity<Telefono>()
                .Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

           
                
            //configuracion tabla direccion
            modelBuilder.Entity<Direccion>()
                .ToTable("Direcciones");
            modelBuilder.Entity<Direccion>()
                .Property(d => d.Pais)
                .HasMaxLength(50)
                .IsRequired();
            modelBuilder.Entity<Direccion>()
                .Property(d => d.DireccionP)
                .HasMaxLength(100)
                .IsRequired();
            modelBuilder.Entity<Direccion>()//key
                .HasKey(d => d.Id);
            modelBuilder.Entity<Direccion>()
                .Property(d => d.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
           
            //configuracion tabla correos
            modelBuilder.Entity<Correo>()
                .ToTable("Correos");
            modelBuilder.Entity<Correo>()
                .Property(c => c.Email)
                .HasMaxLength(60)
                .IsOptional();
            modelBuilder.Entity<Correo>()//key
                .HasKey(c => c.Id);
            modelBuilder.Entity<Correo>()
                .Property(c => c.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

           

            base.OnModelCreating(modelBuilder);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }  
    }
}