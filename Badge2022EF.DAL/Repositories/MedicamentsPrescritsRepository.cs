using Badge2022EF.DAL.Entities;
using Badge2022EF.DAL.Repositories.Abstracts;
using Badge2022EF.DAL.Repositories.Interface;
using Badge2022EF.DAL.Repositories.Mappers;
using Badge2022EF.Models.Concretes;

using Microsoft.EntityFrameworkCore;

namespace Badge2022EF.DAL.Repositories
    {
    public class MedicamentsPrescritRepository : BaseRepository<MedicamentsPrescrits>, IMedicamentsPrescritsRepository
        {
        public MedicamentsPrescritRepository(BDPMContext context) : base(context)
        {
        }

        public override MedicamentsPrescrits GetOne(long id)
        {
            return _db.MedicamentsPrescrits.Find(id)!.ToModel();
        }
        public override IEnumerable<MedicamentsPrescrits> GetOne2(long id)
            {
            yield return _db.MedicamentsPrescrits.Find(id)!.ToModel();
            }
        public override IEnumerable<MedicamentsPrescrits> GetAll()
            {
            return _db.MedicamentsPrescrits.Select(m => m.ToModel());
            }
        public IEnumerable<MedicamentsPrescrits> GetAll(int limit, int offset)
        {
            return _db.MedicamentsPrescrits.Skip(offset).Take(limit).Select(m => m.ToModel());
        }

        public override bool Add(MedicamentsPrescrits MedicamentsPrescrit)
            {
            MedicamentsPrescritEntity toInsert = MedicamentsPrescrit.ToEntity();
            _db.MedicamentsPrescrits.Add(toInsert);

            try
                {
                _db.SaveChanges();
                return true;
                }
            catch (DbUpdateException)
                {
                _db.MedicamentsPrescrits.Remove(toInsert);
                return false;
                }
            }

        public override bool Update(MedicamentsPrescrits MedicamentsPrescrit)
        {
            MedicamentsPrescritEntity toUpdate = _db.MedicamentsPrescrits.Find(MedicamentsPrescrit.MPMedicamentId)!;
            toUpdate.Medicamentid = MedicamentsPrescrit.MPMedicamentId;
            _db.MedicamentsPrescrits.Remove(_db.MedicamentsPrescrits.Find(MedicamentsPrescrit.MPMedicamentId)!);
            toUpdate = MedicamentsPrescrit.ToEntity();
            _db.MedicamentsPrescrits.Add(toUpdate);
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

        public override bool Delete(long id)
            {
            try
                {
                _db.MedicamentsPrescrits.Remove(_db.MedicamentsPrescrits.Find(id)!);
                _db.SaveChanges();
                return true;
                }
            catch (DbUpdateException)
                {
                return false;

                }
            }
        }
    }