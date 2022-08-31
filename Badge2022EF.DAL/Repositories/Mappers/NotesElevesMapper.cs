using Badge2022EF.DAL.Entities;
using Badge2022EF.Models.Concretes;

namespace Badge2022EF.DAL.Repositories.Mappers
    {
    public static class NotesElevesMapper
    {
        public static NotesEleves ToModel(this NotesEleveEntity Entity)
            {
            NotesEleves NotesEleve = new(Entity.nnote)
            {
                npid = Entity.npid,
                ncid = Entity.ncid,
                nnote = Entity.nnote
            };
            return NotesEleve;
            }

        public static NotesEleveEntity ToEntity(this NotesEleves Model)
            {
            return new NotesEleveEntity()
                {
                npid = Model.npid,
                ncid = Model.ncid,
                nnote = Model.nnote
            };
            }
        }
    }
