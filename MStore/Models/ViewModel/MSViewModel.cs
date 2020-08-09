using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MStore.Models.ViewModel
{
    public class MSViewModel
    {
        public Genres Genres { get; set; }

        public Singles Singles { get; set; }

        //DropDown Lists
        public IEnumerable<SelectListItem> GenresList { get; set; }
    }
}
