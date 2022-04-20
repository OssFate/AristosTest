using AristosTest.Shared.DataModel;
using Microsoft.EntityFrameworkCore;

namespace AristosTest.Api.Data;

public class AirportContext : DbContext
{
    public AirportContext(DbContextOptions<AirportContext> options) : base(options) { }

    public DbSet<AirLine> AirLines => Set<AirLine>();
    public DbSet<Airport> Airports => Set<Airport>();
    public DbSet<PassengerType> PassengerTypes => Set<PassengerType>();
    public DbSet<Reservation> Reservations => Set<Reservation>();   
}
