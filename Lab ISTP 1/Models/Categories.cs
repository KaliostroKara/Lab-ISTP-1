using System.ComponentModel.DataAnnotations;

namespace Lab_ISTP_1.Models
{
    public class Categories
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Category name is required")]
        [MaxLength(50, ErrorMessage = "Category name cannot exceed 50 characters")]
        public string Name { get; set; }

        [MaxLength(100, ErrorMessage = "Category description cannot exceed 100 characters")]
        public string CategoryDescription { get; set; }

        public Categories()
        {
            // задание значений по умолчанию для свойств Name и CategoryDescription
            Name = "Default Name";
            CategoryDescription = "Default Description";
        }
    }

}
