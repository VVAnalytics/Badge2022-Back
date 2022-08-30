using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Badge2022EF.DAL.Config
    {
    public class ExamenEntityConfig : IEntityTypeConfiguration<ExamenEntity>
        {
        public void Configure(EntityTypeBuilder<ExamenEntity> builder)
            {
                builder.ToTable("examens");
                builder.HasComment("TRIAL");

                builder.HasKey(m => m.eid)
                   .HasName("PK_ExamenEntity")
                   .IsClustered();

                builder.Property(e => e.enom)
                    .HasColumnType("nvarchar(256)")
                    .HasColumnName("enom")
                    .IsRequired()
                    .HasComment("TRIAL");
        }
    }
}
