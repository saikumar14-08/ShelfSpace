using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShelfSpace_Razor.Data;
using ShelfSpace_Razor.Models;

namespace ShelfSpace_Razor.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDBContext _db;
        public CreateModel(ApplicationDBContext db)
        {
            _db = db;
        }
        [BindProperty]
        public Category Category { get; set; } = new();
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            _db.Add(Category);
            _db.SaveChanges();
            TempData["success"] = "Category created successfully!!";
            return RedirectToPage("Index");
        }
    }
}
