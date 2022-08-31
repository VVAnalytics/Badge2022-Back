using Badge2022EF.DAL.Entities;
using Badge2022EF.Models.Concretes;

namespace Badge2022EF.DAL.Repositories.Mappers
    {
    public static class CourseMappers
        {
        public static Cours ToModel(this CoursEntity Entity)
            {
            Cours cours = new(Entity.cid, Entity.cnom ?? "")
            {
                cid = Entity.cid,
                cnom = Entity.cnom ?? ""
            };
            return cours;
            }

        public static CoursEntity ToEntity(this Cours Model)
            {
            return new CoursEntity()
                {
                cid = Model.cid,
                cnom = Model.cnom,
                };
            }
        }
    }
