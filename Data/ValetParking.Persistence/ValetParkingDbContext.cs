using Microsoft.EntityFrameworkCore;
using ValetParking.Persistence.Entities;

namespace ValetParking.Persistence
    {
    public class ValetParkingDbContext : DbContext
    {       
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
         optionsBuilder.UseSqlServer("Data Source=EZE1-LLN-B02894;Persist Security Info=False;User ID=sa;Password=ElDeko87;Initial Catalog=ValetParkingDB;");
        }

        public DbSet<ConfigurationEntity> ConfigurationEntities { get; set; }
        public DbSet<ParkingSlotEntity> ParkingSlots  { get; set; }
        public DbSet<PasswordRecoveryEntity> PasswordRecoveryEntities { get; set; }
        public DbSet<ReservationEntity> Reservations { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<VehicleEntity> Vehicles { get; set; }        
    }
}