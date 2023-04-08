using System.ComponentModel.DataAnnotations;

namespace Farma.DTO
{
    public class ProductTypeCreateDTO
    {
        [Required(ErrorMessage = "Category must have a name.")]
        [MaxLength(30, ErrorMessage = "Product category must be less than 30 characters.")]
        public string Category { get; set; } = string.Empty!;

        [Required(ErrorMessage = "Category must have a description.")]
        [MaxLength(200, ErrorMessage = "Category description must be less than 200 characters.")]
        public string CategoryDescription { get; set; } = string.Empty!;
    }
}
