using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProductManagmentApp.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter Product Name.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please Enter Product Description.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please Enter Product Price")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Please Enter product category")]
        public string CategoryId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please Enter product ")]
        public DateTime? ExpirationDate { get; set; }
        public Category? Category { get; set; }

        public bool IsExpired => ExpirationDate > DateTime.Today;
    }
}
