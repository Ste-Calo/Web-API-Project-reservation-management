using Microsoft.EntityFrameworkCore;
using RestaurantSaaS.Models;

namespace RestaurantSaaS.Data;

public class AppDbContext : DbContext
{
    // Il costruttore: passa le opzioni di configurazione (es. quale motore usare) alla classe base
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // Ecco il tuo DbSet<T>! Spiega a EF Core di creare una tabella per i Tavoli...
    public DbSet<Table> Tables { get; set; }
    
    // ...e una tabella per le Prenotazioni.
    public DbSet<Reservation> Reservations { get; set; }
}