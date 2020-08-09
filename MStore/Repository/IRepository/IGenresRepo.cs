using Microsoft.AspNetCore.Mvc.Rendering;
using MStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MStore.Repository.IRepository
{
    public interface IGenresRepo : IRepository<Genres>
    {
        IEnumerable<SelectListItem> GetDropDownListForGenres();

        void Update(Genres genres);
    }
}
