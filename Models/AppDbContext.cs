using Microsoft.EntityFrameworkCore;

namespace TestApp.Models
{
    public class AppDbContext : DbContext
    {

        public DbSet<Persona> tblPersonas { get; set; }
        //public DbSet<HorarioIn> tblHorarioIn { get; set; }
        //public DbSet<HorarioOut> tblHorarioOut { get; set; }
        public DbSet<Horario> tblHorarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = configuration
                .GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }

    }
}
