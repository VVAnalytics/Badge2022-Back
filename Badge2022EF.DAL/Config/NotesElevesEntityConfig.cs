using Badge2022EF.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Badge2022EF.DAL.Config
    {
    public class NotesEleveEntityConfig : IEntityTypeConfiguration<NotesEleveEntity>
        {
        public void Configure(EntityTypeBuilder<NotesEleveEntity> builder)
            {
                builder.ToTable("notesEleve");
                builder.HasComment("TRIAL");

                builder.HasKey(sc => new { sc.npid, sc.ncid });
                builder.HasOne<PersonneEntity>(sc => sc.nPersonnes)
                    .WithMany(s => s.uNotesEleve)
                    .HasForeignKey(sc => sc.npid);

                builder.HasOne<CoursEntity>(sc => sc.nCours)
                    .WithMany(s => s.cNotesEleve)
                    .HasForeignKey(sc => sc.ncid);

                builder.Property(e => e.nnote)
                        .HasColumnName("note")
                        .IsRequired()
                        .HasComment("TRIAL");

            }
        }
    }
