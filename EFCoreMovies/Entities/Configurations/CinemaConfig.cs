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

            builder.HasMany(c => c.CinemasHalls).WithOne(ch => ch.Cinema)
                // not necessary because of convention
                //.HasForeignKey(ch => ch.CinemaId)
                // in the case where a rule would be to have to delete cinema halls before a cinema is deleted
                // will through an error when a cinema with existing cinema halls exist
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.CinemaDetail).WithOne(cd => cd.Cinema).HasForeignKey<CinemaDetail>(cd => cd.Id);

            builder.OwnsOne(c => c.Address, add =>
            {
                add.Property(p => p.Street).HasColumnName("Street");
                add.Property(p => p.Country).HasColumnName("Country");
                add.Property(p => p.Province).HasColumnName("Province");
            });
        }
    }
}
