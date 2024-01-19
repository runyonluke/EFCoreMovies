using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace EFCoreMovies.Entities.Configurations
{
    public class GenreConfig : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            //modelBuilder.Entity<Genre>().ToTable(name: "GenresTbl", schema: "movies");
            builder.Property(p => p.Name)
                //.HasColumnName("GenreName")
                .HasMaxLength(150).IsRequired();
        }
    }
}
