using Badge2022EF.DAL.Repositories.Interface;

namespace Badge2022EF.DAL.Repositories.Abstracts
{
    public abstract class BaseRepository<T> : IRepository<T>
        where T : class
    {
        protected readonly Badge2022Context _db;
        public BaseRepository(Badge2022Context context)
        {
            _db = context;
        }

        public abstract T GetOne(int id);
        public abstract IEnumerable<T> GetOne2(int id);
        public abstract IEnumerable<T> GetAll();

        public abstract bool Add(T Model);

        public abstract bool Update(T Model);

        public abstract bool Delete(int id);
    }
}
