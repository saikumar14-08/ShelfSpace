using Microsoft.AspNetCore.Mvc;
using ShelfSpaceWeb.Data;
using ShelfSpaceWeb.Models;

namespace ShelfSpaceWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDBContext _db; 
        public CategoryController(ApplicationDBContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> cats = _db.Categories.ToList();
            return View(cats);
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
