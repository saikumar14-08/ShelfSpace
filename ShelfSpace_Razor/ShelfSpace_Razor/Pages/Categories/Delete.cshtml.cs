using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShelfSpace_Razor.Data;
using ShelfSpace_Razor.Models;

namespace ShelfSpace_Razor.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDBContext _db;

        public DeleteModel(ApplicationDBContext db)
        {
            _db = db;
        }
        [BindProperty]
        public Category? Category { get; set; }
        public void OnGet(int? id)
        {
            if (id != null && id != 0)
            {
                Category = _db.Categories.Find(id);
            }
        }

        public IActionResult OnPost()
        {
            Category? obj = _db.Categories.Find(Category.Id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully!!";
            return RedirectToPage("Index");
        }
    }
}
