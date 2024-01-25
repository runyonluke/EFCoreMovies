using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace EFCoreMovies.Entities.Configurations
{
    public class MovieActorConfig : IEntityTypeConfiguration<MovieActor>
    {
        public void Configure(EntityTypeBuilder<MovieActor> builder)
        {
            builder.HasKey(p => new { p.MovieId, p.ActorId });

            // one to many relationships
            builder.HasOne(p => p.Actor).WithMany(p => p.MoviesActors).HasForeignKey(p => p.ActorId);
            builder.HasOne(p => p.Movie).WithMany(p => p.MoviesActors).HasForeignKey(p => p.MovieId);
        }
    }
}
