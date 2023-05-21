namespace Lab_ISTP_1.Models
{
    public class ViewModel
    {
        public IEnumerable<Lab_ISTP_1.Models.Users> Users { get; set; }
        public IEnumerable<Lab_ISTP_1.Models.Categories> Categories { get; set; }
        public IEnumerable<Lab_ISTP_1.Models.Products> Products { get; set; }
        public IEnumerable<Lab_ISTP_1.Models.Orders> Orders { get; set; }
    }
}
