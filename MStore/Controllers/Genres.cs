using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MStore.Models.ViewModel;
using MStore.Repository.IRepository;

namespace MStore.Controllers
{
    public class Genres : Controller
    {
        private readonly IUnitofWork _unitofWork;

        [BindProperty]
        public MSViewModel MSVM { get; set; }

        public Genres(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddGenre(int? id)
        {
            MSVM = new MSViewModel() { Genres = new Models.Genres() };

            if(id != null)
            {
                MSVM.Genres = _unitofWork.Genres.Get(id.GetValueOrDefault());
            }

            return View(MSVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddGenre()
        {
            if (ModelState.IsValid)
            {
                if(MSVM.Genres.Id == 0)
                {
                    _unitofWork.Genres.Add(MSVM.Genres);
                }
                else
                {
                    _unitofWork.Genres.Update(MSVM.Genres);
                }

                _unitofWork.Save();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(MSVM);
            }
        }

        #region API CALLS

        public IActionResult GetAll()
        {
            return Json(new { data = _unitofWork.Genres.GetAll() });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var gFromDb = _unitofWork.Genres.Get(id);

            if(gFromDb == null)
            {
                return Json(new { success = false, message = "Error Deleting Genre!" });
            }

            _unitofWork.Genres.Remove(gFromDb);
            _unitofWork.Save();

            return Json(new { success = true, message = "Genre Deleted Successfully!" });
        }

        #endregion
    }
}
