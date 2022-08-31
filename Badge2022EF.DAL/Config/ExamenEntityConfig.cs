using Badge2022EF.DAL.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System.Diagnostics;
using System.Reflection.Emit;

namespace Badge2022EF.DAL.Config
    {
    public class ExamenEntityConfig : IEntityTypeConfiguration<ExamenEntity>
        {
        public void Configure(EntityTypeBuilder<ExamenEntity> builder)
            {
                builder.ToTable("examens");
                builder.HasComment("TRIAL");

                builder.HasKey(e => new { e.eid })
                   .IsClustered();

                builder.Property(e => e.enom)
                    .HasColumnType("nvarchar(256)")
                    .HasColumnName("enom")
                    .IsRequired()
                    .HasComment("TRIAL");
        }
    }
}
