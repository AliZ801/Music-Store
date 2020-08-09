using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MStore.Models.ViewModel;
using MStore.Repository.IRepository;

namespace MStore.Controllers
{
    public class Singles : Controller
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        [BindProperty]
        public MSViewModel MSVM { get; set; }

        public Singles(IUnitofWork unitofWork, IWebHostEnvironment hostEnvironment)
        {
            _unitofWork = unitofWork;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddSingle(int? id)
        {
            MSVM = new MSViewModel()
            {
                Singles = new Models.Singles(),
                GenresList = _unitofWork.Genres.GetDropDownListForGenres()
            };

            if(id != null)
            {
                MSVM.Singles = _unitofWork.Singles.Get(id.GetValueOrDefault());
            }

            return View(MSVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddSingle()
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;

                if(MSVM.Singles.Id == 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"Images\Singles");
                    var extension = Path.GetExtension(files[0].FileName);

                    using(var fileStreams = new FileStream(Path.Combine(uploads, fileName+extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStreams);
                    }

                    MSVM.Singles.ImageUrl = @"\Images\Singles\" + fileName + extension;

                    _unitofWork.Singles.Add(MSVM.Singles);
                }
                else
                {
                    var sFromDb = _unitofWork.Singles.Get(MSVM.Singles.Id);

                    if(files.Count > 0)
                    {
                        string fileName = Guid.NewGuid().ToString();
                        var uploads = Path.Combine(webRootPath, @"Images\Singles");
                        var extension_new = Path.GetExtension(files[0].FileName);
                        var imagePath = Path.Combine(webRootPath, sFromDb.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }

                        MSVM.Singles.ImageUrl = @"\Images\Singles\" + fileName + extension_new;
                    }
                    else
                    {
                        MSVM.Singles.ImageUrl = sFromDb.ImageUrl;
                    }

                    _unitofWork.Singles.Update(MSVM.Singles);
                }

                _unitofWork.Save();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                MSVM.GenresList = _unitofWork.Genres.GetDropDownListForGenres();

                return View(MSVM);
            }
        }

        #region API CALLS

        public IActionResult GetAll()
        {
            return Json(new { data = _unitofWork.Singles.GetAll(includeProperties: "Genres") });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var sFromDb = _unitofWork.Singles.Get(id);

            if(sFromDb == null)
            {
                return Json(new { success = false, message = "Error Deleting Single!" });
            }

            _unitofWork.Singles.Remove(sFromDb);
            _unitofWork.Save();

            return Json(new { success = true, message = "Single Deleted Successfully!" });
        }

        #endregion
    }
}
