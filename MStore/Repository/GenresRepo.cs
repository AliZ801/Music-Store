using Microsoft.AspNetCore.Mvc.Rendering;
using MStore.Data;
using MStore.Models;
using MStore.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MStore.Repository
{
    public class GenresRepo : Repository<Genres>, IGenresRepo
    {
        private readonly ApplicationDbContext _db;

        public GenresRepo(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetDropDownListForGenres()
        {
            return _db.Genres.Select(i => new SelectListItem()
            {
                Text = i.Genre,
                Value = i.Id.ToString()
            });
        }

        public void Update(Genres genres)
        {
            var gFromDb = _db.Genres.FirstOrDefault(i => i.Id == genres.Id);

            gFromDb.Genre = genres.Genre;

            _db.SaveChanges();
        }
    }
}
