using Badge2022EF.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Badge2022EF.DAL.Config
{
    public class PersonneEntityConfig : IEntityTypeConfiguration<PersonneEntity>
    {
        public void Configure(EntityTypeBuilder<PersonneEntity> builder)
        {
            builder.ToTable("personne");
            builder.HasComment("TRIAL");

            /* builder.HasKey(m => m.Id)
           .HasName("PK_PersonneEntity")
           .IsClustered(); */

            builder.Property(e => e.unom)
            .HasColumnType("nvarchar(256)")
            .HasColumnName("unom")
            .IsRequired()
            .HasComment("TRIAL");

            builder.Property(e => e.uprenom)
            .HasColumnType("nvarchar(256)")
            .HasColumnName("uprenom")
            .IsRequired()
            .HasComment("TRIAL");

            builder.Property(e => e.Email)
            .HasColumnType("nvarchar(256)")
            .HasColumnName("email")
            .IsRequired()
            .HasComment("TRIAL");

            builder.Property(e => e.udate)
            .HasColumnType("date")
            .HasColumnName("udate_naissance")
            .IsRequired()
            .HasComment("TRIAL");

            builder.Property(e => e.urue)
            .HasColumnType("nvarchar(256)")
            .HasColumnName("urue")
            .IsRequired()
            .HasComment("TRIAL");

            builder.Property(e => e.ucodep)
            .HasColumnType("nvarchar(5)")
            .HasColumnName("ucodep")
            .IsRequired()
            .HasComment("TRIAL");

            builder.Property(e => e.uville)
            .HasColumnType("nvarchar(256)")
            .HasColumnName("uville")
            .IsRequired()
            .HasComment("TRIAL");

            builder.Property(e => e.upays)
            .HasColumnType("nvarchar(256)")
            .HasColumnName("upays")
            .IsRequired()
            .HasComment("TRIAL");

            builder.HasMany(u => u.urole)
                    .WithMany(r => r.Personnes)
                    .UsingEntity<RoleEntity>(
                        userRole => userRole.HasOne<RoleEntity>()
                            .WithMany()
                            .HasForeignKey(ur => ur.Id)
                            .IsRequired(),
                        userRole => userRole.HasOne<PersonneEntity>()
                            .WithMany()
                            .HasForeignKey(us => us.Id)
                            .IsRequired());

            builder.HasOne(c => c.uformation)
                   .WithMany(e => e.fPersonnes);

            builder.HasIndex(x => x.Email).IsUnique();
        }
    }
}
