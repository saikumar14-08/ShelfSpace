using Microsoft.AspNetCore.Mvc;
using Shelf.DataAccess.Data;
using Shelf.DataAccess.Repository.IRepository;


using Shelf.Models;

namespace ShelfSpaceWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork; 
        public CategoryController(IUnitOfWork db)
        {
            _unitOfWork = db;
        }
        public IActionResult Index()
        {
            List<Category> cats = _unitOfWork.Category.GetAll().ToList();
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
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category created successfully!";
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
            Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);
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
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category updated successfully!";
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
                return NotFound();
            }
            Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? obj = _unitOfWork.Category.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully!";
            return RedirectToAction("Index");         
        }
    }
}
