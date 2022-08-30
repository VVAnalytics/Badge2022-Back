using Badge2022EF.DAL.Entities;
using Badge2022EF.Models.Concretes;

namespace Badge2022EF.DAL.Repositories.Mappers
    {
    public static class MedicamentsPrescritMappers
        {
        public static MedicamentsPrescrits ToModel(this MedicamentsPrescritEntity Entity)
            {
            MedicamentsPrescrits MedicamentsPrescrit = new(Entity.Ordonnanceid,
                                    Entity.Medicamentid,
                                    Entity.Quantite,
                                    Entity.Prise);
            return MedicamentsPrescrit;
            }

        public static MedicamentsPrescritEntity ToEntity(this MedicamentsPrescrits Model)
            {
            return new MedicamentsPrescritEntity()
                {
                Ordonnanceid = Model.MPOrdonnanceId,
                Medicamentid = Model.MPMedicamentId,
                Quantite = Model.MPQuantite,
                Prise = Model.MPPrise
                };
            }
        }
    }
