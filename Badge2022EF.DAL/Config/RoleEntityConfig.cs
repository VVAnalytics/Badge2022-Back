﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Badge2022EF.DAL.Config 
{
    public class RoleEntityConfig : IEntityTypeConfiguration<RoleEntity>
    {
        public void Configure(EntityTypeBuilder<RoleEntity> builder)
        {
            builder.ToTable("role");
            builder.HasComment("TRIAL");

            builder.HasKey(m => m.Id)
           .HasName("PK_RoleEntity")
           .IsClustered();
            builder
            .Property(m => m.Id)
            .ValueGeneratedOnAdd();

            builder.Property(e => e.Name)
            .HasColumnType("nvarchar(256)")
            .HasColumnName("Name")
            .IsRequired()
            .HasComment("TRIAL");
            builder.HasIndex(x => x.Name).IsUnique();
        }
    }
}
