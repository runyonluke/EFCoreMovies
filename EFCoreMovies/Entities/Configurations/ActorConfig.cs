using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreMovies.Entities.Configurations
{
    public class ActorConfig : IEntityTypeConfiguration<Actor>
    {
        public void Configure(EntityTypeBuilder<Actor> builder)
        {
            builder.Property(p => p.Name)
                // we used the override ConfigureConventions above
                //.HasMaxLength(150)
                .IsRequired();
            // we used the override ConfigureConventions above
            //modelBuilder.Entity<Actor>().Property(p => p.DateOfBirth).HasColumnType("date");
            builder.Property(p => p.Biography).HasColumnType("text");

            // we don't have to do this since we are following the convention by doing "_<lowerCaseFirstLetter><restOfWord>"
            //builder.Property(p => p.Name).HasField("_name");
        }
    }
}
