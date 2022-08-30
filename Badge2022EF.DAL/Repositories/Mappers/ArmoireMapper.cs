using Badge2022EF.DAL.Entities;
using Badge2022EF.Models.Concretes;

namespace Badge2022EF.DAL.Repositories.Mappers
    {
    public static class ArmoireMappers
        {
        public static Cours ToModel(this ArmoireEntity Entity)
            {
            Cours Armoire = new(Entity.Id, Entity.Nom ?? "", Entity.Patientid)
            {
                ArmoID = Entity.Id,
                ArmoName = Entity.Nom ?? ""
            };
            return Armoire;
            }

        public static ArmoireEntity ToEntity(this Cours Model)
            {
            return new ArmoireEntity()
                {
                Id = Model.ArmoID,
                Nom = Model.ArmoName,
                Patientid = Model.ArmoPatient,
                };
            }
        }
    }
