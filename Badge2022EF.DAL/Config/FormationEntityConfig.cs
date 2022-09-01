using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Badge2022EF.DAL.Config
{
    public class FormationEntityConfig : IEntityTypeConfiguration<FormationEntity>
        {
        public void Configure(EntityTypeBuilder<FormationEntity> builder)
            {
                builder.ToTable("formations");
                builder.HasComment("TRIAL");

                builder.HasKey(m => m.fid)
                   .HasName("PK_ExamenEntity")
                   .IsClustered();
                builder
                    .Property(m => m.fid)
                    .ValueGeneratedOnAdd();

                builder.Property(e => e.fnom)
                    .HasColumnType("nvarchar(256)")
                    .HasColumnName("fnom")
                    .IsRequired()
                    .HasComment("TRIAL");
                
                builder.HasMany(c => c.fCours)
                       .WithOne(e => e.cform);

                builder.HasMany(c => c.fPersonnes)
                       .WithOne(e => e.uformation);


        }
    }
}
