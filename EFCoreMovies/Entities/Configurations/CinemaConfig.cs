using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreMovies.Entities.Configurations
{
    public class CinemaConfig : IEntityTypeConfiguration<Cinema>
    {
        public void Configure(EntityTypeBuilder<Cinema> builder)
        {
            builder.Property(p => p.Name).IsRequired();

            // configure one-to-one relationship between tables
            builder.HasOne(c => c.CinemaOffer).WithOne().HasForeignKey<CinemaOffer>(co => co.CinemaId);

            // not necessary because of convention
            //builder.HasMany(c => c.CinemasHalls).WithOne(ch => ch.Cinema).HasForeignKey(ch => ch.CinemaId);
        }
    }
}
