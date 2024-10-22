using System.ComponentModel.DataAnnotations;

namespace Students.API.Models.DTO
{
    public class AddHousingRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage ="Cannot Accept Housing Code")]
        [MaxLength(100, ErrorMessage ="Name exceed limitations")]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string Location { get; set; }
        [Required]
        public int Price { get; set; }
    }
}
