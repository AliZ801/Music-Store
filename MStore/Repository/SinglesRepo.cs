using MStore.Data;
using MStore.Models;
using MStore.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MStore.Repository
{
    public class SinglesRepo : Repository<Singles>, ISinglesRepo
    {
        private readonly ApplicationDbContext _db;

        public SinglesRepo(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Singles singles)
        {
            var sFromDb = _db.Singles.FirstOrDefault(i => i.Id == singles.Id);

            sFromDb.Title = singles.Title;
            sFromDb.Artist = singles.Artist;
            sFromDb.GenreId = singles.GenreId;
            sFromDb.ReleasedDate = singles.ReleasedDate;
            sFromDb.ImageUrl = singles.ImageUrl;

            _db.SaveChanges();
        }
    }
}
