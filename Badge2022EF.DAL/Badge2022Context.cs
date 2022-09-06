// EntityFrameworkCore\Add-Migration MyFirstMigration
// EntityFrameworkCore\update-database

using Badge2022EF.DAL.Config;
using Badge2022EF.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Badge2022EF.DAL
{
    public partial class Badge2022Context : IdentityDbContext<PersonneEntity, RoleEntity, int>
    {
        public Badge2022Context()
        {
        }
        public Badge2022Context(DbContextOptions<Badge2022Context> options)
            : base(options)
        {
            Cours = Set<CoursEntity>();
            Examens = Set<ExamenEntity>();
            Formations = Set<FormationEntity>();
            NotesEleves = Set<NotesEleveEntity>();
            Personnes = Set<PersonneEntity>();
            Roles = Set<RoleEntity>();
        }

        public virtual DbSet<CoursEntity> Cours { get; set; }
        public virtual DbSet<ExamenEntity> Examens { get; set; }
        public virtual DbSet<FormationEntity> Formations { get; set; }
        public virtual DbSet<NotesEleveEntity> NotesEleves { get; set; }
        public override DbSet<RoleEntity> Roles { get; set; }
        public virtual DbSet<PersonneEntity> Personnes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableDetailedErrors();
            if (!optionsBuilder.IsConfigured)
            {
                if (!optionsBuilder.IsConfigured)
                {
                    string csbuilder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .Build().GetConnectionString("Badge2022Works").ToString();

                    optionsBuilder.UseSqlServer(csbuilder);
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CoursEntityConfig());
            modelBuilder.ApplyConfiguration(new ExamenEntityConfig());
            modelBuilder.ApplyConfiguration(new FormationEntityConfig());
            modelBuilder.ApplyConfiguration(new NotesEleveEntityConfig());
            modelBuilder.ApplyConfiguration(new PersonneEntityConfig());
            modelBuilder.ApplyConfiguration(new RoleEntityConfig());

            OnModelCreatingPartial(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
