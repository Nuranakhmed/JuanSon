using Core.Entities;
using DAL;
using Juan.Areas.Admin.ViewModels.Slider;
using Juan.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Juan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SlideItemController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public SlideItemController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public IActionResult Index()
        {
            IEnumerable<SlideItem> slideItems = _db.SlideItems;
            return View(slideItems);
        }
        //Create get
        public IActionResult Create()
        {
            return View();
        }
        //Create post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderCreateVM slideItem)
        {
            if (slideItem.Photo == null)
            {
                ModelState.AddModelError("Photo", "Bos kecile bilmez ");
                return View();
            }
            if (!slideItem.Photo.CheckFileType("image"))
            {
                ModelState.AddModelError("Photo", "Faylin tipi yalniz sekil olmalidir");
                return View();
            }
            if (slideItem.Photo.CheckFileSize(5))
            {
                ModelState.AddModelError("Photo", "Uzunlugu asa bilmezsiniz");
                return View();
            }

            string wwwroot = _env.WebRootPath;
            var filename = await slideItem.Photo.SaveFileAsync(_env.WebRootPath, "assets", "img", "slider");
            SlideItem dbSlide = new SlideItem
            {
                Title = slideItem.Title,
                SubTitle = slideItem.SubTitle,
                Description = slideItem.Description,
                Photo = filename
            };
            _db.SlideItems.Add(dbSlide);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public  async Task<IActionResult> Detail(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
           SlideItem dbslide=  await _db.SlideItems.FindAsync(id);
            if (dbslide == null)
            {
                return BadRequest();
            }
            return View(dbslide);
        }
        //delete get
        public  IActionResult  Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            SlideItem dbslide =  _db.SlideItems.Find(id);
            if (dbslide == null)
            {
                return BadRequest();
            }
            return View(dbslide);
        }
        //delete post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id,string name)
        {
            SlideItem dbItem=_db.SlideItems.Find(id);
            if (dbItem == null) return BadRequest();
            string path = Path.Combine(_env.WebRootPath, "assets", "img", "slider", dbItem.Photo);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _db.SlideItems.Remove(dbItem);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
