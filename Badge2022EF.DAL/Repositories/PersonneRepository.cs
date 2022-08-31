using Badge2022EF.DAL.Entities;
using Badge2022EF.DAL.Repositories.Abstracts;
using Badge2022EF.DAL.Repositories.Interface;
using Badge2022EF.DAL.Repositories.Mappers;
using Badge2022EF.Models.Concretes;
using Microsoft.EntityFrameworkCore;

namespace Badge2022EF.DAL.Repositories
{
    public class PersonneRepository : BaseRepository<Personnes>, IPersonneRepository
    {
        public PersonneRepository(Badge2022Context context) : base(context)
        {
        }

        public override Personnes GetOne(int id)
        {
            return _db.Personnes.Find(id)!.ToModel();
        }
        public override IEnumerable<Personnes> GetOne2(int id)
        {
            yield return _db.Personnes.Find(id)!.ToModel();
        }
        public override IEnumerable<Personnes> GetAll()
        {
            return _db.Personnes.Select(m => m.ToModel());
        }
        public override bool Add(Personnes Personne)
        {
            PersonneEntity toInsert = Personne.ToEntity();
            _db.Personnes.Add(toInsert);
            try
            {
                _db.SaveChanges();
                return true;
            }
            catch (DbUpdateException)
            {
                _db.Personnes.Remove(toInsert);
                return false;
            }
        }
        public override bool Update(Personnes Personne)
        {
            PersonneEntity toUpdate = _db.Personnes.Find(Personne.Id)!;
            toUpdate.Id = int.Parse(Personne.Id);
            _db.Personnes.Remove(_db.Personnes.Find(Personne.Id)!);
            toUpdate = Personne.ToEntity();
            _db.Personnes.Add(toUpdate);
            try
            {
                _db.SaveChanges();
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }

        public override bool Delete(int id)
        {
            try
            {
                _db.Personnes.Remove(_db.Personnes.Find(id)!);
                _db.SaveChanges();
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }

        public bool GetUser(string email, string password)
        {
            // https://codepedia.info/jwt-authentication-in-aspnet-core-web-api-token
            foreach (Personnes pers in this.GetAll().ToList())
            {
                if (pers.PasswordHash == password && pers.Email == email ) return true;
            }
            return false;
        }
    }
}
