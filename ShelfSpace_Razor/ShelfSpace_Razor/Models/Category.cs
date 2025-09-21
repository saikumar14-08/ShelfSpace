using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShelfSpace_Razor.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please give Category name")]
        [MaxLength(100)]
        [DisplayName("Category Name:")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please give a display order value")]
        [Range(1, 100, ErrorMessage = "Range in between 1 and 100 only. 🙂")]
        [DisplayName("Display Order:")]
        public int? DisplayOrder { get; set; }
    }
}
