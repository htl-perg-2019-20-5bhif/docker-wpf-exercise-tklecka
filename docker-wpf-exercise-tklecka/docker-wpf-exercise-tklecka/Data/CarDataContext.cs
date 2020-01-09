using docker_wpf_exercise_tklecka.Model;
using Microsoft.EntityFrameworkCore;

namespace docker_wpf_exercise_tklecka.Data
{
    public class CarDataContext : DbContext
    {
        public CarDataContext(DbContextOptions<CarDataContext> options) : base(options) { }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }
}
