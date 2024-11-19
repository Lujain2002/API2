using System.ComponentModel.DataAnnotations;

namespace Task2API.DTOs.Product
{
    public class CreateProductDto
    {
        [Required(ErrorMessage =" Name is required, please fill it")]
        [MinLength(5),MaxLength(30)]
        public string Name { get; set; }
        [Required(ErrorMessage = " Description is required, please fill it")]
        [MinLength(10)]
        public string Description { get; set; }
        [Required(ErrorMessage = " Price is required, please fill it")]
        [Range(20,3000)]
        public double Price { get; set; }
    }
}
