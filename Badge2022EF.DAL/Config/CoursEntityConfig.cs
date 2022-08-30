using Badge2022EF.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Badge2022EF.DAL.Config
{
    public class CoursEntityConfig : IEntityTypeConfiguration<CoursEntity>
        {
        public void Configure(EntityTypeBuilder<CoursEntity> builder)
            {
                builder.ToTable("examens");
                builder.HasComment("TRIAL");

                builder.HasKey(m => m.cid)
                   .HasName("PK_ExamenEntity")
                   .IsClustered();

                builder.Property(e => e.cnom)
                    .HasColumnType("nvarchar(256)")
                    .HasColumnName("enom")
                    .IsRequired()
                    .HasComment("TRIAL");

                builder.HasMany(c => c.cexams)
                       .WithOne(e => e.eCours);


                builder.HasMany(c => c.cform)
                       .WithOne(e => e.fCours);

        }
    }
}
