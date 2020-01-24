﻿using Test.Db.Model.Placeholder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Test.Db.Model.Configuration
{
    public class PlaceholderEntityConfig : IEntityTypeConfiguration<PlaceholderEntity>
    {
        public void Configure(EntityTypeBuilder<PlaceholderEntity> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
