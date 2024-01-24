using EFCoreMovies.Entities;
using EFCoreMovies.Entities.Keyless;
using EFCoreMovies.Entities.Seeding;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EFCoreMovies
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        // sets the default of types that will affect all tables, no longer need to duplicate model builder property changes for dates, strings, etc.
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<DateTime>().HaveColumnType("date");
            configurationBuilder.Properties<string>().HaveMaxLength(150);
        }

        // we moved these to a config folder in the Entities folder
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // the below method would have to be repeated for every config file
            //modelBuilder.ApplyConfiguration(new GenreConfig());
            // the below method does not have to be repeated for every config file
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            Module3Seeding.Seed(modelBuilder);

            // tutorial has below, modified for pg
            //modelBuilder.Entity<CinemaWithoutLocation>().ToSqlQuery("Select Id, Name FROM Cinemas").ToView(null);
            modelBuilder.Entity<CinemaWithoutLocation>().ToSqlQuery("SELECT \"Id\", \"Name\" FROM public.\"Cinemas\"").ToView(null);
        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<CinemaOffer> CinemaOffers { get; set; }
        public DbSet<CinemaHall> CinemasHalls { get; set; }
        public DbSet<MovieActor> MoviesActors { get; set; }
        public DbSet<CinemaWithoutLocation> CinemaWithoutLocations { get; set; }
    }
}
