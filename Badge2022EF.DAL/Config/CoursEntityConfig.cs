using Badge2022EF.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics;

namespace Badge2022EF.DAL.Config
{
    public class CoursEntityConfig : IEntityTypeConfiguration<CoursEntity>
        {
        public void Configure(EntityTypeBuilder<CoursEntity> builder)
            {
                builder.ToTable("cours");
                builder.HasComment("TRIAL");

                builder.HasKey(c => new { c.cid })
                   .IsClustered();

                builder.Property(e => e.cnom)
                    .HasColumnType("nvarchar(256)")
                    .HasColumnName("enom")
                    .IsRequired()
                    .HasComment("TRIAL");

                builder.HasMany<ExamenEntity>(e => e.cexams)
                       .WithOne(c => c.eCours);
        }
    }
}
