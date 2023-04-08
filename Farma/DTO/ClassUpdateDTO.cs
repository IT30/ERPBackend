using System.ComponentModel.DataAnnotations;

namespace Farma.DTO
{
    public class ClassUpdateDTO
    {
        [Required(ErrorMessage = "Class must have an ID.")]
        public Guid IDClass { get; set; }

        [Required(ErrorMessage = "Class must have a class symbol.")]
        [MaxLength(1, ErrorMessage = "Class must be only 1 characters.")]
        public string Class { get; set; } = string.Empty!;

        [Required(ErrorMessage = "Class must have a description.")]
        [MaxLength(200, ErrorMessage = "Class description must be less than 200 characters.")]
        public string ClassDescription { get; set; } = string.Empty!;
    }
}
