using Badge2022EF.DAL.Entities;
using Badge2022EF.Models.Concretes;

namespace Badge2022EF.DAL.Repositories.Mappers
{
    public static class MedecinMappers  
    {
        public static NotesEleves ToModel(this MedecinEntity Entity)
        {
            NotesEleves Medecin = new(Entity.Nom ?? "",
                                    Entity.Inami ?? "",
                                    Entity.Rue ?? "",
                                    Entity.Ville ?? "",
                                    Entity.Telephone ?? "",
                                    Entity.Gsm ?? "",
                                    Entity.Fax ?? "",
                                    Entity.Email ?? "")
            {
                MedecinId = Entity.IdMedecin
            };
            return Medecin;
        }

        public static MedecinEntity ToEntity(this NotesEleves Model)
        {
            return   new MedecinEntity()
            {
                IdMedecin = Model.MedecinId,
                Nom = Model.MedecinName,
                Inami = Model.MedecinInami,
                Rue = Model.MedecinRue,
                Telephone = Model.MedecinTelephone,
                Gsm = Model.MedecinGsm,
                Fax = Model.MedecinFax,
                Email = Model.MedecinEmail,
                Ville = Model.MedecinVille
                };
        }
    }
}
