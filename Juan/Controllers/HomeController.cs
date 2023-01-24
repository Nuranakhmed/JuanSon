using Core.Entities;
using DAL;
using Microsoft.AspNetCore.Mvc;

namespace Juan.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;
        public HomeController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<SlideItem> items = _db.SlideItems;
            return View(items);
        }
    }
}
