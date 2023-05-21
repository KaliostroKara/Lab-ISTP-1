using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab_ISTP_1.Models
{

    public class Products
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0, 9999999.99, ErrorMessage = "Price must be between 0 and 9999999.99")]
        public decimal Price { get; set; }

        [ForeignKey("Categories")]
        public int CategoryId { get; set; }

        public virtual Categories Categories { get; set; }

        public Products()
        {
            Name = string.Empty;
            Description = string.Empty;
            Categories = new Categories();
        }
    }
}