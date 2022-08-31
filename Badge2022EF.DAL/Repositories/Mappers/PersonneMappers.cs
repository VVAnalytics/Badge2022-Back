using Badge2022EF.DAL.Entities;
using Badge2022EF.Models.Concretes;

namespace Badge2022EF.DAL.Repositories.Mappers
{
    public static class PersonneMappers
    {
        public static Personnes ToModel(this PersonneEntity? Entity)
        {
            Personnes Personne = new(
                uNom: Entity?.unom ?? string.Empty,
                uPrenom: Entity?.uprenom ?? string.Empty,
                uEmail: Entity?.Email ?? string.Empty,
                uDate: Entity?.udate ?? DateTime.MinValue,
                uRue: Entity?.urue ?? string.Empty,
                uCodep: Entity?.ucodep ?? string.Empty,
                uVille: Entity?.uville ?? string.Empty,
                uPays: Entity?.upays ?? string.Empty
                )
            {
                Id = Entity?.Id.ToString()
            };
            return Personne;
        }

        public static PersonneEntity ToEntity(this Personnes Model)
        {
            return new PersonneEntity()
            {
                unom = Model.unom,
                uprenom = Model.uprenom,
                Email = Model.Email,
                udate = Model.udate,
                urue = Model.urue,
                ucodep = Model.ucodep,
                uville = Model.uville,
                upays = Model.upays
            };
        }

    }
}
