using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShelfSpace_Razor.Data;
using ShelfSpace_Razor.Models;

namespace ShelfSpace_Razor.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDBContext _db;
        public IndexModel(ApplicationDBContext db)
        {
            _db = db;
        }
        public List<Category> CategoryList { get; set; } = new();
        public void OnGet()
        {
            CategoryList = _db.Categories.ToList();

        }
    }
}
