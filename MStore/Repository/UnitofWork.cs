using MStore.Data;
using MStore.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MStore.Repository
{
    public class UnitofWork : IUnitofWork
    {
        private readonly ApplicationDbContext _db;

        public UnitofWork(ApplicationDbContext db)
        {
            _db = db;
            Genres = new GenresRepo(_db);
            Singles = new SinglesRepo(_db);
        }

        public IGenresRepo Genres { get; private set; }

        public ISinglesRepo Singles { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
