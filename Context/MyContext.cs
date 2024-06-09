using Microsoft.EntityFrameworkCore;
using WebApplication2.Entities;

namespace WebApplication2;

public class MyContext: DbContext
{
    public MyContext()
    {
    }

    public MyContext(DbContextOptions<MyContext> options)
        : base(options)
    {
    }
    public DbSet<BoatStandard> BoatStandards { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<ClientCategory> ClientCategories { get; set; }
    public DbSet<Sailboat> Sailboats  { get; set; }
    public DbSet<SailboatReservation> SailboatReservations  { get; set; }
   
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-K6I7727\\SQLEXPRESS02;Initial Catalog=mydb;Integrated Security=True;Trust Server Certificate=True;");

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ClientCategory>()
            .HasKey(i => i.IdClientCategory);
        builder.Entity<ClientCategory>()
            .Property(x => x.IdClientCategory)
            .ValueGeneratedOnAdd()
            .IsRequired();
        builder.Entity<ClientCategory>()
            .Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();
        builder.Entity<ClientCategory>()
            .Property(e => e.DiscountPerc).HasColumnType("int").IsRequired();
        builder.Entity<ClientCategory>()
            .ToTable("ClientCategory");

        
        builder.Entity<Client>()
            .HasKey(i => i.IdClient);
        builder.Entity<Client>()
            .Property(x => x.IdClient)
            .ValueGeneratedOnAdd()
            .IsRequired();
        builder.Entity<Client>()
            .Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();
        builder.Entity<Client>()
            .Property(x => x.LastName)
            .HasMaxLength(100)
            .IsRequired();
        builder.Entity<Client>()
            .Property(x => x.Birthday)
            .IsRequired();
        builder.Entity<Client>()
            .Property(x => x.Pesel)
            .HasMaxLength(100)
            .IsRequired();
        builder.Entity<Client>()
            .Property(x => x.Email)
            .HasMaxLength(100)
            .IsRequired();
        builder.Entity<Client>()
            .HasOne<ClientCategory>(bents => bents.ClientCategory)
            .WithMany(x => x.Clients)
            .HasForeignKey(x => x.IdClientCategory);
        builder.Entity<Client>().ToTable("Client");
        
        builder.Entity<Reservation>(entity =>
        {
            entity.HasKey(x => x.IdReservation);
            entity.Property(x => x.IdReservation)
                .ValueGeneratedOnAdd()
                .IsRequired();
            entity.Property(x => x.DateTo)
                .IsRequired();
            entity.Property(x => x.DateFrom)
                .IsRequired();
            entity.Property(e => e.Capacity).HasColumnType("int").IsRequired();
            entity.Property(e => e.NumOfBoats).HasColumnType("int").IsRequired();
            entity.Property(e => e.Fullfilled).HasColumnType("bit").IsRequired();
            entity.Property(e => e.Price).HasColumnType("money").IsRequired(false);
            entity.Property(x => x.CancelReason)
                .HasMaxLength(200).IsRequired(false);

            entity.HasOne(x => x.Client)
                .WithMany(x => x.Reservations)
                .HasForeignKey(x => x.IdClient);
            entity.HasOne(x => x.BoatStandard)
                .WithMany(x => x.Reservations)
                .HasForeignKey(x => x.IdBoatStandard);

            entity.ToTable("Reservation");
        });
        builder.Entity<BoatStandard>(entity =>
        {
            entity.HasKey(x => x.IdBoatStandard);
            entity.Property(x => x.IdBoatStandard)
                .ValueGeneratedOnAdd()
                .IsRequired();
            entity.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();
            entity.Property(x => x.Level).IsRequired();

            entity.ToTable("BoatStandard");
        });
        builder.Entity<Sailboat>(entity =>
        {
            entity.HasKey(x => x.IdSailboat);
            entity.Property(x => x.IdSailboat)
                .ValueGeneratedOnAdd()
                .IsRequired();
            entity.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();
            entity.Property(x => x.Description)
                .HasMaxLength(100)
                .IsRequired();
            entity.Property(x => x.Capacity).IsRequired();
            entity.Property(e => e.Price).HasColumnType("money");
            entity.HasOne(x => x.BoatStandard)
                .WithMany(x => x.Sailboats)
                .HasForeignKey(x => x.IdBoatStandard);
            
            entity.ToTable("Sailboat");

        });
        builder.Entity<SailboatReservation>(entity =>
        {
            entity.HasKey(x => x.IdReservation);
            entity.HasKey(x => x.IdSailboat);

            entity.HasOne(d => d.Reservation)
                .WithMany(p => p.SailboatReservations)
                .HasForeignKey(d => d.IdReservation)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Sailboat)
                .WithMany(p => p.SailboatReservations)
                .HasForeignKey(d => d.IdSailboat)
                .OnDelete(DeleteBehavior.ClientSetNull);
            
            entity.ToTable("SailboatReservation");

        });
    }
}