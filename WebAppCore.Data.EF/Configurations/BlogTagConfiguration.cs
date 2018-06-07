﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAppCore.Data.EF.Extensions;
using WebAppCore.Data.Entities;

namespace WebAppCore.Data.EF.Configurations
{
    public class BlogTagConfiguration : DbEntityConfiguration<BlogTag>
    {
        public override void Configure(EntityTypeBuilder<BlogTag> entity)
        {
            entity.Property(c => c.TagId).HasMaxLength(50).IsRequired()
            .HasColumnType("varchar(50)");
            // etc.
        }
    }
}