using Badge2022EF.DAL.Entities;
using Badge2022EF.Models.Concretes;

namespace Badge2022EF.DAL.Repositories.Mappers
    {
    public static class MedicamentMappers
        {
        public static Medicaments ToModel(this MedicamentEntity Entity)
            {
            Medicaments Medicament = new(Entity.Id, Entity.Nom ?? "")
            {
                MedicamentId = Entity.Id
            };
            return Medicament;
            }

        public static MedicamentEntity ToEntity(this Medicaments Model)
            {
            return new MedicamentEntity()
                {
                Id = Model.MedicamentId,
                Nom = Model.MedicamentNom
                };
            }
        }
    }
