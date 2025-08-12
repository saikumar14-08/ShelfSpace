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

// This method is to handle the POST call for the Category table.
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if(obj.Name.ToLower() == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name","Category Name and Display Order cannot be same.");
            }
            if(ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            // After saving the changes to DB we have to redirect to our Index
            //where we get to see all the categories.
            return View();
        }
    }
}
