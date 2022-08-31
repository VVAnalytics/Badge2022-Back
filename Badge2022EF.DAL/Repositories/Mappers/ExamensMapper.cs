using Badge2022EF.DAL.Entities;
using Badge2022EF.Models.Concretes;

namespace Badge2022EF.DAL.Repositories.Mappers
    {
    public static class ExamensMappers
    {
        public static Examens ToModel(this ExamenEntity Entity)
            {
            Examens Examen = new(Entity.eid,
                                    Entity.enom,
                                    Entity.enote)
            { 
            };

            return Examen;
            }

        public static ExamenEntity ToEntity(this Examens Model)
            {
            return new ExamenEntity()
                {
                eid = Model.eid,
                enom = Model.enom,
                enote = Model.enote,
                };
            }
        }
    }
