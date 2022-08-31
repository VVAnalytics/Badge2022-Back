using Badge2022EF.DAL.Entities;
using Badge2022EF.Models.Concretes;

namespace Badge2022EF.DAL.Repositories.Mappers
    {
    public static class FormationsMapper
    {
        public static Formations ToModel(this FormationEntity Entity)
            {
            Formations Formation = new(Entity.fid,
                                    Entity.fnom)
            {
                fid = Entity.fid,
                fnom = Entity.fnom,
            };
            return Formation;
            }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static FormationEntity ToEntity(this Formations Model)
            {
            return new FormationEntity()
                {
                fid = Model.fid,
                fnom = Model.fnom,
                };
            }
        }
    }
