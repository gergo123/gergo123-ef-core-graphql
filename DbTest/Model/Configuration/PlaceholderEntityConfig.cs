using DbTest.Model.Placeholder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DbTest.Model.Configuration
{
    public class PlaceholderEntityConfig : IEntityTypeConfiguration<PlaceholderEntity>
    {
        public void Configure(EntityTypeBuilder<PlaceholderEntity> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
