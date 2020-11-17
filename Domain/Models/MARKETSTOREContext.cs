using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace Domain.Models
{
    public partial class MARKETSTOREContext : DbContext
    {
        public MARKETSTOREContext()
        {
        }

        public MARKETSTOREContext(DbContextOptions<MARKETSTOREContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Banner> Banner { get; set; }
        public virtual DbSet<Bannerdetalle> Bannerdetalle { get; set; }
        public virtual DbSet<Canasta> Canasta { get; set; }
        public virtual DbSet<Canastadetalle> Canastadetalle { get; set; }
        public virtual DbSet<Carrito> Carrito { get; set; }
        public virtual DbSet<Carritoproducto> Carritoproducto { get; set; }
        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Direccion> Direccion { get; set; }
        public virtual DbSet<Especificacion> Especificacion { get; set; }
        public virtual DbSet<Favorito> Favorito { get; set; }
        public virtual DbSet<Ordencompra> Ordencompra { get; set; }
        public virtual DbSet<Ordencompradetalle> Ordencompradetalle { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                Console.WriteLine(System.IO.Directory.GetCurrentDirectory());
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
                optionsBuilder.UseSqlServer("Name=connectionDB");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Banner>(entity =>
            {
                entity.ToTable("BANNER");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.FechaFin).HasColumnType("datetime");

                entity.Property(e => e.FechaMod).HasColumnType("datetime");

                entity.Property(e => e.FechaReg).HasColumnType("datetime");

                entity.Property(e => e.Imagen)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.Link).HasColumnType("text");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Bannerdetalle>(entity =>
            {
                entity.ToTable("BANNERDETALLE");

                entity.HasOne(d => d.Banner)
                    .WithMany(p => p.Bannerdetalle)
                    .HasForeignKey(d => d.BannerId)
                    .HasConstraintName("FK__BANNERDET__Banne__619B8048");

                entity.HasOne(d => d.Producto)
                    .WithMany(p => p.Bannerdetalle)
                    .HasForeignKey(d => d.ProductoId)
                    .HasConstraintName("FK__BANNERDET__Produ__628FA481");
            });

            modelBuilder.Entity<Canasta>(entity =>
            {
                entity.ToTable("CANASTA");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.FechaFin).HasColumnType("datetime");

                entity.Property(e => e.FechaMod).HasColumnType("datetime");

                entity.Property(e => e.FechaReg).HasColumnType("datetime");

                entity.Property(e => e.Imagen)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Canastadetalle>(entity =>
            {
                entity.ToTable("CANASTADETALLE");

                entity.HasOne(d => d.Canasta)
                    .WithMany(p => p.Canastadetalle)
                    .HasForeignKey(d => d.CanastaId)
                    .HasConstraintName("FK__CANASTADE__Canas__6754599E");

                entity.HasOne(d => d.Producto)
                    .WithMany(p => p.Canastadetalle)
                    .HasForeignKey(d => d.ProductoId)
                    .HasConstraintName("FK__CANASTADE__Produ__68487DD7");
            });

            modelBuilder.Entity<Carrito>(entity =>
            {
                entity.ToTable("CARRITO");

                entity.Property(e => e.FechaMod).HasColumnType("datetime");

                entity.Property(e => e.FechaReg).HasColumnType("datetime");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Carrito)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CARRITO__Usuario__5165187F");
            });

            modelBuilder.Entity<Carritoproducto>(entity =>
            {
                entity.ToTable("CARRITOPRODUCTO");

                entity.Property(e => e.Subtotal).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.Carrito)
                    .WithMany(p => p.Carritoproducto)
                    .HasForeignKey(d => d.CarritoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CARRITOPR__Carri__5441852A");

                entity.HasOne(d => d.Producto)
                    .WithMany(p => p.Carritoproducto)
                    .HasForeignKey(d => d.ProductoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CARRITOPR__Produ__5535A963");
            });

            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.ToTable("CATEGORIA");

                entity.Property(e => e.Denominacion)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Imagen)
                    .IsRequired()
                    .HasColumnType("text");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("CLIENTE");

                entity.Property(e => e.Apellidos)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Nombres)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Cliente)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CLIENTE__Usuario__440B1D61");
            });

            modelBuilder.Entity<Direccion>(entity =>
            {
                entity.ToTable("DIRECCION");

                entity.Property(e => e.CodigoPostal)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion1)
                    .IsRequired()
                    .HasColumnName("Direccion")
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Numero)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Referencia).HasColumnType("text");

                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Especificacion>(entity =>
            {
                entity.ToTable("ESPECIFICACION");

                entity.Property(e => e.Detalle)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Valor)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.HasOne(d => d.Producto)
                    .WithMany(p => p.Especificacion)
                    .HasForeignKey(d => d.ProductoId)
                    .HasConstraintName("FK__ESPECIFIC__Produ__4E88ABD4");
            });

            modelBuilder.Entity<Favorito>(entity =>
            {
                entity.ToTable("FAVORITO");

                entity.Property(e => e.FechaReg).HasColumnType("datetime");

                entity.HasOne(d => d.Producto)
                    .WithMany(p => p.Favorito)
                    .HasForeignKey(d => d.ProductoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FAVORITO__Produc__59063A47");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Favorito)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FAVORITO__Usuari__5812160E");
            });

            modelBuilder.Entity<Ordencompra>(entity =>
            {
                entity.ToTable("ORDENCOMPRA");

                entity.Property(e => e.Impuesto).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Moneda)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.NroOrdenCompra)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.PrecioEnvio).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Subtotal).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.Carrito)
                    .WithMany(p => p.Ordencompra)
                    .HasForeignKey(d => d.CarritoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ORDENCOMP__Carri__5BE2A6F2");

                entity.HasOne(d => d.Direccion)
                    .WithMany(p => p.Ordencompra)
                    .HasForeignKey(d => d.DireccionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ORDENCOMP__Direc__5CD6CB2B");
            });

            modelBuilder.Entity<Ordencompradetalle>(entity =>
            {
                entity.ToTable("ORDENCOMPRADETALLE");

                entity.Property(e => e.GastoEnvio).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Subtotal).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.OrdenCompra)
                    .WithMany(p => p.Ordencompradetalle)
                    .HasForeignKey(d => d.OrdenCompraId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ORDENCOMP__Orden__70DDC3D8");

                entity.HasOne(d => d.Producto)
                    .WithMany(p => p.Ordencompradetalle)
                    .HasForeignKey(d => d.ProductoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ORDENCOMP__Produ__6FE99F9F");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.ToTable("PRODUCTO");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.Descuento).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FinDescuento).HasColumnType("datetime");

                entity.Property(e => e.Imagen)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.InicioDescuento).HasColumnType("datetime");

                entity.Property(e => e.Medida)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.PalabrasClave).HasColumnType("text");

                entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Resumen)
                    .IsRequired()
                    .HasColumnType("text");

                entity.HasOne(d => d.Categoria)
                    .WithMany(p => p.Producto)
                    .HasForeignKey(d => d.CategoriaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PRODUCTO__Estado__4BAC3F29");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.ToTable("ROL");

                entity.Property(e => e.Denominacion)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("USUARIO");

                entity.Property(e => e.Contrasena)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Correo)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Estado).HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaMod).HasColumnType("datetime");

                entity.Property(e => e.FechaReg).HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Rol)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.RolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__USUARIO__RolId__403A8C7D");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
