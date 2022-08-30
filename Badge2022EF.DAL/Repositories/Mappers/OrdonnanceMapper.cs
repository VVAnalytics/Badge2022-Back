using Badge2022EF.DAL.Entities;
using Badge2022EF.Models.Concretes;

namespace Badge2022EF.DAL.Repositories.Mappers
    {
    public static class OrdonnanceMappers
        {
        public static Formations ToModel(this OrdonnanceEntity Entity)
            {
            Formations Ordonnance = new(Entity.Id,
                                    Entity.Codebarre ?? "",
                                    Entity.Datecree,
                                    Entity.Dateexpire,
                                    Entity.Medecinid,
                                    Entity.Pharmacieid,
                                    Entity.Patientid)
                                    ;
            return Ordonnance;
            }

        public static OrdonnanceEntity ToEntity(this Formations Model)
            {
            return new OrdonnanceEntity()
                {
                Id = Model.OrdonnanceId,
                Codebarre = Model.OrdonnanceCode_barre,
                Datecree = Model.OrdonnanceDate_creer,
                Dateexpire = Model.OrdonnanceDate_expire,
                Medecinid = Model.OrdonnanceMedecinid,
                Pharmacieid = Model.OrdonnancePharmacieid,
                Patientid = Model.OrdonnancePatient
                };
            }
        }
    }

