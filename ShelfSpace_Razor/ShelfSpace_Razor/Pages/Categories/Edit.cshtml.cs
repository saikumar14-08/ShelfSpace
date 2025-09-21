using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShelfSpace_Razor.Data;
using ShelfSpace_Razor.Models;

namespace ShelfSpace_Razor.Pages.Categories
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDBContext _db;

        public EditModel(ApplicationDBContext db) 
        {
            _db = db;            
        }
        [BindProperty]
        public Category? Category { get; set; }
        public void OnGet(int? id)
        {
            if(id != null && id!=0)
            {
                Category = _db.Categories.Find(id);
            }
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _db.Update(Category);
                _db.SaveChanges();
                TempData["success"] = "Category updated successfully!!";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
