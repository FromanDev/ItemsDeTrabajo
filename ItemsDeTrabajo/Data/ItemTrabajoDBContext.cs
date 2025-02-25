using ItemsDeTrabajo.Models;
using Microsoft.EntityFrameworkCore;

namespace ItemsDeTrabajo.Data
{
    public class ItemTrabajoDBContext : DbContext
    {
        public ItemTrabajoDBContext(DbContextOptions<ItemTrabajoDBContext> options) : base(options)
        {
        }

        public virtual DbSet<ItemTrabajo> ItemTrabajo { get; set; }
        public virtual DbSet<DistribucionItemTrabajo> DistribucionItemTrabajo { get; set; }
        public virtual DbSet<Empleado> Empleado { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ItemTrabajo>(entity =>
            {
                entity.HasKey(e => e.IdItem);

                entity.Property(e => e.IdItem)
                    .UseIdentityColumn()
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.NombreItem)
                .HasMaxLength(100);

                entity.Property(e => e.FechaEntregaItem)
                .HasColumnType("datetime");

                entity.Property(e => e.RelevanciaItem)
                .HasColumnType("int");

                entity.Property(e => e.AsignadoUsuario)
                .HasColumnType("int");
            });

            modelBuilder.Entity<DistribucionItemTrabajo>(entity =>
            {
                entity.HasKey(e => e.IdDistribucion);

                entity.Property(e => e.IdDistribucion)
                    .UseIdentityColumn()
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IdItem)
                .HasColumnType("int");

                entity.Property(e => e.IdEmpleado)
                .HasColumnType("int");

                entity.Property(e => e.FechaAsignacion)
                .HasColumnType("datetime");

                entity.Property(e => e.StatusItemTrabajo)
                .HasColumnType("int");
            });

            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.HasKey(e => e.IdEmpleado);

                entity.Property(e => e.IdEmpleado)
                    .UseIdentityColumn()
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.NombreEmpleado)
                .HasMaxLength(100);
            });
        }
    }
}