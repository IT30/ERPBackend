using System.ComponentModel.DataAnnotations;

namespace Farma.DTO
{
    public class OriginCreateDTO
    {
        [Required(ErrorMessage = "Origin must have a name.")]
        [MaxLength(15, ErrorMessage = "Class name must be less than 15 characters.")]
        public string OriginName { get; set; } = string.Empty!;

        [Required(ErrorMessage = "Origin must have a description.")]
        [MaxLength(200, ErrorMessage = "Class description must be less than 200 characters.")]
        public string OriginDescription { get; set; } = string.Empty!;
    }
}
