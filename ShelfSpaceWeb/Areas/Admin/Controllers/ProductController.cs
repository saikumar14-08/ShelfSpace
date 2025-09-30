using Microsoft.AspNetCore.Mvc;
using Shelf.DataAccess.Repository.IRepository;
using Shelf.Models;

namespace ShelfSpaceWeb.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork db)
        {
            _unitOfWork = db;
        }
        public IActionResult Index()
        {
            List<Product> products = _unitOfWork.Product.GetAll().ToList();
            return View(products);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(product);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(); 
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product? prodFromDb = _unitOfWork.Product.Get(u => u.Id == id);
            if (prodFromDb == null)
            {
                return NotFound();
            }
            return View(prodFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(product);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            Product? prodFromDb = _unitOfWork.Product.Get(u => u.Id == id);
            if (prodFromDb == null)
            {
                return NotFound();
            }
            return View(prodFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult Delete(int id)
        {
            Product? obj = _unitOfWork.Product.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}
