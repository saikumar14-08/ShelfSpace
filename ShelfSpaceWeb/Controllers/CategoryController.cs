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
                ModelState.AddModelError("Name","Category Name and Display Order cannot be same.");
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

        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
// Here, you can also navigate to your own page.
                return NotFound();
            }
            Category? categoryFromDb = _db.Categories.Find(id);
            //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u => u.Id == id);
            //Category? categoryFromDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();

            if(categoryFromDb == null)
            {
                return NotFound();
            }    
            return View(categoryFromDb);
        }
        // This method is to edit the data of the category table.
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            // After saving the changes to DB we have to redirect to our Index
            //where we get to see all the categories.
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                // Here, you can also navigate to your own page.
                return NotFound();
            }
            Category? categoryFromDb = _db.Categories.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Categories.Find(id);
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
