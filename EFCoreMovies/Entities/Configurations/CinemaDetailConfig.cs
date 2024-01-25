using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreMovies.Entities.Configurations
{
    public class CinemaDetailConfig : IEntityTypeConfiguration<CinemaDetail>
    {

        public void Configure(EntityTypeBuilder<CinemaDetail> builder)
        {
            // example of table splitting, allows us to use one table in database for multiple entities
            // useful for a very large table and/or if certain properties are rarely used
            builder.ToTable("Cinemas");  
        }
    }
}
