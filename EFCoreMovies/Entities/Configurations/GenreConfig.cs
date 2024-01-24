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

            builder.HasQueryFilter(g => !g.IsDeleted);

            builder.HasIndex(p => p.Name).IsUnique().HasFilter("public.\"Genres\".\"IsDeleted\" = 'false'");

            // shadow property
            builder.Property<DateTime>("CreatedDate").HasColumnType("timestamp").HasDefaultValueSql("NOW()");
        }
    }
}
