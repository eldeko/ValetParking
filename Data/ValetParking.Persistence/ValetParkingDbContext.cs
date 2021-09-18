using Microsoft.EntityFrameworkCore;
using ValetParking.Persistence.Entities;
using Microsoft.Extensions.Configuration;
namespace ValetParking.Persistence
    {
    public class ValetParkingDbContext : DbContext
    {       

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
         optionsBuilder.UseSqlServer("Data Source=clusterdbinstance.chnoqufozou7.sa-east-1.rds.amazonaws.com;Persist Security Info=False;User ID=sa;Password=ElDeko87;Initial Catalog=ValetParkingDB;");
        }

        //Configurations to be replaced by key vault
        public DbSet<ConfigurationEntity> ConfigurationEntities { get; set; }

        public DbSet<ParkingSlotEntity> ParkingSlots  { get; set; }
        public DbSet<PasswordRecoveryEntity> PasswordRecoveryEntities { get; set; }
        public DbSet<ReservationEntity> Reservations { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<VehicleEntity> Vehicles { get; set; }        
    }
}
